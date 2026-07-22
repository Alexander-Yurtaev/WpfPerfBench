using FluentAssertions;

namespace WpfPerfBench.Tests.ViewModels;

public partial class InitViewModelTests
{
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
        _propertyChangedViewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(WpfPerfBench.ViewModels.InitViewModel.Fio))
            {
                count++;
            }
        };

        // Act
        ViewModel.Fio = fio;

        // Assert
        count.Should().Be(1);
        ViewModel.HasErrors.Should().BeFalse();
    }

    [Fact]
    public void Fio_Should_Be_Required()
    {
        // Arrange

        // Act
        ViewModel.Fio = null!;

        // Assert
        ViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)ViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Fio));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("ФИО обязательно для заполнения");
    }

    [Fact]
    public void Fio_Should_Be_Correct_Length()
    {
        // Arrange

        // Act
        ViewModel.Fio = "Ив";

        // Assert
        ViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)ViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Fio));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("ФИО должно содержать минимум 3 символа");
    }

    [Fact]
    public void Fio_Should_Correct_Format()
    {
        // Arrange

        // Act
        ViewModel.Fio = "Иванов";

        // Assert
        ViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)ViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Fio));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("Введите ФИО полностью (Имя и Фамилию)");
    }

    [Fact]
    public void Fio_Should_HasErrors_When_Set_Empty()
    {
        // Arrange
        ViewModel.Fio = "Иванов И.";

        // Act
        ViewModel.Fio = "";

        // Assert
        ViewModel.HasErrors.Should().BeTrue();
    }

    #endregion Fio

    #region Email

    [Theory]
    [InlineData("a@a")]
    public void Email_Should_Success_ForCorrectValue(string email)
    {
        // Arrange
        var count = 0;
        _propertyChangedViewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(WpfPerfBench.ViewModels.InitViewModel.Email))
            {
                count++;
            }
        };

        // Act
        ViewModel.Email = email;

        // Assert
        count.Should().Be(1);
        ViewModel.HasErrors.Should().BeFalse();
    }

    [Fact]
    public void Email_Should_Be_Required()
    {
        // Arrange

        // Act
        ViewModel.Email = null!;

        // Assert
        ViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)ViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Email));
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
        ViewModel.Email = email;

        // Assert
        ViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)ViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Email));
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
        _propertyChangedViewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(WpfPerfBench.ViewModels.InitViewModel.Password))
            {
                count++;
            }
        };

        // Act
        ViewModel.Password = password;

        // Assert
        count.Should().Be(1);
        ViewModel.HasErrors.Should().BeFalse();
    }

    [Fact]
    public void Password_Should_Be_Required()
    {
        // Arrange

        // Act
        ViewModel.Password = null!;

        // Assert
        ViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)ViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Password));
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
        ViewModel.Password = password;

        // Assert
        ViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)ViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Password));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("Пароль должен содержать минимум 8 символов");
    }

    [Fact]
    public void Password_Should_Has_Number()
    {
        // Arrange

        // Act
        ViewModel.Password = new string('a', 10);

        // Assert
        ViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)ViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Password));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("Пароль должен содержать хотя бы одну цифру");
    }

    [Fact]
    public void Password_Should_Has_Capital_Char()
    {
        // Arrange

        // Act
        ViewModel.Password = new string('1', 10);

        // Assert
        ViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)ViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.Password));
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
        _propertyChangedViewModel.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(WpfPerfBench.ViewModels.InitViewModel.ConfirmPassword))
            {
                count++;
            }
        };

        // Act
        ViewModel.Password = password;
        ViewModel.ConfirmPassword = password;

        // Assert
        count.Should().Be(1);
    }

    [Fact]
    public void ConfirmPassword_Should_Be_Required()
    {
        // Arrange

        // Act
        ViewModel.ConfirmPassword = null!;

        // Assert
        ViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)ViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.ConfirmPassword));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("Подтверждение пароля обязательно");
    }

    [Fact]
    public void ConfirmPassword_Should_Be_Equals_Password()
    {
        // Arrange

        // Act
        ViewModel.ConfirmPassword = "A1aaaaaa";
        ViewModel.ConfirmPassword = "A2aaaaaa";

        // Assert
        ViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)ViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.ConfirmPassword));
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
        ViewModel.ConnectionString = null!;

        // Assert
        ViewModel.HasErrors.Should().BeTrue();
        var errors = (List<string>)ViewModel.GetErrors(nameof(WpfPerfBench.ViewModels.InitViewModel.ConnectionString));
        errors.Count.Should().Be(1);
        errors[0].Should().Be("ConnectionString обязательно для заполнения");
    }

    #endregion ConnectionString
}