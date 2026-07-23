namespace WpfPerfBench.Interfaces;

public interface ILoadableAsync
{
    Task LoadAsync(CancellationToken ct);
}