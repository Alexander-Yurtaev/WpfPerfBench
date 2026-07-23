using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using WpfPerfBench.Enums;
using WpfPerfBench.Interfaces;

namespace WpfPerfBench.Managers;

public class ThemeManager : IThemeManager, ILoadable
{
    public ThemeManager()
    {
        Themes = [];
    }

    public ObservableCollection<ThemeItem> Themes { get; set; }

    #region Implementation of ILoadable

    public void Load()
    {
        Themes.Add(ThemeItem.CreateDefault(Theme.Dark));
        Themes.Add(ThemeItem.Create(Theme.Light));

        foreach (var theme in Themes)
        {
            theme.PropertyChanged += ThemeOnPropertyChanged;
        }
    }

    private void ThemeOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(ThemeItem.IsSelected)) return;
        var item = (ThemeItem)sender!;
        if (!item.IsSelected) return;
        ResetOtherThemes(item);
        ApplyTheme(item);
    }

    #endregion

    #region Private Methods

    private void ResetOtherThemes(ThemeItem item)
    {
        foreach (var themeItem in Themes.Where(t => t.IsSelected && t.Title != item.Title))
        {
            themeItem.IsSelected = false;
        }
    }

    private void ApplyTheme(ThemeItem themeItem)
    {
        var newThemeDict = new ResourceDictionary
        {
            Source = themeItem.GetThemeUrl()
        };

        var appResources = Application.Current.Resources;
        var oldThemeDict = appResources.MergedDictionaries
            .FirstOrDefault(d => d.Source?.OriginalString?.Contains("Theme") == true);
        if (oldThemeDict != null)
        {
            var index = appResources.MergedDictionaries.IndexOf(oldThemeDict);
            appResources.MergedDictionaries[index] = newThemeDict;
        }
        else
        {
            appResources.MergedDictionaries.Add(newThemeDict);
        }
    }

    #endregion Private Methods
}