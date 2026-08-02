using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Core.Enums;

namespace WpfPerfBench.Wrappers;

public partial class MigrationItem(string name) : ObservableObject
{
    [ObservableProperty] private MigrationStatus _status = MigrationStatus.Pending;

    public string Name { get; set; } = name;
    
    public static MigrationItem CreateApplied(string name) => new MigrationItem(name) { Status = MigrationStatus.Applied };
    public static MigrationItem CreateFailed(string name) => new MigrationItem(name) { Status = MigrationStatus.Failed };
}