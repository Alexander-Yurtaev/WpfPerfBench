using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfPerfBench.WPF.Converters;

public class NullableVisibleConverter : IValueConverter
{
    #region Implementation of IValueConverter

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is not null ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    #endregion
}