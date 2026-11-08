// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TournamentRegistrationControllerPage09Tests.cs" company="Tardis Technologies">
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
using Microsoft.Extensions.Primitives;
using Moq;
using OdysseyMvc2024.Controllers;
using OdysseyMvc2024.Models;
using OdysseyMvc2024.ViewData.TournamentRegistration;
using System.Globalization;

namespace OdysseyMvc2024.UnitTests.Controllers;

public sealed class TournamentRegistrationControllerPage09Tests
{
    [Fact]
    public void Page09Get_WhenRegistrationIsAvailable_ReturnsPage09ViewWithDerivedSummaryFields()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page09(10);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<Page09ViewData>().Subject;

        model.TournamentRegistration.Should().NotBeNull();
        model.Division.Should().Be("Primary");
        model.IsDoingSpontaneous.Should().Be("Yes");
        model.SchoolName.Should().Be("Test School");
        model.JudgeFirstName.Should().Be("Jamie");
        model.JudgeLastName.Should().Be("Doe");
        model.ProblemName.Should().Be("Problem 2");
    }

    [Fact]
    public void Page09Get_WhenProblemNameIsMissing_SetsFallbackProblemName()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState(problemName: string.Empty);
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page09(10);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<Page09ViewData>().Subject;

        model.ProblemName.Should().Be("(Could not obtain problem name)");
    }

    [Fact]
    public void Page09Get_WhenRegistrationRecordIsMissing_RedirectsToError()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        repository.Setup(r => r.GetTournamentRegistrationById(It.IsAny<int>())).Returns((TournamentRegistration?)null);
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page09(10);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Error");
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page09Get_WhenRegistrationIsNotAvailable_RedirectsToCurrentState(string state)
    {
        // Arrange
        var repository = CreateRepository(CreateConfigForState(state));
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page09(10);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be(state);
    }

    [Fact]
    public void Page09Post_WhenRegistrationIsAvailable_RedirectsToPage10()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        var form = new FormCollection([]);

        // Act
        var result = controller.Page09(10, string.Empty, string.Empty, form);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Page10");
        redirect.RouteValues.Should().NotBeNull();
        redirect.RouteValues!["id"].Should().Be(10);
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page09Post_WhenRegistrationIsNotAvailable_RedirectsToCurrentState(string state)
    {
        // Arrange
        var repository = CreateRepository(CreateConfigForState(state));
        using var controller = CreateController(repository.Object);

        var form = new FormCollection([]);

        // Act
        var result = controller.Page09(10, string.Empty, string.Empty, form);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be(state);
    }

    [Fact]
    public void Page09Post_WhenRegistrationRecordIsMissing_RedirectsToError()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        repository.Setup(r => r.GetTournamentRegistrationById(It.IsAny<int>())).Returns((TournamentRegistration?)null);
        using var controller = CreateController(repository.Object);

        var form = new FormCollection([]);

        // Act
        var result = controller.Page09(10, string.Empty, string.Empty, form);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Error");
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

    private static Mock<IOdysseyRepository> CreateRepositoryForAvailableState(string problemName = "Problem 2")
    {
        return CreateRepository(CreateAvailableRegistrationConfig(), problemName);
    }

    private static Mock<IOdysseyRepository> CreateRepository(Dictionary<string, string> config, string problemName = "Problem 2")
    {
        var repository = new Mock<IOdysseyRepository>();
        repository.SetupGet(r => r.Config).Returns(config);
        repository.SetupGet(r => r.TournamentInfo).Returns(new Event());
        repository.SetupGet(r => r.RegionName).Returns("NoVA North");
        repository.SetupGet(r => r.RegionNumber).Returns("9");

        repository.Setup(r => r.GetTournamentRegistrationById(It.IsAny<int>()))
            .Returns(new TournamentRegistration
            {
                Id = 10,
                Division = "0",
                Spontaneous = true,
                SchoolID = 20,
                JudgeID = 30,
                ProblemID = 2
            });

        repository.Setup(r => r.GetSchoolNameFromSchoolId(It.IsAny<int?>())).Returns("Test School");

        var judgeFirstName = "Jamie";
        var judgeLastName = "Doe";
        repository.Setup(r => r.GetJudgeNameFromJudgeId(It.IsAny<short?>(), out judgeFirstName, out judgeLastName));

        repository.Setup(r => r.GetProblemNameFromProblemId(It.IsAny<int?>())).Returns(problemName);

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
