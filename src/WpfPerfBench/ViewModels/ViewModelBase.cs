using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Core.Interfaces.Services;
using WpfPerfBench.Data;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.ViewModels.Controls;

namespace WpfPerfBench.ViewModels;

public abstract partial class ViewModelBase : ObservableObject, IViewModelBase
{
    protected INavigationService NavigationService { get; }
    protected IUserSession UserSession { get; }

    protected ViewModelBase(INavigationService navigationService, IUserSession userSession)
    {
        NavigationService = navigationService;
        UserSession = userSession;
    }

    [ObservableProperty]
    private HeaderViewModel _header = new Controls.HeaderViewModel("Заголовок");
}