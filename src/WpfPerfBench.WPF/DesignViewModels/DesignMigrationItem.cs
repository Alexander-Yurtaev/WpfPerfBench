using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfPerfBench.Core.Enums;

namespace WpfPerfBench.WPF.DesignViewModels;

public class DesignMigrationItem : INotifyPropertyChanged
{
    private MigrationStatus _status;

    public DesignMigrationItem()
    {
        Name = "20260709162035_InitialPostgres";
        SetField(ref _status, MigrationStatus.Pending, nameof(Status));
        Task.Run(async () => await RefreshStatus());
    }

    private async Task RefreshStatus()
    {
        while (true)
        {
            foreach (var value in Enum.GetValues<MigrationStatus>())
            {
                SetField(ref _status, value, nameof(Status));
                await Task.Delay(5000);
            }
        }
    }

    public string Name { get; set; }

    public MigrationStatus Status
    {
        get => _status;
        set => _status = value;
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}