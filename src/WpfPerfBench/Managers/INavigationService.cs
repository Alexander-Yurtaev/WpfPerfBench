using WpfPerfBench.Core.Enum;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.Managers;

public interface INavigationService
{
    Page CurrentPage { get; set; }

    int TotalSteps { get; }

    event EventHandler<NavigateEventArgs>? OnNavigateNext;
    void NavigateNext();

    void AddPage(Page page, Func<IViewModelBase> factory);
}