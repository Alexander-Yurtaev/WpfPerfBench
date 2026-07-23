using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Core.Enum;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.Managers;
using WpfPerfBench.ViewModels.Controls;

namespace WpfPerfBench.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private IViewModelBase? _currentViewModel;

    public MainWindowViewModel(
        Func<IInitViewModel> initViewModel,
        Func<IMigrationViewModel> migrationViewModel,
        Func<ISeedViewModel> seedViewModel,
        Func<IStandViewModel> standViewModel,
        INavigationService navigationService,
        IThemeManager themeManager)
    {
        ThemeManager = themeManager;
        _navigationService = navigationService;

        _navigationService.AddPage(Page.Init, initViewModel);
        _navigationService.AddPage(Page.Migration, migrationViewModel);
        _navigationService.AddPage(Page.Seed, seedViewModel);
        _navigationService.AddPage(Page.Stand, standViewModel);

        _navigationService.OnNavigateNext += NavigationServiceOnNavigateNext;
        _navigationService.NavigateNext();
    }

    public IThemeManager ThemeManager { get; }

    [ObservableProperty]
    private HeaderViewModel _header;

    private void NavigationServiceOnNavigateNext(object? sender, NavigateEventArgs e)
    {
        CurrentViewModel = e.Factory();
        Header = new HeaderViewModel("WPF Performance Demo — интерактивный макет", _navigationService);
    }
}