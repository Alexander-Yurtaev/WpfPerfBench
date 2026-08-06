using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Core.Enums;
using WpfPerfBench.WPF.Interfaces.Managers;
using WpfPerfBench.WPF.Interfaces.ViewModels;

namespace WpfPerfBench.WPF.Managers;

public partial class NavigationService : ObservableObject, INavigationService
{
    private readonly Dictionary<NavigationType, bool> _allowed = [];
    private readonly Dictionary<Page, Func<IViewModelBase>> _factories = [];

    [ObservableProperty] 
    private Page _currentPage;

    [ObservableProperty]
    private IViewModelBase? _currentViewModel;

    public int CurrentPageNumber => (int)CurrentPage;
    public int TotalPages => _factories.Count;

    public void AllowPrev(bool allow)
    {
        _allowed[NavigationType.Prev] = allow;
    }
    public void AllowNext(bool allow)
    {
        _allowed[NavigationType.Next] = allow;
    }

    public void Block()
    {
        AllowPrev(false);
        AllowNext(false);
        RefreshCommands();
    }

    public void UnBlock()
    {
        AllowPrev(CanPrev());
        AllowNext(CanNext());
        RefreshCommands();
    }

    public void RefreshCommands()
    {
        NavigatePrevCommand.NotifyCanExecuteChanged();
        NavigateNextCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute = nameof(_navigatePrevCommandCanExecute))]
    private void NavigatePrev()
    {
        var prevPage = Enum.GetValues<Page>()
            .LastOrDefault(p => p < CurrentPage, Page.None);

        if (prevPage is Page.None)
        {
            return;
        }

        CurrentPage = prevPage;
    }

    private bool _navigatePrevCommandCanExecute() => GetAllowPrev() && CanPrev();

    [RelayCommand(CanExecute = nameof(_navigateNextCommandCanExecute))]
    private void NavigateNext()
    {
        var nextPage = Enum.GetValues<Page>()
            .FirstOrDefault(p => p > CurrentPage, Page.None);

        if (nextPage is Page.None)
        {
            return;
        }

        CurrentPage = nextPage;
    }

    private bool _navigateNextCommandCanExecute() => GetAllowNext() && CanNext();

    public void AddPage(Page page, Func<IViewModelBase> factory)
    {
        _factories.Add(page, factory);
    }

    private bool CanPrev()
    {
        var firstPage = Enum.GetValues<Page>()
            .Where(p => p != Page.None)
            .FirstOrDefault(Page.None);
        return firstPage != Page.None && CurrentPage > firstPage;
    }

    private bool CanNext()
    {
        var lastPage = Enum.GetValues<Page>()
            .Where(p => p != Page.None)
            .LastOrDefault(Page.None);
        return lastPage != Page.None && CurrentPage < lastPage;
    }

    private bool GetAllowPrev() => _allowed.GetValueOrDefault(NavigationType.Prev, true);
    private bool GetAllowNext() => _allowed.GetValueOrDefault(NavigationType.Next, true);

    partial void OnCurrentPageChanged(Page value)
    {
        OnPropertyChanged(nameof(CurrentPageNumber));
        if (!_factories.TryGetValue(value, out var factory)) return;
        CurrentViewModel = factory();
        RefreshCommands();
    }
}