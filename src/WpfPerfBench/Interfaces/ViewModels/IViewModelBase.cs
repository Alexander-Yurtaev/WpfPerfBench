using System.ComponentModel;
using WpfPerfBench.ViewModels;

namespace WpfPerfBench.Interfaces.ViewModels;

public interface IViewModelBase
{
    public HeaderViewModel Header { get; set; }

    public string FooterTitle { get; set; }

    event PropertyChangedEventHandler? PropertyChanged;
    event PropertyChangingEventHandler? PropertyChanging;
}