using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfPerfBench.ViewModels.Controls;

public partial class HeaderViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _iconPath = "pack://application:,,,/Resources/Icons/wpb.ico";
    
    [ObservableProperty]
    private string _title = string.Empty;

    public HeaderViewModel(string title)
    {
        Title = title;
    }
}