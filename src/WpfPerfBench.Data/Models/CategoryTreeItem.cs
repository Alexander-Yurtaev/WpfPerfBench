namespace WpfPerfBench.Data.Models;

public class CategoryTreeItem
{
    public CategoryTreeItem(int id, string name, int? parentId, int itemsCount)
    {
        Id = id;
        Name = name;
        ParentId = parentId;
        ItemsCount = itemsCount;
        Children = [];
    }

    public int Id { get; }
    public string Name { get; }
    public int? ParentId { get; }
    public int ItemsCount { get; }
    public List<CategoryTreeItem> Children { get; set; }
}