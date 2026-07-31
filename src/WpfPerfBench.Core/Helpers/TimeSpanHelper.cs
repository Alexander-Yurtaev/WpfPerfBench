namespace WpfPerfBench.Core.Helpers;

public static class TimeSpanHelper
{
    public const string HmsStringFormat = @"hh\:mm\:ss";
    public const string HmsfStringFormat = @"hh\:mm\:ss\.ff";

    public static string ToHmsFormatString(TimeSpan value)
    {
        return value.ToString(HmsStringFormat);
    }

    public static string ToHmsfFormatString(TimeSpan value)
    {
        return value.ToString(HmsfStringFormat);
    }
}