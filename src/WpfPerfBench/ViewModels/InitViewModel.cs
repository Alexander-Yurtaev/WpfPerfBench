using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using WpfPerfBench.Core.Services;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Enums;
using WpfPerfBench.Data.Services;

namespace WpfPerfBench.ViewModels;

public enum InitState
{
    Init,
    Migration,
    Ready
}

public partial class InitViewModel : ValidationViewModelBase
{
    private const string TestConnectionKey = "<TestConnection>";

    private readonly INavigationService _navigationService;
    private readonly IUserSession _userSession;
    private readonly IDataService _dataService;

    public InitViewModel(
        INavigationService navigationService, 
        IUserSession userSession,
        IDataService dataService)
    {
        _navigationService = navigationService;
        _userSession = userSession;
        _dataService = dataService;
        Header = new Header("🚀", "Окно инициализации");
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
    private DataProvider[] _dbTypes = [DataProvider.Postgres, DataProvider.MsSql];

    [ObservableProperty]
    private DataProvider _dbType = DataProvider.Postgres;

    [ObservableProperty]
    private string _connectionString = string.Empty;

    [ObservableProperty]
    private InitState _currentState;

    [ObservableProperty] 
    private string _validationStatus;

    #region Overrides of ValidationViewModelBase

    public override void Validate()
    {
        ClearError(TestConnectionKey);
        base.Validate();
    }

    #endregion

    #region Test

    private bool CanTest() => CurrentState == InitState.Init;

    [RelayCommand(CanExecute = nameof(CanTest))]
    private async Task Test()
    {
        Validate();
        if (!IsValid) return;

        _userSession.Fio = Fio;
        _userSession.DataProvider = DbType;
        _userSession.ConnectionString = ConnectionString;

        var isChecked = await _dataService.TestConnection(CancellationToken.None);

        if (!isChecked)
        {
            AddError(TestConnectionKey, "Ошибка при подключении к БД");
            OnErrorsChanged(TestConnectionKey);
            return;
        }

        CurrentState = InitState.Migration;
    }

    #endregion Test

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

    partial void OnCurrentStateChanged(InitState value)
    {
        NotifyCanExecuteChangedForAllCommands();
    }

    private void NotifyCanExecuteChangedForAllCommands()
    {
        TestCommand.NotifyCanExecuteChanged();
        MigrateCommand.NotifyCanExecuteChanged();
        NextCommand.NotifyCanExecuteChanged();
    }

    #region Overrides of ValidationViewModelBase

    protected override void OnErrorsChanged(string propertyName)
    {
        base.OnErrorsChanged(propertyName);
        ValidationStatus = Errors.TryGetValue(TestConnectionKey, out var errors)
            ? errors.First()
            : HasErrors
                ? "❌ Некоторые поля заполнены некорректно • Исправьте ошибки"
                : "✅ Все поля валидны";
    }

    #endregion

    #region Migrate

    private bool CanMigrate() => CurrentState == InitState.Migration;

    [RelayCommand(CanExecute = nameof(CanMigrate))]
    private void Migrate()
    {
        
    }

    #endregion Migrate

    #region Next

    private bool CanNext() => CurrentState == InitState.Ready;

    [RelayCommand(CanExecute = nameof(CanNext))]
    private void Next()
    {
        _navigationService.NavigateNext();
    }

    #endregion Next

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
        if (string.IsNullOrWhiteSpace(Fio))
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