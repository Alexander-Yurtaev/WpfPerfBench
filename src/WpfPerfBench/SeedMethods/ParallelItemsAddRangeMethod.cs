using WpfPerfBench.Data;
using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Metrics;
using WpfPerfBench.Data.Models;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Interfaces.Managers;
using WpfPerfBench.Services;

namespace WpfPerfBench.SeedMethods;

public class ParallelItemsAddRangeMethod : SeedMethodBase
{
    public ParallelItemsAddRangeMethod(
        IDataService dataService,
        IGeneratorService generatorService,
        IMessageService messageService,
        INavigationService navigationService)
        : base(dataService, generatorService, messageService, navigationService)
    {
        Title = "Parallel AddRange + SaveChangesAsync";
        Description = "Параллельное добавление всех записей с последующим сохранением всех изменений.";
    }

    #region Overrides of SeedMethodBase

    protected override async Task<ResultBase> Seed(ISeedMethodMetricsRefresher metrics, CancellationToken ct)
    {
        metrics.UpdateIsIndeterminate(true);

        // 1. Prepare
        await using (var dbPrepare = DataService.CreateContext())
        {
            var result = await Prepare(dbPrepare, ct);
            if (result is FailResult failResult) return failResult;
        }

        // 2. Seed
        var tasks = new List<Task>();
        var results = new List<ResultBase>();
        var dbs = new List<IWpfPerfBenchContext>();
        var parallelCount = 10;
        var totalItemCount = 0;
        var itemList = new List<List<Item>>();
        for (int i = 0; i < parallelCount; i++)
        {
            var items = await GeneratorService.GenerateListItemModel(1_000_000 / parallelCount, ct);
            itemList.Add(items);
        }

        foreach (var items in itemList)
        {
            totalItemCount += items.Count;
            metrics.UpdateTotalItemCount(totalItemCount);
            var db = DataService.CreateContext();
            dbs.Add(db);
            var task = OnSeed(db, items, metrics, ct).ContinueWith(t => results.Add(t.Result), ct);
            tasks.Add(task);
        }

        try
        {
            await Task.Run(() => Task.WaitAll(tasks, ct), ct);
        }
        catch (TaskCanceledException)
        {
            return ResultBase.CancelResult("Операция отменена");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ResultBase.FailResult(e.Message);
        }
        finally
        {
            tasks.Clear();
            itemList.Clear();

            foreach (var db in dbs)
            {
                await db.DisposeAsync();
            }
        }

        // FailResult
        var failResults = results
            .Where(r => r is FailResult)
            .Cast<FailResult>()
            .ToArray();

        if (failResults.Any())
        {
            var messagesSet = new HashSet<string>(failResults.Select(r => r.Message));
            var message = string.Join(Environment.NewLine, messagesSet);
            return ResultBase.FailResult(message);
        }

        // CancelResult
        var cancelResults = results
            .Where(r => r is CancelResult)
            .Cast<CancelResult>()
            .ToArray();

        if (cancelResults.Any())
        {
            var messagesSet = new HashSet<string>(cancelResults.Select(r => r.Message));
            var message = string.Join(Environment.NewLine, messagesSet);
            return ResultBase.CancelResult(message);
        }

        // SuccessResult
        return ResultBase.SuccessResult();
    }

    private async Task<ResultBase> Prepare(IWpfPerfBenchContext db, CancellationToken ct)
    {
        var result = await DataService.CleanItems(db, ct);
        return result;
    }

    private async Task<ResultBase> OnSeed(
        IWpfPerfBenchContext db,
        List<Item> items,
        ISeedMethodMetricsRefresher metrics,
        CancellationToken ct)
    {
        try
        {
            var result = await DataService.SeedItems(db, items, metrics, ct);
            return result;
        }
        catch (TaskCanceledException)
        {
            return ResultBase.CancelResult("Процесс заполнения БД данными был прерван.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ResultBase.FailResult(e.Message);
        }
        finally
        {
            metrics.UpdateIsIndeterminate(false);
        }
    }

    #endregion
}