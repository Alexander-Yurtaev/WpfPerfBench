using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.ViewModels;

public abstract partial class ViewModelBase : ObservableObject, IViewModelBase
{
    [ObservableProperty]
    private HeaderViewModel _header = new HeaderViewModel("", "");

    [ObservableProperty]
    private string _footerTitle = string.Empty;
}