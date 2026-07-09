using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WpfPrefBench.Data;
using WpfPrefBench.Data.DataContexts;

namespace WpfPrefBench.Data.Repositories;

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

    public async Task<List<Models.Category>> Categories()
    {
        var db = _factory.CreateContext(_userSession.DataProvider, _userSession.ConnectionString!);
        var categories = await db.Categories.ToListAsync();
        var result = _mapper.Map<List<Models.Category>>(categories);
        return result;
    }
}