namespace WpfPerfBench.ViewModels;

public interface IInitProgressStandViewModel
{
    void SetConnectionProgress(string message);
    void SetMigrationStatus(string message);
    void SetTotalRecords(string message);
    void SetProgressMessage(string message);

    string ConnectionProgress { get; set; }

    string MigrationStatus { get; set; }

    string TotalRecords { get; set; }

    string ProgressMessage { get; set; }
}