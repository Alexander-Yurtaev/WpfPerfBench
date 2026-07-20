using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfPerfBench.ViewModels;

public partial class HeaderViewModel : ObservableObject
{
    [ObservableProperty]
    private string _icon = string.Empty;

    [ObservableProperty]
    private string _title = string.Empty;

    public HeaderViewModel(string icon, string title)
    {
        Icon = icon;
        Title = title;
    }
}