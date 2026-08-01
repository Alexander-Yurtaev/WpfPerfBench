using System.Collections.ObjectModel;
using WpfPerfBench.Data;

namespace WpfPerfBench.Managers;

public interface IConsoleManager
{
    ObservableCollection<LogItem> LogItems { get; }
    void Log(string title, string value);
    void Log(string title, TimeSpan value);
}