using System.ComponentModel;

namespace WpfPerfBench.Enums;

public enum SeedStatus
{
    [Description("")]
    None,

    [Description("Выполняется...")]
    Processing,

    [Description("Выполнено")]
    Finished,

    [Description("Ошибка")]
    Failed,

    [Description("Прервано")]
    Canceled
}