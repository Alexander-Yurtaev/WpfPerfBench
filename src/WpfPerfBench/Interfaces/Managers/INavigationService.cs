using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Core.Enums;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.Interfaces.Managers;

public interface INavigationService
{
    Page CurrentPage { get; set; }

    IViewModelBase? CurrentViewModel { get; set; }

    int TotalPages { get; }

    void AddPage(Page page, Func<IViewModelBase> factory);

    void AllowPrev(bool allow);
    void AllowNext(bool allow);

    void Block();
    
    void UnBlock();

    RelayCommand NavigatePrevCommand { get; }
    RelayCommand NavigateNextCommand { get; }
    void RefreshCommands();
}