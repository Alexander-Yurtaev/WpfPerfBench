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

    public async Task<bool> CheckConnection(CancellationToken ct)
    {
        var db = CreateContext();
        return await db.Database.CanConnectAsync(ct);
    }
}