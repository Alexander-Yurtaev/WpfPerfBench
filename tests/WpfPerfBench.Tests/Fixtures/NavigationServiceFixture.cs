using Moq;
using WpfPerfBench.Core.Enums;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.Tests.Fixtures;

public class NavigationServiceFixture
{
    public NavigationServiceFixture()
    {
        Factories = [];
        Factories.Add(Page.Init, () => new Mock<IInitViewModel>().Object);
        Factories.Add(Page.Migration, () => new Mock<IMigrationViewModel>().Object);
        Factories.Add(Page.Seed, () => new Mock<ISeedViewModel>().Object);
        Factories.Add(Page.Stand, () => new Mock<IStandViewModel>().Object);
    }

    public Dictionary<Page, Func<IViewModelBase>> Factories { get; set; }
}