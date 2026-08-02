using FluentAssertions;
using Moq;
using WpfPerfBench.Core.Interfaces;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Factories;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.Tests.Data;
using WpfPerfBench.ViewModels;

namespace WpfPerfBench.Tests.ViewModels;

public partial class SeedViewModelTests : PageViewModelTestsBase<ISeedViewModel>
{
    public SeedViewModelTests()
    {
        var generatorServiceMock = new Mock<IGeneratorService>();

        var testSeedMethod = new TestSeedMethod(
            DataServiceMock.Object,
            generatorServiceMock.Object,
            MessageServiceMock.Object);


        var seedMethodFactoryMock = new Mock<ISeedMethodFactory>();
        seedMethodFactoryMock.Setup(x => x.Create(It.IsAny<Type>()))
            .Returns(testSeedMethod);

        ViewModel = new SeedViewModel(
            NavigationServiceMock.Object,
            UserSessionMock.Object,
            seedMethodFactoryMock.Object);
    }

    [Fact]
    public void Should_Success_Fill_SeedMethods()
    {
        // Arrange

        // Act
        if (ViewModel is ILoadable loadable)
        {
            loadable.Load();
        }

        // Assert
        ViewModel.SeedMethods.Should().NotBeEmpty();
        ViewModel.SeedMethods[0].Should().NotBeNull();
    }
}
