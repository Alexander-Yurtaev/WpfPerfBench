using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using WpfPerfBench.Core.Enum;
using WpfPerfBench.Core.Interfaces.Services;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Enums;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Enums;
using WpfPerfBench.Interfaces;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.Managers;

namespace WpfPerfBench.ViewModels;

public partial class InitViewModel : ValidationViewModelBase, IInitViewModel, INavigatable
{
    private readonly IUserSession _userSession;
    private readonly IDataService _dataService;
    private readonly IBusyManager _busyManager;
    private readonly IMessageService _messageService;

    private bool _isSuccessTested;

    public InitViewModel(
        INavigationService navigationService, 
        IUserSession userSession,
        IDataService dataService,
        IBusyManager busyManager,
        IMessageService messageService) : base(navigationService, userSession)
    {
        _navigationService = navigationService;
        _userSession = userSession;
        _dataService = dataService;
        _busyManager = busyManager;
        _messageService = messageService;
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

    //[ObservableProperty] 
    //private string _validationStatus;

    #region Test

    private bool CanTest() => NavigationService.CurrentPage == Page.Init && IsValid;

    [RelayCommand(CanExecute = nameof(CanTest))]
    private async Task Test()
    {
        var ct = _busyManager.ShowIndicator("Проверка подключения БД", $"{_userSession.DataProvider}");
        try
        {
            Validate();
            if (!IsValid)
            {
                return;
            }

            InitUserSession();

            var db = _dataService.CreateContext();

            var resultTest = await _dataService.TestConnection(db, ct);

            _isSuccessTested = resultTest.Success;
            if (!_isSuccessTested)
            {
                _messageService.ShowErrorMessage(resultTest.Message);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _isSuccessTested = false;
            _messageService.ShowErrorMessage(e.Message);
        }
        finally
        {
            RefreshCommands();
            SendOnNavigatable(NavigationType.Next, _isSuccessTested);
            _busyManager.CloseIndicator();
        }
    }

    #endregion Test

    //#region Migrate

    //private bool CanMigrate() => NavigationService.CurrentPage == Page.Migration;

    //[RelayCommand(CanExecute = nameof(CanMigrate))]
    //private async Task Migrate(CancellationToken ct)
    //{
    //    // InitProgressStand.SetMigrationStatus("Подготовка к миграции ...");

    //    try
    //    {
    //        var db = _dataService.CreateContext();

    //        var result = await _dataService.GetPendingMigrationsAsync(db, ct);

    //        if (!result.Success)
    //        {
    //            NavigationService.NavigateNext();
    //            return;
    //        }

    //        var migrations = (result as NamesResult)?.Names.ToArray()
    //                         ?? 
    //                         throw new InvalidOperationException("Ошибка получения списка миграций");

    //        foreach (var migration in migrations)
    //        {
    //            await _dataService.Migrate(db, migration, ct);
    //        }

    //        NavigationService.NavigateNext();
    //    }
    //    catch (Exception e)
    //    {
    //        Console.WriteLine(e);
    //    }
    //}

    //#endregion Migrate

    //#region Next

    //private bool CanNext() => true; //CurrentPage == Page.Ready;

    //[RelayCommand(CanExecute = nameof(CanNext))]
    //private void Next()
    //{
    //    NavigationService.NavigateNext();
    //}

    //#endregion Next

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

    protected override void RefreshCommands()
    {
        TestCommand.NotifyCanExecuteChanged();
    }

    //#region Overrides of ValidationViewModelBase

    //protected override void OnErrorsChanged(string propertyName)
    //{
    //    base.OnErrorsChanged(propertyName);
    //    ValidationStatus = HasErrors
    //            ? "❌ Некоторые поля заполнены некорректно • Исправьте ошибки"
    //            : "✅ Все поля валидны";
    //}

    //#endregion

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

    #region Implementation of INavigatable

    public event EventHandler<NavigatableEventArgs>? OnNavigatable;

    private void SendOnNavigatable(NavigationType type, bool allowed)
    {
        OnNavigatable?.Invoke(this, new NavigatableEventArgs(type, allowed));
    }

    #endregion
}