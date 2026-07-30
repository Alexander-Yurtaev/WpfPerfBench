using System.Globalization;
using System.Windows.Data;

namespace WpfPerfBench.Converters;

public class BytesToFormatStringConverter : IValueConverter
{
    #region Implementation of IValueConverter

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var bCount = value is long i ? i : 0;

        if (bCount < 1024)
        {
            return $"{bCount:N0}";
        }

        var kCount = bCount / 1024m;
        if (kCount < 1024L)
        {
            return kCount == (long)kCount ? $"{kCount:N0} Kb" : $"{kCount:N1} Kb";
        }

        var mCount = kCount / 1024m;
        if (mCount < 1024L)
        {
            return mCount == (long)mCount ? $"{mCount:N0} Mb" : $"{mCount:N1} Mb";
        }

        var gCount = mCount / 1024m;
        if (gCount < 1024L)
        {
            return gCount == (long)gCount ? $"{gCount:N0} Mb" : $"{gCount:N1} Mb";
        }

        var tCount = gCount / 1024m;
        return tCount == (long)tCount ? $"{tCount:N0} Tb" : $"{tCount:N1} Tb";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    #endregion
}