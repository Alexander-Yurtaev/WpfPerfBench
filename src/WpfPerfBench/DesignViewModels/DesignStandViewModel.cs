using WpfPerfBench.Data.Models;

namespace WpfPerfBench.DesignViewModels;

public class DesignStandViewModel
{
    public DesignStandViewModel()
    {
        Icon = "👋";
        Fio = "Ивано Иван Иванович";
        DataProvider = "DataProvider";
        TotalRecordCount = 1_000_000;
        TreeItems = GetTreeItems();
    }

    public string Icon { get; set; }
    public string Fio { get; set; }
    public string DataProvider { get; set; }
    public int TotalRecordCount { get; set; }
    public CategoryTreeItem[] TreeItems { get; set; }

    private CategoryTreeItem[] GetTreeItems()
    {
        var parent = new CategoryTreeItem(1, "Category 1", null, 2000);

        var child = new CategoryTreeItem(2, "Child 11", 1, 1000);
        parent.Children.Add(child);

        return [parent];
    }
}