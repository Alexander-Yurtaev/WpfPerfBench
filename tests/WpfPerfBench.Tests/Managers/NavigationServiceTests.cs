using FluentAssertions;
using WpfPerfBench.Core.Enums;
using WpfPerfBench.Tests.Fixtures;
using WpfPerfBench.WPF.Interfaces.Managers;
using WpfPerfBench.WPF.Managers;

namespace WpfPerfBench.Tests.Managers;

public class NavigationServiceTests : IClassFixture<NavigationServiceFixture>
{
    private readonly INavigationService _service;

    public NavigationServiceTests(NavigationServiceFixture fixture)
    {
        _service = new NavigationService();
        foreach (var factory in fixture.Factories)
        {
            _service.AddPage(factory.Key, factory.Value);
        }
    }

    [Fact]
    public void Four_AddPage_Should_Add_Four_Pages()
    {
        // Arrange

        // Act
        
        // Assert
        _service.TotalPages.Should().Be(4);
    }

    [Fact]
    public void Should_Be_None_After_Initialization()
    {
        // Arrange

        // Act

        // Assert
        _service.CurrentViewModel.Should().BeNull();
        _service.CurrentPage.Should().Be(Page.None);
        _service.CurrentPageNumber.Should().Be(0);
        _service.NavigatePrevCommand.CanExecute(null).Should().BeFalse();
        _service.NavigateNextCommand.CanExecute(null).Should().BeTrue();
    }

    [Fact]
    public void NavigateNextCommand_Should_Move_Next_Page()
    {
        // Arrange

        // Act
        // Assert
        foreach (var page in Enum.GetValues<Page>())
        {
            if (page == Page.None) continue;
            _service.NavigateNextCommand.Execute(null);
            _service.CurrentPage.Should().Be(page);
            _service.CurrentViewModel.Should().NotBeNull();
            _service.CurrentViewModel.GetType().Name.Should().Contain($"{page}ViewModel");
        }
    }

    [Fact]
    public void NavigateNextCommand_From_Last_Page_Should_Not_Move_Another_Page()
    {
        // Arrange
        _service.CurrentPage = Page.Stand;

        // Act
        _service.NavigateNextCommand.Execute(null);

        // Assert
        _service.CurrentPage.Should().Be(Page.Stand);
        _service.CurrentViewModel.Should().NotBeNull();
        _service.CurrentViewModel.GetType().Name.Should().Contain($"{Page.Stand}ViewModel");
    }

    [Fact]
    public void NavigatePrevCommand_From_First_Page_Should_Not_Move_Another_Page()
    {
        // Arrange
        _service.CurrentPage = Page.Init;

        // Act
        _service.NavigatePrevCommand.Execute(null);

        // Assert
        _service.CurrentPage.Should().Be(Page.Init);
        _service.CurrentViewModel.Should().NotBeNull();
        _service.CurrentViewModel.GetType().Name.Should().Contain($"{Page.Init}ViewModel");
    }

