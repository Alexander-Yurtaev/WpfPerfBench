namespace WpfPerfBench.Data.Models;

public class Item : BaseModel
{
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public decimal Price { get; set; }
    public float Weight { get; set; }
    public bool IsFragile { get; set; }
    public bool IsUrgent { get; set; }
    public string? Comments { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double DeliveryLatitude { get; set; }
    public double DeliveryLongitude { get; set; }
    public bool IsDeleted { get; set; }
}