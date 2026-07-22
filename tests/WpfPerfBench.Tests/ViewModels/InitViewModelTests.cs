using System.ComponentModel;
using Moq;
using WpfPerfBench.Core.Interfaces.Services;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.Tests.ViewModels;

public partial class InitViewModelTests
{
    private readonly IInitViewModel _initViewModel;
    private readonly INotifyPropertyChanged _propertyChangedViewModel;

    public InitViewModelTests()
    {
        var navigationServiceMock = new Mock<INavigationService>();
        var userSessionMock = new Mock<IUserSession>();
        var dataServiceMock = new Mock<IDataService>();

        _initViewModel = new WpfPerfBench.ViewModels.InitViewModel(
            navigationServiceMock.Object,
            userSessionMock.Object,
            dataServiceMock.Object);
        
        _propertyChangedViewModel = (INotifyPropertyChanged)_initViewModel;
    }
}