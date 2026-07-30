using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Models;

namespace WpfPerfBench.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IMapper _mapper;

    public CategoryRepository(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<List<Models.Category>> Categories(
        IWpfPerfBenchContext db, 
        CancellationToken ct = default)
    {
        var categories = await db.Categories
            .AsNoTracking()
            .ToListAsync(ct);

        var result = _mapper.Map<List<Models.Category>>(categories);
        return result;
    }

    public async Task<List<CategoryTreeItem>> HierarchyCategories(
        IWpfPerfBenchContext db,
        CancellationToken ct = default)
    {
        var categories = await db.Categories
            .Include(c => c.Items)
            .AsNoTracking()
            .Select(c => new CategoryTreeItem(c.Id, c.Name, c.ParentId, c.Items.Count))
            .ToListAsync(ct);

        var lookup = categories.ToLookup(c => c.ParentId);

        CategoryTreeItem BuildTree(int parentId)
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