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

    public async Task<bool> TestConnection(CancellationToken ct)
    {
        try
        {
            var db = CreateContext();
            return await db.Database.CanConnectAsync(ct);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    //public async Task<bool> TestConnection(CancellationToken ct)
    //{
    //    try
    //    {
    //        await using var context = CreateContext();

    //        // Получаем информацию о попытке подключения
    //        var connection = context.Database.GetDbConnection();
    //        Console.WriteLine($"Попытка подключения к: {connection.ConnectionString}");

    //        // Пробуем открыть соединение вручную
    //        await connection.OpenAsync(ct);
    //        Console.WriteLine("✅ Соединение открыто успешно!");

    //        // Проверяем состояние
    //        Console.WriteLine($"Состояние: {connection.State}");
    //        Console.WriteLine($"База данных: {connection.Database}");
    //        Console.WriteLine($"Источник данных: {connection.DataSource}");

    //        await connection.CloseAsync();
    //        return true;
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine($"❌ Ошибка: {e.Message}");
    //        Console.WriteLine($"Детали: {e.InnerException?.Message}");
    //        Console.WriteLine($"Stack Trace: {e.StackTrace}");
    //        return false;
    //    }
    //}
}