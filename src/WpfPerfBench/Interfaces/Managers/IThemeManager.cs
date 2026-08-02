using System.Collections.ObjectModel;
using ThemeItem = WpfPerfBench.ViewModels.ThemeItem;

namespace WpfPerfBench.Interfaces.Managers;

public interface IThemeManager
{
    ObservableCollection<ThemeItem> Themes { get; set; }
    void Load();
}