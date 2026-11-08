using System.ComponentModel.DataAnnotations;
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

namespace OdysseyMvc2024.UnitTests.Controllers;

public sealed class TournamentRegistrationControllerPage02Tests
{
    [Fact]
    public void Page02Get_WhenRegistrationIsAvailable_ReturnsPage02ViewWithSchoolList()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page02(7);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeAssignableTo<Page02ViewData>().Subject;

        model.SchoolList.Should().NotBeNull();
        model.SchoolList.Should().HaveCount(2);
        model.SchoolList.Select(item => item.Value).Should().Contain(["29", "30"]);
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page02Get_WhenRegistrationIsNotAvailable_RedirectsToCurrentState(string state)
    {
        // Arrange
        var config = CreateConfigForState(state);
        var repository = CreateRepository(config);
        using var controller = CreateController(repository.Object);

        // Act
        var result = controller.Page02(7);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be(state);
    }

    [Fact]
    public void Page02Post_WithSelectedSchoolAndNoSchoolList_UpdatesRegistrationAndRedirectsToPage03()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        var viewData = new Page02ViewData
        {
            SelectedSchool = 29
        };

        // Act
        var result = controller.Page02(7, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Page03");
        redirect.RouteValues.Should().NotBeNull();
        redirect.RouteValues!["id"].Should().Be(7);

        repository.Verify(
            r => r.UpdateTournamentRegistration(
                7,
                2,
                It.Is<TournamentRegistration?>(registration => registration != null && registration.SchoolID == 29)),
            Times.Once);
    }

    [Fact]
    public void Page02Post_WhenModelStateIsInvalid_ReturnsPage02ViewAndDoesNotUpdateRegistration()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        using var controller = CreateController(repository.Object);

        var viewData = new Page02ViewData
        {
            SelectedSchool = 29
        };

        controller.ModelState.AddModelError("SelectedSchool", "Please select a school.");

        // Act
        var result = controller.Page02(7, viewData);

        // Assert
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var model = viewResult.Model.Should().BeAssignableTo<Page02ViewData>().Subject;

        model.SchoolList.Should().NotBeNull();
        model.SchoolList.Should().HaveCount(2);

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
    public void Page02Post_WhenRegistrationIsNotAvailable_RedirectsToCurrentStateAndDoesNotUpdate(string state)
    {
        // Arrange
        var config = CreateConfigForState(state);
        var repository = CreateRepository(config);
        using var controller = CreateController(repository.Object);

        var viewData = new Page02ViewData
        {
            SelectedSchool = 29
        };

        // Act
        var result = controller.Page02(7, viewData);

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
    public void Page02Post_WhenRepositoryUpdateThrows_RedirectsToHomeIndex()
    {
        // Arrange
        var repository = CreateRepositoryForAvailableState();
        repository
            .Setup(r => r.UpdateTournamentRegistration(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<TournamentRegistration?>()))
            .Throws(new InvalidOperationException("Database failure"));

        using var controller = CreateController(repository.Object);

        var viewData = new Page02ViewData
        {
            SelectedSchool = 29
        };

        // Act
        var result = controller.Page02(7, viewData);

        // Assert
        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Index");
        redirect.ControllerName.Should().Be("Home");
    }

    [Fact]
    public void Page02ViewData_SchoolList_HasValidateNeverAttribute()
    {
        // Arrange
        var property = typeof(Page02ViewData).GetProperty(nameof(Page02ViewData.SchoolList));

        // Act
        var validateNeverAttributes = property!.GetCustomAttributes(typeof(ValidateNeverAttribute), inherit: true);
        var hasValidateNever = validateNeverAttributes.Length > 0;

        // Assert
        hasValidateNever.Should().BeTrue();
    }

    [Fact]
    public void Page02ViewData_SelectedSchool_HasRequiredAttribute()
    {
        // Arrange
        var property = typeof(Page02ViewData).GetProperty(nameof(Page02ViewData.SelectedSchool));

        // Act
        var requiredAttribute = property!
            .GetCustomAttributes(typeof(RequiredAttribute), inherit: true)
            .Cast<RequiredAttribute>()
            .SingleOrDefault();

        // Assert
        requiredAttribute.Should().NotBeNull();
        requiredAttribute!.ErrorMessage.Should().Contain("Please select one of the schools or organizations");
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
        repository.SetupGet(r => r.Schools).Returns(new List<School>
        {
            new() { ID = 29, Name = "School 29" },
            new() { ID = 30, Name = "School 30" }
        });

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
