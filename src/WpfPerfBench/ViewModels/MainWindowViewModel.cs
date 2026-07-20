using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Core.Services;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly Dictionary<int, Func<IViewModelBase>> _viewModels = [];

    [ObservableProperty]
    private IViewModelBase? _currentViewModel;

    [ObservableProperty]
    private int _currentStep;

    [ObservableProperty]
    private int _totalSteps;

    public MainWindowViewModel(
        Func<IInitViewModel> initViewModel,
        Func<ISeedViewModel> seedViewModel,
        Func<IStandViewModel> standViewModel,
        INavigationService navigationService)
    {
        _navigationService = navigationService;
        
        _viewModels.Add(1, initViewModel);
        _viewModels.Add(2, seedViewModel);
        _viewModels.Add(3, standViewModel);

        TotalSteps = _viewModels.Count();

        _navigationService.OnNavigateNext += NavigationServiceOnNavigateNext;
        _navigationService.NavigateNext();
    }

    private void NavigationServiceOnNavigateNext(object? sender, EventArgs e)
    {
        NavigateTo(CurrentStep + 1);
    }

    private void NavigateTo(int step)
    {
        if (_viewModels.TryGetValue(step, out var factory))
        {
            CurrentStep = step;
            CurrentViewModel = factory();
        }
        else
        {
            CurrentStep = 0;
            CurrentViewModel = null;
        }
    }
}