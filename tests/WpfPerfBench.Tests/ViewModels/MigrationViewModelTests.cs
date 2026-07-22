using FluentAssertions;
using Moq;
using WpfPerfBench.Core.Enum;
using WpfPerfBench.Core.Interfaces.Services;
using WpfPerfBench.Data;
using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Interfaces;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.Tests.ViewModels;

public class MigrationViewModelTests
{
    private readonly IMigrationViewModel _viewModel;

    private readonly Mock<IDataService> _dataServiceMock;
    private readonly Mock<INavigationService> _navigationServiceMock;
    private readonly Mock<IUserSession> _userSessionMock;
    private readonly Mock<IMessageService> _messageService;
    

    public MigrationViewModelTests()
    {
        _dataServiceMock = new Mock<IDataService>();
        _navigationServiceMock = new Mock<INavigationService>();
        _userSessionMock = new Mock<IUserSession>();
        _messageService = new Mock<IMessageService>();

        _viewModel = new WpfPerfBench.ViewModels.MigrationViewModel(
            _dataServiceMock.Object, 
            _navigationServiceMock.Object,
            _userSessionMock.Object,
            _messageService.Object);
    }

    [Fact]
    public async Task LoadAsync_Should_Fill_Items_Collection()
    {
        // Arrange
        var loadableVm = (ILoadable)_viewModel;

        _dataServiceMock.Setup(ds =>
                ds.GetPendingMigrationsAsync(It.IsAny<IWpfPerfBenchContext>(), It.IsAny<CancellationToken>()))
            .Returns(GetPendingMigrations);
        _dataServiceMock.Setup(ds =>
                ds.GetAppliedMigrationsAsync(It.IsAny<IWpfPerfBenchContext>(), It.IsAny<CancellationToken>()))
            .Returns(GetAppliedMigrations);

        // Act
        await loadableVm.LoadAsync(CancellationToken.None);

        // Assert
        _viewModel.Items.Should().NotBeNullOrEmpty();
        _viewModel.Items.Should().HaveCount(6);

        var pendingExpected = (NamesResult)await GetPendingMigrations();
        foreach (var name in pendingExpected.Names)
        {
            _viewModel.Items
                .Where(i => i.Status == MigrationStatus.Pending)
                .Should().Contain(i => i.Name == name);
        }

        var appliedExpected = (NamesResult)await GetAppliedMigrations();
        foreach (var name in appliedExpected.Names)
        {
            _viewModel.Items
                .Where(i => i.Status == MigrationStatus.Applied)
                .Should().Contain(i => i.Name == name);
        }
    }

    [Fact]
    public async Task Show_ErrorMessage_When_GetPendingMigrations_Failed()
    {
        // Arrange
        var loadableVm = (ILoadable) _viewModel;

        _dataServiceMock.Setup(ds =>
                ds.GetPendingMigrationsAsync(It.IsAny<IWpfPerfBenchContext>(), It.IsAny<CancellationToken>()))
            .Returns(GetPendingMigrationsFail);
        _dataServiceMock.Setup(ds =>
                ds.GetAppliedMigrationsAsync(It.IsAny<IWpfPerfBenchContext>(), It.IsAny<CancellationToken>()))
            .Returns(GetAppliedMigrations);

        // Act
        await loadableVm.LoadAsync(CancellationToken.None);

        // Assert
        _viewModel.Items.Should().BeEmpty();
        _messageService.Verify(v => v.ShowErrorMessage("Ошибка получения миграций"), Times.Once);
    }

    [Fact]
    public async Task Show_ErrorMessage_When_GetAppliedMigrations_Failed()
    {
        // Arrange
        var loadableVm = (ILoadable)_viewModel;

        _dataServiceMock.Setup(ds =>
                ds.GetPendingMigrationsAsync(It.IsAny<IWpfPerfBenchContext>(), It.IsAny<CancellationToken>()))
            .Returns(GetPendingMigrations);
        _dataServiceMock.Setup(ds =>
                ds.GetAppliedMigrationsAsync(It.IsAny<IWpfPerfBenchContext>(), It.IsAny<CancellationToken>()))
            .Returns(GetAppliedMigrationsFail);

        // Act
        await loadableVm.LoadAsync(CancellationToken.None);

        // Assert
        _viewModel.Items.Should().BeEmpty();
        _messageService.Verify(v => v.ShowErrorMessage("Ошибка получения миграций"), Times.Once);
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
        return await Task.FromResult<ResultBase>(new NamesResult(items));
    }

    private async Task<ResultBase> GetAppliedMigrations()
    {
        var items = new List<string>
        {
            "AppliedMigration #1",
            "AppliedMigration #2",
            "AppliedMigration #3"
        };
        return await Task.FromResult<ResultBase>(new NamesResult(items));
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