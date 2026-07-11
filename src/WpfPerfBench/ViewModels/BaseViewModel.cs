using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfPerfBench.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _icon = string.Empty;

    [ObservableProperty]
    private string _footerTitle = string.Empty;
}