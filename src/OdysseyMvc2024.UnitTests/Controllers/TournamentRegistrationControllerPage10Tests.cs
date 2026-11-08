// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TournamentRegistrationControllerPage10Tests.cs" company="Tardis Technologies">
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
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using OdysseyMvc2024.Controllers;
using OdysseyMvc2024.Models;
using OdysseyMvc2024.ViewData.TournamentRegistration;
using System.Globalization;

namespace OdysseyMvc2024.UnitTests.Controllers;

public sealed class TournamentRegistrationControllerPage10Tests
{
    [Fact]
    public void Page10Get_WhenRegistrationIsAvailableAndNoJudgeAssignmentError_ReturnsSummaryViewAndUpdatesRegistration()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState(withJudgeAssignmentError: false);
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page10(10);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<Page10ViewData>().Subject;

        model.TournamentRegistration.Should().NotBeNull();
        model.SchoolName.Should().Be("Test School");
        model.ProblemName.Should().Be("Problem 2");
        model.Division.Should().Be("Primary");
        model.JudgeFirstName.Should().Be("Jamie");
        model.JudgeLastName.Should().Be("Doe");
        model.MailBody.Should().NotBeNull();

        repository.Verify(
            r => r.UpdateTournamentRegistration(
                10,
                10,
                It.Is<TournamentRegistration?>(registration =>
                    registration != null &&
                    registration.Id == 10)),
            Times.Once);
    }

    [Fact]
    public void Page10Get_WhenJudgeAssignmentErrorOccurs_ReturnsErrorViewAndDoesNotUpdateRegistration()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState(withJudgeAssignmentError: true);
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page10(10);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<Page10ViewData>().Subject;

        model.JudgeErrorMessage.Should().Be("Judge is already assigned to a different team.");

        repository.Verify(
            r => r.UpdateTournamentRegistration(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<TournamentRegistration?>()),
            Times.Never);
    }

    [Fact]
    public void Page10Get_WhenRegistrationRecordIsMissing_RedirectsToError()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState(withJudgeAssignmentError: false);
        repository.Setup(r => r.GetTournamentRegistrationById(It.IsAny<int>())).Returns((TournamentRegistration?)null);
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page10(10);

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
    public void Page10Get_WhenEmailPartialRenders_UsesPage10ViewDataModel()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState(withJudgeAssignmentError: false);
        object? renderedModel = null;
        using var controller = CreateController(
            repository.Object,
            includeEmailPartialView: true,
            onRenderViewContext: viewContext => renderedModel = viewContext.ViewData.Model);

        // Act
        var result = controller.Page10(10);

        // Assert
        result.Should().BeOfType<ViewResult>();
        renderedModel.Should().BeOfType<Page10ViewData>();
    }

    [Fact]
    public void Page10Get_WhenEmailPartialViewIsMissing_ThrowsInvalidOperationException()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState(withJudgeAssignmentError: false);
        using var controller = CreateController(repository.Object, includeEmailPartialView: false);

        // Act
        Action action = () => controller.Page10(10);

        // Assert
        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("*TournamentRegistration/EmailPartial*");
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page10Get_WhenRegistrationIsNotAvailable_RedirectsToCurrentState(string state)
    {
        // Arrange
        var repository = CreateRepository(CreateConfigForState(state), withJudgeAssignmentError: false);
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page10(10);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be(state);
    }

    private static TournamentRegistrationController CreateController(
        IOdysseyRepository repository,
        bool includeEmailPartialView = true,
        Action<Microsoft.AspNetCore.Mvc.Rendering.ViewContext>? onRenderViewContext = null)
    {
        var controller = new TournamentRegistrationController(repository);

        var compositeViewEngine = new Mock<ICompositeViewEngine>();
        if (includeEmailPartialView)
        {
            var view = new Mock<IView>();
            view.SetupGet(v => v.Path).Returns("TournamentRegistration/EmailPartial");
            view.Setup(v => v.RenderAsync(It.IsAny<Microsoft.AspNetCore.Mvc.Rendering.ViewContext>()))
                .Callback<Microsoft.AspNetCore.Mvc.Rendering.ViewContext>(viewContext => onRenderViewContext?.Invoke(viewContext))
                .Returns(Task.CompletedTask);

            compositeViewEngine
                .Setup(v => v.FindView(It.IsAny<ActionContext>(), "TournamentRegistration/EmailPartial", false))
                .Returns(ViewEngineResult.Found("TournamentRegistration/EmailPartial", view.Object));
        }
        else
        {
            compositeViewEngine
                .Setup(v => v.FindView(It.IsAny<ActionContext>(), "TournamentRegistration/EmailPartial", false))
                .Returns(ViewEngineResult.NotFound("TournamentRegistration/EmailPartial", ["/Views/Shared/TournamentRegistration/EmailPartial.cshtml"]));
        }

        var services = new ServiceCollection()
            .AddSingleton(compositeViewEngine.Object)
            .BuildServiceProvider();

        var httpContext = new DefaultHttpContext
        {
            RequestServices = services
        };
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

    private static Mock<IOdysseyRepository> CreateRepositoryForAvailableState(bool withJudgeAssignmentError)
    {
        return CreateRepository(CreateAvailableRegistrationConfig(), withJudgeAssignmentError);
    }

    private static Mock<IOdysseyRepository> CreateRepository(Dictionary<string, string> config, bool withJudgeAssignmentError)
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
                CoachEmailAddress = "invalid-email",
                Division = "0",
                SchoolID = 20,
                JudgeID = 30,
                ProblemID = 2
            });

        repository.Setup(r => r.GetSchoolNameFromSchoolId(It.IsAny<int?>())).Returns("Test School");
        repository.Setup(r => r.GetProblemNameFromProblemId(It.IsAny<int?>())).Returns("Problem 2");

        var judgeFirstName = "Jamie";
        var judgeLastName = "Doe";
        repository.Setup(r => r.GetJudgeNameFromJudgeId(It.IsAny<short?>(), out judgeFirstName, out judgeLastName));

        if (withJudgeAssignmentError)
        {
            repository.Setup(r => r.GetJudgeIdFromTournamentRegistrationId(It.IsAny<int>()))
                .Returns((short)30);

            var errorMessage = "Judge is already assigned to a different team.";
            repository
                .Setup(r => r.UpdateJudgeRecordWithTournamentRegistrationId(It.IsAny<short?>(), It.IsAny<int>(), out errorMessage))
                .Returns(1);
        }
        else
        {
            repository.SetupSequence(r => r.GetJudgeIdFromTournamentRegistrationId(It.IsAny<int>()))
                .Returns((short?)null)
                .Returns((short)30);

            var noErrorMessage = string.Empty;
            repository
                .Setup(r => r.UpdateJudgeRecordWithTournamentRegistrationId(It.IsAny<short?>(), It.IsAny<int>(), out noErrorMessage))
                .Returns(0);
        }

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
            ["WebmasterEmail"] = "webmaster@novanorth.org",
            ["WebmasterEmailPassword"] = "password",
            ["EmailServer"] = "localhost",
            ["AcceptingPayPal"] = "false"
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
