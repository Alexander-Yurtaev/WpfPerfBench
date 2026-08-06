using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace WpfPerfBench.WPF.Converters;

public class EnumDescriptionConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null) return string.Empty;

        var enumType = value.GetType();
        var fieldInfo = enumType.GetField(value.ToString() ?? "");

        if (fieldInfo == null) return value.ToString() ?? "<NULL>";

        var attributes = (DescriptionAttribute[])fieldInfo
            .GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes.Length > 0 ? attributes[0].Description : (value.ToString() ?? "<NULL>");
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}