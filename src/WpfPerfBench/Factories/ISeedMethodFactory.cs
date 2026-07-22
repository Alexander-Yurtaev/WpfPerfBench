using WpfPerfBench.SeedMethods;

namespace WpfPerfBench.Factories;

public interface ISeedMethodFactory
{
    T Create<T>() where T : SeedMethodBase;
    SeedMethodBase Create(Type type);
}