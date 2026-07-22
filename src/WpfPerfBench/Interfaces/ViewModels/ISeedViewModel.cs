using System.Collections.ObjectModel;
using WpfPerfBench.SeedMethods;

namespace WpfPerfBench.Interfaces.ViewModels;

public interface ISeedViewModel : IViewModelBase
{
    ObservableCollection<SeedMethodBase> SeedMethods { get; set; }
}