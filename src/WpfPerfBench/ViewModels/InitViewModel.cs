using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using WpfPerfBench.Core.Services;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Enums;
using WpfPerfBench.Data.Services;

namespace WpfPerfBench.ViewModels;

public enum InitState
{
    Init,
    Busy,
    Migration,
    Feed,
    Ready
}

public partial class InitViewModel : ValidationViewModelBase, IInitViewModel
{
    private const string SystemErrorMessageKey = "<SystemErrorMessage>";

    private readonly INavigationService _navigationService;
    private readonly IUserSession _userSession;
    private readonly IDataService _dataService;

    public InitViewModel(
        IInitProgressStandViewModel initProgressStand,
        INavigationService navigationService, 
        IUserSession userSession,
        IDataService dataService)
    {
        InitProgressStand = initProgressStand;
        InitProgressStand.InitProgressbar(0, 3);
        _navigationService = navigationService;
        _userSession = userSession;
        _dataService = dataService;
        Header = new HeaderViewModel("🚀", "Окно инициализации");
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

    [ObservableProperty]
    private IInitProgressStandViewModel _initProgressStand;

    #region Overrides of ValidationViewModelBase

    public override void Validate()
    {
        ClearError(SystemErrorMessageKey);
        base.Validate();
    }

    #endregion

    #region Test

    private bool CanTest() => CurrentState == InitState.Init;

    [RelayCommand(CanExecute = nameof(CanTest))]
    private async Task Test()
    {
        CurrentState = InitState.Busy;

        // ToDo устранить задержку при установке статуса
        InitProgressStand.SetConnectionProgress("Валидация данных ...");

        try
        {
            Validate();
            if (!IsValid)
            {
                CurrentState = InitState.Init;
                return;
            }

            InitUserSession();

            // Test Connection
            InitProgressStand.SetConnectionProgress("Проверка подключения к БД ...");

            var db = _dataService.CreateContext();

            var resultTest = await _dataService.TestConnection(db, CancellationToken.None);

            if (!resultTest.Success)
            {
                InitProgressStand.SetConnectionProgress(resultTest.Message);
                AddError(SystemErrorMessageKey, "Ошибка!");
                OnErrorsChanged(SystemErrorMessageKey);
                CurrentState = InitState.Init;
                return;
            }

            InitProgressStand.SetConnectionProgress("Проверка подключения: готово.");
            InitProgressStand.SetProgress(1);

            // Test Migration
            var resultMigration = await _dataService.GetPendingMigrationsAsync(db, CancellationToken.None);

            if (!resultMigration.Success)
            {
                AddError(SystemErrorMessageKey, resultMigration.Message);
                OnErrorsChanged(SystemErrorMessageKey);
                CurrentState = InitState.Init;
                return;
            }

            var migrationsCount = (resultMigration as NamesResult)?.Names.Count()
                             ??
                             throw new InvalidOperationException("Ошибка получения списка миграций");

            if (migrationsCount > 0)
            {
                CurrentState = InitState.Migration;
            }
            else
            {
                InitProgressStand.SetMigrationStatus($"Новых миграций нет");
                InitProgressStand.SetProgress(2);
                var count = await db.Items.CountAsync(CancellationToken.None);
                InitProgressStand.SetTotalRecords(count.ToString("N0"));
                CurrentState = InitState.Feed;
            }   
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            CurrentState = InitState.Init;
        }
    }

    #endregion Test

    #region Migrate

    private bool CanMigrate() => CurrentState == InitState.Migration;

    [RelayCommand(CanExecute = nameof(CanMigrate))]
    private async Task Migrate(CancellationToken ct)
    {
        CurrentState = InitState.Busy;

        InitProgressStand.SetMigrationStatus("Подготовка к миграции ...");

        try
        {
            var db = _dataService.CreateContext();

            var result = await _dataService.GetPendingMigrationsAsync(db, ct);

            if (!result.Success)
            {
                AddError(SystemErrorMessageKey, result.Message);
                OnErrorsChanged(SystemErrorMessageKey);
                CurrentState = InitState.Migration;
                return;
            }

            var migrations = (result as NamesResult)?.Names.ToArray()
                             ?? 
                             throw new InvalidOperationException("Ошибка получения списка миграций");

            var counter = 0;
            var total = migrations.Count();
            InitProgressStand.SetMigrationStatus($"Миграции применены ({counter} из {total})");
            foreach (var migration in migrations)
            {
                await _dataService.Migrate(db, migration, ct);
                counter++;
                InitProgressStand.SetMigrationStatus($"Миграции применены ({counter} из {total})");
            }

            CurrentState = InitState.Feed;
            InitProgressStand.SetProgress(2);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            CurrentState = InitState.Migration;
        }
    }

    #endregion Migrate

    #region Feed

    private bool CanFeed() => CurrentState == InitState.Feed;

    [RelayCommand(CanExecute = nameof(CanFeed))]
    private async Task Feed()
    {
        CurrentState = InitState.Busy;

        try
        {
            var db = _dataService.CreateContext();

            var result = await _dataService.CleanItems(db, CancellationToken.None);

            if (!result.Success)
            {
                AddError(SystemErrorMessageKey, result.Message);
                OnErrorsChanged(SystemErrorMessageKey);
                CurrentState = InitState.Feed;
                return;
            }

            await Task.Delay(5000);

            CurrentState = InitState.Ready;
            InitProgressStand.SetProgress(3);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            CurrentState = InitState.Feed;
        }
    }

    #endregion Feed

    #region Next

    private bool CanNext() => CurrentState == InitState.Ready;

    [RelayCommand(CanExecute = nameof(CanNext))]
    private void Next()
    {
        _navigationService.NavigateNext();
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

    partial void OnCurrentStateChanged(InitState value)
    {
        InitProgressStand.ShowBusy(value == InitState.Busy);
        NotifyCanExecuteChangedForAllCommands();
    }

    private void NotifyCanExecuteChangedForAllCommands()
    {
        TestCommand.NotifyCanExecuteChanged();
        MigrateCommand.NotifyCanExecuteChanged();
        FeedCommand.NotifyCanExecuteChanged();
        NextCommand.NotifyCanExecuteChanged();
    }

    #region Overrides of ValidationViewModelBase

    protected override void OnErrorsChanged(string propertyName)
    {
        base.OnErrorsChanged(propertyName);
        ValidationStatus = Errors.TryGetValue(SystemErrorMessageKey, out var errors)
            ? errors.First()
            : HasErrors
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