using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using WpfPrefBench.Core.Services;

namespace WpfPrefBench.Tests;

public class AutoMapperTests
{
    private readonly IMapper _mapper;

    public AutoMapperTests()
    {
        var config = new MapperConfiguration(cfg => 
        {
            cfg.AddMaps(typeof(App).Assembly);
        }, new NullLoggerFactory());

        _mapper = config.CreateMapper();
    }

    [Fact]
    public void AutoMapper_Configuration_IsValid()
    {
        // Проверяем все конфигурации маппинга
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    [Fact]
    public void Map_Category_Model_To_Entity_Success()
    {
        // Arrange
        var model = GeneratorService.GenerateCategoryModel();

        // Act
        var entity = _mapper.Map<Data.Entities.Category>(model);

        // Assert
        entity.Should().NotBeNull();
    }

    [Fact]
    public void Map_Category_Entity_To_Model_Success()
    {
        // Arrange
        var model = GeneratorService.GenerateCategoryEntity();

        // Act
        var entity = _mapper.Map<Data.Models.Category>(model);

        // Assert
        entity.Should().NotBeNull();
    }

    [Fact]
    public void Map_List_Category_Model_To_Entity_Success()
    {
        // Arrange
        var models = GeneratorService.GenerateListCategoryModel(10);

        // Act
        var entities = _mapper.Map<List<Data.Entities.Category>>(models);

        // Assert
        entities.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void Map_EmptyList_Category_Model_To_Entity_Success()
    {
        // Arrange
        var models = new List<Data.Models.Category>();

        // Act
        var entities = _mapper.Map<List<Data.Entities.Category>>(models);

        // Assert
        entities.Should().NotBeNull();
        entities.Should().BeEmpty();
    }

    [Fact]
    public void Map_List_Category_Entity_To_Model_Success()
    {
        // Arrange
        var models = GeneratorService.GenerateListCategoryEntity(10);

        // Act
        var entities = _mapper.Map<List<Data.Models.Category>>(models);

        // Assert
        entities.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void Map_EmptyList_Category_Entity_To_Model_Success()
    {
        // Arrange
        var models = new List<Data.Entities.Category>();

        // Act
        var entities = _mapper.Map<List<Data.Models.Category>>(models);

        // Assert
        entities.Should().NotBeNull();
        entities.Should().BeEmpty();
    }
}
