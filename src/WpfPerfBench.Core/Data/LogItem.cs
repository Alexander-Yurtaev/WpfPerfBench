namespace WpfPerfBench.Core.Data;

public class LogItem
{
    public LogItem(DateTime time, string title, string value)
    {
        Time = time;
        Title = title;
        Value = value;
    }

    public DateTime Time { get; set; }
    public string Title { get; set; }
    public string Value { get; set; }
}