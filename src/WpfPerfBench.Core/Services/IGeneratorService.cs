namespace WpfPerfBench.Core.Services;

public interface IGeneratorService
{
    Data.Models.Category GenerateCategoryModel();
    Data.Entities.Category GenerateCategoryEntity();
    List<Data.Models.Category> GenerateListCategoryModel(int count);
    List<Data.Entities.Category> GenerateListCategoryEntity(int count);
}