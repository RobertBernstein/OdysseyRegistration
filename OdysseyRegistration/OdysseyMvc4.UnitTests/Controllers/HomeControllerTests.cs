using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OdysseyMvc2024.Controllers;
using OdysseyMvc2024.Models;
using OdysseyMvc2024.ViewData;
using OdysseyMvc4.UnitTests.Helpers;

namespace OdysseyMvc4.UnitTests.Controllers;

/// <summary>
/// Tests for the HomeController business logic that must be preserved
/// during the migration from .NET Framework 4.8 (OdysseyMvc4) to .NET 10 (OdysseyMvc2024).
/// </summary>
public class HomeControllerTests
{
    private readonly Mock<IOdysseyRepository> _mockRepo;
    private readonly HomeController _controller;

    public HomeControllerTests()
    {
        _mockRepo = TestHelper.CreateMockRepository();
        _controller = new HomeController(_mockRepo.Object)
        {
            FriendlyRegistrationName = "Test Registration"
        };
        TestHelper.SetupControllerContext(_controller);
    }

    [Fact]
    public void Index_ReturnsViewResult()
    {
        var result = _controller.Index();

        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public void Index_SetsViewDataMessage()
    {
        _controller.Index();

        _controller.ViewData["Message"].Should().Be("Welcome to the NoVA North Odyssey of the Mind Region 9 Registration web pages.");
    }

    [Fact]
    public void Index_ReturnsViewWithBaseViewData()
    {
        var result = _controller.Index() as ViewResult;

        result.Should().NotBeNull();
        result!.Model.Should().BeOfType<BaseViewData>();
    }

    [Fact]
    public void Index_PopulatesBaseViewDataConfig()
    {
        var result = _controller.Index() as ViewResult;
        var viewData = result!.Model as BaseViewData;

        viewData.Should().NotBeNull();
        viewData!.Config.Should().NotBeNull();
        viewData.Config.Should().ContainKey("RegionName");
        viewData.Config["RegionName"].Should().Be("NoVA North");
    }

    [Fact]
    public void Index_AccessesRepositoryConfig()
    {
        _controller.Index();

        _mockRepo.Verify(r => r.Config, Times.AtLeastOnce());
    }

    [Fact]
    public void Index_AccessesRepositoryRegionName()
    {
        _controller.Index();

        _mockRepo.Verify(r => r.RegionName, Times.AtLeastOnce());
    }

    [Fact]
    public void Index_AccessesRepositoryRegionNumber()
    {
        _controller.Index();

        _mockRepo.Verify(r => r.RegionNumber, Times.AtLeastOnce());
    }

    [Fact]
    public void Index_PopulatesBaseViewDataTournamentInfo()
    {
        var result = _controller.Index() as ViewResult;
        var viewData = result!.Model as BaseViewData;

        viewData.Should().NotBeNull();
        viewData!.TournamentInfo.Should().NotBeNull();
        viewData.TournamentInfo!.EventName.Should().Be("Regional Tournament");
    }

    [Fact]
    public void Index_CallsSetBaseViewData()
    {
        var result = _controller.Index() as ViewResult;
        var viewData = result!.Model as BaseViewData;

        // SetBaseViewData should populate RegionName, RegionNumber, and TournamentInfo
        viewData.Should().NotBeNull();
        viewData!.RegionName.Should().Be("NoVA North");
        viewData.RegionNumber.Should().Be("9");
    }
}
