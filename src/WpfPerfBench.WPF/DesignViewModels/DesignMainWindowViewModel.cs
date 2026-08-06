using WpfPerfBench.WPF.Managers;
using WpfPerfBench.WPF.ViewModels;
using NavigationService = WpfPerfBench.WPF.Managers.NavigationService;

namespace WpfPerfBench.WPF.DesignViewModels;

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