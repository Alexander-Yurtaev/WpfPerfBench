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
}