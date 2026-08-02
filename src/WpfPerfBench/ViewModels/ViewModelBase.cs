using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Data;
using WpfPerfBench.Interfaces.Managers;
using WpfPerfBench.Interfaces.ViewModels;

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
}