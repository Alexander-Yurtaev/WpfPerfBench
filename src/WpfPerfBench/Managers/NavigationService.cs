using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Enums;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.Managers;

public partial class NavigationService : ObservableObject, INavigationService
{
    [ObservableProperty] 
    private Page _currentPage;

    public int CurrentPageNumber => (int)CurrentPage;

    public int TotalSteps => _factories.Count;

    private readonly Dictionary<Page, Func<IViewModelBase>> _factories = [];

    public event EventHandler<NavigateEventArgs>? OnNavigate;

    public void NavigatePrev()
    {
        var prevPage = Enum.GetValues<Page>()
            .LastOrDefault(p => p < CurrentPage, Page.None);

        if (prevPage is Page.None)
        {
            return;
        }

        CurrentPage = prevPage;
        OnNavigate?.Invoke(this, new NavigateEventArgs(CurrentPage, _factories[CurrentPage]));
    }

    public void NavigateNext()
    {
        var nextPage = Enum.GetValues<Page>()
            .FirstOrDefault(p => p > CurrentPage, Page.None);

        if (nextPage is Page.None)
        {
            return;
        }

        CurrentPage = nextPage;
        OnNavigate?.Invoke(this, new NavigateEventArgs(CurrentPage, _factories[CurrentPage]));
    }

    public void AddPage(Page page, Func<IViewModelBase> factory)
    {
        _factories.Add(page, factory);
    }

    public bool CanPrev()
    {
        var firstPage = Enum.GetValues<Page>()
            .Where(p => p != Page.None)
            .FirstOrDefault(Page.None);
        return firstPage != Page.None && CurrentPage != firstPage;
    }

    public bool CanNext()
    {
        var lastPage = Enum.GetValues<Page>()
            .Where(p => p != Page.None)
            .LastOrDefault(Page.None);
        return lastPage != Page.None && CurrentPage != lastPage;
    }

    partial void OnCurrentPageChanged(Page value)
    {
        OnPropertyChanged(nameof(CurrentPageNumber));
    }
}