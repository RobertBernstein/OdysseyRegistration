// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TournamentRegistrationControllerResendEmailTests.cs" company="Tardis Technologies">
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

public sealed class TournamentRegistrationControllerResendEmailTests
{
    [Fact]
    public void ResendEmailGet_ReturnsViewWithBaseData()
    {
        // Arrange
        var repository = CreateRepository();
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.ResendEmail();

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<ResendEmailViewData>().Subject;

        model.Config.Should().NotBeNull();
        model.TournamentInfo.Should().NotBeNull();
        model.TeamNumber.Should().Be(0);
    }

    [Fact]
    public async Task ResendEmailPost_WhenNoRecipientsSelected_ReturnsErrorMessageAndDoesNotBuildSummary()
    {
        // Arrange
        var repository = CreateRepository();
        using var controller = CreateController(repository.Object);

        var form = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
        {
            [nameof(ResendEmailViewData.TeamNumber)] = "10",
            [nameof(ResendEmailViewData.CoachCheckbox)] = "false",
            [nameof(ResendEmailViewData.AltCoachCheckbox)] = "false"
        });

        // Act
        ActionResult result = await controller.ResendEmail(form, CancellationToken.None);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<ResendEmailViewData>().Subject;

        model.ErrorMessage.Should().Be("No one was selected to resend the registration information to, so no e-mail was sent.");
        model.Success.Should().BeFalse();

        repository.Verify(r => r.GetTournamentRegistrationById(It.IsAny<int>()), Times.Once);
        repository.Verify(r => r.GetJudgeNameFromJudgeId(It.IsAny<short?>(), out It.Ref<string>.IsAny, out It.Ref<string>.IsAny), Times.Never);
    }

    [Fact]
    public async Task ResendEmailPost_WithDefaultInput_ReturnsNoRecipientsErrorMessage()
    {
        // Arrange
        var repository = CreateRepository();
        using var controller = CreateController(repository.Object);

        var form = new FormCollection([]);

        // Act
        ActionResult result = await controller.ResendEmail(form, CancellationToken.None);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<ResendEmailViewData>().Subject;

        model.ErrorMessage.Should().Be("No one was selected to resend the registration information to, so no e-mail was sent.");
        model.Success.Should().BeFalse();

        repository.Verify(r => r.GetTournamentRegistrationById(It.IsAny<int>()), Times.Once);
        repository.Verify(r => r.GetJudgeNameFromJudgeId(It.IsAny<short?>(), out It.Ref<string>.IsAny, out It.Ref<string>.IsAny), Times.Never);
        repository.Verify(r => r.GetSchoolNameFromSchoolId(It.IsAny<int?>()), Times.Never);
        repository.Verify(r => r.GetProblemNameFromProblemId(It.IsAny<int?>()), Times.Never);
    }

    [Fact]
    public async Task ResendEmailPost_WhenCoachRecipientSelected_PopulatesSummaryPath()
    {
        // Arrange
        var repository = CreateRepository();
        using var controller = CreateController(repository.Object);

        var form = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
        {
            [nameof(ResendEmailViewData.TeamNumber)] = "10",
            [nameof(ResendEmailViewData.CoachCheckbox)] = "true",
            [nameof(ResendEmailViewData.AltCoachCheckbox)] = "false"
        });

        // Act
        ActionResult result = await controller.ResendEmail(form, CancellationToken.None);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<ResendEmailViewData>().Subject;

        model.ErrorMessage.Should().BeNull();

        repository.Verify(r => r.GetTournamentRegistrationById(It.IsAny<int>()), Times.Once);
        repository.Verify(r => r.GetJudgeNameFromJudgeId(It.IsAny<short?>(), out It.Ref<string>.IsAny, out It.Ref<string>.IsAny), Times.Once);
        repository.Verify(r => r.GetSchoolNameFromSchoolId(It.IsAny<int?>()), Times.Once);
        repository.Verify(r => r.GetProblemNameFromProblemId(It.IsAny<int?>()), Times.Once);
    }

    private static TournamentRegistrationController CreateController(IOdysseyRepository repository, string queryString = "")
    {
        var controller = new TournamentRegistrationController(repository);

        var view = new Mock<IView>();
        view.SetupGet(v => v.Path).Returns("TournamentRegistration/EmailPartial");
        view.Setup(v => v.RenderAsync(It.IsAny<Microsoft.AspNetCore.Mvc.Rendering.ViewContext>())).Returns(Task.CompletedTask);

        var compositeViewEngine = new Mock<ICompositeViewEngine>();
        compositeViewEngine
            .Setup(v => v.FindView(It.IsAny<ActionContext>(), "TournamentRegistration/EmailPartial", false))
            .Returns(ViewEngineResult.Found("TournamentRegistration/EmailPartial", view.Object));

        var services = new ServiceCollection()
            .AddSingleton(compositeViewEngine.Object)
            .BuildServiceProvider();

        var httpContext = new DefaultHttpContext
        {
            RequestServices = services
        };

        httpContext.Request.Host = new HostString("www.novanorth.org");
        if (!string.IsNullOrEmpty(queryString))
        {
            httpContext.Request.QueryString = new QueryString(queryString);
        }

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

    private static Mock<IOdysseyRepository> CreateRepository()
    {
        var repository = new Mock<IOdysseyRepository>();
        repository.SetupGet(r => r.Config).Returns(CreateConfig());
        repository.SetupGet(r => r.TournamentInfo).Returns(new Event());
        repository.SetupGet(r => r.RegionName).Returns("NoVA North");
        repository.SetupGet(r => r.RegionNumber).Returns("9");

        repository.Setup(r => r.GetTournamentRegistrationById(It.IsAny<int>()))
            .Returns(new TournamentRegistration
            {
                Id = 10,
                CoachEmailAddress = "coach@example.com",
                AltCoachEmailAddress = "altcoach@example.com",
                SchoolID = 20,
                JudgeID = 30,
                ProblemID = 2
            });

        var judgeFirstName = "Jamie";
        var judgeLastName = "Doe";
        repository.Setup(r => r.GetJudgeNameFromJudgeId(It.IsAny<short?>(), out judgeFirstName, out judgeLastName));

        repository.Setup(r => r.GetSchoolNameFromSchoolId(It.IsAny<int?>())).Returns("Test School");
        repository.Setup(r => r.GetProblemNameFromProblemId(It.IsAny<int?>())).Returns("Problem 2");

        return repository;
    }

    private static Dictionary<string, string> CreateConfig()
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
}
