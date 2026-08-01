namespace WpfPerfBench.Data;

public class LogItem
{
    public LogItem(DateTime time, string level, string message)
    {
        Time = time;
        Level = level;
        Message = message;
    }

    public DateTime Time { get; set; }
    public string Level { get; set; }
    public string Message { get; set; }
}