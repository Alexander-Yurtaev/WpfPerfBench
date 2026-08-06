using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Core.Enums;
using WpfPerfBench.WPF.Interfaces.Managers;
using WpfPerfBench.WPF.Interfaces.ViewModels;
using WpfPerfBench.WPF.ViewModels.Controls;

namespace WpfPerfBench.WPF.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private INavigationService _navigationService;

    public MainWindowViewModel(
        Func<IInitViewModel> initViewModel,
        Func<IMigrationViewModel> migrationViewModel,
        Func<ISeedViewModel> seedViewModel,
        Func<IStandViewModel> standViewModel,
        INavigationService navigationService,
        IThemeManager themeManager,
        IBusyManager busyManager)
    {
        ThemeManager = themeManager;
        BusyManager = busyManager;
        this.NavigationService = navigationService;

        NavigationService.AddPage(Page.Init, initViewModel);
        NavigationService.AddPage(Page.Migration, migrationViewModel);
        NavigationService.AddPage(Page.Seed, seedViewModel);
        NavigationService.AddPage(Page.Stand, standViewModel);

        NavigationService.NavigateNextCommand.Execute(null);

        Header = new HeaderViewModel("WPF Performance Demo", ThemeManager)
        {
            Description = "интерактивный макет"
        };
    }

    public IThemeManager ThemeManager { get; }
    public IBusyManager BusyManager { get; }

    [ObservableProperty]
    private HeaderViewModel _header;
}