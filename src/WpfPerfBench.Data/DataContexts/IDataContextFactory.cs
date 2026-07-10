using WpfPerfBench.Data.Enums;

namespace WpfPerfBench.Data.DataContexts;

public interface IDataContextFactory
{
    IWpfPerfBenchContext CreateContext(DataProvider provider, string connectionString);
}