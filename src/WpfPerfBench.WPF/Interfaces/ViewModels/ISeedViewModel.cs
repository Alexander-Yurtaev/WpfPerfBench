using System.Collections.ObjectModel;
using SeedMethodBase = WpfPerfBench.WPF.SeedMethods.SeedMethodBase;

namespace WpfPerfBench.WPF.Interfaces.ViewModels;

public interface ISeedViewModel : IViewModelBase
{
    ObservableCollection<SeedMethodBase> SeedMethods { get; set; }
}