using WpfPrefBench.Data.Enums;

namespace WpfPrefBench.Data.DataContexts;

public interface IDataContextFactory
{
    IWpfPrefBenchContext CreateContext(DataProvider provider, string connectionString);
}