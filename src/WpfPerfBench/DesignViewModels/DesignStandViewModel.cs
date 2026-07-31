using WpfPerfBench.Core.Helpers;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Models;

namespace WpfPerfBench.DesignViewModels;

public class DesignStandViewModel
{
    public DesignStandViewModel()
    {
        Icon = "👋";
        Fio = "Иванов Иван Иванович";
        DataProvider = "DataProvider";
        TotalRecordCount = 1_000_000;
        TreeItems = GetTreeItems();
        SelectedItem = TreeItems.FirstOrDefault();
        var item = new Item
        {
            Id = 1,
            CategoryId = (SelectedItem as CategoryTreeItem)!.Id,
            Name = "Item1"
        };
        Items = [item];
        StatItems =
        [
            new StatItem("Загрузка дерева", TimeSpanHelper.ToHmsfFormatString(TimeSpan.Parse("12:34:56.789")))
        ];
    }

    public string Icon { get; set; }
    public string Fio { get; set; }
    public string DataProvider { get; set; }
    public int TotalRecordCount { get; set; }
    public CategoryTreeItem[] TreeItems { get; set; }
    public object? SelectedItem { get; set; }
    public List<Item> Items { get; set; }
    public List<StatItem> StatItems { get; set; }

    private CategoryTreeItem[] GetTreeItems()
    {
        var parent = new CategoryTreeItem(1, "Category 1", null, 2000);

        var child = new CategoryTreeItem(2, "Child 11", 1, 1000);
        parent.Children.Add(child);

        return [parent];
    }
}