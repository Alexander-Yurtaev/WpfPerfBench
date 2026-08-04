using Microsoft.EntityFrameworkCore;

namespace WpfPerfBench.Data.DataContexts;

public class SqlServerDataContext : DbContextBase
{
    public SqlServerDataContext(string connectionString) : base(connectionString)
    {
        
    }

    public SqlServerDataContext(DbContextOptions options) : base(options)
    {
        
    }

    #region Overrides of DbContext

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }   
    }

    #endregion
}