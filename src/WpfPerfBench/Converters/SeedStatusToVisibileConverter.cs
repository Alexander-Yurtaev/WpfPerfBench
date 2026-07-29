using System.Globalization;
using System.Windows;
using System.Windows.Data;
using WpfPerfBench.Enums;

namespace WpfPerfBench.Converters;

public class SeedStatusToVisibilityConverter : IValueConverter
{
    #region Implementation of IValueConverter

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var status = value is SeedStatus seedStatus ? seedStatus : SeedStatus.None;

        var expected = parameter is SeedStatus expectedStatus ? expectedStatus : SeedStatus.None;

        return (expected.HasFlag(status)) ? Visibility.Visible : Visibility.Collapsed;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    #endregion
}