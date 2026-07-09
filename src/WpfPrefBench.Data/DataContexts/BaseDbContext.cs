using Microsoft.EntityFrameworkCore;
using WpfPrefBench.Data.Entities;

namespace WpfPrefBench.Data.DataContexts;

public abstract class BaseDbContext(DbContextOptions options) : DbContext(options)
{
    #region Overrides of DbContext

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(c =>
        {
            c.HasKey(x => x.Id);
            c.Property(x => x.Name).HasMaxLength(100).IsRequired(true);
            c.Property(x => x.IsActive).HasDefaultValue(false).IsRequired(true);
            c.Property(x => x.SortOrder).HasDefaultValue(0).IsRequired(true);
            c.Property(x => x.Color)
                .HasDefaultValue("#000000")
                .HasMaxLength(20)
                .IsRequired(true);

            c.HasOne(x => x.Parent)
                .WithMany(a => a.Child)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            c.HasIndex(x => x.ParentId);
        });
    }

    #endregion
}