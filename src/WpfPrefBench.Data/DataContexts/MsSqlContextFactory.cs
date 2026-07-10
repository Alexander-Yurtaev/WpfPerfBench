using Microsoft.EntityFrameworkCore.Design;

namespace WpfPrefBench.Data.DataContexts;

public class MsSqlContextFactory : IDesignTimeDbContextFactory<MsSqlDataContext>
{
    #region Implementation of IDesignTimeDbContextFactory<out MsSqlDataContext>

    public MsSqlDataContext CreateDbContext(string[] args)
    {
        return args.Length == 0 
            ? throw new ArgumentNullException(nameof(args)) 
            : new MsSqlDataContext(args[0]);
    }

    #endregion
}