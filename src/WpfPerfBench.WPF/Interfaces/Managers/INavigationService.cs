using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Core.Enums;
using WpfPerfBench.WPF.Interfaces.ViewModels;

namespace WpfPerfBench.WPF.Interfaces.Managers;

public interface INavigationService
{
    Page CurrentPage { get; set; }

    IViewModelBase? CurrentViewModel { get; set; }
    int CurrentPageNumber { get; }
    int TotalPages { get; }

    void AddPage(Page page, Func<IViewModelBase> factory);

    void AllowPrev(bool allow);
    void AllowNext(bool allow);

    void Block();
    
    void UnBlock();

    IRelayCommand NavigatePrevCommand { get; }
    IRelayCommand NavigateNextCommand { get; }
    void RefreshCommands();
}