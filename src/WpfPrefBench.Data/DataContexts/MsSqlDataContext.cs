using Microsoft.EntityFrameworkCore;

namespace WpfPrefBench.Data.DataContexts;

public class MsSqlDataContext : BaseDbContext
{
    public MsSqlDataContext(string connectionString) : base(connectionString)
    {
        
    }

    public MsSqlDataContext(DbContextOptions options) : base(options)
    {
        
    }

    #region Overrides of DbContext

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString)
            .UseSeeding(CategorySeed)
            .UseAsyncSeeding(CategorySeedAsync);
    }

    #endregion
}