namespace WpfPerfBench.Core.Interfaces;

public interface ILoadableAsync
{
    Task LoadAsync(CancellationToken ct);
}