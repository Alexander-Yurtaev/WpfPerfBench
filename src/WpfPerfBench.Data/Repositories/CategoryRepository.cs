using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Models;

namespace WpfPerfBench.Data.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IMapper _mapper;
    private readonly IDataContextFactory _factory;
    private readonly IUserSession _userSession;

    public CategoryRepository(
        IMapper mapper,
        IDataContextFactory factory, 
        IUserSession userSession)
    {
        _mapper = mapper;
        _factory = factory;
        _userSession = userSession;
    }

    public async Task<List<Models.Category>> Categories(CancellationToken ct = default)
    {
        var db = _factory.CreateContext(_userSession.DataProvider, _userSession.ConnectionString!);
        var categories = await db.Categories
            .AsNoTracking()
            .ToListAsync(ct);

        var result = _mapper.Map<List<Models.Category>>(categories);
        return result;
    }

    public async Task<List<Category>> HierarchyCategories(CancellationToken ct = default)
    {
        var categories = await Categories(ct);
        var lookup = categories.ToLookup(c => c.ParentId);

        Category BuildTree(int parentId)
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