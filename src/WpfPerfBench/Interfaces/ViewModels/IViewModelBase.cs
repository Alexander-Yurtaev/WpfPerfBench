using WpfPerfBench.ViewModels.Controls;

namespace WpfPerfBench.Interfaces.ViewModels;

public interface IViewModelBase
{
    public HeaderViewModel Header { get; set; }
}