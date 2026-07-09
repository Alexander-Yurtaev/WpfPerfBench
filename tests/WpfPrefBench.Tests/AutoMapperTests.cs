using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;

namespace WpfPrefBench.Tests;

public class AutoMapperTests
{
    private IMapper _mapper;

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
}
