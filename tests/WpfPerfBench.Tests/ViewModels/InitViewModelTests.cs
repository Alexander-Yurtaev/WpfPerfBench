using System.ComponentModel;
using WpfPerfBench.WPF.Interfaces.ViewModels;
using WpfPerfBench.WPF.ViewModels;

namespace WpfPerfBench.Tests.ViewModels;

public partial class InitViewModelTests : PageViewModelTestsBase<IInitViewModel>
{
    private readonly INotifyPropertyChanged _propertyChangedViewModel;

    public InitViewModelTests()
    {
        ViewModel = new InitViewModel(
            NavigationServiceMock.Object,
            UserSessionMock.Object,
            DataServiceMock.Object,
            BusyManagerMock.Object,
            MessageServiceMock.Object);
        
        _propertyChangedViewModel = (INotifyPropertyChanged)ViewModel;
    }
}