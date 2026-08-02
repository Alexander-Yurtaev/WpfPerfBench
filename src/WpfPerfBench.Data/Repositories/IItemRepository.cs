using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Metrics;

namespace WpfPerfBench.Data.Repositories;

public interface IItemRepository
{
    Task CleanItems(IWpfPerfBenchContext db, CancellationToken ct = default);
    Task Seed(IWpfPerfBenchContext db, List<Models.Item> items, ISeedMethodMetricsRefresher metrics, CancellationToken ct = default);
    Task<List<Models.Item>> GetItemsByCategoryId(IWpfPerfBenchContext db, int categoryId, CancellationToken ct = default);
}