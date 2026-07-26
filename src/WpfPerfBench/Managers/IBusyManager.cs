using CommunityToolkit.Mvvm.Input;

namespace WpfPerfBench.Managers;

public interface IBusyManager
{
    CancellationToken ShowStandardIndicator(string text, string subText = "Пожалуйста, подождите");
    CancellationToken ShowProgressIndicator(double min, double max, string text, string subText = "");
    void SetProgressIndicator(double value);
    void SetLargeIndicator(double value);
    CancellationToken ShowLargeIndicator(double max, string text, string subText = "");
    CancellationToken ShowCompactIndicator(string text, string subText = "");
    void CloseIndicator();
    void RefreshToken();

    bool IsBusy { get; set; }

    string BusyText { get; set; }

    string BusySubText { get; set; }

    double Minimum { get; set; }

    double Maximum { get; set; }

    double Value { get; set; }

    IRelayCommand CancelCommand { get; }
}