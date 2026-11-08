using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Moq;
using OdysseyMvc2024.Controllers;
using OdysseyMvc2024.Models;
using OdysseyMvc2024.ViewData.TournamentRegistration;
using System.Runtime.CompilerServices;

namespace OdysseyMvc2024.UnitTests.Controllers;

public sealed class TournamentRegistrationControllerPage06Tests
{
    [Fact]
    public void Page06Get_WhenRegistrationIsAvailable_ReturnsPage06ViewWithGradeChoices()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page06(10);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<Page06ViewData>().Subject;

        model.GradeChoices.Should().NotBeNull();
        var gradeChoices = model.GradeChoices.ToList();
        gradeChoices.Should().HaveCount(13);
        gradeChoices.Select(choice => choice.Text).Should().ContainInOrder("Kindergarten", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12");
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page06Get_WhenRegistrationIsNotAvailable_RedirectsToCurrentState(string state)
    {
        // Arrange
        var repository = CreateRepository(CreateConfigForState(state));
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page06(10);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be(state);
    }

    [Fact]
    public void Page06Post_WithRequiredMemberData_UpdatesRegistrationAndRedirectsToPage07()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        var viewData = new Page06ViewData
        {
            MemberFirstName1 = "Charlie",
            MemberLastName1 = "Bernstein",
            MemberGrade1 = "6"
        };

        // Act
        var result = controller.Page06(10, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Page07");
        redirect.RouteValues.Should().NotBeNull();
        redirect.RouteValues!["id"].Should().Be(10);

        repository.Verify(
            r => r.UpdateTournamentRegistration(
                10,
                6,
                It.Is<TournamentRegistration?>(registration =>
                    registration != null &&
                    registration.MemberFirstName1 == "Charlie" &&
                    registration.MemberLastName1 == "Bernstein" &&
                    registration.MemberGrade1 == "6")),
            Times.Once);
    }

    [Fact]
    public void Page06Post_WhenModelStateIsInvalid_ReturnsPage06ViewAndRepopulatesGradeChoices()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        var viewData = new Page06ViewData
        {
            MemberFirstName1 = "Charlie",
            MemberLastName1 = "Bernstein",
            MemberGrade1 = "6"
        };

        controller.ModelState.AddModelError(nameof(Page06ViewData.MemberFirstName1), "Invalid member first name.");

        // Act
        var result = controller.Page06(10, viewData);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeOfType<Page06ViewData>().Subject;

        model.GradeChoices.Should().NotBeNull();
        model.GradeChoices.Should().HaveCount(13);

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
    public void Page06Post_WhenRegistrationIsNotAvailable_RedirectsToCurrentStateAndDoesNotUpdate(string state)
    {
        // Arrange
        var repository = CreateRepository(CreateConfigForState(state));
        using var controller = CreateController(repository.Object);

        var viewData = new Page06ViewData
        {
            MemberFirstName1 = "Charlie",
            MemberLastName1 = "Bernstein",
            MemberGrade1 = "6"
        };

        // Act
        var result = controller.Page06(10, viewData);

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
    public void Page06Post_WhenRepositoryUpdateThrows_RedirectsToHomeIndex()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        repository
            .Setup(r => r.UpdateTournamentRegistration(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<TournamentRegistration?>()))
            .Throws(new InvalidOperationException("Database failure"));

        using var controller = CreateController(repository.Object);

        var viewData = new Page06ViewData
        {
            MemberFirstName1 = "Charlie",
            MemberLastName1 = "Bernstein",
            MemberGrade1 = "6"
        };

        // Act
        var result = controller.Page06(10, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Index");
        redirect.ControllerName.Should().Be("Home");
    }

    [Fact]
    public void Page06ViewData_GradeChoices_HasValidateNeverAttribute()
    {
        // Arrange
        var property = typeof(Page06ViewData).GetProperty(nameof(Page06ViewData.GradeChoices));

        // Act
        var hasValidateNever = property!
            .GetCustomAttributes(typeof(ValidateNeverAttribute), inherit: true)
            .Length > 0;

        // Assert
        hasValidateNever.Should().BeTrue();
    }

    [Fact]
    public void Page06ViewData_GradeChoices_DoesNotHaveRequiredMemberAttribute()
    {
        // Arrange
        var property = typeof(Page06ViewData).GetProperty(nameof(Page06ViewData.GradeChoices));

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
