using System.Collections.ObjectModel;
using ThemeItem = WpfPerfBench.WPF.ViewModels.ThemeItem;

namespace WpfPerfBench.WPF.Interfaces.Managers;

public interface IThemeManager
{
    ObservableCollection<ThemeItem> Themes { get; set; }
    void Load();
}