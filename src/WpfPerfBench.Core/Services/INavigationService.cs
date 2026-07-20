using WpfPerfBench.Core.Enum;

namespace WpfPerfBench.Core.Services;

public interface INavigationService
{
    Page CurrentPage { get; set; }

    event EventHandler<EventArgs>? OnNavigateNext;
    void NavigateNext();
}