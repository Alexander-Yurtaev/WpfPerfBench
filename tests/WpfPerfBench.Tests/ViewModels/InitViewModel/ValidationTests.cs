using FluentAssertions;
using Moq;
using WpfPerfBench.Core.Services;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Services;

namespace WpfPerfBench.Tests.ViewModels.InitViewModel;

public class ValidationTests
{
    private readonly WpfPerfBench.ViewModels.InitViewModel _initViewModel;

    public ValidationTests()
    {
        var navigationServiceMock = new Mock<INavigationService>();
        var userSessionMock = new Mock<IUserSession>();
        var dataServiceMock = new Mock<IDataService>();

        _initViewModel = new WpfPerfBench.ViewModels.InitViewModel(
            navigationServiceMock.Object,
            userSessionMock.Object,
            dataServiceMock.Object);
    }

    #region Fio

    [Theory]
    [InlineData("Иванов Иван Иванович")]
    [InlineData("Иванов Иван")]
    [InlineData("Иванов И. И.")]
    [InlineData("Иванов И.")]
    public void Fio_Should_Success_ForCorrectValue(string fio)
    {
        // Arrange
        var count = 0;
        _initViewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(WpfPerfBench.ViewModels.InitViewModel.Fio))
            {
                count++;
            }
        };

        // Act
        _initViewModel.Fio = fio;

        // Assert
        count.Should().Be(1);
        _initViewModel.HasErrors.Should().BeFalse();
    }

    [Fact]
    public void Fio_Should_Be_Required()
    {
        // Arrange

        // Act
        _initViewModel.Fio = null!;

        // Assert
        _initViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)_initViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Fio));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("ФИО обязательно для заполнения");
    }

    [Fact]
    public void Fio_Should_Be_Correct_Length()
    {
        // Arrange

        // Act
        _initViewModel.Fio = "Ив";

        // Assert
        _initViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)_initViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Fio));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("ФИО должно содержать минимум 3 символа");
    }

    [Fact]
    public void Fio_Should_Correct_Format()
    {
        // Arrange

        // Act
        _initViewModel.Fio = "Иванов";

        // Assert
        _initViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)_initViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Fio));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("Введите ФИО полностью (Имя и Фамилию)");
    }

    [Fact]
    public void Fio_Should_HasErrors_When_Set_Empty()
    {
        // Arrange
        _initViewModel.Fio = "Иванов И.";

        // Act
        _initViewModel.Fio = "";

        // Assert
        _initViewModel.HasErrors.Should().BeTrue();
    }

    #endregion Fio

    #region Email

    [Theory]
    [InlineData("a@a")]
    public void Email_Should_Success_ForCorrectValue(string email)
    {
        // Arrange
        var count = 0;
        _initViewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(WpfPerfBench.ViewModels.InitViewModel.Email))
            {
                count++;
            }
        };

        // Act
        _initViewModel.Email = email;

        // Assert
        count.Should().Be(1);
        _initViewModel.HasErrors.Should().BeFalse();
    }

    [Fact]
    public void Email_Should_Be_Required()
    {
        // Arrange

        // Act
        _initViewModel.Email = null!;

        // Assert
        _initViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)_initViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Email));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("Email обязателен для заполнения");
    }

    [Theory]
    [InlineData("a")]
    [InlineData("a@")]
    [InlineData("@a")]
    public void Email_Should_Correct_Format(string email)
    {
        // Arrange

        // Act
        _initViewModel.Email = email;

        // Assert
        _initViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)_initViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Email));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("Некорректный формат email");
    }

    #endregion Email

    #region Password

    [Theory]
    [InlineData("A1aaaaaa")]
    public void Password_Should_Success_ForCorrectValue(string password)
    {
        // Arrange
        var count = 0;
        _initViewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(WpfPerfBench.ViewModels.InitViewModel.Password))
            {
                count++;
            }
        };

        // Act
        _initViewModel.Password = password;

        // Assert
        count.Should().Be(1);
        _initViewModel.HasErrors.Should().BeFalse();
    }

    [Fact]
    public void Password_Should_Be_Required()
    {
        // Arrange

        // Act
        _initViewModel.Password = null!;

        // Assert
        _initViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)_initViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Password));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("Пароль обязателен для заполнения");
    }

    [Theory]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("123")]
    [InlineData("1234")]
    [InlineData("12345")]
    [InlineData("123456")]
    [InlineData("1234567")]
    public void Password_Should_Be_Correct_Length(string password)
    {
        // Arrange

        // Act
        _initViewModel.Password = password;

        // Assert
        _initViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)_initViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Password));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("Пароль должен содержать минимум 8 символов");
    }

    [Fact]
    public void Password_Should_Has_Number()
    {
        // Arrange

        // Act
        _initViewModel.Password = new string('a', 10);

        // Assert
        _initViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)_initViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Password));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("Пароль должен содержать хотя бы одну цифру");
    }

    [Fact]
    public void Password_Should_Has_Capital_Char()
    {
        // Arrange

        // Act
        _initViewModel.Password = new string('1', 10);

        // Assert
        _initViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)_initViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Password));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("Пароль должен содержать хотя бы одну заглавную букву");
    }

    #endregion Password

    #region ConfirmPassword

    [Theory]
    [InlineData("A1aaaaaa")]
    public void ConfirmPassword_Should_Success_ForCorrectValue(string password)
    {
        // Arrange
        var count = 0;
        _initViewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(WpfPerfBench.ViewModels.InitViewModel.ConfirmPassword))
            {
                count++;
            }
        };

        // Act
        _initViewModel.Password = password;
        _initViewModel.ConfirmPassword = password;

        // Assert
        count.Should().Be(1);
    }

    [Fact]
    public void ConfirmPassword_Should_Be_Required()
    {
        // Arrange

        // Act
        _initViewModel.ConfirmPassword = null!;

        // Assert
        _initViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)_initViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.ConfirmPassword));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("Подтверждение пароля обязательно");
    }

    [Fact]
    public void ConfirmPassword_Should_Be_Equals_Password()
    {
        // Arrange

        // Act
        _initViewModel.ConfirmPassword = "A1aaaaaa";
        _initViewModel.ConfirmPassword = "A2aaaaaa";

        // Assert
        _initViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)_initViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.ConfirmPassword));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("Пароли не совпадают");
    }

    #endregion ConfirmPassword

    #region ConnectionString

    [Fact]
    public void ConnectionString_Should_Be_Required()
    {
        // Arrange

        // Act
        _initViewModel.ConnectionString = null!;

        // Assert
        _initViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)_initViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.ConnectionString));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("ConnectionString обязательно для заполнения");
    }

    #endregion ConnectionString
}