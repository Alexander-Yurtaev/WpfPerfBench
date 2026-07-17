using WpfPerfBench.Data.DataContexts;

namespace WpfPerfBench.Data.Services;

public interface IDataService
{
    IWpfPerfBenchContext CreateContext();
    Task<ResultBase> TestConnection(IWpfPerfBenchContext db, CancellationToken ct);
    Task<ResultBase> GetPendingMigrationsAsync(IWpfPerfBenchContext db, CancellationToken ct);
    Task<ResultBase> Migrate(IWpfPerfBenchContext db, string migrationName, CancellationToken ct);
    Task<ResultBase> CleanItems(IWpfPerfBenchContext db, CancellationToken ct);
    Task<ResultBase> Feed(IWpfPerfBenchContext db, CancellationToken ct);
}