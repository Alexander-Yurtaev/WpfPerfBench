namespace WpfPrefBench.Data.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int ParentId { get; set; }
        public virtual Category? Parent { get; set; }
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }
        public string Color { get; set; } = "#000000";
        public virtual List<Category> Child { get; set; } = [];
    }
}
