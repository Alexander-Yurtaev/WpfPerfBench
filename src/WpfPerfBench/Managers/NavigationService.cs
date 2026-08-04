using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Core.Enums;
using WpfPerfBench.Interfaces.Managers;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.Managers;

public partial class NavigationService : ObservableObject, INavigationService
{
    private RelayCommand _navigatePrevCommand = null!;
    private RelayCommand _navigateNextCommand = null!;

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

    public RelayCommand NavigatePrevCommand
    {
        get
        {
            return _navigatePrevCommand ??= new RelayCommand(NavigatePrev, () => GetAllowPrev() && CanPrev());
        }
    }

    public RelayCommand NavigateNextCommand
    {
        get
        {
            return _navigateNextCommand ??= new RelayCommand(NavigateNext, () => GetAllowNext() && CanNext());
        }
    }

    public void RefreshCommands()
    {
        NavigatePrevCommand.NotifyCanExecuteChanged();
        NavigateNextCommand.NotifyCanExecuteChanged();
    }

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
        var factory = _factories.GetValueOrDefault(value, null);
        if (factory is null) return;
        CurrentViewModel = factory();
        RefreshCommands();
    }
}