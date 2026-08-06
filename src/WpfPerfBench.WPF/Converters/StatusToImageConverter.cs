using System.Globalization;
using System.Windows.Data;
using WpfPerfBench.Core.Enums;

namespace WpfPerfBench.WPF.Converters;

public class StatusToImageConverter : IValueConverter
{
    public string PendingIcon { get; set; } = string.Empty;
    public string SuccessIcon { get; set; } = string.Empty;
    public string FailIcon { get; set; } = string.Empty;
    public string SkippedIcon { get; set; } = string.Empty;
    public string ProcessingIcon { get; set; } = string.Empty;

    #region Implementation of IValueConverter

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var status = value is MigrationStatus migrationStatus 
            ? migrationStatus 
            : MigrationStatus.Skipped;

        return status switch
        {
            MigrationStatus.Pending => PendingIcon,
            MigrationStatus.Applied => SuccessIcon,
            MigrationStatus.Failed => FailIcon,
            MigrationStatus.Skipped => SkippedIcon,
            MigrationStatus.Processing => ProcessingIcon,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    #endregion
}