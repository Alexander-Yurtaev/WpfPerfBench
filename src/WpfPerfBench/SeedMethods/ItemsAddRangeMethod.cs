using WpfPerfBench.Data;
using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Metrics;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Services;

namespace WpfPerfBench.SeedMethods;

public class ItemsAddRangeMethod : SeedMethodBase
{
    public ItemsAddRangeMethod(
        IDataService dataService, 
        IGeneratorService generatorService,
        IMessageService messageService) 
        : base(dataService, generatorService, messageService)
    {
        Title = "AddRange + SaveChangesAsync";
        Description = "Добавление всех записей с последующим сохранением всех изменений.";
    }

    #region Overrides of SeedMethodBase

    protected override async Task<ResultBase> Prepare(IWpfPerfBenchContext db, CancellationToken ct)
    {
        var result = await DataService.CleanItems(db, ct);
        return result;
    }

    protected override async Task<ResultBase> OnSeed(
        IWpfPerfBenchContext db, 
        ISeedMethodMetricsRefresher metrics,
        CancellationToken ct)
    {
        try
        {
            metrics.UpdateIsIndeterminate(true);
            var items = await GeneratorService.GenerateListItemModel(1_000_000, ct);
            metrics.UpdateTotalItemCount(items.Count);
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