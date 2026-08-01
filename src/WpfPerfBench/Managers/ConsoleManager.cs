using System.Collections.ObjectModel;
using WpfPerfBench.Core.Helpers;
using WpfPerfBench.Data;

namespace WpfPerfBench.Managers;

public class ConsoleManager : IConsoleManager
{
    public ConsoleManager()
    {
        LogItems = [];
    }

    public ObservableCollection<LogItem> LogItems { get; set; }
    public void Log(string title, string value)
    {
        var logItem = new LogItem(DateTime.Now, title, value);
        LogItems.Add(logItem);
    }

    public void Log(string title, TimeSpan timeSpan)
    {
        var value = TimeSpanHelper.ToHmsFormatString(timeSpan);
        if (value == "00:00:00")
        {
            value = TimeSpanHelper.ToHmsfFormatString(timeSpan);
        }

        Log(title, value);
    }
}