namespace WpfPrefBench.Data.Repositories;

public interface ICategoryRepository
{
    Task<List<Models.Category>> Categories();
}