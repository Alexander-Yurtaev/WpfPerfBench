using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfPerfBench.Managers;

public partial class BusyManager : ObservableObject, IBusyManager
{
    [ObservableProperty] private bool _isBusy;
}