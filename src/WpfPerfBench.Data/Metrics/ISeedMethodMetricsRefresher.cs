namespace WpfPerfBench.Data.Metrics;

public interface ISeedMethodMetricsRefresher
{
    void UpdateProcessedItemCount(int count);
    void AddProcessedItemCount(int count);
    void UpdateTotalItemCount(int count);
    void UpdateDuration(TimeSpan duration);
    void UpdateMemoryBefore(bool callGC = false);
    void UpdateMemoryAfter(bool callGC = false);
    void UpdateIsIndeterminate(bool value);
    void Clean();
}