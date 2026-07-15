using WpfPerfBench.Data.DataContexts;

namespace WpfPerfBench.Data.Services;

public interface IDataService
{
    IWpfPerfBenchContext CreateContext();
    Task<bool> TestConnection(CancellationToken ct);
}