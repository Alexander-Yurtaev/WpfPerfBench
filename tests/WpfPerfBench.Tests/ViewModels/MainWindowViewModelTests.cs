using FluentAssertions;
using Moq;
using WpfPerfBench.Core.Services;
using WpfPerfBench.Data.Repositories;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.Managers;
using WpfPerfBench.ViewModels;

namespace WpfPerfBench.Tests.ViewModels;

public class MainWindowViewModelTests
{
    private readonly MainWindowViewModel _viewModel;
    private readonly NavigationService _navigationService;

    public MainWindowViewModelTests()
    {
        _navigationService = new NavigationService();
        var categoryRepositoryMock = new Mock<ICategoryRepository>();

        var initViewModelMock = new Mock<Func<IInitViewModel>>();
        initViewModelMock.Setup(m => m.Invoke())
            .Returns(new Mock<IInitViewModel>().Object);

        var migrationViewModelMock = new Mock<Func<IMigrationViewModel>>();
        migrationViewModelMock.Setup(m => m.Invoke())
            .Returns(new Mock<IMigrationViewModel>().Object);

        var seedViewModelMock = new Mock<Func<ISeedViewModel>>();
        seedViewModelMock.Setup(m => m.Invoke())
            .Returns(new Mock<ISeedViewModel>().Object);

        var standViewModelMock = new Mock<Func<IStandViewModel>>();
        standViewModelMock.Setup(m => m.Invoke())
            .Returns(new Mock<IStandViewModel>().Object);

        var themeManagerMock = new Mock<IThemeManager>();

        _viewModel = new MainWindowViewModel(
            initViewModelMock.Object,
            migrationViewModelMock.Object,
            seedViewModelMock.Object,
            standViewModelMock.Object, 
            _navigationService,
            themeManagerMock.Object);
    }

    //[Fact]
    //public void ViewModel_Init_To_FirstStep()
    //{
    //    // Arrange
        
    //    // Act
        
    //    // Assert
    //    _viewModel.CurrentStep.Should().Be(1);
    //    _viewModel.CurrentViewModel.Should().NotBeNull();
    //    _viewModel.CurrentViewModel.Should().BeAssignableTo<IInitViewModel>();
    //}

    //[Fact]
    //public void NavigateTo_NextStep_Success()
    //{
    //    // Arrange
    //    _viewModel.CurrentStep = 1;

    //    // Act
    //    _navigationService.NavigateNext();

    //    // Assert
    //    _viewModel.CurrentStep.Should().Be(2);
    //    _viewModel.CurrentViewModel.Should().NotBeNull();
    //    _viewModel.CurrentViewModel.Should().BeAssignableTo<IMigrationViewModel>();
    //}

    //[Fact]
    //public void NavigateFrom_Negative_Step_Success()
    //{
    //    // Arrange
    //    _viewModel.CurrentStep = -1;

    //    // Act
    //    _navigationService.NavigateNext();

    //    // Assert
    //    _viewModel.CurrentStep.Should().Be(0);
    //    _viewModel.CurrentViewModel.Should().BeNull();
    //}

    //[Fact]
    //public void NavigateFrom_TooLarge_Step_Success()
    //{
    //    // Arrange
    //    _viewModel.CurrentStep = _viewModel.TotalSteps + 1;

    //    // Act
    //    _navigationService.NavigateNext();

    //    // Assert
    //    _viewModel.CurrentStep.Should().Be(0);
    //    _viewModel.CurrentViewModel.Should().BeNull();
    //}
}