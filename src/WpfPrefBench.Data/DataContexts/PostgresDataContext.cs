using Microsoft.EntityFrameworkCore;

namespace WpfPrefBench.Data.DataContexts;

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
        optionsBuilder.UseNpgsql(ConnectionString)
            .UseAsyncSeeding(CategorySeed);
    }

    #endregion
}