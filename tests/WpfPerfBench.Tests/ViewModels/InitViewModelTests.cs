using System.ComponentModel;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.Tests.ViewModels;

public partial class InitViewModelTests : PageViewModelTestsBase<IInitViewModel>
{
    private readonly INotifyPropertyChanged _propertyChangedViewModel;

    public InitViewModelTests() : base()
    {
        ViewModel = new WpfPerfBench.ViewModels.InitViewModel(
            NavigationServiceMock.Object,
            UserSessionMock.Object,
            DataServiceMock.Object,
            BusyManagerMock.Object,
            MessageServiceMock.Object);
        
        _propertyChangedViewModel = (INotifyPropertyChanged)ViewModel;
    }
}