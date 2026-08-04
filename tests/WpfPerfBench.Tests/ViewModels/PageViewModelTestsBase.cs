using Moq;
using WpfPerfBench.Core.Interfaces;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Interfaces.Managers;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.Services;

namespace WpfPerfBench.Tests.ViewModels;

public abstract class PageViewModelTestsBase<T> where T : IViewModelBase
{
    protected T ViewModel = default!;

    protected Mock<INavigationService> NavigationServiceMock;
    protected Mock<IUserSession> UserSessionMock;
    protected Mock<IDataService> DataServiceMock;
    protected Mock<IBusyManager> BusyManagerMock;
    protected Mock<IMessageService> MessageServiceMock;
    protected Mock<IConsoleManager> ConsoleManagerMock;

    protected PageViewModelTestsBase()
    {
        NavigationServiceMock = new Mock<INavigationService>();
        UserSessionMock = new Mock<IUserSession>();
        DataServiceMock = new Mock<IDataService>();
        BusyManagerMock = new Mock<IBusyManager>();
        MessageServiceMock = new Mock<IMessageService>();
        ConsoleManagerMock = new Mock<IConsoleManager>();
    }
}