using WpfPerfBench.Data.Models;

namespace WpfPerfBench.ViewModels;

public interface ITreeViewHelper
{
    CategoryTreeItem? SelectedTreeItem { get; set; }
}