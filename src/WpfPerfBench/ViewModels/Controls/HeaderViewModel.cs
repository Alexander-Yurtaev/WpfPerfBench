using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Managers;

namespace WpfPerfBench.ViewModels.Controls;

public partial class HeaderViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _iconPath = "pack://application:,,,/Resources/Icons/wpb.ico";
    
    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _description = string.Empty;

    public HeaderViewModel(string title, 
        IThemeManager themeManager)
    {
        ThemeManager = themeManager;
        Title = title;
    }

    public IThemeManager ThemeManager { get; }

    public bool HasDescription => !string.IsNullOrEmpty(Description);

    partial void OnDescriptionChanged(string value)
    {
        OnPropertyChanged(nameof(HasDescription));
    }
}