using FluentAssertions;
using Moq;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Factories;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.Tests.Data;
using WpfPerfBench.ViewModels;

namespace WpfPerfBench.Tests.ViewModels;

public partial class SeedViewModelTests : PageViewModelTestsBase<ISeedViewModel>
{
    private readonly Mock<IGeneratorService> _generatorServiceMock;
    private readonly Mock<ISeedMethodFactory> _seedMethodFactoryMock;

    public SeedViewModelTests()
    {
        _generatorServiceMock = new Mock<IGeneratorService>();

        var testSeedMethod = new TestSeedMethod(
            DataServiceMock.Object,
            _generatorServiceMock.Object,
            MessageServiceMock.Object);


        _seedMethodFactoryMock = new Mock<ISeedMethodFactory>();
        _seedMethodFactoryMock.Setup(x => x.Create(It.IsAny<Type>()))
            .Returns(testSeedMethod);

        ViewModel = new SeedViewModel(
            NavigationServiceMock.Object,
            UserSessionMock.Object,
            DataServiceMock.Object,
            _generatorServiceMock.Object,
            _seedMethodFactoryMock.Object);
    }

    [Fact]
    public void Should_Success_Fill_SeedMethods()
    {
        // Arrange
        // Act

        // Assert
        ViewModel.SeedMethods.Should().NotBeEmpty();
        ViewModel.SeedMethods[0].Should().NotBeNull();
    }
}
