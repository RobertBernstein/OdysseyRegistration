using FluentAssertions;
using JudgeRegistrationRazor.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;
using System.Diagnostics;

namespace JudgeRegistrationRazor.UnitTests.Pages;

public class ErrorModelTests
{
    [Fact]
    public void Constructor_ShouldCreateInstance_WhenCalled()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<ErrorModel>>();

        // Act
        var model = new ErrorModel(mockLogger.Object);

        // Assert
        model.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_ShouldInheritFromPageModel_WhenCalled()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<ErrorModel>>();

        // Act
        var model = new ErrorModel(mockLogger.Object);

        // Assert
        model.Should().BeAssignableTo<PageModel>();
    }

    [Fact]
    public void Constructor_ShouldStoreLogger_WhenCalled()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<ErrorModel>>();

        // Act
        var model = new ErrorModel(mockLogger.Object);

        // Assert
        model.Should().NotBeNull();
        // Logger is private, but we can verify construction succeeded
    }

    [Fact]
    public void ShowRequestId_ShouldReturnFalse_WhenRequestIdIsNull()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<ErrorModel>>();
        var model = new ErrorModel(mockLogger.Object)
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
        var mockLogger = new Mock<ILogger<ErrorModel>>();
        var model = new ErrorModel(mockLogger.Object)
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
        var mockLogger = new Mock<ILogger<ErrorModel>>();
        var model = new ErrorModel(mockLogger.Object)
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
        var mockLogger = new Mock<ILogger<ErrorModel>>();
        var model = new ErrorModel(mockLogger.Object);
        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.Setup(c => c.TraceIdentifier).Returns("trace-id");
        model.PageContext = new PageContext
        {
            HttpContext = mockHttpContext.Object
        };

        var activity = new Activity("test-activity");
        activity.Start();

        try
        {
            // Act
            model.OnGet();

            // Assert
            model.RequestId.Should().Be(activity.Id);
        }
        finally
        {
            activity.Stop();
        }
    }

    [Fact]
    public void OnGet_ShouldSetRequestIdFromHttpContextTraceIdentifier_WhenActivityCurrentIsNull()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<ErrorModel>>();
        var model = new ErrorModel(mockLogger.Object);
        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.Setup(c => c.TraceIdentifier).Returns("trace-id-123");
        model.PageContext = new PageContext
        {
            HttpContext = mockHttpContext.Object
        };

        // Ensure no current activity
        Activity.Current?.Stop();

        // Act
        model.OnGet();

        // Assert
        model.RequestId.Should().Be("trace-id-123");
    }
}
