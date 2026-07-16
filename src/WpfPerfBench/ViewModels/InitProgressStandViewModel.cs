using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;

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
}