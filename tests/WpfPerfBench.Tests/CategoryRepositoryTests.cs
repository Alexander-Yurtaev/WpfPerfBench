using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using WpfPerfBench.Data;
using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Enums;
using WpfPerfBench.Data.Repositories;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Tests.Factories;

namespace WpfPerfBench.Tests;

public class CategoryRepositoryTests
{
    private readonly IMapper _mapper;

    public CategoryRepositoryTests()
    {
        _mapper = TestMapperFactory.CreateMapper();
    }

    [Theory]
    [InlineData(DataProvider.MsSql)]
    [InlineData(DataProvider.Postgres)]
    public async Task Call_CategoryRepository_Success(DataProvider provider)
    {
        // Arrange
        var context = CreateDataContext(provider);
        await SeedHierarchyCategories(context);

        var factoryMock = new Mock<IDataContextFactory>();
        var userSessionMock = new Mock<UserSession>();
        
        var dataServiceMock = new Mock<DataService>(factoryMock.Object, userSessionMock.Object);
        dataServiceMock.Setup(x => x.CreateContext())
            .Returns(context);

        var repository = new CategoryRepository(_mapper);

        // Act
        var result = await repository.Categories(context, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(12);
    }

    [Theory]
    [InlineData(DataProvider.MsSql)]
    [InlineData(DataProvider.Postgres)]
    public async Task Call_HierarchyCategories_Success(DataProvider provider)
    {
        // Arrange
        var context = CreateDataContext(provider);
        await SeedHierarchyCategories(context);

        var factoryMock = new Mock<IDataContextFactory>();
        var userSessionMock = new Mock<UserSession>();

        var dataServiceMock = new Mock<DataService>(factoryMock.Object, userSessionMock.Object);
        dataServiceMock.Setup(x => x.CreateContext())
            .Returns(context);

        var repository = new CategoryRepository(_mapper);

        // Act
        var result = await repository.HierarchyCategories(context, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);

        var parent = result[0];
        parent.Should().NotBeNull();
        parent.Children.Should().NotBeEmpty();
        parent.Children.Should().HaveCount(2);

        var child2 = parent.Children.FirstOrDefault(c => c.Id == 2);
        child2.Should().NotBeNull();
        child2.Children.Should().NotBeEmpty();
        child2.Children.Should().HaveCount(4);

        var child7 = parent.Children.FirstOrDefault(c => c.Id == 7);
        child7.Should().NotBeNull();
        child7.Children.Should().NotBeEmpty();
        child7.Children.Should().HaveCount(5);
    }

    #region Private Methods

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
        var categories = DbContextBase.GetCategories();
        context.Categories.AddRange(categories);
        await context.SaveChangesAsync(CancellationToken.None);
    }

    #endregion Private Methods
}