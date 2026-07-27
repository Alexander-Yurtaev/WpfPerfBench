using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Managers;

namespace WpfPerfBench.Controls.ContentIndicators;

public partial class ProgressContentIndicator(IBusyManager busyManager) : BaseContentIndicator(busyManager)
{
    [ObservableProperty] private double _minimum;
    [ObservableProperty] private double _maximum;
    [ObservableProperty] private double _value;
    [ObservableProperty] private double _percent;

    partial void OnMinimumChanged(double value)
    {
        Percent = CalculatePercent();
    }

    partial void OnMaximumChanged(double value)
    {
        Percent = CalculatePercent();
    }

    partial void OnValueChanged(double value)
    {
        Percent = CalculatePercent();
    }

    private double CalculatePercent()
    {
        return (Value - Minimum) / (Maximum - Minimum);
    }
}