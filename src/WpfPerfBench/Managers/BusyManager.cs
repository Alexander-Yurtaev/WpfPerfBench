using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WpfPerfBench.Managers;

public partial class BusyManager : ObservableObject, IBusyManager
{
    private CancellationTokenSource _tokenSource = new();

    [ObservableProperty] private bool _isBusy;

    [ObservableProperty] private string _busyText = string.Empty;

    [ObservableProperty] private string _busySubText = string.Empty;

    public CancellationToken ShowIndicator(string text, string subText = "Пожалуйста, подождите")
    {
        BusyText = text;
        BusySubText = subText;
        IsBusy = true;
        RefreshToken();
        return _tokenSource.Token;
    }

    public void CloseIndicator()
    {
        IsBusy = false;
    }

    public void RefreshToken()
    {
        if (_tokenSource.IsCancellationRequested)
        {
            _tokenSource = new CancellationTokenSource();
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        var _ = _tokenSource.CancelAsync();
    }
}