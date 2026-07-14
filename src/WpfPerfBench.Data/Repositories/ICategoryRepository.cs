namespace WpfPerfBench.Data.Repositories;

public interface ICategoryRepository
{
    Task<List<Models.Category>> Categories(CancellationToken ct = default);
    Task<List<Models.Category>> HierarchyCategories(CancellationToken ct = default);
}