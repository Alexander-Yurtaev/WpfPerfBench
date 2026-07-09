using Microsoft.EntityFrameworkCore;

namespace WpfPrefBench.Data.DataContexts;

public class PostgresDataContext(DbContextOptions<PostgresDataContext> optionsBuilderOptions) 
    : BaseDbContext(optionsBuilderOptions);