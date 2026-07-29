using WpfPerfBench.Enums;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.Managers;

public interface INavigationService
{
    Page CurrentPage { get; set; }

    int TotalSteps { get; }

    event EventHandler<NavigateEventArgs>? OnNavigate;

    void NavigatePrev();

    void NavigateNext();

    void AddPage(Page page, Func<IViewModelBase> factory);

    bool CanNext();

    bool CanPrev();
}