using WpfPerfBench.Core.Helpers;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Models;
using WpfPerfBench.Managers;

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

        var logItems = new List<LogItem>
        {
            new LogItem(DateTime.Now, "Загрузка дерева", TimeSpanHelper.ToHmsfFormatString(TimeSpan.Parse("12:34:56.789")))
        };

        ConsoleManager = new ConsoleManager();
        foreach (var item in logItems)
        {
            ConsoleManager.LogItems.Add(item);
        }
    }

    public string Icon { get; set; }
    public string Fio { get; set; }
    public string DataProvider { get; set; }
    public int TotalRecordCount { get; set; }
    public CategoryTreeItem[] TreeItems { get; set; }
    public object? SelectedItem { get; set; }
    public List<Item> Items { get; set; }
    public IConsoleManager ConsoleManager { get; set; }

    private CategoryTreeItem[] GetTreeItems()
    {
        var parent = new CategoryTreeItem(1, "Category 1", null, 2000);

        var child = new CategoryTreeItem(2, "Child 11", 1, 1000);
        parent.Children.Add(child);

        return [parent];
    }
}