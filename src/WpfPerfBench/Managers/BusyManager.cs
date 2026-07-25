using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfPerfBench.Managers;

public partial class BusyManager : ObservableObject, IBusyManager
{
    private CancellationTokenSource _tokenSource = new();

    [ObservableProperty] private bool _isBusy;

    public CancellationToken CreateToken()
    {
        if (_tokenSource.IsCancellationRequested)
        {
            _tokenSource = new CancellationTokenSource();
        }
        return _tokenSource.Token;
    }
}