using System.Collections.ObjectModel;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Models;

namespace WpfPerfBench.Interfaces.ViewModels;

public interface IStandViewModel : IViewModelBase
{
    Task LoadAsync(CancellationToken ct);

    string Icon { get; set; }

    string Fio { get; set; }

    string DataProvider { get; set; }
    int TotalRecordCount { get; set; }

    ObservableCollection<StatItem> StatItems { get; set; }

    ObservableCollection<CategoryTreeItem> TreeItems { get; set; }
}