using WpfPerfBench.Core.Enums;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.Managers;

namespace WpfPerfBench.Interfaces.Managers;

public interface INavigationService
{
    Page CurrentPage { get; set; }

    int TotalPages { get; }

    event EventHandler<NavigateEventArgs>? OnNavigate;

    void NavigatePrev();

    void NavigateNext();

    void AddPage(Page page, Func<IViewModelBase> factory);

    bool CanNext();

    bool CanPrev();
}