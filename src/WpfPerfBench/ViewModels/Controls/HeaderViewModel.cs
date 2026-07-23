using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Core.Interfaces.Services;
using WpfPerfBench.Managers;

namespace WpfPerfBench.ViewModels.Controls;

public partial class HeaderViewModel : ObservableObject
{
    [ObservableProperty] 
    private string _iconPath = "pack://application:,,,/Resources/Icons/wpb.ico";
    
    [ObservableProperty]
    private string _title = string.Empty;

    public HeaderViewModel(string title, INavigationService navigationService)
    {
        NavigationService = navigationService;
        Title = title;
    }

    public INavigationService NavigationService { get; }
}