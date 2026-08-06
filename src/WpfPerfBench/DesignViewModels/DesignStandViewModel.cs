using System.Collections.ObjectModel;
using WpfPerfBench.Core.Data;
using WpfPerfBench.Core.Helpers;
using WpfPerfBench.Core.Managers;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Models;
using WpfPerfBench.ViewModels;

namespace WpfPerfBench.DesignViewModels;

public class DesignStandViewModel : StandViewModel
{
    public DesignStandViewModel() : base(
        null!, 
        null!, 
        null!, 
        new UserSession(), 
        null!,
        new ConsoleManager())
    {
        Icon = "👋";
        Fio = "Иванов Иван Иванович";
        DataProvider = "DataProvider";
        TotalRecordCount = 1_000_000;
        TreeItems = new ObservableCollection<CategoryTreeItem>(GetTreeItems());
        SelectedItem = TreeItems.FirstOrDefault();
        var items = Enumerable.Range(1, 10)
            .Select(r => new Item { Id = r, CategoryId = 1, Name = $"Item{r}" })
            .ToList();
        foreach (var item in items)
        {
            Items.Add(item);
        }

        var logItems = new List<LogItem>
        {
            new LogItem(DateTime.Now, "Загрузка дерева", TimeSpanHelper.ToHmsfFormatString(TimeSpan.Parse("12:34:56.789")))
        };

        foreach (var item in logItems)
        {
            ConsoleManager.LogItems.Add(item);
        }
    }

    public object? SelectedItem { get; set; }

    private CategoryTreeItem[] GetTreeItems()
    {
        var parent = new CategoryTreeItem(1, "Category 1", null, 2000);

        var child = new CategoryTreeItem(2, "Child 11", 1, 1000);
        parent.Children.Add(child);

        return [parent];
    }
}