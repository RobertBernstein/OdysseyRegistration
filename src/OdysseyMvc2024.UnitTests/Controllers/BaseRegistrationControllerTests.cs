using System.Globalization;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Moq;
using OdysseyMvc2024.Controllers;
using OdysseyMvc2024.Models;

namespace OdysseyMvc2024.UnitTests.Controllers;

using RegistrationType = BaseRegistrationController.RegistrationType;

public sealed class BaseRegistrationControllerTests
{
    [Theory]
    [InlineData(RegistrationType.Tournament)]
    [InlineData(RegistrationType.Judges)]
    public void IsRegistrationClosed_WhenCloseDateInFuture_ReturnsFalse(RegistrationType registrationType)
    {
        // Arrange
        var config = CreateDefaultConfig();
        config[$"{registrationType}RegistrationCloseDateTime"] = DateTime.Now.AddDays(30).ToString(CultureInfo.InvariantCulture);

        var repository = CreateRepository(config);
        using var controller = CreateController(repository.Object, registrationType);

        // Act
        bool isClosed = controller.IsRegistrationClosed(registrationType);

        // Assert
        isClosed.Should().BeFalse();
    }

    [Theory]
    [InlineData(RegistrationType.Tournament)]
    [InlineData(RegistrationType.Judges)]
    public void IsRegistrationClosed_WhenCloseDateInPast_ReturnsTrue(RegistrationType registrationType)
    {
        // Arrange
        var config = CreateDefaultConfig();
        config[$"{registrationType}RegistrationCloseDateTime"] = DateTime.Now.AddDays(-30).ToString(CultureInfo.InvariantCulture);

        var repository = CreateRepository(config);
        using var controller = CreateController(repository.Object, registrationType);

        // Act
        bool isClosed = controller.IsRegistrationClosed(registrationType);

        // Assert
        isClosed.Should().BeTrue();
    }

    [Theory]
    [InlineData(RegistrationType.Tournament)]
    [InlineData(RegistrationType.Judges)]
    public void IsRegistrationClosed_WhenCloseDateIsInvalid_ReturnsTrue(RegistrationType registrationType)
    {
        // Arrange
        var config = CreateDefaultConfig();
        config[$"{registrationType}RegistrationCloseDateTime"] = "2/29/2025 09:00:00";

        var repository = CreateRepository(config);
        using var controller = CreateController(repository.Object, registrationType);

        // Act
        bool isClosed = controller.IsRegistrationClosed(registrationType);

        // Assert
        isClosed.Should().BeTrue();
    }

    [Theory]
    [InlineData(RegistrationType.Tournament)]
    [InlineData(RegistrationType.Judges)]
    public void IsRegistrationClosed_WhenCloseDateIsValidLeapDayInFuture_ReturnsFalse(RegistrationType registrationType)
    {
        // Arrange
        var config = CreateDefaultConfig();
        config[$"{registrationType}RegistrationCloseDateTime"] = "2/29/2032 09:00:00";

        var repository = CreateRepository(config);
        using var controller = CreateController(repository.Object, registrationType);

        // Act
        bool isClosed = controller.IsRegistrationClosed(registrationType);

        // Assert
        isClosed.Should().BeFalse();
    }

    [Theory]
    [InlineData(RegistrationType.Tournament)]
    [InlineData(RegistrationType.Judges)]
    public void IsRegistrationComingSoon_WhenOpenDateInFuture_ReturnsTrue(RegistrationType registrationType)
    {
        // Arrange
        var config = CreateDefaultConfig();
        config[$"{registrationType}RegistrationOpenDateTime"] = DateTime.Now.AddDays(30).ToString(CultureInfo.InvariantCulture);

        var repository = CreateRepository(config);
        using var controller = CreateController(repository.Object, registrationType);

        // Act
        bool isComingSoon = controller.IsRegistrationComingSoon(registrationType);

        // Assert
        isComingSoon.Should().BeTrue();
    }

