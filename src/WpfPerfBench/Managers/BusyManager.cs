using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Controls.ContentIndicators;
using BaseContentIndicator = WpfPerfBench.Controls.ContentIndicators.BaseContentIndicator;
using LargeContentIndicator = WpfPerfBench.Controls.ContentIndicators.LargeContentIndicator;
using ProgressContentIndicator = WpfPerfBench.Controls.ContentIndicators.ProgressContentIndicator;

namespace WpfPerfBench.Managers;

public partial class BusyManager : ObservableObject, IBusyManager
{
    private CancellationTokenSource _tokenSource = new();

    [ObservableProperty] private bool _isBusy;

    [ObservableProperty] private BaseContentIndicator _contentIndicator;

    public BusyManager()
    {
        ContentIndicator = new StandardContentIndicator(this);
    }

    public CancellationToken ShowStandardIndicator(string text, string subText = "Пожалуйста, подождите")
    {
        ContentIndicator = new StandardContentIndicator(this)
        {
            BusyText = text,
            BusySubText = subText
        };
        RefreshToken();
        IsBusy = true;
        return _tokenSource.Token;
    }

    public CancellationToken ShowProgressIndicator(double min, double max, string text, string subText = "0%")
    {
        ContentIndicator = new ProgressContentIndicator(this)
        {
            Minimum = min,
            Maximum = max,
            BusyText = text,
            BusySubText = subText
        };

        RefreshToken();
        IsBusy = true;
        return _tokenSource.Token;
    }

    public CancellationToken ShowLargeIndicator(double max, string text, string subTextFormat = "Загружено 0 / 0 элементов")
    {
        ContentIndicator = new LargeContentIndicator(this)
        {
            Maximum = max,
            BusyText = text,
            BusySubTextFormat = subTextFormat
        };

        RefreshToken();
        IsBusy = true;
        return _tokenSource.Token;
    }

    public void SetProgressIndicator(double value)
    {
        if (ContentIndicator is ProgressContentIndicator indicator)
        {
            indicator.Value = value;
        }
    }

    public void SetLargeIndicator(double value)
    {
        if (ContentIndicator is LargeContentIndicator indicator)
        {
            indicator.Value = value;
        }
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

    public async Task CancelAsync()
    {
        await _tokenSource.CancelAsync();
    }
}