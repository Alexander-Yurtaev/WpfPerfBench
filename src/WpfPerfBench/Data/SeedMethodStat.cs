namespace WpfPerfBench.Data;

public class SeedMethodStat
{
    public int ProcessedItemCount { get; set; }
    public int TotalItemCount { get; set; }
    public TimeSpan Duration { get; set; }
    public int Memory { get; set; }
    public string MemoryUnit { get; set; } = string.Empty;
}