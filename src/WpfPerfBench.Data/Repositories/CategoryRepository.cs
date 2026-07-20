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

    public async Task<List<Models.Category>> HierarchyCategories(
        IWpfPerfBenchContext db,
        CancellationToken ct = default)
    {
        var categories = await Categories(db, ct);
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

    public async Task CleanItems(IWpfPerfBenchContext db, CancellationToken ct = default)
    {
        try
        {
            await db.Items.ExecuteDeleteAsync(ct);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Seed(IWpfPerfBenchContext db, List<Item> items, CancellationToken ct = default)
    {
        var entities = _mapper.Map<List<Entities.Item>>(items);
        await db.Items.AddRangeAsync(entities, ct);
        await db.SaveChangesAsync(ct);
    }
}