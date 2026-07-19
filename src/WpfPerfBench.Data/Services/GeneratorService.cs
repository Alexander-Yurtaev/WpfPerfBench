using Bogus;

namespace WpfPerfBench.Data.Services;

public class GeneratorService : IGeneratorService
{
    public Models.Category GenerateCategoryModel()
    {
        var faker = new Faker<Models.Category>()
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

    public Entities.Category GenerateCategoryEntity()
    {
        var faker = new Faker<Entities.Category>()
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

    public List<Models.Category> GenerateListCategoryModel(int count)
    {
        var faker = new Faker<Models.Category>()
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

    public List<Entities.Category> GenerateListCategoryEntity(int count)
    {
        var faker = new Faker<Entities.Category>()
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

    public Models.Item GenerateItemModel()
    {
        var faker = new Faker<Models.Item>()
            .Generate();

        return faker;
    }

    public List<Models.Item> GenerateListItemModel(int count)
    {
        var faker = new Faker<Models.Item>()
            .RuleFor(i => i.CategoryId, f => f.Random.Int(1,12))
            .RuleFor(i => i.Name, f => f.Company.CompanyName())
            .RuleFor(i => i.CreatedAt, f => f.Date.Recent().ToUniversalTime())
            .RuleFor(i => i.Price, f => f.Random.Decimal())
            .RuleFor(i => i.Weight, f => f.Random.Float())
            .RuleFor(i => i.IsFragile, f => f.Random.Bool())
            .RuleFor(i => i.IsUrgent, f => f.Random.Bool())
            .RuleFor(i => i.Comments, f => f.Lorem.Paragraph())
            .RuleFor(i => i.Latitude, f => f.Random.Double())
            .RuleFor(i => i.Longitude, f => f.Random.Double())
            .RuleFor(i => i.DeliveryLatitude, f => f.Random.Double())
            .RuleFor(i => i.DeliveryLongitude, f => f.Random.Double())
            .RuleFor(i => i.IsDeleted, f => f.Random.Bool())
            .Generate(count);

        return faker.ToList();
    }
}