using WpfPerfBench.Data;
using WpfPerfBench.Data.DataContexts;
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
        Title = "Поштучная вставка (без транзакции)";
        Description = "Вставка каждой записи отдельным INSERT. Самый медленный способ, но простой для понимания.";
    }

    #region Overrides of SeedMethodBase

    protected override async Task<ResultBase> Prepare(IWpfPerfBenchContext db, CancellationToken ct)
    {
        var result = await DataService.CleanItems(db, ct);
        return result;
    }

    protected override async Task<ResultBase> OnSeed(IWpfPerfBenchContext db, CancellationToken ct)
    {
        try
        {
            var items = await GeneratorService.GenerateListItemModel(1_000_000, ct);
            var result = await DataService.SeedItems(db, items, ct);
            return result;
        }
        catch (TaskCanceledException ex)
        {
            return ResultBase.CancelResult("Процесс заполнения БД данными был прерван.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ResultBase.FailResult(e.Message);
        }
    }

    #endregion
}