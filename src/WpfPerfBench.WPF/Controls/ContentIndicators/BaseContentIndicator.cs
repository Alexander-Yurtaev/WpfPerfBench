using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.WPF.Interfaces.Managers;

namespace WpfPerfBench.WPF.Controls.ContentIndicators;

public abstract partial class BaseContentIndicator(IBusyManager busyManager) : ObservableObject
{
    [ObservableProperty] private string _busyText = string.Empty;
    [ObservableProperty] private string _busySubText = string.Empty;
    [ObservableProperty] private bool _isIndeterminate;

    [RelayCommand]
    private async Task Cancel()
    {
        await busyManager.CancelAsync();
    }
}