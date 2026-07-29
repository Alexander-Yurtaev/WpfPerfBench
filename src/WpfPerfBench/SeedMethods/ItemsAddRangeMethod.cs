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

    protected override async Task<bool> Prepare(IWpfPerfBenchContext db, CancellationToken ct)
    {
        var result = await DataService.CleanItems(db, ct);

        if (result.Success == true) return true;
        
        MessageService.ShowErrorMessage(result.Message);
        return false;
    }

    protected override async Task OnSeed(IWpfPerfBenchContext db, CancellationToken ct)
    {
        try
        {
            var items = GeneratorService.GenerateListItemModel(1_000_000);
            var result = await DataService.SeedItems(db, items, ct);

            if (result.Success == false)
            {
                MessageService.ShowErrorMessage(result.Message);
            }

            MessageService.ShowSuccessMessage("Загрузка...", "Данные загружены.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            MessageService.ShowErrorMessage(e.Message);
        }
    }

    #endregion
}