namespace WpfPerfBench.Core.Services;

public interface INavigationService
{
    event EventHandler<EventArgs>? OnNavigateNext;
    void NavigateNext();
}