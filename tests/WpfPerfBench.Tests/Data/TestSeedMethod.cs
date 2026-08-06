using WpfPerfBench.Data;
using WpfPerfBench.Data.Metrics;
using WpfPerfBench.Data.Services;
using WpfPerfBench.WPF.Interfaces.Managers;
using WpfPerfBench.WPF.SeedMethods;
using WpfPerfBench.WPF.Services;

namespace WpfPerfBench.Tests.Data;

public class TestSeedMethod : SeedMethodBase
{
    public TestSeedMethod(
        IDataService dataService, 
        IGeneratorService generatorService, 
        IMessageService messageService,
        INavigationService navigationService) 
        : base(dataService, generatorService, messageService, navigationService)
    {
    }

    #region Overrides of SeedMethodBase

    protected override async Task<ResultBase> Seed(ISeedMethodMetricsRefresher metrics, CancellationToken ct)
    {
        await Prepare();
        return await OnSeed();
    }

    private async Task Prepare()
    {
        await Task.FromResult(ResultBase.SuccessResult());
    }

    private async Task<ResultBase> OnSeed()
    {
        return await Task.FromResult(ResultBase.SuccessResult());
    }

    #endregion
}