namespace WpfPerfBench.Data;

public interface ISeedMethodStat
{
    void UpdateProcessedItemCount(int count);
    void UpdateTotalItemCount(int count);
    void UpdateDuration(TimeSpan duration);
    void UpdateMemory(int memory, string unit);
    void UpdateIsIndeterminate(bool value);
    void Clean();
}