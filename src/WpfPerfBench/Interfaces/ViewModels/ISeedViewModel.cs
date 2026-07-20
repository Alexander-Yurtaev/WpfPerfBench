using CommunityToolkit.Mvvm.Input;

namespace WpfPerfBench.Interfaces.ViewModels;

public interface ISeedViewModel : IViewModelBase
{
    IRelayCommand NextCommand { get; }

    IAsyncRelayCommand SeedCommand { get; }
}