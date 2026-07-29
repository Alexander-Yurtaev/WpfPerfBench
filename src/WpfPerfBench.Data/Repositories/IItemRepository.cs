using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Models;

namespace WpfPerfBench.Data.Repositories;

public interface IItemRepository
{
    Task CleanItems(IWpfPerfBenchContext db, CancellationToken ct = default);
    Task Seed(IWpfPerfBenchContext db, List<Item> items, ISeedMethodStat stat, CancellationToken ct = default);
}