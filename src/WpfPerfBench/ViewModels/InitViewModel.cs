using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Data.Enums;

namespace WpfPerfBench.ViewModels;

public partial class InitViewModel : BaseViewModel
{
    public InitViewModel()
    {
        Title = "Окно инициализации";
        Icon = "🚀";
        FooterTitle = "Окно инициализации: валидация в реальном времени, выбор БД, прогресс-бар";
    }

    [ObservableProperty]
    private string _fio = string.Empty;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _confirmPassword = string.Empty;

    [ObservableProperty]
    private string _dbType = string.Empty;

    [ObservableProperty]
    private DataProvider _connectionString = DataProvider.Postgres;

    [ObservableProperty]
    private bool _migrateCheck;
}