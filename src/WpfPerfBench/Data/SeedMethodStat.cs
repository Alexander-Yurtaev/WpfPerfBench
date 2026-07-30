namespace WpfPerfBench.Data;

public class SeedMethodStat
{
    public int ProcessedItemCount { get; set; }
    public int TotalItemCount { get; set; }
    public TimeSpan Duration { get; set; }
    public long MemoryBefore { get; set; }
    public long MemoryAfter { get; set; }
}