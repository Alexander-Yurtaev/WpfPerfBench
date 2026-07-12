using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Enums;

namespace WpfPerfBench.ViewModels;

public partial class StandViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _fio;

    [ObservableProperty]
    private DataProvider _dataProvider;

    [ObservableProperty]
    private string? _connectionString;

    public StandViewModel(IUserSession userSession)
    {
        Title = "Рабочий стенд";
        Icon = "🧪";
        FooterTitle = "Рабочий стенд: дерево, детали с контролами, виртуализированный список, карта с маршрутом";

        Fio = userSession.Fio;
        DataProvider = userSession.DataProvider;
        ConnectionString = userSession.ConnectionString;
    }
}