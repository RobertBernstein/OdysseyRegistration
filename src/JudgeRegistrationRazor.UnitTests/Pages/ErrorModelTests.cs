using FluentAssertions;
using JudgeRegistrationRazor.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics;

namespace JudgeRegistrationRazor.UnitTests;

public class ErrorModelTests
{
    [Fact]
    public void Constructor_ShouldCreateInstance_WhenCalled()
    {
        // Arrange

        // Act
        var model = new ErrorModel();

        // Assert
        model.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_ShouldInheritFromPageModel_WhenCalled()
    {
        // Arrange

        // Act
        var model = new ErrorModel();

        // Assert
        model.Should().BeAssignableTo<PageModel>();
    }

    [Fact]
    public void Constructor_ShouldInitializeRequestIdToNull_WhenCalled()
    {
        // Arrange
        var model = new ErrorModel();

        // Act
        var requestId = model.RequestId;

        // Assert
        requestId.Should().BeNull();
    }

    [Fact]
    public void ShowRequestId_ShouldReturnFalse_WhenRequestIdIsNull()
    {
        // Arrange
        var model = new ErrorModel
        {
            RequestId = null
        };

        // Act
        var result = model.ShowRequestId;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void ShowRequestId_ShouldReturnFalse_WhenRequestIdIsEmpty()
    {
        // Arrange
        var model = new ErrorModel
        {
            RequestId = string.Empty
        };

        // Act
        var result = model.ShowRequestId;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void ShowRequestId_ShouldReturnTrue_WhenRequestIdHasValue()
    {
        // Arrange
        var model = new ErrorModel
        {
            RequestId = "test-request-id"
        };

        // Act
        var result = model.ShowRequestId;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void OnGet_ShouldSetRequestIdFromActivityCurrent_WhenActivityCurrentExists()
    {
        // Arrange
        var model = new ErrorModel();
        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.Setup(c => c.TraceIdentifier).Returns("trace-id");
        model.PageContext = new PageContext
        {
            HttpContext = mockHttpContext.Object
        };

        using var activity = new Activity("test-activity");
        activity.Start();

        // Act
        model.OnGet();

        // Assert
        model.RequestId.Should().Be(activity.Id);
    }

    [Fact]
    public void OnGet_ShouldSetRequestIdFromHttpContextTraceIdentifier_WhenActivityCurrentIsNull()
    {
        // Arrange
        var model = new ErrorModel();
        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.Setup(c => c.TraceIdentifier).Returns("trace-id-123");
        model.PageContext = new PageContext
        {
            HttpContext = mockHttpContext.Object,
        };

        // Ensure no current activity
        Activity.Current?.Stop();

        // Act
        model.OnGet();

        // Assert
        model.RequestId.Should().Be("trace-id-123");
    }
}
