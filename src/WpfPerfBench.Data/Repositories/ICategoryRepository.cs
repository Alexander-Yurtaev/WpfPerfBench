namespace WpfPerfBench.Data.Repositories;

public interface ICategoryRepository
{
    Task<List<Models.Category>> Categories();
    Task<List<Models.Category>> HierarchyCategories();
}