using System.ComponentModel;

namespace WpfPerfBench.Core.Enum;

public enum MigrationStatus
{
    [Description("Ожидание")]
    Pending,

    [Description("Выполнена")]
    Applied,

    [Description("Ошибка")]
    Failed,

    [Description("Пропущена")]
    Skipped,

    [Description("Выполняется...")]
    Processing
}