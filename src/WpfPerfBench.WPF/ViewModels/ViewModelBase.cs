using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Data;
using WpfPerfBench.WPF.Interfaces.Managers;
using WpfPerfBench.WPF.Interfaces.ViewModels;

namespace WpfPerfBench.WPF.ViewModels;

public abstract class ViewModelBase : ObservableObject, IViewModelBase
{
    protected INavigationService NavigationService { get; }
    protected IUserSession UserSession { get; }

    protected ViewModelBase(INavigationService navigationService, IUserSession userSession)
    {
        NavigationService = navigationService;
        UserSession = userSession;
    }
}