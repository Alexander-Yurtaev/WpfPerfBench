using Microsoft.EntityFrameworkCore;
using WpfPerfBench.Data.Entities;

namespace WpfPerfBench.Data.DataContexts;

public interface IWpfPerfBenchContext
{
    DbSet<Category> Categories { get; set; }
    DbSet<Item> Items { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}