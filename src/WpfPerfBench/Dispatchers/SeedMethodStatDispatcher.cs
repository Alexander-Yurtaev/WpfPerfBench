using System.Windows;
using WpfPerfBench.Data.Metrics;
using WpfPerfBench.Wrappers;

namespace WpfPerfBench.Dispatchers;

public class SeedMethodStatDispatcher : ISeedMethodMetricsRefresher
{
    private readonly SeedMethodMetricsWrapper _wrapper;

    public SeedMethodStatDispatcher(SeedMethodMetricsWrapper wrapper)
    {
        _wrapper = wrapper;
    }

    #region Implementation of ISeedMethodStat

    public void UpdateProcessedItemCount(int count)
    {
        Application.Current.Dispatcher.Invoke(() => _wrapper.UpdateProcessedItemCount(count));
    }

    public void UpdateTotalItemCount(int count)
    {
        Application.Current.Dispatcher.Invoke(() => _wrapper.UpdateTotalItemCount(count));
    }

    public void UpdateDuration(TimeSpan duration)
    {
        Application.Current.Dispatcher.Invoke(() => _wrapper.UpdateDuration(duration));
    }

    public void UpdateMemoryBefore(long memory)
    {
        Application.Current.Dispatcher.Invoke(() => _wrapper.UpdateMemoryBefore(memory));
    }

    public void UpdateMemoryAfter(long memory)
    {
        Application.Current.Dispatcher.Invoke(() => _wrapper.UpdateMemoryBefore(memory));
    }

    public void UpdateIsIndeterminate(bool value)
    {
        Application.Current.Dispatcher.Invoke(() => _wrapper.UpdateIsIndeterminate(value));
    }

    public void Clean()
    {
        UpdateProcessedItemCount(0);
        UpdateTotalItemCount(0);
        UpdateDuration(TimeSpan.Zero);
        UpdateMemoryBefore(0);
        UpdateMemoryAfter(0);
    }

    #endregion
}