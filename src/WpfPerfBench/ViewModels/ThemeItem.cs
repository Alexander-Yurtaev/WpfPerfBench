using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Core.Enums;

namespace WpfPerfBench.ViewModels;

public partial class ThemeItem(string iconPath, string title) : ObservableObject
{
    public Theme Theme { get; init; }

    public string IconPath { get; } = iconPath;

    public string Title { get; } = title;

    [ObservableProperty]
    private bool _isSelected;

    public static ThemeItem CreateDefault(Theme theme)
    {
        var item = Create(theme);
        item.IsSelected = true;
        return item;
    }

    public static ThemeItem Create(Theme theme)
    {
        return theme switch
        {
            Theme.Light => new ThemeItem("pack://application:,,,/Resources/Icons/light.png", "Светлая")
                { Theme = theme },
            _ => new ThemeItem("pack://application:,,,/Resources/Icons/dark.png", "Темная")
                { Theme = theme },
        };
    }

    public Uri GetThemeUrl()
    {
        return Theme switch
        {
            Theme.Light => new Uri("Themes/LightTheme.xaml", UriKind.Relative),
            _ => new Uri("Themes/DarkTheme.xaml", UriKind.Relative),
        };
    }
}