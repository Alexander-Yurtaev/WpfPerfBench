using CommunityToolkit.Mvvm.Input;

namespace WpfPerfBench.Managers;

public interface IBusyManager
{
    bool IsBusy { get; set; }

    CancellationToken ShowIndicator(string text, string subText = "");

    void CloseIndicator();

    void RefreshToken();
}