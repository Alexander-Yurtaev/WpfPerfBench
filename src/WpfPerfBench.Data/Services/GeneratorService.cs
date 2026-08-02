using Bogus;

namespace WpfPerfBench.Data.Services;

public class GeneratorService : IGeneratorService
{
    public Models.Category GenerateCategoryModel()
    {
        var faker = new Faker<Models.Category>("ru")
            .RuleFor(u => u.Id, f => f.IndexFaker)
            .RuleFor(u => u.Name, f => f.Commerce.Categories(1).First())
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
        var faker = new Faker<Entities.Category>("ru")
            .RuleFor(u => u.Id, f => f.Random.Int())
            .RuleFor(u => u.Name, f => f.Commerce.Categories(1).First())
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
        var faker = new Faker<Models.Category>("ru")
            .RuleFor(u => u.Id, f => f.Random.Int())
            .RuleFor(u => u.Name, f => f.Commerce.Categories(1).First())
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
        var faker = new Faker<Entities.Category>("ru")
            .RuleFor(u => u.Id, f => f.Random.Int())
            .RuleFor(u => u.Name, f => f.Commerce.Categories(1).First())
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
        var faker = new Faker<Models.Item>("ru")
            .Generate();

        return faker;
    }

    public async Task<List<Models.Item>> GenerateListItemModel(int count, CancellationToken ct)
    {
        var result = await Task.Run(() =>
        {
            var faker = new Faker<Models.Item>("ru")
                .RuleFor(i => i.CategoryId, f => f.Random.Int(1, 12))
                .RuleFor(i => i.Name, f => f.Company.CompanyName())
                .RuleFor(i => i.CreatedAt, f => f.Date.Recent().ToUniversalTime())
                .RuleFor(i => i.Price, f => f.Random.Decimal())
                .RuleFor(i => i.Weight, f => f.Random.Float())
                .RuleFor(i => i.IsFragile, f => f.Random.Bool())
                .RuleFor(i => i.IsUrgent, f => f.Random.Bool())
                .RuleFor(i => i.Comments, f => f.Lorem.Paragraph())
                .RuleFor(i => i.Latitude, f => f.Address.Latitude())
                .RuleFor(i => i.Longitude, f => f.Address.Longitude())
                .RuleFor(i => i.DeliveryLatitude, f => f.Address.Latitude())
                .RuleFor(i => i.DeliveryLongitude, f => f.Address.Longitude())
                .RuleFor(i => i.IsDeleted, f => f.Random.Bool())
                .Generate(count);

            return faker.ToList();
        }, ct);

        return result;
    }
}