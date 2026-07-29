using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Models;

namespace WpfPerfBench.Data.Services;

public interface IDataService
{
    IWpfPerfBenchContext CreateContext();
    Task<ResultBase> TestConnection(IWpfPerfBenchContext db, CancellationToken ct);
    Task<ResultBase> GetAppliedMigrationsAsync(IWpfPerfBenchContext db, CancellationToken ct);
    Task<ResultBase> GetPendingMigrationsAsync(IWpfPerfBenchContext db, CancellationToken ct);
    Task<ResultBase> Migrate(IWpfPerfBenchContext db, string migrationName, CancellationToken ct);
    Task<ResultBase> CleanItems(IWpfPerfBenchContext db, CancellationToken ct);
    Task<ResultBase> SeedItems(IWpfPerfBenchContext db, List<Item> items, ISeedMethodStat stat, CancellationToken ct);
}