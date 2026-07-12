using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using WpfPerfBench.Data.Enums;

namespace WpfPerfBench.ViewModels;

public partial class InitViewModel : ValidationViewModelBase
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
    private DataProvider[] _dbTypes = [DataProvider.Postgres, DataProvider.MsSql];

    [ObservableProperty]
    private DataProvider _dbType = DataProvider.Postgres;

    [ObservableProperty]
    private string _connectionString = string.Empty;

    #region Overrides of ObservableObject

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        ValidateProperty(e.PropertyName);
        base.OnPropertyChanged(e);
    }

    #endregion

    protected override void ValidateProperty(string? propertyName)
    {
        if (string.IsNullOrEmpty(propertyName)) return;

        Errors.Remove(propertyName);
        var errors = new List<string>();

        switch (propertyName)
        {
            case nameof(Fio):
                ValidateFio(errors);
                break;
            case nameof(Email):
                ValidateEmail(errors);
                break;
            case nameof(Password):
                ValidatePassword(errors);
                break;
            case nameof(ConfirmPassword):
                ValidateConfirmPassword(errors);
                break;
            case nameof(DbType):
                ValidateDbType(errors);
                break;
            case nameof(ConnectionString):
                ValidateConnectionString(errors);
                break;
            default:
                return;
        }

        if (errors.Any())
            Errors[propertyName] = errors;

        OnErrorsChanged(propertyName);
    }

    #region Validation rules

    private void ValidateFio(List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(Fio))
            errors.Add("ФИО обязательно для заполнения");
        else if (Fio.Length < 3)
            errors.Add("ФИО должно содержать минимум 3 символа");
        else if (Fio.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length < 2)
            errors.Add("Введите ФИО полностью (Имя и Фамилию)");
    }

    private void ValidateEmail(List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(Email))
        {
            errors.Add("Email обязателен для заполнения");
            return;
        }

        try
        {
            var addr = new System.Net.Mail.MailAddress(Email);
            if (addr.Address != Email)
                errors.Add("Некорректный формат email");
        }
        catch
        {
            errors.Add("Некорректный формат email");
        }
    }

    private void ValidatePassword(List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(Password))
        {
            errors.Add("Пароль обязателен для заполнения");
            return;
        }

        if (Password.Length < 8)
            errors.Add("Пароль должен содержать минимум 8 символов");

        if (!Password.Any(char.IsDigit))
            errors.Add("Пароль должен содержать хотя бы одну цифру");

        if (!Password.Any(char.IsUpper))
            errors.Add("Пароль должен содержать хотя бы одну заглавную букву");
    }

    private void ValidateConfirmPassword(List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(ConfirmPassword))
        {
            errors.Add("Подтверждение пароля обязательно");
            return;
        }

        if (ConfirmPassword != Password)
            errors.Add("Пароли не совпадают");
    }

    private void ValidateDbType(List<string> errors)
    {

    }

    private void ValidateConnectionString(List<string> errors)
    {
        if (string.IsNullOrWhiteSpace(ConnectionString))
            errors.Add("ConnectionString обязательно для заполнения");
    }

    #endregion Validation rules
}