using WpfPerfBench.WPF.SeedMethods;

namespace WpfPerfBench.WPF.Factories;

public interface ISeedMethodFactory
{
    T Create<T>() where T : SeedMethodBase;
    SeedMethodBase Create(Type type);
}