using Microsoft.EntityFrameworkCore;

namespace WpfPrefBench.Data.DataContexts;

public class MsSqlDataContext(DbContextOptions<MsSqlDataContext> optionsBuilderOptions) 
    : BaseDbContext(optionsBuilderOptions);