using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Core.Services;

namespace WpfPerfBench.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly Dictionary<int, Func<ObservableObject>> _viewModels = [];

    [ObservableProperty]
    private ObservableObject? _currentViewModel;

    [ObservableProperty]
    private int _currentStep;

    [ObservableProperty]
    private int _totalSteps;

    public MainWindowViewModel(
        Func<InitViewModel> initViewModel, 
        Func<StandViewModel> standViewModel,
        INavigationService navigationService)
    {
        _navigationService = navigationService;
        
        _viewModels.Add(1, initViewModel);
        _viewModels.Add(2, standViewModel);

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