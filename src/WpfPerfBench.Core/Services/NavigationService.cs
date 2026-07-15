namespace WpfPerfBench.Core.Services;

public class NavigationService : INavigationService
{
    public event EventHandler<EventArgs>? OnNavigateNext;

    public void NavigateNext()
    {
        OnNavigateNext?.Invoke(this, EventArgs.Empty);
    }
}