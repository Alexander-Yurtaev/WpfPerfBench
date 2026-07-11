using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfPerfBench.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly List<ObservableObject> _viewModels = [];

    [ObservableProperty]
    private ObservableObject? _currentViewModel;

    [ObservableProperty]
    private int _currentStep;

    [ObservableProperty]
    private int _totalSteps;

    public MainWindowViewModel(InitViewModel initViewModel, StandViewModel standViewModel)
    {
        _viewModels.Add(initViewModel);
        _viewModels.Add(standViewModel);
        CurrentViewModel = _viewModels.FirstOrDefault();

        TotalSteps = _viewModels.Count();
        CurrentStep = CurrentViewModel is null
                                        ? 0
                                        : _viewModels.IndexOf(CurrentViewModel) + 1;
    }
}