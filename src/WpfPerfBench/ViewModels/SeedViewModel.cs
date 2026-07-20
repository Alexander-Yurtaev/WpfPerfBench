using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Core.Services;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Enum;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.ViewModels;

public partial class SeedViewModel : ViewModelBase, ISeedViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IUserSession _userSession;
    private readonly IDataService _dataService;
    private readonly IGeneratorService _generatorService;

    public SeedViewModel(
        INavigationService navigationService,
        IUserSession userSession,
        IDataService dataService,
        IGeneratorService generatorService)
    {
        _navigationService = navigationService;
        _userSession = userSession;
        _dataService = dataService;
        _generatorService = generatorService;
        Header = new HeaderViewModel("🚀", "Окно заполнения данными");
        FooterTitle = "Окно заполнения данными: генерация данных, заполнения БД разными методами";
    }

    [ObservableProperty]
    private InitState _currentState;

    #region Seed

    private bool CanSeed() => CurrentState == InitState.Seed;

    [RelayCommand(CanExecute = nameof(CanSeed))]
    private async Task Seed()
    {
        CurrentState = InitState.Busy;

        try
        {
            var db = _dataService.CreateContext();

            // CleanItems
            var result = await _dataService.CleanItems(db, CancellationToken.None);

            if (!result.Success)
            {
                CurrentState = InitState.Seed;
                return;
            }

            // Seed
            var items = _generatorService.GenerateListItemModel(1_000_000);
            result = await _dataService.SeedItems(db, items, CancellationToken.None);

            if (!result.Success)
            {
                CurrentState = InitState.Seed;
                return;
            }

            CurrentState = InitState.Ready;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            CurrentState = InitState.Seed;
        }
    }

    #endregion Seed

    #region Next

    private bool CanNext() => CurrentState == InitState.Ready;

    [RelayCommand(CanExecute = nameof(CanNext))]
    private void Next()
    {
        _navigationService.NavigateNext();
    }

    #endregion Next
}