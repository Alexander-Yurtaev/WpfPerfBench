using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Commands;

namespace WpfPerfBench.ViewModels.Dialogs;

public partial class BaseDialog : ObservableObject
{
    [ObservableProperty] private string _iconSource = string.Empty;
    [ObservableProperty] private string _header = string.Empty;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private List<RelayUICommand> _commands = [];

    public BaseDialog(string iconSource, string header, string description)
    {
        IconSource = iconSource;
        Header = header;
        Description = description;
    }
}