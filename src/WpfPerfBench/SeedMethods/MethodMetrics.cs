using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfPerfBench.SeedMethods;

public partial class MethodMetrics : ObservableObject
{
    [ObservableProperty]
    private TimeSpan _duration;

    [ObservableProperty]
    private float _memory;
}