using FluentAssertions;
using Moq;
using WpfPerfBench.Core.Interfaces;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Models;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.ViewModels;

namespace WpfPerfBench.Tests.ViewModels;

public class StandViewModelTests : PageViewModelTestsBase<IStandViewModel>
{
    public StandViewModelTests()
    {
        ViewModel = new StandViewModel(
            BusyManagerMock.Object,
            MessageServiceMock.Object,
            NavigationServiceMock.Object,
            UserSessionMock.Object,
            DataServiceMock.Object,
            ConsoleManagerMock.Object);
    }

    [Fact]
    public async Task LoadAsync_Should_Fill_TreeItems_Collection()
    {
        // Arrange
        var loadableVm = (ILoadableAsync)ViewModel;

        DataServiceMock.Setup(ds =>
                ds.HierarchyCategories(It.IsAny<CancellationToken>()))
            .Returns(GetHierarchyCategories);

        // Act
        await loadableVm.LoadAsync(CancellationToken.None);

        // Assert
        ViewModel.TreeItems.Should().NotBeNullOrEmpty();
        ViewModel.TreeItems.Should().HaveCount(1);
    }

    #region Private Methods

    private async Task<ResultBase> GetHierarchyCategories()
    {
        var cat1 = new CategoryTreeItem(1, "Категория #1", null, 10);
        var cat11 = new CategoryTreeItem(2, "Категория #11", 1, 5);
        var cat12 = new CategoryTreeItem(3, "Категория #12", 1, 5);
        cat1.Children.Add(cat11);
        cat1.Children.Add(cat12);

        var items = new List<CategoryTreeItem>
        {
            cat1
        };
        return await Task.FromResult<ResultBase>(new EntityResult<CategoryTreeItem>(items));
    }

    #endregion Private Methods
}