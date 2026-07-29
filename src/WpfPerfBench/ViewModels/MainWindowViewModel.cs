using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Enums;
using WpfPerfBench.Interfaces;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.Managers;
using WpfPerfBench.ViewModels.Controls;

namespace WpfPerfBench.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly Dictionary<object, bool> _allowed = [];

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

        NavigationService.OnNavigate += NavigationServiceOnNavigate;
        NavigationService.NavigateNext();

        Header = new HeaderViewModel("WPF Performance Demo", ThemeManager)
        {
            Description = "интерактивный макет"
        };
    }

    public IThemeManager ThemeManager { get; }
    public IBusyManager BusyManager { get; }

    [ObservableProperty]
    private HeaderViewModel _header;

    private void NavigationServiceOnNavigate(object? sender, NavigateEventArgs e)
    {
        UnsubscribeFromNavigatable(CurrentViewModel as INavigatable);
        CurrentViewModel = e.Factory();
        SubscribeFromNavigatable(CurrentViewModel as INavigatable);

        RefreshCommands();
    }

    private void UnsubscribeFromNavigatable(INavigatable? navigatable)
    {
        if (navigatable is null) return;
        navigatable.OnNavigatable -= OnNavigatable;
    }

    private void SubscribeFromNavigatable(INavigatable? navigatable)
    {
        if (navigatable is null) return;
        navigatable.OnNavigatable += OnNavigatable;
    }

    private void OnNavigatable(object? sender, NavigatableEventArgs e)
    {
        _allowed[e.Type] = e.Allowed;
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

    private bool CanNext()
    {
        var allowed = _allowed.GetValueOrDefault(NavigationType.Next, false);
        return allowed && NavigationService.CanNext();
    }

    [RelayCommand(CanExecute = nameof(CanNext))]
    private void Next()
    {
        NavigationService.NavigateNext();
    }

    #endregion Next
}