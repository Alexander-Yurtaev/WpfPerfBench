using WpfPerfBench.Data.DataContexts;

namespace WpfPerfBench.Data.Repositories;

public interface ICategoryRepository
{
    Task<List<Models.Category>> Categories(IWpfPerfBenchContext db, CancellationToken ct = default);
    Task<List<Models.Category>> HierarchyCategories(IWpfPerfBenchContext db, CancellationToken ct = default);
    Task CleanItems(IWpfPerfBenchContext db, CancellationToken ct = default);
}