using WpfPerfBench.Data.DataContexts;

namespace WpfPerfBench.Data.Repositories;

public interface IItemRepository
{
    Task CleanItems(IWpfPerfBenchContext db, CancellationToken ct = default);
    Task Seed(IWpfPerfBenchContext db, List<Models.Item> items, CancellationToken ct = default);
}