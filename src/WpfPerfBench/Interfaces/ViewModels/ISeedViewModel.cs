using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Enum;

namespace WpfPerfBench.Interfaces.ViewModels;

public interface ISeedViewModel : IViewModelBase
{
    InitState CurrentState { get; set; }

    IRelayCommand NextCommand { get; }

    IAsyncRelayCommand SeedCommand { get; }
}