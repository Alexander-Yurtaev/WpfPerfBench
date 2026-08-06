using System.Collections.ObjectModel;
using WpfPerfBench.WPF.ViewModels;

namespace WpfPerfBench.WPF.DesignViewModels;

public class DesignThemeSwitcher
{
    public DesignThemeSwitcher()
    {
        var light = new ThemeItem("pack://application:,,,/WpfPerfBench.WPF;component/Resources/Icons/light.ico", "Светлая");
        var dark = new ThemeItem("pack://application:,,,/WpfPerfBench.WPF;component/Resources/Icons/dark.ico", "Темная");
        ThemeItem[] themes = [light, dark];
        Themes = new ObservableCollection<ThemeItem>(themes);
    }
    public ObservableCollection<ThemeItem> Themes { get; set; }
}