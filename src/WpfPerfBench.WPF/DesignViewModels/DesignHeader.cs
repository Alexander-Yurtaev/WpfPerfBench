using System.Collections.ObjectModel;
using WpfPerfBench.WPF.Interfaces.Managers;
using WpfPerfBench.WPF.Managers;
using WpfPerfBench.WPF.ViewModels;

namespace WpfPerfBench.WPF.DesignViewModels;

public class DesignHeader
{
    public DesignHeader()
    {
        IconPath = "pack://application:,,,/WpfPerfBench.WPF;component/Resources/Icons/wpb.ico";
        Title = "WPF Performance Demo";
        Description = "интерактивный макет";
        ThemeManager = new ThemeManager();
    }

    public string IconPath { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool HasDescription => !string.IsNullOrEmpty(Description);
    public IThemeManager ThemeManager { get; set; }
}

public class DesignThemeManager : IThemeManager
{
    public DesignThemeManager()
    {
        var light = new ThemeItem("pack://application:,,,/WpfPerfBench.WPF;component/Resources/Icons/light.ico", "Светлая");
        var dark = new ThemeItem("pack://application:,,,/WpfPerfBench.WPF;component/Resources/Icons/dark.ico", "Темная");
        ThemeItem[] themes = [light, dark];
        Themes = new ObservableCollection<ThemeItem>(themes);
    }

    #region Implementation of IThemeManager

    public ObservableCollection<ThemeItem> Themes { get; set; }

    public void Load()
    {
        
    }

    #endregion
}