using Microsoft.EntityFrameworkCore.Design;

namespace WpfPerfBench.Data.DataContexts;

public class SqlServerContextFactory : IDesignTimeDbContextFactory<SqlServerDataContext>
{
    #region Implementation of IDesignTimeDbContextFactory<out SqlServerDataContext>

    public SqlServerDataContext CreateDbContext(string[] args)
    {
        return args.Length == 0 
            ? throw new ArgumentNullException(nameof(args)) 
            : new SqlServerDataContext(args[0]);
    }

    #endregion
}