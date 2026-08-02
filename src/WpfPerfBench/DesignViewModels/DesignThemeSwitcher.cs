using System.Collections.ObjectModel;
using WpfPerfBench.ViewModels;

namespace WpfPerfBench.DesignViewModels;

public class DesignThemeSwitcher
{
    public DesignThemeSwitcher()
    {
        var light = new ThemeItem("pack://application:,,,/WpfPerfBench;component/Resources/Icons/light.ico", "Светлая");
        var dark = new ThemeItem("pack://application:,,,/WpfPerfBench;component/Resources/Icons/dark.ico", "Темная");
        ThemeItem[] themes = [light, dark];
        Themes = new ObservableCollection<ThemeItem>(themes);
    }
    public ObservableCollection<ThemeItem> Themes { get; set; }
}