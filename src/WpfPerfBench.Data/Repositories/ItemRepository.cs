using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Metrics;

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

    public async Task Seed(
        IWpfPerfBenchContext db, 
        List<Models.Item> items, 
        ISeedMethodMetricsRefresher metrics,
        CancellationToken ct = default)
    {
        await Task.Run(async () =>
        {
            var entities = _mapper.Map<List<Entities.Item>>(items);
            db.Items.AddRange(entities);
            await db.SaveChangesAsync(ct);
            metrics.AddProcessedItemCount(entities.Count);
        }, ct);
    }

    public async Task<List<Models.Item>> GetItemsByCategoryId(
        IWpfPerfBenchContext db, 
        int categoryId, 
        CancellationToken ct = default)
    {
        try
        {
            var entities = await db.Items
                .Where(i => i.CategoryId == categoryId)
                .AsNoTracking()
                .ToListAsync(ct);

            var items = _mapper.Map<List<Models.Item>>(entities);
            return items;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}