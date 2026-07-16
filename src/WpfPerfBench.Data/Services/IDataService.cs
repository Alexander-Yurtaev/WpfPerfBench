using Microsoft.EntityFrameworkCore;
using WpfPerfBench.Data.DataContexts;

namespace WpfPerfBench.Data.Services;

public interface IDataService
{
    IWpfPerfBenchContext CreateContext();
    Task<ResultBase> TestConnection(CancellationToken ct);
    Task<ResultBase> GetPendingMigrationsAsync(DbContext db, CancellationToken ct);
    Task<ResultBase> Migrate(DbContext db, string migrationName, CancellationToken ct);
}