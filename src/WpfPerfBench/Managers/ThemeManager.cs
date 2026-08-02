using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using WpfPerfBench.Core.Enums;
using WpfPerfBench.Core.Interfaces;
using WpfPerfBench.Interfaces;
using WpfPerfBench.Interfaces.Managers;

namespace WpfPerfBench.Managers;

public class ThemeManager : IThemeManager, ILoadable
{
    public ThemeManager()
    {
        Themes = [];
    }

    public ObservableCollection<ViewModels.ThemeItem> Themes { get; set; }

    #region Implementation of ILoadable

    public void Load()
    {
        Themes.Add(ViewModels.ThemeItem.CreateDefault(Theme.Dark));
        Themes.Add(ViewModels.ThemeItem.Create(Theme.Light));

        foreach (var theme in Themes)
        {
            theme.PropertyChanged += ThemeOnPropertyChanged;
        }
    }

    private void ThemeOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(ViewModels.ThemeItem.IsSelected)) return;
        var item = (ViewModels.ThemeItem)sender!;
        if (!item.IsSelected) return;
        ResetOtherThemes(item);
        ApplyTheme(item);
    }

    #endregion

    #region Private Methods

    private void ResetOtherThemes(ViewModels.ThemeItem item)
    {
        foreach (var themeItem in Themes.Where(t => t.IsSelected && t.Title != item.Title))
        {
            themeItem.IsSelected = false;
        }
    }

    private void ApplyTheme(ViewModels.ThemeItem themeItem)
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