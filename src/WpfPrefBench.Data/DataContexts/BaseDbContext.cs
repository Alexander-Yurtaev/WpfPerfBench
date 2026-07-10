using Microsoft.EntityFrameworkCore;
using WpfPrefBench.Data.Entities;

namespace WpfPrefBench.Data.DataContexts;

public abstract class BaseDbContext : DbContext, IWpfPrefBenchContext
{
    protected string ConnectionString = "";

    protected BaseDbContext(string connectionString)
    {
        ConnectionString = connectionString;
    }

    protected BaseDbContext(DbContextOptions options) : base(options)
    {
    }

    #region Implementation of IWpfPrefBenchContext

    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }

    #endregion

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

        modelBuilder.Entity<Item>(i =>
        {
            i.HasKey(x => x.Id).HasName("Уникальный ID");
            i.Property(x => x.CategoryId).HasComment("Привязка к категории");
            i.Property(x => x.Name).HasMaxLength(200).IsRequired(true).HasComment("Название элемента");
            i.Property(x => x.Status).HasMaxLength(50).IsRequired(true).HasComment("Статус");
            i.Property(x => x.Priority).HasMaxLength(50).IsRequired(true).HasComment("Приоритет");
            i.Property(x => x.CreatedAt).IsRequired(true).HasComment("Дата создания");
            i.Property(x => x.Price).IsRequired(true).HasComment("Цена");
            i.Property(x => x.Weight).IsRequired(true).HasComment("Вес");
            i.Property(x => x.IsFragile).IsRequired(true).HasDefaultValue(false).HasComment("Хрупкий");
            i.Property(x => x.IsUrgent).IsRequired(true).HasDefaultValue(false).HasComment("Срочный");
            i.Property(x => x.Comments).IsRequired(false).HasComment("Комментарий");
            i.Property(x => x.Latitude).IsRequired(true).HasComment("Широта (отправление)");
            i.Property(x => x.Longitude).IsRequired(true).HasComment("Долгота (отправление)");
            i.Property(x => x.DeliveryLatitude).IsRequired(true).HasComment("Широта (доставки)");
            i.Property(x => x.DeliveryLongitude).IsRequired(true).HasComment("Долгота (доставки)");
            i.Property(x => x.IsDeleted).IsRequired(true).HasDefaultValue(false).HasComment("Удалено");

            i.HasOne(a => a.Category)
                .WithMany(b => b.Items)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    #endregion

    protected async Task CategorySeed(DbContext context, bool flag, CancellationToken ct)
    {
        if (!context.Set<Category>().Any())
        {
            var categories = new Category[]
            {
                new Category { Id = 1, Name = "Все заказы", ParentId = null, IsActive = true, Color = "#000000" },
                new Category { Id = 2, Name = "По статусам", ParentId = 1, IsActive = true, Color = "#333333" },
                new Category { Id = 3, Name = "Новые", ParentId = 2, IsActive = true, Color = "#4CAF50" },
                new Category { Id = 4, Name = "В пути", ParentId = 2, IsActive = true, Color = "#FF9800" },
                new Category { Id = 5, Name = "Доставлены", ParentId = 2, IsActive = true, Color = "#2196F3" },
                new Category { Id = 6, Name = "Отменены", ParentId = 2, IsActive = true, Color = "#F44336" },
                new Category { Id = 7, Name = "По зонам", ParentId = 1, IsActive = true, Color = "#333333" },
                new Category { Id = 8, Name = "Центр", ParentId = 7, IsActive = true, Color = "#E91E63" },
                new Category { Id = 9, Name = "Север", ParentId = 7, IsActive = true, Color = "#00BCD4" },
                new Category { Id = 10, Name = "Юг", ParentId = 7, IsActive = true, Color = "#88C34A" },
                new Category { Id = 11, Name = "Запад", ParentId = 7, IsActive = true, Color = "#9C27B0" },
                new Category { Id = 12, Name = "Восток", ParentId = 7, IsActive = true, Color = "#FF5722" },
            };
            await context.Set<Category>().AddRangeAsync(categories, ct);
            await context.SaveChangesAsync(ct);
        }
    }
}