using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Metrics;
using WpfPerfBench.Data.Models;

namespace WpfPerfBench.Data.Repositories;

public interface IItemRepository
{
    Task CleanItems(IWpfPerfBenchContext db, CancellationToken ct = default);
    Task Seed(IWpfPerfBenchContext db, List<Item> items, ISeedMethodMetricsRefresher metrics, CancellationToken ct = default);
    Task<List<Item>> GetItemsByCategoryId(IWpfPerfBenchContext db, int categoryId, CancellationToken ct = default);
}