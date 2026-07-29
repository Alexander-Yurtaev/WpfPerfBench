using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Data;

namespace WpfPerfBench.Wrappers;

public class SeedMethodStatWrapper(SeedMethodStat model) : ObservableObject
{
    private readonly SeedMethodStat _model = model;

    public int ProcessedItemCount
    {
        get => _model.ProcessedItemCount;
        set
        {
            if (_model.ProcessedItemCount == value) return;
            _model.ProcessedItemCount = value;
            OnPropertyChanged();
        }
    }

    public int TotalItemCount => _model.TotalItemCount;

    public TimeSpan Duration
    {
        get => _model.Duration;
        set
        {
            if (_model.Duration == value) return;
            _model.Duration = value;
            OnPropertyChanged();
        }
    }

    public string MemoryString => $"{_model.Memory} {_model.MemoryUnit}";

    public void UpdateProcessedItemCount(int count)
    {
        ProcessedItemCount = count;
    }

    public void UpdateDuration(TimeSpan duration)
    {
        Duration = duration;
    }

    public void UpdateMemory(int memory, string unit)
    {
        _model.Memory = memory;
        _model.MemoryUnit = unit;
        OnPropertyChanged(nameof(MemoryString));
    }
}