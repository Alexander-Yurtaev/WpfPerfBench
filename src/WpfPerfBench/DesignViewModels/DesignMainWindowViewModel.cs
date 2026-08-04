using WpfPerfBench.Managers;
using WpfPerfBench.ViewModels;

namespace WpfPerfBench.DesignViewModels;

public class DesignMainWindowViewModel : MainWindowViewModel
{
    public DesignMainWindowViewModel() : base(
        () => new DesignInitViewModel(),
        () => new DesignMigrationViewModel(),
        () => new DesignSeedViewModel(),
        () => new DesignStandViewModel(),
        new NavigationService(),
        new ThemeManager(),
        new BusyManager())
    {
        NavigationService.CurrentViewModel = new DesignInitViewModel();
        //NavigationService.CurrentViewModel = new DesignMigrationViewModel();
        //NavigationService.CurrentViewModel = new DesignSeedViewModel();
        //NavigationService.CurrentViewModel = new DesignStandViewModel();
    }
}