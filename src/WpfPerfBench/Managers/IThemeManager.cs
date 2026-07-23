using System.Collections.ObjectModel;

namespace WpfPerfBench.Managers;

public interface IThemeManager
{
    ObservableCollection<ThemeItem> Themes { get; set; }
    void Load();
}