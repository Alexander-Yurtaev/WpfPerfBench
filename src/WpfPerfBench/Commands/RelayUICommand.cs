using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WpfPerfBench.Commands;

public partial class RelayUICommand : ObservableObject
{
    [ObservableProperty] private string _title;
    [ObservableProperty] private RelayCommand _command;


    public RelayUICommand(string title, RelayCommand command)
    {
        Title = title;
        Command = command;
    }
}