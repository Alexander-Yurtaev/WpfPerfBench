using WpfPerfBench.Core.Enums;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.Managers;

public class NavigateEventArgs
{
    public Page ToPage { get; }
    public Func<IViewModelBase> Factory { get; }

    public NavigateEventArgs(Page toPage, Func<IViewModelBase> factory)
    {
        ToPage = toPage;
        Factory = factory;
    }
}