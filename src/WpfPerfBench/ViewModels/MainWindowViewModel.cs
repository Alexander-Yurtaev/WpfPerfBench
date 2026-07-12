using System.ComponentModel;
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
        TotalSteps = _viewModels.Count();

        NextCommand.Execute(null);
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
        var vm = ConvertToValidationViewModelBase(CurrentViewModel);
        if (vm is not null)
        {
            vm.Validate();
            RefreshCommand();
            if (!NextCommand.CanExecute(null)) return;
            vm.ErrorsChanged -= VmOnErrorsChanged;
        }

        var index = CurrentViewModel is null ? -1 : _viewModels.IndexOf(CurrentViewModel);
        index++;
        CurrentViewModel = _viewModels[index];
        vm = ConvertToValidationViewModelBase(CurrentViewModel);
        if (vm is null) return;
        vm.ErrorsChanged += VmOnErrorsChanged;
    }

    private ValidationViewModelBase? ConvertToValidationViewModelBase(ObservableObject? currentViewModel)
    {
        return currentViewModel as ValidationViewModelBase;
    }

    private void VmOnErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
    {
        RefreshCommand();
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