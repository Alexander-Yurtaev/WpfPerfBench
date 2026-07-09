using Microsoft.EntityFrameworkCore;
using WpfPrefBench.Data.Entities;

namespace WpfPrefBench.Data.DataContexts;

public interface IWpfPrefBenchContext
{
    DbSet<Category> Categories { get; set; }
    DbSet<Item> Items { get; set; }
}