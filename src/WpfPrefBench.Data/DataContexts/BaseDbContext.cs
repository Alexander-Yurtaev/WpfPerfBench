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

            c.HasData([
                new Category{Id=1, Name = "Все заказы", ParentId = null, IsActive = true, Color = "#000000"},
                new Category{Id=2, Name = "По статусам", ParentId = 1, IsActive = true, Color = "#333333"},
                new Category{Id=3, Name = "Новые", ParentId = 2, IsActive = true, Color = "#4CAF50"},
                new Category{Id=4, Name = "В пути", ParentId = 2, IsActive = true, Color = "#FF9800"},
                new Category{Id=5, Name = "Доставлены", ParentId = 2, IsActive = true, Color = "#2196F3"},
                new Category{Id=6, Name = "Отменены", ParentId = 2, IsActive = true, Color = "#F44336"},
                new Category{Id=7, Name = "По зонам", ParentId = 1, IsActive = true, Color = "#333333"},
                new Category{Id=8, Name = "Центр", ParentId = 7, IsActive = true, Color = "#E91E63"},
                new Category{Id=9, Name = "Север", ParentId = 7, IsActive = true, Color = "#00BCD4"},
                new Category{Id=10, Name = "Юг", ParentId = 7, IsActive = true, Color = "#88C34A"},
                new Category{Id=11, Name = "Запад", ParentId = 7, IsActive = true, Color = "#9C27B0"},
                new Category{Id=12, Name = "Восток", ParentId = 7, IsActive = true, Color = "#FF5722"},
            ]);
        });
    }

    #endregion
}