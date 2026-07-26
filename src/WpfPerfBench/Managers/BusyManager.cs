using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WpfPerfBench.Managers;

public partial class BusyManager : ObservableObject, IBusyManager
{
    private CancellationTokenSource _tokenSource = new();

    [ObservableProperty] private bool _isBusy;

    [ObservableProperty] private string _busyText = string.Empty;

    [ObservableProperty] private string _busySubText = string.Empty;

    [ObservableProperty] private double _minimum;

    [ObservableProperty] private double _maximum;

    [ObservableProperty] private double _value;

    public CancellationToken ShowStandardIndicator(string text, string subText = "Пожалуйста, подождите")
    {
        BusyText = text;
        BusySubText = subText;
        RefreshToken();
        IsBusy = true;
        return _tokenSource.Token;
    }

    public CancellationToken ShowProgressIndicator(double min, double max, string text, string subText = "0%")
    {
        Minimum = min;
        Maximum = max;
        BusyText = text;
        BusySubText = subText;
        RefreshToken();
        IsBusy = true;
        return _tokenSource.Token;
    }

    public void SetProgressIndicator(double value)
    {
        Value = value;
    }

    public void SetLargeIndicator(double value)
    {
        Value = value;
    }

    public CancellationToken ShowLargeIndicator(double max, string text, string subText = "Загружено 0 / 0 элементов")
    {
        Maximum = max;
        BusyText = text;
        BusySubText = subText;
        RefreshToken();
        IsBusy = true;
        return _tokenSource.Token;
    }

    public CancellationToken ShowCompactIndicator(string text, string subText = "")
    {
        BusyText = text;
        BusySubText = subText;
        RefreshToken();
        IsBusy = true;
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