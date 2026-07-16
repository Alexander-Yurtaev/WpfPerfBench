using Microsoft.EntityFrameworkCore;
using WpfPerfBench.Data.DataContexts;

namespace WpfPerfBench.Data.Services;

public class DataService : IDataService
{
    private readonly IDataContextFactory _factory;
    private readonly IUserSession _userSession;

    public DataService(
        IDataContextFactory factory,
        IUserSession userSession)
    {
        _factory = factory;
        _userSession = userSession;
    }

    public virtual IWpfPerfBenchContext CreateContext()
    {
        return _factory.CreateContext(_userSession.DataProvider, _userSession.ConnectionString!);
    }

    public async Task<ResultBase> TestConnection(CancellationToken ct)
    {
        try
        {
            var db = CreateContext();
            var success = await db.Database.CanConnectAsync(ct);
            return success 
                ? ResultBase.SuccessResult() 
                : ResultBase.FailResult("Ошибка при подключении к БД");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ResultBase.FailResult(e.Message);
        }
    }

    public async Task<ResultBase> GetPendingMigrationsAsync(DbContext db, CancellationToken ct)
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

    public async Task<ResultBase> Migrate(DbContext db, string migrationName, CancellationToken ct)
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
}