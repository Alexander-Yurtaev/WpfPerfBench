using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Models;

namespace WpfPerfBench.Data.Repositories;

public interface ICategoryRepository
{
    Task<List<Models.Category>> Categories(IWpfPerfBenchContext db, CancellationToken ct = default);
    Task<List<CategoryTreeItem>> HierarchyCategories(IWpfPerfBenchContext db, CancellationToken ct = default);
}