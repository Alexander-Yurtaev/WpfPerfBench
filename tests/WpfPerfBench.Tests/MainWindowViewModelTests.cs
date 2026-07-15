using FluentAssertions;
using Moq;
using WpfPerfBench.Core.Services;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Repositories;
using WpfPerfBench.ViewModels;

namespace WpfPerfBench.Tests;

public class MainWindowViewModelTests
{
    private readonly MainWindowViewModel _viewModel;
    private readonly NavigationService _navigationService;

    public MainWindowViewModelTests()
    {
        _navigationService = new NavigationService();
        var userSessionMock = new Mock<IUserSession>();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();

        var initViewModelMock = new Mock<Func<InitViewModel>>();
        initViewModelMock.Setup(m => m.Invoke())
            .Returns(new InitViewModel(_navigationService));

        var standViewModelMock = new Mock<Func<StandViewModel>>();
        standViewModelMock.Setup(m => m.Invoke())
            .Returns(new StandViewModel(userSessionMock.Object, categoryRepositoryMock.Object));

        _viewModel = new MainWindowViewModel(
            initViewModelMock.Object, 
            standViewModelMock.Object, 
            _navigationService);
    }

    [Fact]
    public void ViewModel_Init_To_FirstStep()
    {
        // Arrange
        
        // Act
        
        // Assert
        _viewModel.CurrentStep.Should().Be(1);
        _viewModel.CurrentViewModel.Should().NotBeNull();
        _viewModel.CurrentViewModel.Should().BeOfType<InitViewModel>();
    }

    [Fact]
    public void NavigateTo_NextStep_Success()
    {
        // Arrange
        _viewModel.CurrentStep = 1;

        // Act
        _navigationService.NavigateNext();

        // Assert
        _viewModel.CurrentStep.Should().Be(2);
        _viewModel.CurrentViewModel.Should().NotBeNull();
        _viewModel.CurrentViewModel.Should().BeOfType<StandViewModel>();
    }

    [Fact]
    public void NavigateFrom_Negative_Step_Success()
    {
        // Arrange
        _viewModel.CurrentStep = -1;

        // Act
        _navigationService.NavigateNext();

        // Assert
        _viewModel.CurrentStep.Should().Be(0);
        _viewModel.CurrentViewModel.Should().BeNull();
    }

    [Fact]
    public void NavigateFrom_TooLarge_Step_Success()
    {
        // Arrange
        _viewModel.CurrentStep = _viewModel.TotalSteps + 1;

        // Act
        _navigationService.NavigateNext();

        // Assert
        _viewModel.CurrentStep.Should().Be(0);
        _viewModel.CurrentViewModel.Should().BeNull();
    }
}