using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Services;
using WpfPerfBench.SeedMethods;
using WpfPerfBench.Services;

namespace WpfPerfBench.Tests.Data;

public class TestSeedMethod : SeedMethodBase
{
    public TestSeedMethod(
        IDataService dataService, 
        IGeneratorService generatorService, 
        IMessageService messageService) : base(dataService, generatorService, messageService)
    {
    }

    #region Overrides of SeedMethodBase

    protected override async Task<bool> Prepare(IWpfPerfBenchContext db, CancellationToken ct)
    {
        return await Task.FromResult(true);
    }

    protected override async Task OnSeed(IWpfPerfBenchContext db, CancellationToken ct)
    {
        await Task.Delay(0, ct);
    }

    #endregion
}