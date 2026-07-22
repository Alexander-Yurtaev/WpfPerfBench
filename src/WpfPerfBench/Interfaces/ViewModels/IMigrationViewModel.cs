using System.Collections.ObjectModel;
using WpfPerfBench.Data;

namespace WpfPerfBench.Interfaces.ViewModels;

public interface IMigrationViewModel : IViewModelBase
{
    ObservableCollection<MigrationItem> Items { get; set; }
}