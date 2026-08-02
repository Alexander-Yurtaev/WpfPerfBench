using CommunityToolkit.Mvvm.Input;

namespace WpfPerfBench.DesignViewModels;

public class DesignLargeBusyIndicator
{
    public DesignLargeBusyIndicator()
    {
        BusyText = "Построение карты...";
        Maximum = 23_400;
        Value = 20_061;
        BusySubText = $"Загружено {Value:N0} / {Maximum:N0} маркеров";
        Percent = Value/Maximum;
        CancelCommand = new AsyncRelayCommand(
            async () => await Task.Delay(0), 
            () => true);
    }

    public string BusyText { get; set; }
    public string BusySubText { get; set; }
    public double Maximum { get; set; }
    public double Value { get; set; }
    public double Percent { get; set; }
    public IAsyncRelayCommand CancelCommand { get; set; }
}