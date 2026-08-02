using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Commands;
using WpfPerfBench.Core.Enums;

namespace WpfPerfBench.ViewModels.Dialogs;

public partial class BaseDialog : ObservableObject
{
    [ObservableProperty] private string _iconSource = string.Empty;
    [ObservableProperty] private string _header = string.Empty;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private RelayUICommand? _primaryCommand;
    [ObservableProperty] private RelayUICommand? _secondaryCommand;

    public BaseDialog(string iconSource, string header, string description)
    {
        IconSource = iconSource;
        Header = header;
        Description = description;
    }

    public DialogResult Result { get; private set; }

    protected void PrimaryClick() => Result = DialogResult.Primary;
    protected void SecondaryClick() => Result = DialogResult.Secondary;
}