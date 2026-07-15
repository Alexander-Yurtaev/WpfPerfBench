using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WpfPerfBench.Data.Services;

namespace WpfPerfBench.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IMapper _mapper;
    private readonly IDataService _dataService;

    public CategoryRepository(
        IMapper mapper,
        IDataService dataService)
    {
        _mapper = mapper;
        _dataService = dataService;
    }

    public async Task<List<Models.Category>> Categories(CancellationToken ct = default)
    {
        var db = _dataService.CreateContext();
        var categories = await db.Categories
            .AsNoTracking()
            .ToListAsync(ct);

        var result = _mapper.Map<List<Models.Category>>(categories);
        return result;
    }

    public async Task<List<Models.Category>> HierarchyCategories(CancellationToken ct = default)
    {
        var categories = await Categories(ct);
        var lookup = categories.ToLookup(c => c.ParentId);

        Models.Category BuildTree(int parentId)
        {
            var parent = categories.First(c => c.Id == parentId);
            var children = lookup[parentId];
            foreach (var child in children)
            {
                parent.Children.Add(BuildTree(child.Id));
            }
            return parent;
        }

        return lookup[null].Select(c => BuildTree(c.Id)).ToList();
    }
}