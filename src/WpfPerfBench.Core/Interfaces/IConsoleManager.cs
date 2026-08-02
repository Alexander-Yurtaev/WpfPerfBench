using System.Collections.ObjectModel;
using WpfPerfBench.Core.Data;

namespace WpfPerfBench.Core.Interfaces;

public interface IConsoleManager
{
    ObservableCollection<LogItem> LogItems { get; }
    void Log(string title, string value);
    void Log(string title, TimeSpan value);
}