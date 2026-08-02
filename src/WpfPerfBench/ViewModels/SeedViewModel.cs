using System.Collections.ObjectModel;
using System.Reflection;
using WpfPerfBench.Data;
using WpfPerfBench.Factories;
using WpfPerfBench.Interfaces.Managers;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.SeedMethods;

namespace WpfPerfBench.ViewModels;

public class SeedViewModel : ViewModelBase, ISeedViewModel
{
    private readonly ISeedMethodFactory _seedMethodFactory;

    public SeedViewModel(
        INavigationService navigationService,
        IUserSession userSession,
        ISeedMethodFactory seedMethodFactory) : base(navigationService, userSession)
    {
        _seedMethodFactory = seedMethodFactory;
        FillSeedMethods();
    }

    public int SeedCount => 1_000_000;

    public ObservableCollection<SeedMethodBase> SeedMethods { get; set; } = [];

    #region Static Methods

    private void FillSeedMethods()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var seedMethods = assembly.GetTypes()
            .Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(SeedMethodBase)));
        
        foreach (var seedMethod in seedMethods)
        {
            var method = _seedMethodFactory.Create(seedMethod);
            SeedMethods.Add(method);
        }
    }

    #endregion Static Methods
}