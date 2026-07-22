using WpfPerfBench.Core.Enum;

namespace WpfPerfBench.Data;

public class MigrationItem(string name)
{
    public string Name { get; set; } = name;
    public MigrationStatus Status { get; set; } = MigrationStatus.Pending;

    public static MigrationItem CreateApplied(string name) => new MigrationItem(name) { Status = MigrationStatus.Applied };
    public static MigrationItem CreateFailed(string name) => new MigrationItem(name) { Status = MigrationStatus.Failed };
}