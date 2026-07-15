using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WpfPerfBench.Data.Entities;

namespace WpfPerfBench.Data.DataContexts;

public interface IWpfPerfBenchContext : IDisposable, IAsyncDisposable
{
    DbSet<Category> Categories { get; set; }
    DbSet<Item> Items { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DatabaseFacade Database { get; }
}