using Microsoft.EntityFrameworkCore.Design;

namespace WpfPrefBench.Data.DataContexts;

public class PostgresContextFactory : IDesignTimeDbContextFactory<PostgresDataContext>
{
    #region Implementation of IDesignTimeDbContextFactory<out PostgresDataContext>

    public PostgresDataContext CreateDbContext(string[] args)
    {
        return args.Length == 0 
            ? throw new ArgumentNullException(nameof(args)) 
            : new PostgresDataContext(args[0]);
    }

    #endregion
}