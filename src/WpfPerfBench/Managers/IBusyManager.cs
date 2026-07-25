namespace WpfPerfBench.Managers;

public interface IBusyManager
{
    bool IsBusy { get; set; }

    CancellationToken CreateToken();
}