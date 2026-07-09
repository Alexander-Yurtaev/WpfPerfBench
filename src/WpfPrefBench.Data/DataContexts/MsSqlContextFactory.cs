using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WpfPrefBench.Data.DataContexts;

public class MsSqlContextFactory : IDesignTimeDbContextFactory<MsSqlDataContext>
{
    #region Implementation of IDesignTimeDbContextFactory<out MsSqlDataContext>

    public MsSqlDataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MsSqlDataContext>();

        if (args.Length == 0) throw new ArgumentNullException(nameof(args));

        optionsBuilder.UseSqlServer(args[0]);

        return new MsSqlDataContext(optionsBuilder.Options);
    }

    #endregion
}