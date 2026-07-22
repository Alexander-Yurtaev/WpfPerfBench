using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Core.Interfaces.Services;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Services;
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

    #region Seed

    private bool CanSeed() => false; //CurrentPage == Page.Seed;

    [RelayCommand(CanExecute = nameof(CanSeed))]
    private async Task Seed()
    {
        try
        {
            var db = _dataService.CreateContext();

            // CleanItems
            var result = await _dataService.CleanItems(db, CancellationToken.None);

            if (!result.Success)
            {
                //CurrentPage = Page.Seed;
                return;
            }

            // Seed
            var items = _generatorService.GenerateListItemModel(1_000_000);
            result = await _dataService.SeedItems(db, items, CancellationToken.None);

            if (!result.Success)
            {
                // CurrentPage = Page.Seed;
                return;
            }

            //CurrentPage = Page.Stand;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            //CurrentPage = Page.Seed;
        }
    }

    #endregion Seed

    #region Next

    private bool CanNext() => false; //CurrentPage == Page.Stand;

    [RelayCommand(CanExecute = nameof(CanNext))]
    private void Next()
    {
        _navigationService.NavigateNext();
    }

    #endregion Next
}