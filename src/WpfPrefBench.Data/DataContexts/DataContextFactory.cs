using WpfPrefBench.Data.Enums;

namespace WpfPrefBench.Data.DataContexts;

public class DataContextFactory : IDataContextFactory
{
    public IWpfPrefBenchContext CreateContext(DataProvider provider, string connectionString)
    {
        switch (provider)
        {
            case DataProvider.MsSql:
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