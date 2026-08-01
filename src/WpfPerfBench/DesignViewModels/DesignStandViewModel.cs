using WpfPerfBench.Core.Helpers;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Models;
using Item = WpfPerfBench.Data.Entities.Item;

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
        Items = Enumerable.Range(1, 10)
            .Select(r => new Item { Id = r, CategoryId = 1, Name = $"Item{r}" })
            .ToList();
        
        StatItems =
        [
            new StatItem("Загрузка дерева", TimeSpanHelper.ToHmsfFormatString(TimeSpan.Parse("12:34:56.789")))
        ];

        LogItems = Enumerable.Range(1, 10)
            .Select(r => new LogItem(DateTime.Now.AddHours(r), "INFO", $"Message #{r}"))
            .ToList();
    }

    public string Icon { get; set; }
    public string Fio { get; set; }
    public string DataProvider { get; set; }
    public int TotalRecordCount { get; set; }
    public CategoryTreeItem[] TreeItems { get; set; }
    public object? SelectedItem { get; set; }
    public List<Item> Items { get; set; }
    public List<StatItem> StatItems { get; set; }
    public List<LogItem> LogItems { get; set; }

    private CategoryTreeItem[] GetTreeItems()
    {
        var parent = new CategoryTreeItem(1, "Category 1", null, 2000);

        var child = new CategoryTreeItem(2, "Child 11", 1, 1000);
        parent.Children.Add(child);

        return [parent];
    }
}