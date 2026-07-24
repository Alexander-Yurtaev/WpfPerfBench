using WpfPerfBench.Managers;

namespace WpfPerfBench.Interfaces;

public interface INavigatable
{
    event EventHandler<NavigatableEventArgs>? OnNavigatable;
}