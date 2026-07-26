namespace WpfPerfBench.Managers;

public interface IBusyManager
{
    CancellationToken ShowStandardIndicator(string text, string subText = "Пожалуйста, подождите");
    CancellationToken ShowProgressIndicator(double min, double max, string text, string subText = "");
    void SetProgressIndicator(double value);
    void SetLargeIndicator(double value);
    CancellationToken ShowLargeIndicator(double max, string text, string subText = "");
    void CloseIndicator();
    void RefreshToken();
    Task CancelAsync();

    bool IsBusy { get; set; }
}