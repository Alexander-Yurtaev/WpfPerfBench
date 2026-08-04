using WpfPerfBench.Data.Models;

namespace WpfPerfBench.Interfaces.ViewModels;

public interface ITreeViewHelper
{
    CategoryTreeItem? SelectedTreeItem { get; set; }
}