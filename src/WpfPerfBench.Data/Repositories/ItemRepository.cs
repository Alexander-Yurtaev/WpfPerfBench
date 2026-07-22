using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Models;

namespace WpfPerfBench.Data.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly IMapper _mapper;

    public ItemRepository(IMapper mapper)
    {
        _mapper = mapper;
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