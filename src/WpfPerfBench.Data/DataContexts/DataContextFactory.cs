using WpfPerfBench.Data.Enums;

namespace WpfPerfBench.Data.DataContexts;

public class DataContextFactory : IDataContextFactory
{
    public IWpfPerfBenchContext CreateContext(DataProvider provider, string connectionString)
    {
        switch (provider)
        {
            case DataProvider.SqlServer:
            {
                var factory = new MsSqlContextFactory();
                return factory.CreateDbContext([connectionString]);
            }
            case DataProvider.Postgres:
            {
                var factory = new PostgresContextFactory();
                return factory.CreateDbContext([connectionString]);
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(provider), provider, null);
        }
    }
}