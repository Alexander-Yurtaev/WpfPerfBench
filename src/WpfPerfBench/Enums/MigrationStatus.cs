using System.ComponentModel;

namespace WpfPerfBench.Enums;

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