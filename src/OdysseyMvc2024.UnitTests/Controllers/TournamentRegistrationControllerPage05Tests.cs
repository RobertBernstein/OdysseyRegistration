using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Moq;
using OdysseyMvc2024.Controllers;
using OdysseyMvc2024.Models;
using OdysseyMvc2024.ViewData.TournamentRegistration;

namespace OdysseyMvc2024.UnitTests.Controllers;

public sealed class TournamentRegistrationControllerPage05Tests
{
    [Fact]
    public void Page05Get_WhenRegistrationIsAvailable_ReturnsPage05View()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page05(10);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<Page05ViewData>().Subject;

        model.Config.Should().NotBeNull();
        model.TournamentInfo.Should().NotBeNull();
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page05Get_WhenRegistrationIsNotAvailable_RedirectsToCurrentState(string state)
    {
        // Arrange
        var repository = CreateRepository(CreateConfigForState(state));
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page05(10);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be(state);
    }

    [Fact]
    public void Page05Post_WithValidData_UpdatesRegistrationAndRedirectsToPage06()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        var viewData = CreateValidPage05ViewData();

        // Act
        var result = controller.Page05(10, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Page06");
        redirect.RouteValues.Should().NotBeNull();
        redirect.RouteValues!["id"].Should().Be(10);

        repository.Verify(
            r => r.UpdateTournamentRegistration(
                10,
                5,
                It.Is<TournamentRegistration?>(registration =>
                    registration != null &&
                    registration.CoachFirstName == viewData.CoachFirstName &&
                    registration.CoachLastName == viewData.CoachLastName &&
                    registration.CoachEmailAddress == viewData.CoachEmailAddress &&
                    registration.AltCoachEmailAddress == viewData.AltCoachEmailAddress)),
            Times.Once);
    }

    [Fact]
    public void Page05Post_WhenModelStateIsInvalid_ReturnsPage05ViewAndDoesNotUpdate()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        var viewData = CreateValidPage05ViewData();
        controller.ModelState.AddModelError(nameof(Page05ViewData.CoachFirstName), "Invalid coach first name.");

        // Act
        var result = controller.Page05(10, viewData);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeSameAs(viewData);

        repository.Verify(
            r => r.UpdateTournamentRegistration(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<TournamentRegistration?>()),
            Times.Never);
    }

    [Fact]
    public void Page05Post_WhenCoachEmailIsInvalid_RedirectsToBadCoachEmail()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        var viewData = CreateValidPage05ViewData();
        viewData.CoachEmailAddress = "not-a-valid-email";

        // Act
        var result = controller.Page05(10, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("BadCoachEmail");

        repository.Verify(
            r => r.UpdateTournamentRegistration(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<TournamentRegistration?>()),
            Times.Never);
    }

    [Fact]
    public void Page05Post_WhenAltCoachEmailIsInvalid_RedirectsToBadAltCoachEmail()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        var viewData = CreateValidPage05ViewData();
        viewData.AltCoachEmailAddress = "invalid-alt-email";

        // Act
        var result = controller.Page05(10, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("BadAltCoachEmail");

        repository.Verify(
            r => r.UpdateTournamentRegistration(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<TournamentRegistration?>()),
            Times.Never);
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page05Post_WhenRegistrationIsNotAvailable_RedirectsToCurrentStateAndDoesNotUpdate(string state)
    {
        // Arrange
        var repository = CreateRepository(CreateConfigForState(state));
        using var controller = CreateController(repository.Object);

        var viewData = CreateValidPage05ViewData();

        // Act
        var result = controller.Page05(10, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be(state);

        repository.Verify(
            r => r.UpdateTournamentRegistration(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<TournamentRegistration?>()),
            Times.Never);
    }

    [Fact]
    public void Page05Post_WhenRepositoryUpdateThrows_RedirectsToHomeIndex()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        repository
            .Setup(r => r.UpdateTournamentRegistration(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<TournamentRegistration?>()))
            .Throws(new InvalidOperationException("Database failure"));

        using var controller = CreateController(repository.Object);

        var viewData = CreateValidPage05ViewData();

        // Act
        var result = controller.Page05(10, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Index");
        redirect.ControllerName.Should().Be("Home");
    }

    private static Page05ViewData CreateValidPage05ViewData()
    {
        return new Page05ViewData
        {
            CoachFirstName = "Charlie",
            CoachLastName = "Bernstein",
            CoachAddress = "123 Main St",
            CoachCity = "Fairfax",
            CoachState = "VA",
            CoachZipCode = "22030",
            CoachEveningPhone = "703-555-1212",
            CoachDaytimePhone = "703-555-1213",
            CoachMobilePhone = "703-555-1214",
            CoachEmailAddress = "coach@example.com",
            CoachEmailConfirmation = "coach@example.com",
            AltCoachFirstName = "Alex",
            AltCoachLastName = "Smith",
            AltCoachEveningPhone = "703-555-2212",
            AltCoachDaytimePhone = "703-555-2213",
            AltCoachMobilePhone = "703-555-2214",
            AltCoachEmailAddress = "altcoach@example.com",
            AltCoachEmailConfirmation = "altcoach@example.com"
        };
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

        controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());

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
            ["HomePage"] = "https://www.novanorth.org",
            ["WebmasterEmail"] = "webmaster@novanorth.org"
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
