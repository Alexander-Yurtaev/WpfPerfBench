using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfPerfBench.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    private Header _header = new Header("", "");

    [ObservableProperty]
    private string _footerTitle = string.Empty;
}

public partial class Header : ObservableObject
{
    [ObservableProperty]
    private string _icon = string.Empty;

    [ObservableProperty]
    private string _title = string.Empty;

    public Header(string icon, string title)
    {
        Icon = icon;
        Title = title;
    }
}