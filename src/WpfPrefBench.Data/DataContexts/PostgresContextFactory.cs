using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WpfPrefBench.Data.DataContexts;

public class PostgresContextFactory : IDesignTimeDbContextFactory<PostgresDataContext>
{
    #region Implementation of IDesignTimeDbContextFactory<out PostgresDataContext>

    public PostgresDataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PostgresDataContext>();

        if (args.Length == 0) throw new ArgumentNullException(nameof(args));

        optionsBuilder.UseNpgsql(args[0]);

        return new PostgresDataContext(optionsBuilder.Options);
    }

    #endregion
}