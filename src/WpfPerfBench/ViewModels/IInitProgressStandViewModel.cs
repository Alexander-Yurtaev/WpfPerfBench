namespace WpfPerfBench.ViewModels;

public interface IInitProgressStandViewModel
{
    void SetConnectionProgress(string message);
    void SetMigrationStatus(string message);
    void SetTotalRecords(string message);
    void SetProgressMessage(string message);
    void ShowBusy(bool isBusy);
    void InitProgressbar(double min, double max);
    void SetProgress(double value);

    string ConnectionProgress { get; set; }

    string MigrationStatus { get; set; }

    string TotalRecords { get; set; }

    string ProgressMessage { get; set; }
    bool IsIndeterminate { get; set; }
    double Min { get; set; }
    double Max { get; set; }
    double Value { get; set; }
}