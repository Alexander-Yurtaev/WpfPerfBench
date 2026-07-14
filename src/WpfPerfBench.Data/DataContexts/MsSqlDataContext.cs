using Microsoft.EntityFrameworkCore;

namespace WpfPerfBench.Data.DataContexts;

public class MsSqlDataContext : DbContextBase
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
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConnectionString)
                .UseSeeding(CategorySeed)
                .UseAsyncSeeding(CategorySeedAsync);
        }   
    }

    #endregion
}