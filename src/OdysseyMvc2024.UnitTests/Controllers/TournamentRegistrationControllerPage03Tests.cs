using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Moq;
using OdysseyMvc2024.Controllers;
using OdysseyMvc2024.Models;
using OdysseyMvc2024.ViewData.TournamentRegistration;

namespace OdysseyMvc2024.UnitTests.Controllers;

public sealed class TournamentRegistrationControllerPage03Tests
{
    [Fact]
    public void Page03Get_WhenRegistrationIsAvailable_ReturnsPage03ViewWithDefaults()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page03(7);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<Page03ViewData>().Subject;

        model.NoJudgesFound.Should().BeFalse();
        model.JudgeAlreadyTaken.Should().BeFalse();
        model.ListOfJudgesFound.Should().NotBeNull();
        model.ListOfJudgesFound.Should().BeEmpty();
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page03Get_WhenRegistrationIsNotAvailable_RedirectsToCurrentState(string state)
    {
        // Arrange
        var repository = CreateRepository(CreateConfigForState(state));
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page03(7);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be(state);
    }

    [Fact]
    public void Page03Post_WhenModelStateIsInvalid_ReturnsPage03ViewAndDoesNotQueryOrUpdate()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        var viewData = new Page03ViewData
        {
            JudgeId = "1",
            JudgeFirstName = "F",
            JudgeLastName = "F",
            ListOfJudgesFound = new List<Judge>().AsQueryable()
        };

        controller.ModelState.AddModelError(nameof(Page03ViewData.JudgeFirstName), "Invalid first name.");

        // Act
        var result = controller.Page03(7, viewData);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<Page03ViewData>().Subject;
        model.ListOfJudgesFound.Should().NotBeNull();
        model.ListOfJudgesFound.Should().BeEmpty();

