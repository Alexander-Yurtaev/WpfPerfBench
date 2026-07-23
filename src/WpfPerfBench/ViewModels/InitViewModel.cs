using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using WpfPerfBench.Core.Enum;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Enums;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.Managers;

namespace WpfPerfBench.ViewModels;

public partial class InitViewModel : ValidationViewModelBase, IInitViewModel
{
    private readonly IUserSession _userSession;
    private readonly IDataService _dataService;

    public InitViewModel(
        INavigationService navigationService, 
        IUserSession userSession,
        IDataService dataService) : base(navigationService, userSession)
    {
        _navigationService = navigationService;
        _userSession = userSession;
        _dataService = dataService;
        Header = new Controls.HeaderViewModel("Настройка подключения", NavigationService);
        _navigationService.OnNavigateNext += NavigationServiceOnOnNavigateNext;
    }

    [ObservableProperty]
    private INavigationService _navigationService;

    [ObservableProperty]
    private string _fio = string.Empty;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _confirmPassword = string.Empty;

    [ObservableProperty]
    private DataProvider[] _dbTypes = [DataProvider.Postgres, DataProvider.MsSql];

    [ObservableProperty]
    private DataProvider _dbType = DataProvider.Postgres;

    [ObservableProperty]
    private string _connectionString = string.Empty;

    [ObservableProperty] 
    private string _validationStatus;

    #region Test

    private bool CanTest() => _navigationService.CurrentPage == Page.Init;

    [RelayCommand(CanExecute = nameof(CanTest))]
    private async Task Test()
    {
        try
        {
            Validate();
            if (!IsValid)
            {
                return;
            }

            InitUserSession();

            var db = _dataService.CreateContext();

            var resultTest = await _dataService.TestConnection(db, CancellationToken.None);

            if (!resultTest.Success)
            {
                _navigationService.NavigateNext();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    #endregion Test

    #region Migrate

    private bool CanMigrate() => _navigationService.CurrentPage == Page.Migration;

    [RelayCommand(CanExecute = nameof(CanMigrate))]
    private async Task Migrate(CancellationToken ct)
    {
        // InitProgressStand.SetMigrationStatus("Подготовка к миграции ...");

        try
        {
            var db = _dataService.CreateContext();

            var result = await _dataService.GetPendingMigrationsAsync(db, ct);

            if (!result.Success)
            {
                _navigationService.NavigateNext();
                return;
            }

            var migrations = (result as NamesResult)?.Names.ToArray()
                             ?? 
                             throw new InvalidOperationException("Ошибка получения списка миграций");

            foreach (var migration in migrations)
            {
                await _dataService.Migrate(db, migration, ct);
            }

            _navigationService.NavigateNext();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    #endregion Migrate

    #region Next

    private bool CanNext() => true; //CurrentPage == Page.Ready;

    [RelayCommand(CanExecute = nameof(CanNext))]
    private void Next()
    {
        NavigationService.NavigateNext();
    }

    #endregion Next

    private void InitUserSession()
    {
        _userSession.Fio = Fio;
        _userSession.DataProvider = DbType;
        _userSession.ConnectionString = ConnectionString;
    }

    protected override void ValidateProperty(string? propertyName)
    {
        if (string.IsNullOrEmpty(propertyName)) return;

        this.ClearError(propertyName);

        switch (propertyName)
        {
            case nameof(Fio):
                ValidateFio();
                break;
            case nameof(Email):
                ValidateEmail();
                break;
            case nameof(Password):
                ValidatePassword();
                break;
            case nameof(ConfirmPassword):
                ValidateConfirmPassword();
                break;
            case nameof(DbType):
                ValidateDbType();
                break;
            case nameof(ConnectionString):
                ValidateConnectionString();
                break;
            default:
                return;
        }

        OnErrorsChanged(propertyName);
    }

    private void NavigationServiceOnOnNavigateNext(object? sender, NavigateEventArgs e)
    {
        RefreshCommands();
    }

    private void RefreshCommands()
    {
        TestCommand.NotifyCanExecuteChanged();
        MigrateCommand.NotifyCanExecuteChanged();
        NextCommand.NotifyCanExecuteChanged();
    }

    #region Overrides of ValidationViewModelBase

    protected override void OnErrorsChanged(string propertyName)
    {
        base.OnErrorsChanged(propertyName);
        ValidationStatus = HasErrors
                ? "❌ Некоторые поля заполнены некорректно • Исправьте ошибки"
                : "✅ Все поля валидны";
    }

    #endregion

    #region Overrides of ObservableObject

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        ValidateProperty(e.PropertyName);
        base.OnPropertyChanged(e);
    }

    #endregion

    #region Validation rules

    private void ValidateFio()
    {
        if (string.IsNullOrWhiteSpace(Fio) || string.IsNullOrEmpty(Fio))
        {
            AddError(nameof(Fio), "ФИО обязательно для заполнения");
        }
        else if (Fio.Length < 3)
        {
            AddError(nameof(Fio), "ФИО должно содержать минимум 3 символа");
        }
        else if (Fio.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length < 2)
        {
            AddError(nameof(Fio), "Введите ФИО полностью (Имя и Фамилию)");
        }
    }

    private void ValidateEmail()
    {
        if (string.IsNullOrWhiteSpace(Email))
        {
            AddError(nameof(Email), "Email обязателен для заполнения");
            return;
        }

        try
        {
            var addr = new System.Net.Mail.MailAddress(Email);
            if (addr.Address != Email)
            {
                AddError(nameof(Email), "Некорректный формат email");
            }
        }
        catch
        {
            AddError(nameof(Email), "Некорректный формат email");
        }
    }

    private void ValidatePassword()
    {
        if (string.IsNullOrWhiteSpace(Password))
        {
            AddError(nameof(Password), "Пароль обязателен для заполнения");
        } else if (Password.Length < 8)
        {
            AddError(nameof(Password), "Пароль должен содержать минимум 8 символов");
        } else if (!Password.Any(char.IsDigit))
        {
            AddError(nameof(Password), "Пароль должен содержать хотя бы одну цифру");
        } else if (!Password.Any(char.IsUpper))
        {
            AddError(nameof(Password), "Пароль должен содержать хотя бы одну заглавную букву");
        }
    }

    private void ValidateConfirmPassword()
    {
        if (string.IsNullOrWhiteSpace(ConfirmPassword))
        {
            AddError(nameof(ConfirmPassword), "Подтверждение пароля обязательно");
        } else if (ConfirmPassword != Password)
        {
            AddError(nameof(ConfirmPassword), "Пароли не совпадают");
        }
    }

    private void ValidateDbType()
    {

    }

    private void ValidateConnectionString()
    {
        if (string.IsNullOrWhiteSpace(ConnectionString))
        {
            AddError(nameof(ConnectionString), "ConnectionString обязательно для заполнения");
        }
    }

    #endregion Validation rules
}