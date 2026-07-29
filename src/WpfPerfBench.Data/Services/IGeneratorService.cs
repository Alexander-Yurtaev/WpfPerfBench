namespace WpfPerfBench.Data.Services;

public interface IGeneratorService
{
    Models.Category GenerateCategoryModel();
    Entities.Category GenerateCategoryEntity();
    List<Models.Category> GenerateListCategoryModel(int count);
    List<Entities.Category> GenerateListCategoryEntity(int count);

    Models.Item GenerateItemModel();
    Task<List<Models.Item>> GenerateListItemModel(int count, CancellationToken ct);
}