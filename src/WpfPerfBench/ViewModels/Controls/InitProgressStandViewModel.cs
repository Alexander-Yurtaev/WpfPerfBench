using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.ViewModels.Controls;

namespace WpfPerfBench.ViewModels;

public partial class InitProgressStandViewModel : ViewModelBase, IInitProgressStandViewModel
{
    [ObservableProperty] 
    private string _connectionProgress = "-";

    [ObservableProperty]
    private string _migrationStatus = "-";

    [ObservableProperty]
    private string _totalRecords = "-";

    [ObservableProperty]
    private string _progressMessage = "-";

    [ObservableProperty] 
    private bool _isIndeterminate;

    [ObservableProperty] 
    private double _min;

    [ObservableProperty]
    private double _max;

    [ObservableProperty]
    private double _value;

    public void SetConnectionProgress(string message)
    {
        ConnectionProgress = message;
    }

    public void SetMigrationStatus(string message)
    {
        MigrationStatus = message;
    }

    public void SetTotalRecords(string message)
    {
        TotalRecords = message;
    }

    public void SetProgressMessage(string message)
    {
        ProgressMessage = message;
    }

    public void ShowBusy(bool isBusy)
    {
        IsIndeterminate = isBusy;
    }

    public void InitProgressbar(double min, double max)
    {
        Min = min;
        Max = max;
    }

    public void SetProgress(double value)
    {
        Value = value;
    }
}