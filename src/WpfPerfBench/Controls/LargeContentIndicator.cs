using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Managers;

namespace WpfPerfBench.Controls;

public partial class LargeContentIndicator(IBusyManager busyManager) : BaseContentIndicator(busyManager)
{
    [ObservableProperty] private double _maximum;
    [ObservableProperty] private double _value;
    [ObservableProperty] private double _percent;
    [ObservableProperty] private string _busySubTextFormat = string.Empty;

    partial void OnMaximumChanged(double value)
    {
        Percent = CalculatePercent();
        BusySubText = string.Format(BusySubTextFormat, Value, Maximum);
    }

    partial void OnValueChanged(double value)
    {
        Percent = CalculatePercent();
        BusySubText = string.Format(BusySubTextFormat, Value, Maximum);
    }

    private double CalculatePercent()
    {
        return Value / Maximum;
    }
}