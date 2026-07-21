using Moq;
using WpfPerfBench.Core.Services;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Services;

namespace WpfPerfBench.Tests.ViewModels;

public partial class InitViewModelTests
{
    private readonly WpfPerfBench.ViewModels.InitViewModel _initViewModel;

    public InitViewModelTests()
    {
        var navigationServiceMock = new Mock<INavigationService>();
        var userSessionMock = new Mock<IUserSession>();
        var dataServiceMock = new Mock<IDataService>();

        _initViewModel = new WpfPerfBench.ViewModels.InitViewModel(
            navigationServiceMock.Object,
            userSessionMock.Object,
            dataServiceMock.Object);
    }
}