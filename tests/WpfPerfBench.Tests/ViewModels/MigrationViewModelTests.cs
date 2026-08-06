using FluentAssertions;
using Moq;
using WpfPerfBench.Core.Enums;
using WpfPerfBench.Core.Interfaces;
using WpfPerfBench.Data;
using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.WPF.Interfaces.ViewModels;
using WpfPerfBench.WPF.ViewModels;

namespace WpfPerfBench.Tests.ViewModels;

public class MigrationViewModelTests : PageViewModelTestsBase<IMigrationViewModel>
{

    public MigrationViewModelTests()
    {
        ViewModel = new MigrationViewModel(
            NavigationServiceMock.Object,
            UserSessionMock.Object,
            DataServiceMock.Object,
            MessageServiceMock.Object,
            BusyManagerMock.Object);
    }

    [Fact]
    public async Task LoadAsync_Should_Fill_Items_Collection()
    {
        // Arrange
        var loadableVm = (ILoadableAsync)ViewModel;

        DataServiceMock.Setup(ds =>
                ds.GetPendingMigrationsAsync(It.IsAny<IWpfPerfBenchContext>(), It.IsAny<CancellationToken>()))
            .Returns(GetPendingMigrations);

        DataServiceMock.Setup(ds =>
                ds.GetAppliedMigrationsAsync(It.IsAny<IWpfPerfBenchContext>(), It.IsAny<CancellationToken>()))
            .Returns(GetAppliedMigrations);

        // Act
        await loadableVm.LoadAsync(CancellationToken.None);

        // Assert
        ViewModel.Items.Should().NotBeNullOrEmpty();
        ViewModel.Items.Should().HaveCount(6);

        var pendingExpected = (EntityResult<string>)await GetPendingMigrations();
        foreach (var name in pendingExpected.Entities)
        {
            ViewModel.Items
                .Where(i => i.Status == MigrationStatus.Pending)
                .Should().Contain(i => i.Name == name);
        }

        var appliedExpected = (EntityResult<string>)await GetAppliedMigrations();
        foreach (var name in appliedExpected.Entities)
        {
            ViewModel.Items
                .Where(i => i.Status == MigrationStatus.Applied)
                .Should().Contain(i => i.Name == name);
        }
    }

    [Fact]
    public async Task Show_ErrorMessage_When_GetPendingMigrations_Failed()
    {
        // Arrange
        var loadableVm = (ILoadableAsync)ViewModel;

        DataServiceMock.Setup(ds =>
                ds.GetPendingMigrationsAsync(It.IsAny<IWpfPerfBenchContext>(), It.IsAny<CancellationToken>()))
            .Returns(GetPendingMigrationsFail);
        DataServiceMock.Setup(ds =>
                ds.GetAppliedMigrationsAsync(It.IsAny<IWpfPerfBenchContext>(), It.IsAny<CancellationToken>()))
            .Returns(GetAppliedMigrations);

        // Act
        await loadableVm.LoadAsync(CancellationToken.None);

        // Assert
        ViewModel.Items.Should().BeEmpty();
        MessageServiceMock.Verify(v => v.ShowErrorMessage("Ошибка получения миграций"), Times.Once);
    }

    [Fact]
    public async Task Show_ErrorMessage_When_GetAppliedMigrations_Failed()
    {
        // Arrange
        var loadableVm = (ILoadableAsync)ViewModel;

        DataServiceMock.Setup(ds =>
                ds.GetPendingMigrationsAsync(It.IsAny<IWpfPerfBenchContext>(), It.IsAny<CancellationToken>()))
            .Returns(GetPendingMigrations);

        DataServiceMock.Setup(ds =>
                ds.GetAppliedMigrationsAsync(It.IsAny<IWpfPerfBenchContext>(), It.IsAny<CancellationToken>()))
            .Returns(GetAppliedMigrationsFail);

        // Act
        await loadableVm.LoadAsync(CancellationToken.None);

        // Assert
        ViewModel.Items.Should().BeEmpty();
        MessageServiceMock.Verify(v => v.ShowErrorMessage("Ошибка получения миграций"), Times.Once);
    }

    #region Private Mothods

    private async Task<ResultBase> GetPendingMigrations()
    {
        var items = new List<string>
        {
            "PendingMigration #1",
            "PendingMigration #2",
            "PendingMigration #3"
        };
        return await Task.FromResult<ResultBase>(new EntityResult<string>(items));
    }

    private async Task<ResultBase> GetAppliedMigrations()
    {
        var items = new List<string>
        {
            "AppliedMigration #1",
            "AppliedMigration #2",
            "AppliedMigration #3"
        };
        return await Task.FromResult<ResultBase>(new EntityResult<string>(items));
    }

    private async Task<ResultBase> GetPendingMigrationsFail()
    {
        return await Task.FromResult<ResultBase>(ResultBase.FailResult("Ошибка получения миграций"));
    }

    private async Task<ResultBase> GetAppliedMigrationsFail()
    {
        return await Task.FromResult<ResultBase>(ResultBase.FailResult("Ошибка получения миграций"));
    }

    #endregion Private Mothods
}