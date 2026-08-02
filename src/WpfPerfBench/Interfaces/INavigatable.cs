using WpfPerfBench.Managers;
using WpfPerfBench.Managers.Args;

namespace WpfPerfBench.Interfaces;

public interface INavigatable
{
    event EventHandler<NavigatableEventArgs>? OnNavigatable;
}