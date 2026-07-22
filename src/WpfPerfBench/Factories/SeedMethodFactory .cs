using Microsoft.Extensions.DependencyInjection;
using WpfPerfBench.SeedMethods;

namespace WpfPerfBench.Factories;

public class SeedMethodFactory : ISeedMethodFactory
{
    private readonly IServiceProvider _serviceProvider;

    public SeedMethodFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T Create<T>() where T : SeedMethodBase
    {
        return ActivatorUtilities.CreateInstance<T>(_serviceProvider);
    }

    public SeedMethodBase Create(Type type)
    {
        return (SeedMethodBase)ActivatorUtilities.CreateInstance(_serviceProvider, type);
    }
}