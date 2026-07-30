using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Data.Metrics;

namespace WpfPerfBench.Wrappers;

public partial class SeedMethodMetricsWrapper(SeedMethodMetrics model) : ObservableObject, ISeedMethodMetricsRefresher
{
    private readonly SeedMethodMetrics _model = model;

    public int ProcessedItemCount
    {
        get => _model.ProcessedItemCount;
        private set
        {
            if (_model.ProcessedItemCount == value) return;
            _model.ProcessedItemCount = value;
            OnPropertyChanged();
        }
    }

    public int TotalItemCount
    {
        get => _model.TotalItemCount;
        private set
        {
            if (_model.TotalItemCount == value) return;
            _model.TotalItemCount = value;
            OnPropertyChanged();
        }
    }

    public TimeSpan Duration
    {
        get => _model.Duration;
        private set
        {
            if (_model.Duration == value) return;
            _model.Duration = value;
            OnPropertyChanged();
        }
    }

    public long MemoryBefore
    {
        get => _model.MemoryBefore;
        private set
        {
            if (_model.MemoryBefore == value) return;
            _model.MemoryBefore = value;
            OnPropertyChanged();
        }
    }

    public long MemoryAfter
    {
        get => _model.MemoryAfter;
        private set
        {
            if (_model.MemoryAfter == value) return;
            _model.MemoryAfter = value;
            OnPropertyChanged();
        }
    }

    [ObservableProperty] private bool _isIndeterminate;

    public void UpdateProcessedItemCount(int count)
    {
        ProcessedItemCount = count;
    }

    public void UpdateTotalItemCount(int count)
    {
        TotalItemCount = count;
    }

    public void UpdateDuration(TimeSpan duration)
    {
        Duration = duration;
    }

    public void UpdateMemoryBefore(bool callGC = false)
    {
        if (callGC) ForceGC();
        var before = GC.GetTotalMemory(false);
        MemoryBefore = before;
    }

    public void UpdateMemoryAfter(bool callGC = false)
    {
        if (callGC) ForceGC();
        var before = GC.GetTotalMemory(false);
        MemoryAfter = before;
    }

    public void UpdateIsIndeterminate(bool value)
    {
        IsIndeterminate = value;
    }

    public void Clean()
    {
        UpdateProcessedItemCount(0);
        UpdateTotalItemCount(0);
        UpdateDuration(TimeSpan.Zero);
        MemoryBefore = 0;
        MemoryAfter = 0;
        UpdateIsIndeterminate(false);
    }

    private void ForceGC()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }
}