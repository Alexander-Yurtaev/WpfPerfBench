using WpfPerfBench.Data.DataContexts;

namespace WpfPerfBench.Data.Services;

public interface IDataService
{
    IWpfPerfBenchContext CreateContext();
    Task<ResultBase> TestConnection(CancellationToken ct);
    Task<ResultBase> Migrate(CancellationToken ct);
}