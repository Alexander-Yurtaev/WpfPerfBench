using System.Collections.ObjectModel;
using System.Reflection;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Factories;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.Managers;
using WpfPerfBench.SeedMethods;

namespace WpfPerfBench.ViewModels;

public partial class SeedViewModel : ViewModelBase, ISeedViewModel
{
    private readonly IDataService _dataService;
    private readonly IGeneratorService _generatorService;
    private readonly ISeedMethodFactory _seedMethodFactory;

    public SeedViewModel(
        INavigationService navigationService,
        IUserSession userSession,
        IDataService dataService,
        IGeneratorService generatorService,
        ISeedMethodFactory seedMethodFactory) : base(navigationService, userSession)
    {
        _dataService = dataService;
        _generatorService = generatorService;
        _seedMethodFactory = seedMethodFactory;
        FillSeedMethods();
    }

    public int SeedStats => 1_000_000;

    public ObservableCollection<SeedMethodBase> SeedMethods { get; set; } = [];

    #region Static Methods

    private void FillSeedMethods()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var seedMethods = assembly.GetTypes()
            .Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(SeedMethodBase)));
        
        foreach (var seedMethod in seedMethods)
        {
            var method = (SeedMethodBase)_seedMethodFactory.Create(seedMethod);
            SeedMethods.Add(method);
        }
    }

    #endregion Static Methods
}