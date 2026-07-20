using WpfPerfBench.Core.Enum;

namespace WpfPerfBench.Core.Services;

public class NavigationService : INavigationService
{
    public Page CurrentPage { get; set; }

    public event EventHandler<EventArgs>? OnNavigateNext;

    public void NavigateNext()
    {
        OnNavigateNext?.Invoke(this, EventArgs.Empty);
    }
}