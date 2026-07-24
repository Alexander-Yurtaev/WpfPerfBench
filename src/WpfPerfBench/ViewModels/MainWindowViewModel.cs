using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Core.Enum;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.Managers;
using WpfPerfBench.ViewModels.Controls;

namespace WpfPerfBench.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private IViewModelBase? _currentViewModel;

    [ObservableProperty]
    private INavigationService _navigationService;

    public MainWindowViewModel(
        Func<IInitViewModel> initViewModel,
        Func<IMigrationViewModel> migrationViewModel,
        Func<ISeedViewModel> seedViewModel,
        Func<IStandViewModel> standViewModel,
        INavigationService navigationService,
        IThemeManager themeManager)
    {
        ThemeManager = themeManager;
        this.NavigationService = navigationService;

        NavigationService.AddPage(Page.Init, initViewModel);
        NavigationService.AddPage(Page.Migration, migrationViewModel);
        NavigationService.AddPage(Page.Seed, seedViewModel);
        NavigationService.AddPage(Page.Stand, standViewModel);

        NavigationService.OnNavigate += NavigationServiceOnNavigate;
        NavigationService.NavigateNext();

        Header = new HeaderViewModel("WPF Performance Demo — интерактивный макет", ThemeManager);
    }

    public IThemeManager ThemeManager { get; }

    [ObservableProperty]
    private HeaderViewModel _header;

    private void NavigationServiceOnNavigate(object? sender, NavigateEventArgs e)
    {
        CurrentViewModel = e.Factory();
        RefreshCommands();
    }

    private void RefreshCommands()
    {
        PrevCommand.NotifyCanExecuteChanged();
        NextCommand.NotifyCanExecuteChanged();
    }

    #region Prev

    private bool CanPrev() => NavigationService.CanPrev();

    [RelayCommand(CanExecute = nameof(CanPrev))]
    private void Prev()
    {
        NavigationService.NavigatePrev();
    }

    #endregion Prev

    #region Next

    private bool CanNext() => NavigationService.CanNext();

    [RelayCommand(CanExecute = nameof(CanNext))]
    private void Next()
    {
        NavigationService.NavigateNext();
    }

    #endregion Next
}