using WpfPerfBench.Interfaces.Managers;
using WpfPerfBench.Managers;
using WpfPerfBench.ViewModels;

namespace WpfPerfBench.DesignViewModels;

public class DesignMainWindow
{
    public DesignMainWindow()
    {
        BusyManager = new BusyManager();
        Header = new DesignHeader();
        CurrentViewModel = new DesignInitViewModel();
        //CurrentViewModel = new DesignMigrationViewModel();
        //CurrentViewModel = new DesignSeedViewModel();
        //CurrentViewModel = new DesignStandViewModel();
    }

    public IBusyManager BusyManager { get; set; }
    public DesignHeader Header { get; set; }
    public ViewModelBase? CurrentViewModel { get; set; }
}