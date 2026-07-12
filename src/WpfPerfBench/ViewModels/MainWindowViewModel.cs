using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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

    [ObservableProperty] 
    private ICommand _nextCommand;

    public MainWindowViewModel(InitViewModel initViewModel, Stand stand)
    {
        NextCommand = new RelayCommand(OnNext, CanNext);

        _viewModels.Add(initViewModel);
        _viewModels.Add(stand);
        CurrentViewModel = _viewModels.FirstOrDefault();

        TotalSteps = _viewModels.Count();
        InitCurrentStep();
    }

    private void InitCurrentStep()
    {
        CurrentStep = CurrentViewModel is null
            ? 0
            : _viewModels.IndexOf(CurrentViewModel) + 1;
    }

    public void RefreshCommand()
    {
        ((RelayCommand)NextCommand).NotifyCanExecuteChanged();
    }

    private void OnNext()
    {
        if (CurrentViewModel is not InitViewModel initViewModel) return;
        initViewModel.Validate();
        RefreshCommand();
        if (!NextCommand.CanExecute(null)) return;
        CurrentViewModel = _viewModels.LastOrDefault();
    }

    partial void OnCurrentViewModelChanged(ObservableObject? value)
    {
        InitCurrentStep();
    }

    private bool CanNext()
    {
        return CurrentViewModel is InitViewModel { IsValid: true };
    }
}