    [Theory]
    [InlineData(RegistrationType.Tournament)]
    [InlineData(RegistrationType.Judges)]
    public void IsRegistrationComingSoon_WhenOpenDateIsValidLeapDayInFuture_ReturnsTrue(RegistrationType registrationType)
    {
        // Arrange
        var config = CreateDefaultConfig();
        config[$"{registrationType}RegistrationOpenDateTime"] = "2/29/2032 09:00:00";

        var repository = CreateRepository(config);
        using var controller = CreateController(repository.Object, registrationType);

        // Act
        bool isComingSoon = controller.IsRegistrationComingSoon(registrationType);

        // Assert
        isComingSoon.Should().BeTrue();
    }

    [Theory]
    [InlineData(RegistrationType.Tournament)]
    [InlineData(RegistrationType.Judges)]
    public void IsRegistrationComingSoon_WhenOpenDateInPast_ReturnsFalse(RegistrationType registrationType)
    {
        // Arrange
        var config = CreateDefaultConfig();
        config[$"{registrationType}RegistrationOpenDateTime"] = DateTime.Now.AddDays(-30).ToString(CultureInfo.InvariantCulture);

        var repository = CreateRepository(config);
        using var controller = CreateController(repository.Object, registrationType);

        // Act
        bool isComingSoon = controller.IsRegistrationComingSoon(registrationType);

        // Assert
        isComingSoon.Should().BeFalse();
    }

    [Theory]
    [InlineData(RegistrationType.Tournament)]
    [InlineData(RegistrationType.Judges)]
    public void IsRegistrationDown_WhenFlagIsTrue_ReturnsTrue(RegistrationType registrationType)
    {
        // Arrange
        var config = CreateDefaultConfig();
        config[$"Is{registrationType}RegistrationDown"] = "true";

        var repository = CreateRepository(config);
        using var controller = CreateController(repository.Object, registrationType);

        // Act
        bool isDown = controller.IsRegistrationDown(registrationType);

        // Assert
        isDown.Should().BeTrue();
    }

    [Theory]
    [InlineData(RegistrationType.Tournament)]
    [InlineData(RegistrationType.Judges)]
    public void IsRegistrationDown_WhenFlagIsFalse_ReturnsFalse(RegistrationType registrationType)
    {
        // Arrange
        var config = CreateDefaultConfig();
        config[$"Is{registrationType}RegistrationDown"] = "false";

        var repository = CreateRepository(config);
        using var controller = CreateController(repository.Object, registrationType);

        // Act
        bool isDown = controller.IsRegistrationDown(registrationType);

        // Assert
        isDown.Should().BeFalse();
    }

    private static TestableBaseRegistrationController CreateController(IOdysseyRepository repository, RegistrationType registrationType)
    {
        var controller = new TestableBaseRegistrationController(repository)
        {
            CurrentRegistrationType = registrationType,
            FriendlyRegistrationName = registrationType == RegistrationType.None
                ? string.Empty
                : $"{registrationType} Registration"
        };

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

    private static Mock<IOdysseyRepository> CreateRepository(Dictionary<string, string> config)
    {
        var repository = new Mock<IOdysseyRepository>();
        repository.SetupGet(r => r.Config).Returns(config);
        repository.SetupGet(r => r.RegionName).Returns("NoVA North");
        repository.SetupGet(r => r.RegionNumber).Returns("7");
        repository.SetupGet(r => r.TournamentInfo).Returns(new Event { EventName = "NoVA North" });

        return repository;
    }

    private static Dictionary<string, string> CreateDefaultConfig()
    {
        return new Dictionary<string, string>
        {
            ["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(-30).ToString(CultureInfo.InvariantCulture),
            ["TournamentRegistrationCloseDateTime"] = DateTime.Now.AddDays(30).ToString(CultureInfo.InvariantCulture),
            ["JudgesRegistrationOpenDateTime"] = DateTime.Now.AddDays(-30).ToString(CultureInfo.InvariantCulture),
            ["JudgesRegistrationCloseDateTime"] = DateTime.Now.AddDays(30).ToString(CultureInfo.InvariantCulture),
            ["IsTournamentRegistrationDown"] = "false",
            ["IsJudgesRegistrationDown"] = "false",
            ["HomePage"] = "/"
        };
    }

    private sealed class TestableBaseRegistrationController(IOdysseyRepository repository)
        : BaseRegistrationController(repository)
    {
    }
}