    [Fact]
    public void PrevCommand_Should_Disable_For_First_Page()
    {
        // Arrange
        _service.CurrentPage = Enum.GetValues<Page>()[1];

        //Act

        // Assert
        _service.NavigatePrevCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void NextCommand_Should_Disable_For_Last_Page()
    {
        // Arrange
        _service.CurrentPage = Enum.GetValues<Page>().Last();

        //Act

        // Assert
        _service.NavigateNextCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void Block_Should_Disable_NavigationCommands()
    {
        // Arrange
        _service.CurrentPage = Enum.GetValues<Page>()[2];
        _service.NavigatePrevCommand.CanExecute(null).Should().BeTrue();
        _service.NavigateNextCommand.CanExecute(null).Should().BeTrue();

        // Act
        _service.Block();

        // Assert
        _service.NavigatePrevCommand.CanExecute(null).Should().BeFalse();
        _service.NavigateNextCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void UnBlock_Should_Enable_NavigationCommands()
    {
        // Arrange
        _service.CurrentPage = Enum.GetValues<Page>()[2];
        _service.Block();
        _service.NavigatePrevCommand.CanExecute(null).Should().BeFalse();
        _service.NavigateNextCommand.CanExecute(null).Should().BeFalse();

        // Act
        _service.UnBlock();

        // Assert
        _service.NavigatePrevCommand.CanExecute(null).Should().BeTrue();
        _service.NavigateNextCommand.CanExecute(null).Should().BeTrue();
    }

    [Fact]
    public void UnBlock_FirstPage_Should_Enable_Only_NextNavigationCommand()
    {
        // Arrange
        _service.CurrentPage = Enum.GetValues<Page>()[1];
        _service.Block();
        _service.NavigatePrevCommand.CanExecute(null).Should().BeFalse();
        _service.NavigateNextCommand.CanExecute(null).Should().BeFalse();

        // Act
        _service.UnBlock();

        // Assert
        _service.NavigatePrevCommand.CanExecute(null).Should().BeFalse();
        _service.NavigateNextCommand.CanExecute(null).Should().BeTrue();
    }

    [Fact]
    public void UnBlock_LastPage_Should_Enable_Only_PrevNavigationCommand()
    {
        // Arrange
        _service.CurrentPage = Enum.GetValues<Page>().Last();
        _service.Block();
        _service.NavigatePrevCommand.CanExecute(null).Should().BeFalse();
        _service.NavigateNextCommand.CanExecute(null).Should().BeFalse();

        // Act
        _service.UnBlock();

        // Assert
        _service.NavigatePrevCommand.CanExecute(null).Should().BeTrue();
        _service.NavigateNextCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void AllowPrev_False_Should_Disable_PrevCommand()
    {
        // Arrange
        _service.CurrentPage = Enum.GetValues<Page>()[2];
        _service.NavigatePrevCommand.CanExecute(null).Should().BeTrue();
        _service.NavigateNextCommand.CanExecute(null).Should().BeTrue();

        // Act
        _service.AllowPrev(false);

        // Assert
        _service.NavigatePrevCommand.CanExecute(null).Should().BeFalse();
        _service.NavigateNextCommand.CanExecute(null).Should().BeTrue();
    }

    [Fact]
    public void AllowPrev_True_Should_Enable_PrevCommand()
    {
        // Arrange
        _service.CurrentPage = Enum.GetValues<Page>()[2];
        _service.Block();
        _service.NavigatePrevCommand.CanExecute(null).Should().BeFalse();
        _service.NavigateNextCommand.CanExecute(null).Should().BeFalse();

        // Act
        _service.AllowPrev(true);

        // Assert
        _service.NavigatePrevCommand.CanExecute(null).Should().BeTrue();
        _service.NavigateNextCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void AllowNext_False_Should_Disable_NextCommand()
    {
        // Arrange
        _service.CurrentPage = Enum.GetValues<Page>()[2];
        _service.NavigatePrevCommand.CanExecute(null).Should().BeTrue();
        _service.NavigateNextCommand.CanExecute(null).Should().BeTrue();

        // Act
        _service.AllowNext(false);

        // Assert
        _service.NavigatePrevCommand.CanExecute(null).Should().BeTrue();
        _service.NavigateNextCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public void AllowNext_True_Should_Enable_NextCommand()
    {
        // Arrange
        _service.CurrentPage = Enum.GetValues<Page>()[2];
        _service.Block();
        _service.NavigatePrevCommand.CanExecute(null).Should().BeFalse();
        _service.NavigateNextCommand.CanExecute(null).Should().BeFalse();

        // Act
        _service.AllowNext(true);

        // Assert
        _service.NavigatePrevCommand.CanExecute(null).Should().BeFalse();
        _service.NavigateNextCommand.CanExecute(null).Should().BeTrue();
    }
}