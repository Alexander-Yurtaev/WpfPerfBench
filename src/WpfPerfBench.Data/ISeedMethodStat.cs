namespace WpfPerfBench.Data;

public interface ISeedMethodStat
{
    void UpdateProcessedItemCount(int count);
    void UpdateTotalItemCount(int count);
    void UpdateDuration(TimeSpan duration);
    void UpdateMemoryBefore(long memory);
    void UpdateMemoryAfter(long memory);
    void UpdateIsIndeterminate(bool value);
    void Clean();
}