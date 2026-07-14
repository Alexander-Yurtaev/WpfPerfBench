using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using WpfPerfBench.Data;
using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Enums;
using WpfPerfBench.Data.Repositories;
using WpfPerfBench.Tests.Factories;

namespace WpfPerfBench.Tests;

public class CategoryRepositoryTests
{
    [Theory]
    [InlineData(DataProvider.MsSql)]
    [InlineData(DataProvider.Postgres)]
    public async Task Call_CategoryRepository_Success(DataProvider provider)
    {
        // Arrange
        var context = CreateDataContext(provider);
        await SeedHierarchyCategories(context);

        var mapper = TestMapperFactory.CreateMapper();

        var factoryMock = new Mock<IDataContextFactory>();
        factoryMock.Setup(f => f.CreateContext(It.IsAny<DataProvider>(), It.IsAny<string>()))
            .Returns(context);

        var userSessionMock = new Mock<UserSession>();
        var repository = new CategoryRepository(mapper,
            factoryMock.Object, userSessionMock.Object);

        // Act
        var result = await repository.Categories(CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
    }

    [Theory]
    [InlineData(DataProvider.MsSql)]
    [InlineData(DataProvider.Postgres)]
    public async Task Call_HierarchyCategories_Success(DataProvider provider)
    {
        // Arrange
        var context = CreateDataContext(provider);
        await SeedHierarchyCategories(context);

        var mapper = TestMapperFactory.CreateMapper();

        var factoryMock = new Mock<IDataContextFactory>();
        factoryMock.Setup(f => f.CreateContext(It.IsAny<DataProvider>(), It.IsAny<string>()))
            .Returns(context);

        var userSessionMock = new Mock<UserSession>();
        var repository = new CategoryRepository(mapper,
            factoryMock.Object, userSessionMock.Object);

        // Act
        var result = await repository.HierarchyCategories(CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
    }

    private IWpfPerfBenchContext CreateDataContext(DataProvider provider)
    {
        switch (provider)
        {
            case DataProvider.MsSql:
            {
                var options = new DbContextOptionsBuilder<MsSqlDataContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

                return new MsSqlDataContext(options);
            }
            case DataProvider.Postgres:
            {
                var options = new DbContextOptionsBuilder<PostgresDataContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                    .Options;

                return new PostgresDataContext(options);
            }
            default:
                throw new ArgumentException($"Unknown provider: {provider}");
        }
    }

    private async Task SeedHierarchyCategories(IWpfPerfBenchContext context)
    {
        var parent = context.Categories.Add(new Data.Entities.Category { Id = 1, Name = "Category 1" });
        var child = new Data.Entities.Category
        {
            Id = 11, 
            Name = "Category 11", 
            ParentId = parent.Entity.Id, 
            Parent = parent.Entity
        };
        context.Categories.Add(child);
        await context.SaveChangesAsync(CancellationToken.None);
    }
}