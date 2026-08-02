using System.ComponentModel;

namespace WpfPerfBench.Core.Enums;

[Flags]
public enum SeedStatus
{
    [Description("")]
    None = 1,

    [Description("Выполняется...")]
    Processing = 2,

    [Description("Выполнено")]
    Finished = 4,

    [Description("Ошибка")]
    Failed = 8,

    [Description("Прервано")]
    Canceled = 16,

    [Description("")]
    CanStart = None | Finished | Failed | Canceled,
}