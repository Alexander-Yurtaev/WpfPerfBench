namespace WpfPerfBench.Interfaces;

public interface ILoadable
{
    Task LoadAsync(CancellationToken ct);
}