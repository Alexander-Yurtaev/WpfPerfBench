using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using WpfPerfBench.Core.Enum;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Enums;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Enums;
using WpfPerfBench.Interfaces;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.Managers;
using WpfPerfBench.Services;

namespace WpfPerfBench.ViewModels;

public partial class InitViewModel : ValidationViewModelBase, IInitViewModel, INavigatable
{
    private readonly IUserSession _userSession;
    private readonly IDataService _dataService;
    private readonly IBusyManager _busyManager;
    private readonly IMessageService _messageService;

    private bool? _isSuccessTested;

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
    private SecureString? _password;

    [ObservableProperty]
    private SecureString? _confirmPassword;

    [ObservableProperty]
    private DataProvider[] _dbTypes = [DataProvider.Postgres, DataProvider.MsSql];

    [ObservableProperty]
    private DataProvider _dbType = DataProvider.Postgres;

    [ObservableProperty]
    private string _connectionString = string.Empty;

    #region Test

    private bool CanTest() => NavigationService.CurrentPage == Page.Init && IsValid;

    [RelayCommand(CanExecute = nameof(CanTest))]
    private async Task Test()
    {
        var ct = _busyManager.ShowStandardIndicator("Загрузка...");
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
            if (_isSuccessTested == true)
            {
                _messageService.ShowSuccessMessage("Проверка выполнена!", "Подключение к БД проверено. Вы можете продолжить работу.");
            }
            else if (_isSuccessTested == false)
            {
                _messageService.ShowErrorMessage(resultTest.Message);
            }
            else
            {
                _messageService.ShowWarningMessage("Проверка подключения к БД была прервана.");
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
            if (_isSuccessTested is not null)
            {
                SendOnNavigatable(NavigationType.Next, _isSuccessTested.Value);
            }
            _busyManager.CloseIndicator();
        }
    }

    #endregion Test

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
        ValidatePassword(nameof(Password), Password, ConfirmPassword, ValidatePasswordString);
    }

    private void ValidateConfirmPassword()
    {
        ValidatePassword(nameof(ConfirmPassword), Password, ConfirmPassword, ValidateConfirmPasswordString);
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

    #region SecureString

    public void ValidatePassword(string propertyName,
        SecureString? securePassword, SecureString? secureConfirmPassword,
        Action<string, string?, string?> validatePasswordString)
    {
        if (propertyName == nameof(InitViewModel.Password))
        {
            if (securePassword == null || securePassword.Length == 0)
            {
                var errorMessage = "Пароль обязателен для заполнения";
                AddError(propertyName, errorMessage);
                return;
            }
        }

        if (propertyName == nameof(InitViewModel.ConfirmPassword))
        {
            if (secureConfirmPassword == null || secureConfirmPassword.Length == 0)
            {
                var errorMessage = "Подтверждение пароля обязательно";
                AddError(propertyName, errorMessage);
                return;
            }
        }

        IntPtr ptr = IntPtr.Zero;
        IntPtr ptrConfirm = IntPtr.Zero;
        try
        {
            // Извлекаем строку во временную память
            string? password = null;
            if (securePassword is not null)
            {
                ptr = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                password = Marshal.PtrToStringUni(ptr);
            }

            string? confirmPassword = null;
            if (secureConfirmPassword is not null)
            {
                ptrConfirm = Marshal.SecureStringToGlobalAllocUnicode(secureConfirmPassword);
                confirmPassword = Marshal.PtrToStringUni(ptrConfirm);
            }
            
            // Выполняем валидацию
            validatePasswordString(propertyName, password, confirmPassword);
        }
        finally
        {
            // НЕМЕДЛЕННО очищаем память после валидации
            if (ptr != IntPtr.Zero)
            {
                Marshal.ZeroFreeGlobalAllocUnicode(ptr);
            }

            if (ptrConfirm != IntPtr.Zero)
            {
                Marshal.ZeroFreeGlobalAllocUnicode(ptrConfirm);
            }
        }
    }

    private void ValidatePasswordString(string propertyName, string? password, string? confirmPassword)
    {
        if (password is null)
        {
            AddError(propertyName, "Пароль обязателен для заполнения");
        }
        else if (password.Length < 8)
        {
            AddError(propertyName, "Пароль должен содержать минимум 8 символов");
        }
        else if (!password.Any(char.IsDigit))
        {
            AddError(propertyName, "Пароль должен содержать хотя бы одну цифру");
        }
        else if (!password.Any(char.IsUpper))
        {
            AddError(propertyName, "Пароль должен содержать хотя бы одну заглавную букву");
        }
    }

    private void ValidateConfirmPasswordString(string propertyName, string? password, string? confirmPassword)
    {
        if (string.IsNullOrWhiteSpace(confirmPassword))
        {
            AddError(propertyName, "Подтверждение пароля обязательно");
        }
        else if (password != confirmPassword)
        {
            AddError(propertyName, "Пароли не совпадают");
        }
    }

    #endregion SecureString

    #region Implementation of INavigatable

    public event EventHandler<NavigatableEventArgs>? OnNavigatable;

    private void SendOnNavigatable(NavigationType type, bool allowed)
    {
        OnNavigatable?.Invoke(this, new NavigatableEventArgs(type, allowed));
    }

    #endregion
}