        repository.Verify(r => r.GetJudgeByIdAndName(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        repository.Verify(r => r.UpdateTournamentRegistration(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<TournamentRegistration?>()), Times.Never);
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page03Post_WhenRegistrationIsNotAvailable_RedirectsToCurrentStateAndDoesNotQueryOrUpdate(string state)
    {
        // Arrange
        var repository = CreateRepository(CreateConfigForState(state));
        using var controller = CreateController(repository.Object);

        var viewData = new Page03ViewData
        {
            JudgeId = "1",
            JudgeFirstName = "F",
            JudgeLastName = "F",
            ListOfJudgesFound = new List<Judge>().AsQueryable()
        };

        // Act
        var result = controller.Page03(7, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be(state);

        repository.Verify(r => r.GetJudgeByIdAndName(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        repository.Verify(r => r.UpdateTournamentRegistration(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<TournamentRegistration?>()), Times.Never);
    }

    [Fact]
    public void Page03Post_WhenJudgeLookupThrows_RedirectsToHomeIndex()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        repository
            .Setup(r => r.GetJudgeByIdAndName(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new InvalidOperationException("Database failure"));

        using var controller = CreateController(repository.Object);

        var viewData = new Page03ViewData
        {
            JudgeId = "1",
            JudgeFirstName = "F",
            JudgeLastName = "F",
            ListOfJudgesFound = new List<Judge>().AsQueryable()
        };

        // Act
        var result = controller.Page03(7, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Index");
        redirect.ControllerName.Should().Be("Home");
    }

    [Fact]
    public void Page03Post_WithUnknownJudge_ReturnsViewWithNoJudgesFoundMessageState()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        repository
            .Setup(r => r.GetJudgeByIdAndName(1, "F", "F"))
            .Returns(new List<Judge>().AsQueryable());

        using var controller = CreateController(repository.Object);

        var viewData = new Page03ViewData
        {
            JudgeId = "1",
            JudgeFirstName = "F",
            JudgeLastName = "F",
            ListOfJudgesFound = new List<Judge>().AsQueryable()
        };

        // Act
        var result = controller.Page03(7, viewData);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<Page03ViewData>().Subject;
        model.NoJudgesFound.Should().BeTrue();
        model.JudgeAlreadyTaken.Should().BeFalse();

        repository.Verify(r => r.GetJudgeByIdAndName(1, "F", "F"), Times.Once);
        repository.Verify(r => r.UpdateTournamentRegistration(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<TournamentRegistration?>()), Times.Never);
    }

    [Fact]
    public void Page03Post_WithJudgeAlreadyAssigned_ReturnsViewWithJudgeAlreadyTakenState()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        repository
            .Setup(r => r.GetJudgeByIdAndName(1, "F", "F"))
            .Returns(new List<Judge>
            {
                new() { JudgeID = 1, FirstName = "F", LastName = "F", TeamID = "100" }
            }.AsQueryable());

        using var controller = CreateController(repository.Object);

        var viewData = new Page03ViewData
        {
            JudgeId = "1",
            JudgeFirstName = "F",
            JudgeLastName = "F",
            ListOfJudgesFound = new List<Judge>().AsQueryable()
        };

        // Act
        var result = controller.Page03(7, viewData);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<Page03ViewData>().Subject;
        model.JudgeAlreadyTaken.Should().BeTrue();
        model.NoJudgesFound.Should().BeFalse();

        repository.Verify(r => r.GetJudgeByIdAndName(1, "F", "F"), Times.Once);
        repository.Verify(r => r.UpdateTournamentRegistration(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<TournamentRegistration?>()), Times.Never);
    }

    [Fact]
    public void Page03Post_WithAvailableJudge_UpdatesRegistrationAndRedirectsToPage05()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        repository
            .Setup(r => r.GetJudgeByIdAndName(1, "F", "F"))
            .Returns(new List<Judge>
            {
                new() { JudgeID = 1, FirstName = "F", LastName = "F", TeamID = null! }
            }.AsQueryable());

        using var controller = CreateController(repository.Object);

        var viewData = new Page03ViewData
        {
            JudgeId = "1",
            JudgeFirstName = "F",
            JudgeLastName = "F",
            ListOfJudgesFound = new List<Judge>().AsQueryable()
        };

        // Act
        var result = controller.Page03(7, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Page05");
        redirect.RouteValues.Should().NotBeNull();
        redirect.RouteValues!["id"].Should().Be(7);

        repository.Verify(r => r.GetJudgeByIdAndName(1, "F", "F"), Times.Once);
        repository.Verify(
            r => r.UpdateTournamentRegistration(
                7,
                3,
                It.Is<TournamentRegistration?>(registration => registration != null && registration.JudgeID == 1)),
            Times.Once);
    }

    private static TournamentRegistrationController CreateController(IOdysseyRepository repository)
    {
        var controller = new TournamentRegistrationController(repository);

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Host = new HostString("www.novanorth.org");

        var actionContext = new ActionContext(httpContext, new RouteData(), new ControllerActionDescriptor());
        controller.ControllerContext = new ControllerContext(actionContext);

        var urlHelper = new Mock<IUrlHelper>();
        urlHelper.SetupGet(x => x.ActionContext).Returns(actionContext);
        urlHelper.Setup(x => x.Content(It.IsAny<string>())).Returns((string value) => value);
        urlHelper.Setup(x => x.RouteUrl(It.IsAny<UrlRouteContext>())).Returns(string.Empty);
        controller.Url = urlHelper.Object;

        return controller;
    }

    private static Mock<IOdysseyRepository> CreateRepositoryForAvailableState()
    {
        return CreateRepository(CreateAvailableRegistrationConfig());
    }

    private static Mock<IOdysseyRepository> CreateRepository(Dictionary<string, string> config)
    {
        var repository = new Mock<IOdysseyRepository>();
        repository.SetupGet(r => r.Config).Returns(config);
        repository.SetupGet(r => r.TournamentInfo).Returns(new Event());
        repository.SetupGet(r => r.RegionName).Returns("NoVA North");
        repository.SetupGet(r => r.RegionNumber).Returns("9");

        return repository;
    }

    private static Dictionary<string, string> CreateAvailableRegistrationConfig()
    {
        var now = DateTime.Now;

        return new Dictionary<string, string>
        {
            ["TournamentRegistrationOpenDateTime"] = now.AddDays(-1).ToString("O"),
            ["TournamentRegistrationCloseDateTime"] = now.AddDays(1).ToString("O"),
            ["IsTournamentRegistrationDown"] = "false",
            ["HomePage"] = "https://www.novanorth.org"
        };
    }

    private static Dictionary<string, string> CreateConfigForState(string state)
    {
        var config = CreateAvailableRegistrationConfig();

        switch (state)
        {
            case "Soon":
                config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(1).ToString("O");
                break;
            case "Down":
                config["IsTournamentRegistrationDown"] = "true";
                break;
            case "Closed":
                config["TournamentRegistrationCloseDateTime"] = DateTime.Now.AddDays(-1).ToString("O");
                break;
        }

        return config;
    }
}