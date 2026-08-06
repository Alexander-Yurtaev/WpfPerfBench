using WpfPerfBench.Data.Models;

namespace WpfPerfBench.WPF.Interfaces.ViewModels;

public interface ITreeViewHelper
{
    CategoryTreeItem? SelectedTreeItem { get; set; }
}