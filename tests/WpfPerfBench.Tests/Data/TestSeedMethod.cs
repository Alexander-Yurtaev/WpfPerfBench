using WpfPerfBench.Data;
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

    protected override async Task<ResultBase> Prepare(IWpfPerfBenchContext db, CancellationToken ct)
    {
        return await Task.FromResult(ResultBase.SuccessResult());
    }

    protected override async Task<ResultBase> OnSeed(IWpfPerfBenchContext db, 
        ISeedMethodStat stat, 
        CancellationToken ct)
    {
        return await Task.FromResult(ResultBase.SuccessResult());
    }

    #endregion
}