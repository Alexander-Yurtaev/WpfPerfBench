using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Data;

namespace WpfPerfBench.ViewModels;

public partial class StandViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _icon;

    [ObservableProperty]
    private string _header;

    [ObservableProperty]
    private string _themeIcon;

    [ObservableProperty]
    private StatItem[] _statItems;

    public StandViewModel(IUserSession userSession)
    {
        Title = "Рабочий стенд";
        Icon = "🧪";
        FooterTitle = "Рабочий стенд: дерево, детали с контролами, виртуализированный список, карта с маршрутом";

        Icon = "👋";
        Header = $"{userSession.Fio}|{userSession.DataProvider}";
        ThemeIcon = "☀️";
        StatItems = [];
    }
}