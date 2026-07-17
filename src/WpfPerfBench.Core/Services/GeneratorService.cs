using Bogus;

namespace WpfPerfBench.Core.Services;

public class GeneratorService : IGeneratorService
{
    public Data.Models.Category GenerateCategoryModel()
    {
        var faker = new Faker<Data.Models.Category>()
            .RuleFor(u => u.Id, f => f.Random.Int())
            .RuleFor(u => u.Name, f => f.Company.Bs())
            .RuleFor(u => u.ParentId, f => f.Random.Int())
            //.RuleFor(u => u.Parent, f => f.Random.Int())
            .RuleFor(u => u.IsActive, f => f.Random.Bool())
            .RuleFor(u => u.SortOrder, f => f.Random.Int())
            .RuleFor(u => u.Color, f => f.Random.Int(255).ToString("X"))
            .Generate();

        return faker;
    }

    public Data.Entities.Category GenerateCategoryEntity()
    {
        var faker = new Faker<Data.Entities.Category>()
            .RuleFor(u => u.Id, f => f.Random.Int())
            .RuleFor(u => u.Name, f => f.Company.Bs())
            .RuleFor(u => u.ParentId, f => f.Random.Int())
            //.RuleFor(u => u.Parent, f => f.Random.Int())
            .RuleFor(u => u.IsActive, f => f.Random.Bool())
            .RuleFor(u => u.SortOrder, f => f.Random.Int())
            .RuleFor(u => u.Color, f => f.Random.Int(255).ToString("X"))
            .Generate();

        return faker;
    }

    public List<Data.Models.Category> GenerateListCategoryModel(int count)
    {
        var faker = new Faker<Data.Models.Category>()
            .RuleFor(u => u.Id, f => f.Random.Int())
            .RuleFor(u => u.Name, f => f.Company.Bs())
            .RuleFor(u => u.ParentId, f => f.Random.Int())
            //.RuleFor(u => u.Parent, f => f.Random.Int())
            .RuleFor(u => u.IsActive, f => f.Random.Bool())
            .RuleFor(u => u.SortOrder, f => f.Random.Int())
            .RuleFor(u => u.Color, f => f.Random.Int(255).ToString("X"))
            .Generate(count);

        return faker.ToList();
    }

    public List<Data.Entities.Category> GenerateListCategoryEntity(int count)
    {
        var faker = new Faker<Data.Entities.Category>()
            .RuleFor(u => u.Id, f => f.Random.Int())
            .RuleFor(u => u.Name, f => f.Company.Bs())
            .RuleFor(u => u.ParentId, f => f.Random.Int())
            //.RuleFor(u => u.Parent, f => f.Random.Int())
            .RuleFor(u => u.IsActive, f => f.Random.Bool())
            .RuleFor(u => u.SortOrder, f => f.Random.Int())
            .RuleFor(u => u.Color, f => f.Random.Int(255).ToString("X"))
            .Generate(count);

        return faker.ToList();
    }
}