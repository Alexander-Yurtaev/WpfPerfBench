using Microsoft.EntityFrameworkCore;
using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Repositories;

namespace WpfPerfBench.Data.Services;

public class DataService : IDataService
{
    private readonly IDataContextFactory _factory;
    private readonly IUserSession _userSession;
    private readonly IItemRepository _itemRepository;

    public DataService(
        IDataContextFactory factory,
        IUserSession userSession,
        IItemRepository itemRepository)
    {
        _factory = factory;
        _userSession = userSession;
        _itemRepository = itemRepository;
    }

    public virtual IWpfPerfBenchContext CreateContext()
    {
        return _factory.CreateContext(_userSession.DataProvider, _userSession.ConnectionString!);
    }

    public async Task<ResultBase> TestConnection(IWpfPerfBenchContext db, CancellationToken ct)
    {
        try
        {
            var success = false;
            var task = Task.Run(() => db.Database.CanConnect(), ct);
            await task.ContinueWith(t => success = t.Result, ct);
            return success
                ? ResultBase.SuccessResult()
                : ResultBase.FailResult("Ошибка при подключении к БД");
        }
        catch (TaskCanceledException e)
        {
            return ResultBase.CancelResult();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ResultBase.FailResult(e.Message);
        }
    }

    public async Task<ResultBase> GetAppliedMigrationsAsync(IWpfPerfBenchContext db, CancellationToken ct)
    {
        try
        {
            var migrations = await db.Database.GetAppliedMigrationsAsync(ct);
            return ResultBase.NamesResult(migrations);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ResultBase.FailResult(e.Message);
        }
    }

    public async Task<ResultBase> GetPendingMigrationsAsync(IWpfPerfBenchContext db, CancellationToken ct)
    {
        try
        {
            var migrations = await db.Database.GetPendingMigrationsAsync(ct);
            return ResultBase.NamesResult(migrations);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ResultBase.FailResult(e.Message);
        }
    }

    public async Task<ResultBase> Migrate(IWpfPerfBenchContext db, string migrationName, CancellationToken ct)
    {
        try
        {
            await db.Database.MigrateAsync(migrationName, ct);
            return ResultBase.SuccessResult();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ResultBase.FailResult(e.Message);
        }
    }

    public async Task<ResultBase> CleanItems(IWpfPerfBenchContext db, CancellationToken ct)
    {
        try
        {
            await _itemRepository.CleanItems(db, ct);
            return ResultBase.SuccessResult();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ResultBase.FailResult(e.Message);
        }
    }

    public async Task<ResultBase> SeedItems(IWpfPerfBenchContext db, List<Models.Item> items, CancellationToken ct)
    {
        try
        {
            await _itemRepository.Seed(db, items, ct);
            return ResultBase.SuccessResult();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ResultBase.FailResult(e.GetBaseException().Message);
        }
    }
}