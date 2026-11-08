// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TournamentRegistrationControllerPage08Tests.cs" company="Tardis Technologies">
//   Copyright 2026 Tardis Technologies. All rights reserved.
// </copyright>
// <summary>
//   Tests the controller responsible for handling tournament registration process for Odyssey of the Mind
//   competitions, which manages a multi-step registration flow from initial sign up through confirmation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
using System.Globalization;
using System.Runtime.CompilerServices;

namespace OdysseyMvc2024.UnitTests.Controllers;

public sealed class TournamentRegistrationControllerPage08Tests
{
    [Fact]
    public void Page08Get_WhenRegistrationIsAvailable_ReturnsPage08ViewWithRegistration()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page08(10);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<Page08ViewData>().Subject;

        model.Config.Should().NotBeNull();
        model.TournamentInfo.Should().NotBeNull();
        model.TournamentRegistration.Should().NotBeNull();
        model.TournamentRegistration!.Id.Should().Be(10);
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page08Get_WhenRegistrationIsNotAvailable_RedirectsToCurrentState(string state)
    {
        // Arrange
        var repository = CreateRepository(CreateConfigForState(state));
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page08(10);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be(state);
    }

    [Fact]
    public void Page08Get_WhenRegistrationRecordIsMissing_RedirectsToError()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        repository.Setup(r => r.GetTournamentRegistrationById(It.IsAny<int>())).Returns((TournamentRegistration?)null);
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page08(10);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Error");
    }

    [Fact]
    public void Page08Post_WithValidData_UpdatesRegistrationAndRedirectsToPage09()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        var viewData = new Page08ViewData
        {
            SchedulingIssues = "Coach has two teams.",
            SpecialConsiderations = "Wheelchair access requested.",
            TournamentRegistration = new TournamentRegistration()
        };

        // Act
        var result = controller.Page08(10, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Page09");
        redirect.RouteValues.Should().NotBeNull();
        redirect.RouteValues!["id"].Should().Be(10);

        repository.Verify(
            r => r.UpdateTournamentRegistration(
                10,
                8,
                It.Is<TournamentRegistration?>(registration =>
                    registration != null &&
                    registration.SchedulingIssues == "Coach has two teams." &&
                    registration.SpecialConsiderations == "Wheelchair access requested.")),
            Times.Once);
    }

    [Fact]
    public void Page08Post_WhenModelStateIsInvalid_ReturnsPage08ViewAndDoesNotUpdate()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        var viewData = new Page08ViewData
        {
            SchedulingIssues = "Coach has two teams.",
            SpecialConsiderations = "Wheelchair access requested.",
            TournamentRegistration = new TournamentRegistration()
        };

        controller.ModelState.AddModelError(nameof(Page08ViewData.SchedulingIssues), "Invalid scheduling notes.");

        // Act
        var result = controller.Page08(10, viewData);

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

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page08Post_WhenRegistrationIsNotAvailable_RedirectsToCurrentStateAndDoesNotUpdate(string state)
    {
        // Arrange
        var repository = CreateRepository(CreateConfigForState(state));
        using var controller = CreateController(repository.Object);

        var viewData = new Page08ViewData
        {
            SchedulingIssues = "Coach has two teams.",
            SpecialConsiderations = "Wheelchair access requested.",
            TournamentRegistration = new TournamentRegistration()
        };

        // Act
        var result = controller.Page08(10, viewData);

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
    public void Page08Post_WhenRegistrationRecordIsMissing_RedirectsToErrorAndDoesNotUpdate()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        repository.Setup(r => r.GetTournamentRegistrationById(It.IsAny<int>())).Returns((TournamentRegistration?)null);
        using var controller = CreateController(repository.Object);

        var viewData = new Page08ViewData
        {
            SchedulingIssues = "Coach has two teams.",
            SpecialConsiderations = "Wheelchair access requested.",
            TournamentRegistration = new TournamentRegistration()
        };

        // Act
        var result = controller.Page08(10, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Error");

        repository.Verify(
            r => r.UpdateTournamentRegistration(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<TournamentRegistration?>()),
            Times.Never);
    }

    [Fact]
    public void Page08Post_WhenRepositoryUpdateThrows_RedirectsToHomeIndex()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        repository
            .Setup(r => r.UpdateTournamentRegistration(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<TournamentRegistration?>()))
            .Throws(new InvalidOperationException("Database failure"));

        using var controller = CreateController(repository.Object);

        var viewData = new Page08ViewData
        {
            SchedulingIssues = "Coach has two teams.",
            SpecialConsiderations = "Wheelchair access requested.",
            TournamentRegistration = new TournamentRegistration()
        };

        // Act
        var result = controller.Page08(10, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Index");
        redirect.ControllerName.Should().Be("Home");
    }

    [Fact]
    public void Page08ViewData_TournamentRegistration_DoesNotHaveRequiredMemberAttribute()
    {
        // Arrange
        var property = typeof(Page08ViewData).GetProperty(nameof(Page08ViewData.TournamentRegistration));

        // Act
        var hasRequiredMember = property!
            .GetCustomAttributes(typeof(RequiredMemberAttribute), inherit: true)
            .Length > 0;

        // Assert
        hasRequiredMember.Should().BeFalse();
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

        repository.Setup(r => r.GetTournamentRegistrationById(It.IsAny<int>()))
            .Returns((int id) => new TournamentRegistration { Id = id });

        return repository;
    }

    private static Dictionary<string, string> CreateAvailableRegistrationConfig()
    {
        var now = DateTime.Now;

        return new Dictionary<string, string>
        {
            ["TournamentRegistrationOpenDateTime"] = now.AddDays(-1).ToString("O", CultureInfo.InvariantCulture),
            ["TournamentRegistrationCloseDateTime"] = now.AddDays(1).ToString("O", CultureInfo.InvariantCulture),
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
                config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(1).ToString("O", CultureInfo.InvariantCulture);
                break;
            case "Down":
                config["IsTournamentRegistrationDown"] = "true";
                break;
            case "Closed":
                config["TournamentRegistrationCloseDateTime"] = DateTime.Now.AddDays(-1).ToString("O", CultureInfo.InvariantCulture);
                break;
        }

        return config;
    }
}
