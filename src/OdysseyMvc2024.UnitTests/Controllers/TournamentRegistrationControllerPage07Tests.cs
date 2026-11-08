// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TournamentRegistrationControllerPage07Tests.cs" company="Tardis Technologies">
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

public sealed class TournamentRegistrationControllerPage07Tests
{
    private static readonly IReadOnlyList<string> PrimaryTeamGrades = new[] { "Kindergarten" };

    [Fact]
    public void Page07Get_WhenRegistrationIsAvailable_ReturnsPage07ViewWithProblemData()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page07(10);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<Page07ViewData>().Subject;

        model.DivisionOfTeam.Should().Be(0);
        model.PrimaryProblemName.Should().Be("Primary Problem");
        model.Division123ProblemDropDown.Should().HaveCount(2);
        model.IsDoingSpontaneousDropDown.Should().HaveCount(2);
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page07Get_WhenRegistrationIsNotAvailable_RedirectsToCurrentState(string state)
    {
        // Arrange
        var repository = CreateRepository(CreateConfigForState(state));
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page07(10);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be(state);
    }

    [Fact]
    public void Page07Post_WhenPrimarySelected_UpdatesRegistrationAndRedirectsToPage08()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        var viewData = new Page07ViewData
        {
            DivisionOfTeam = 0,
            DivisionRadioGroup = "0",
            IsDoingSpontaneous = "Yes"
        };

        // Act
        var result = controller.Page07(10, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Page08");
        redirect.RouteValues.Should().NotBeNull();
        redirect.RouteValues!["id"].Should().Be(10);

        repository.Verify(
            r => r.UpdateTournamentRegistration(
                10,
                7,
                It.Is<TournamentRegistration?>(registration =>
                    registration != null &&
                    registration.Division == "0" &&
                    registration.ProblemID == 6 &&
                    registration.Spontaneous == true)),
            Times.Once);
    }

    [Fact]
    public void Page07Post_WhenDivision1Selected_UpdatesRegistrationAndRedirectsToPage08()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        var viewData = new Page07ViewData
        {
            DivisionOfTeam = 0,
            DivisionRadioGroup = "1",
            Division123ProblemChoice = "2"
        };

        // Act
        var result = controller.Page07(10, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Page08");

        repository.Verify(
            r => r.UpdateTournamentRegistration(
                10,
                7,
                It.Is<TournamentRegistration?>(registration =>
                    registration != null &&
                    registration.Division == "1" &&
                    registration.ProblemID == 2 &&
                    registration.Spontaneous == null)),
            Times.Once);
    }

    [Fact]
    public void Page07Post_WhenNonPrimaryTeamSelectedProblem_UpdatesRegistrationAndRedirectsToPage08()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        var viewData = new Page07ViewData
        {
            DivisionOfTeam = 2,
            SelectedProblem = "3"
        };

        // Act
        var result = controller.Page07(10, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Page08");

        repository.Verify(
            r => r.UpdateTournamentRegistration(
                10,
                7,
                It.Is<TournamentRegistration?>(registration =>
                    registration != null &&
                    registration.Division == "2" &&
                    registration.ProblemID == 3)),
            Times.Once);
    }

    [Fact]
    public void Page07Post_WhenModelStateIsInvalid_ReturnsPage07ViewAndRepopulatesDropdowns()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        var viewData = new Page07ViewData
        {
            DivisionOfTeam = 0,
            DivisionRadioGroup = "0",
            IsDoingSpontaneous = "Yes"
        };

        controller.ModelState.AddModelError(nameof(Page07ViewData.DivisionRadioGroup), "Invalid division.");

        // Act
        var result = controller.Page07(10, viewData);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<Page07ViewData>().Subject;

        model.Division123ProblemDropDown.Should().NotBeNull();
        model.Division123ProblemDropDown.Should().HaveCount(2);
        model.IsDoingSpontaneousDropDown.Should().NotBeNull();
        model.IsDoingSpontaneousDropDown.Should().HaveCount(2);
        model.PrimaryProblemName.Should().Be("Primary Problem");

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
    public void Page07Post_WhenRegistrationIsNotAvailable_RedirectsToCurrentStateAndDoesNotUpdate(string state)
    {
        // Arrange
        var repository = CreateRepository(CreateConfigForState(state));
        using var controller = CreateController(repository.Object);

        var viewData = new Page07ViewData
        {
            DivisionOfTeam = 2,
            SelectedProblem = "3"
        };

        // Act
        var result = controller.Page07(10, viewData);

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
    public void Page07Post_WhenRepositoryUpdateThrows_RedirectsToHomeIndex()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        repository
            .Setup(r => r.UpdateTournamentRegistration(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<TournamentRegistration?>()))
            .Throws(new InvalidOperationException("Database failure"));

        using var controller = CreateController(repository.Object);

        var viewData = new Page07ViewData
        {
            DivisionOfTeam = 2,
            SelectedProblem = "3"
        };

        // Act
        var result = controller.Page07(10, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Index");
        redirect.ControllerName.Should().Be("Home");
    }

    [Fact]
    public void Page07ViewData_Dropdowns_DoNotHaveRequiredMemberAttribute()
    {
        // Arrange
        var divisionProperty = typeof(Page07ViewData).GetProperty(nameof(Page07ViewData.Division123ProblemDropDown));
        var spontaneousProperty = typeof(Page07ViewData).GetProperty(nameof(Page07ViewData.IsDoingSpontaneousDropDown));

        // Act
        var divisionHasRequiredMember = divisionProperty!
            .GetCustomAttributes(typeof(RequiredMemberAttribute), inherit: true)
            .Length > 0;
        var spontaneousHasRequiredMember = spontaneousProperty!
            .GetCustomAttributes(typeof(RequiredMemberAttribute), inherit: true)
            .Length > 0;

        // Assert
        divisionHasRequiredMember.Should().BeFalse();
        spontaneousHasRequiredMember.Should().BeFalse();
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

        repository.Setup(r => r.GetMemberGradesByRegistration(It.IsAny<int>()))
            .Returns(PrimaryTeamGrades);

        repository.SetupGet(r => r.ProblemsWithoutPrimaryOrSpontaneous)
            .Returns(new[]
            {
                new Problem { ProblemID = 1, ProblemName = "Problem 1" },
                new Problem { ProblemID = 2, ProblemName = "Problem 2" }
            }.AsQueryable());

        repository.SetupGet(r => r.PrimaryProblem)
            .Returns(new[]
            {
                new Problem { ProblemID = 6, ProblemName = "Primary Problem" }
            }.AsQueryable());

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
            ["PrimaryTeamsMayDoSpontaneous"] = "True",
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
