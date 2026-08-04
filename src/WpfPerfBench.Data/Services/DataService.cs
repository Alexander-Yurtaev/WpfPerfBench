using Microsoft.EntityFrameworkCore;
using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Metrics;
using WpfPerfBench.Data.Models;
using WpfPerfBench.Data.Repositories;

namespace WpfPerfBench.Data.Services;

public class DataService : IDataService
{
    private readonly IDataContextFactory _factory;
    private readonly IUserSession _userSession;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IItemRepository _itemRepository;

    public DataService(
        IDataContextFactory factory,
        IUserSession userSession,
        ICategoryRepository categoryRepository,
        IItemRepository itemRepository)
    {
        _factory = factory;
        _userSession = userSession;
        _categoryRepository = categoryRepository;
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
            Exception? taskException;
            var success = await Task.Run(async () => await db.Database.CanConnectAsync(ct), ct)
                .ContinueWith(task =>
                {
                    taskException = task.Exception;
                    if (taskException is null)
                    {
                        return task.Result;
                    }

                    return (bool?)null;
                }, ct);

            return success switch
            {
                true => ResultBase.SuccessResult(),
                false => ResultBase.FailResult("Ошибка при подключении к БД"),
                null => ResultBase.CancelResult()
            };
        }
        catch (TaskCanceledException)
        {
            return ResultBase.CancelResult();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ResultBase.FailResult(e.Message);
        }
    }

    #region Migrations

    public async Task<ResultBase> GetAppliedMigrationsAsync(IWpfPerfBenchContext db, CancellationToken ct)
    {
        try
        {
            var migrations = await db.Database.GetAppliedMigrationsAsync(ct);
            return ResultBase.EntityResult(migrations);
        }
        catch (TaskCanceledException e)
        {
            Console.WriteLine(e);
            return ResultBase.CancelResult("Операция загрузки миграций отменена.");
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
            return ResultBase.EntityResult(migrations);
        }
        catch (TaskCanceledException e)
        {
            Console.WriteLine(e);
            return ResultBase.CancelResult("Операция загрузки миграций отменена.");
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

    #endregion Migrations

    #region Seed

    public async Task<ResultBase> CleanItems(IWpfPerfBenchContext db, CancellationToken ct)
    {
        try
        {
            await _itemRepository.CleanItems(db, ct);
            return ResultBase.SuccessResult();
        }
        catch (SystemException e) when (e is TaskCanceledException or OperationCanceledException)
        {
            Console.WriteLine(e);
            return ResultBase.CancelResult("Очистка таблицы была отменена");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ResultBase.FailResult(e.Message);
        }
    }

    public async Task<ResultBase> SeedItems(
        IWpfPerfBenchContext db, 
        List<Item> items, 
        ISeedMethodMetricsRefresher metrics,
        CancellationToken ct)
    {
        try
        {
            await _itemRepository.Seed(db, items, metrics, ct);
            return ResultBase.SuccessResult();
        }
        catch (OperationCanceledException e)
        {
            Console.WriteLine(e);
            return ResultBase.CancelResult("Операция заполнения данными отменена");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ResultBase.FailResult(e.GetBaseException().Message);
        }
    }

    #endregion Seed

    #region Category

    public async Task<ResultBase> HierarchyCategories(CancellationToken ct = default)
    {
        try
        {
            await using var db = CreateContext();
            var treeItems = await _categoryRepository.HierarchyCategories(db, ct);
            return ResultBase.EntityResult(treeItems);
        }
        catch (TaskCanceledException e)
        {
            Console.WriteLine(e);
            return ResultBase.CancelResult("Операция получения данных для дерева была отменена");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ResultBase.FailResult(e.Message);
        }
    }

    public async Task<ResultBase> GetItemsByCategoryId(int categoryId, CancellationToken ct = default)
    {
        try
        {
            await using var db = CreateContext();
            var items = await _itemRepository.GetItemsByCategoryId(db, categoryId, ct);
            return ResultBase.EntityResult(items);
        }
        catch (TaskCanceledException e)
        {
            Console.WriteLine(e);
            return ResultBase.CancelResult($"Операция получения данных для categoryId={categoryId} была отменена");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ResultBase.FailResult(e.Message);
        }
    }

    #endregion Category
}