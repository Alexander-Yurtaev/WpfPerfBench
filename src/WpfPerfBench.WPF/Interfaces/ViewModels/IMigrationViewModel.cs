using System.Collections.ObjectModel;
using WpfPerfBench.WPF.Wrappers;

namespace WpfPerfBench.WPF.Interfaces.ViewModels;

public interface IMigrationViewModel : IViewModelBase
{
    ObservableCollection<MigrationItem> Items { get; set; }
}