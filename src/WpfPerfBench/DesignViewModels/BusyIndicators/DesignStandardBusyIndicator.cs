using CommunityToolkit.Mvvm.Input;

namespace WpfPerfBench.DesignViewModels.BusyIndicators;

public class DesignStandardBusyIndicator
{
    public DesignStandardBusyIndicator()
    {
        BusyText = "Загрузка данных...";
        BusySubText = "Пожалуйста, подождите";
        CancelCommand = new AsyncRelayCommand(
            async () => await Task.Delay(0), 
            () => true);
    }

    public string BusyText { get; set; }
    public string BusySubText { get; set; }
    public IAsyncRelayCommand CancelCommand { get; set; }
}