using WpfPerfBench.Core.Enum;
using WpfPerfBench.Core.Interfaces.Services;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.Managers;

public class NavigationService : INavigationService
{
    public Page CurrentPage { get; set; }

    public int TotalSteps => _factories.Count;

    private readonly Dictionary<Page, Func<IViewModelBase>> _factories = [];

    public event EventHandler<NavigateEventArgs>? OnNavigateNext;

    public void NavigateNext()
    {
        var nextPage = Enum.GetValues<Page>()
            .FirstOrDefault(p => p > CurrentPage, Page.None);

        if (nextPage is Page.None)
        {
            return;
        }

        CurrentPage = nextPage;
        OnNavigateNext?.Invoke(this, new NavigateEventArgs(CurrentPage, _factories[CurrentPage]));
    }

    public void AddPage(Page page, Func<IViewModelBase> factory)
    {
        _factories.Add(page, factory);
    }
}