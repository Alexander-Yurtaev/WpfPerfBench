using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using WpfPerfBench.WPF;

namespace WpfPerfBench.Tests.Factories;

public static class TestMapperFactory
{
    public static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(Profilers).Assembly);
        }, new NullLoggerFactory());

        return config.CreateMapper();
    }
}