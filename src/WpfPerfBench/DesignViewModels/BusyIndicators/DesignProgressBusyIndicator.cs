using CommunityToolkit.Mvvm.Input;

namespace WpfPerfBench.DesignViewModels.BusyIndicators;

public class DesignProgressBusyIndicator
{
    public DesignProgressBusyIndicator()
    {
        BusyText = "Импорт данных...";
        BusySubText = "Обработка записей...";
        Minimum = 0;
        Maximum = 23_400;
        Value = 20_061;
        Percent = Value/Maximum;
        CancelCommand = new AsyncRelayCommand(
            async () => await Task.Delay(0), 
            () => true);
    }

    public string BusyText { get; set; }
    public string BusySubText { get; set; }
    public double Minimum { get; set; }
    public double Maximum { get; set; }
    public double Value { get; set; }
    public double Percent { get; set; }
    public IAsyncRelayCommand CancelCommand { get; set; }
}