using Moq;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.ViewModels;

namespace WpfPerfBench.Tests.ViewModels;

public class SeedViewModelTests : PageViewModelTestsBase<ISeedViewModel>
{
    private readonly Mock<IGeneratorService> _generatorService;

    public SeedViewModelTests()
    {
        _generatorService = new Mock<IGeneratorService>();

        ViewModel = new SeedViewModel(
            NavigationServiceMock.Object,
            UserSessionMock.Object,
            DataServiceMock.Object,
            _generatorService.Object);
    }
}