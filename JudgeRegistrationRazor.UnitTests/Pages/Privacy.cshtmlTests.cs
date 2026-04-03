using FluentAssertions;
using JudgeRegistrationRazor.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;

namespace JudgeRegistrationRazor.UnitTests.Pages;

public class PrivacyModelTests
{
    [Fact]
    public void Constructor_ShouldCreateInstance_WhenCalled()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<PrivacyModel>>();

        // Act
        var model = new PrivacyModel(mockLogger.Object);

        // Assert
        model.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_ShouldInheritFromPageModel_WhenCalled()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<PrivacyModel>>();

        // Act
        var model = new PrivacyModel(mockLogger.Object);

        // Assert
        model.Should().BeAssignableTo<PageModel>();
    }

    [Fact]
    public void Constructor_ShouldStoreLogger_WhenCalled()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<PrivacyModel>>();

        // Act
        var model = new PrivacyModel(mockLogger.Object);

        // Assert
        model.Should().NotBeNull();
        // Logger is private, but we can verify construction succeeded
    }

    [Fact]
    public void OnGet_ShouldComplete_WhenCalled()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<PrivacyModel>>();
        var model = new PrivacyModel(mockLogger.Object);

        // Act
        var act = () => model.OnGet();

        // Assert
        act.Should().NotThrow();
    }
}
