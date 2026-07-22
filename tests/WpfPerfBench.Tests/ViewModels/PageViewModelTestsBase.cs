using Moq;
using WpfPerfBench.Core.Interfaces.Services;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.Tests.ViewModels;

public abstract class PageViewModelTestsBase<T> where T : IViewModelBase
{
    protected T ViewModel;

    protected Mock<INavigationService> NavigationServiceMock;
    protected Mock<IUserSession> UserSessionMock;
    protected Mock<IDataService> DataServiceMock;

    protected PageViewModelTestsBase()
    {
        NavigationServiceMock = new Mock<INavigationService>();
        UserSessionMock = new Mock<IUserSession>();
        DataServiceMock = new Mock<IDataService>();
    }
}