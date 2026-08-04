using System.Collections.ObjectModel;
using System.Reflection;
using WpfPerfBench.Core.Interfaces;
using WpfPerfBench.Data;
using WpfPerfBench.Factories;
using WpfPerfBench.Interfaces.Managers;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.SeedMethods;

namespace WpfPerfBench.ViewModels;

public class SeedViewModel : ViewModelBase, ISeedViewModel, ILoadable
{
    private readonly ISeedMethodFactory _seedMethodFactory;

    public SeedViewModel(
        INavigationService navigationService,
        IUserSession userSession,
        ISeedMethodFactory seedMethodFactory) : base(navigationService, userSession)
    {
        _seedMethodFactory = seedMethodFactory;
    }

    public int SeedCount => 1_000_000;

    public ObservableCollection<SeedMethodBase> SeedMethods { get; set; } = [];

    #region Implementation of ILoadable

    public void Load()
    {
        FillSeedMethods();
    }

    #endregion

    #region Static Methods

    protected virtual void FillSeedMethods()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var seedMethods = assembly.GetTypes()
            .Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(SeedMethodBase)));
        
        foreach (var seedMethod in seedMethods)
        {
            var method = _seedMethodFactory.Create(seedMethod);
            if (SeedMethods.Any(m => m.Title == method.Title)) continue;
            SeedMethods.Add(method);
        }
    }

    #endregion Static Methods
}