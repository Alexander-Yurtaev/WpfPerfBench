using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Data;

namespace WpfPerfBench.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IUserSession _userSession;
    private readonly List<Func<ObservableObject>> _viewModels = [];

    [ObservableProperty]
    private ObservableObject? _currentViewModel;

    [ObservableProperty]
    private int _currentStep;

    [ObservableProperty]
    private int _totalSteps;

    public MainWindowViewModel(Func<InitViewModel> initViewModel, Func<StandViewModel> standViewModel, IUserSession userSession)
    {
        _userSession = userSession;
        
        _viewModels.Add(initViewModel);
        _viewModels.Add(standViewModel);

        CurrentStep = 0;
        TotalSteps = _viewModels.Count();

        NextCommand.Execute(null);
    }

    [RelayCommand(CanExecute = nameof(CanNext))]
    private void OnNext()
    {
        var vm = ConvertToValidationViewModelBase(CurrentViewModel);
        if (vm is not null)
        {
            vm.Validate();
            OnPropertyChanged(nameof(CanNext));
            RefreshCommand();
            if (!NextCommand.CanExecute(null)) return;
            vm.ErrorsChanged -= VmOnErrorsChanged;

            if (vm is InitViewModel initViewModel)
            {
                _userSession.Fio = initViewModel.Fio;
                _userSession.DataProvider = initViewModel.DbType;
                _userSession.ConnectionString = initViewModel.ConnectionString;
            }
        }

        CurrentStep++;
        vm = ConvertToValidationViewModelBase(CurrentViewModel);
        if (vm is null) return;
        vm.ErrorsChanged += VmOnErrorsChanged;
    }

    public void RefreshCommand()
    {
        NextCommand.NotifyCanExecuteChanged();
    }

    private ValidationViewModelBase? ConvertToValidationViewModelBase(ObservableObject? currentViewModel)
    {
        return currentViewModel as ValidationViewModelBase;
    }

    private void VmOnErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
    {
        OnPropertyChanged(nameof(CanNext));
    }

    partial void OnCurrentStepChanged(int value)
    {
        CurrentViewModel = _viewModels[CurrentStep - 1]();
    }

    private bool CanNext => CurrentViewModel is InitViewModel { IsValid: true };
}