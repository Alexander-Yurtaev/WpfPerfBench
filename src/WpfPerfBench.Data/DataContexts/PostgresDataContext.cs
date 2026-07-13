using Microsoft.EntityFrameworkCore;

namespace WpfPerfBench.Data.DataContexts;

public class PostgresDataContext : BaseDbContext
{
    public PostgresDataContext(string connectionString) : base(connectionString)
    {
        
    }

    public PostgresDataContext(DbContextOptions options) : base(options)
    {
        
    }

    #region Overrides of DbContext

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(ConnectionString)
                .UseSeeding(CategorySeed)
                .UseAsyncSeeding(CategorySeedAsync);
        }
    }

    #endregion
}