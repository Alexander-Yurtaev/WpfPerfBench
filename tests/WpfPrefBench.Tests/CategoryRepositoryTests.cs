using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using WpfPrefBench.Data;
using WpfPrefBench.Data.DataContexts;
using WpfPrefBench.Data.Enums;
using WpfPrefBench.Data.Repositories;

namespace WpfPrefBench.Tests;

public class CategoryRepositoryTests
{
    [Theory]
    [InlineData(DataProvider.MsSql)]
    [InlineData(DataProvider.Postgres)]
    public async Task Call_CategoryRepository_Success(DataProvider provider)
    {
        // Arrange
        IWpfPrefBenchContext context = null!;
        if (provider == DataProvider.MsSql)
        {
            var options = new DbContextOptionsBuilder<MsSqlDataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new MsSqlDataContext(options);
        }
        else if (provider == DataProvider.Postgres)
        {
            var options = new DbContextOptionsBuilder<PostgresDataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new PostgresDataContext(options);
        }
        else
        {
            throw new ArgumentException($"Unknown provider: {provider}");
        }

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<List<Data.Models.Category>>(It.IsAny<List<Data.Entities.Category>>()))
            .Returns(new List<Data.Models.Category>());

        var factoryMock = new Mock<IDataContextFactory>();
        factoryMock.Setup(f => f.CreateContext(It.IsAny<DataProvider>(), It.IsAny<string>()))
            .Returns(context);

        var userSessionMock = new Mock<UserSession>();
        var repository = new CategoryRepository(mapperMock.Object,
            factoryMock.Object, userSessionMock.Object);

        // Act
        var result = await repository.Categories();

        // Assert
        result.Should().NotBeNull();
    }
}