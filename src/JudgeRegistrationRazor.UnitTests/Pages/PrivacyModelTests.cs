using FluentAssertions;
using JudgeRegistrationRazor.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JudgeRegistrationRazor.UnitTests.Pages;

public class PrivacyModelTests
{
    [Fact]
    public void Constructor_ShouldCreateInstance_WhenCalled()
    {
        // Arrange

        // Act
        var model = new PrivacyModel();

        // Assert
        model.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_ShouldInheritFromPageModel_WhenCalled()
    {
        // Arrange

        // Act
        var model = new PrivacyModel();

        // Assert
        model.Should().BeAssignableTo<PageModel>();
    }

    [Fact]
    public void OnGet_ShouldNotThrow_WhenHttpContextIsAvailable()
    {
        // Arrange
        var model = new PrivacyModel
        {
            PageContext = new PageContext
            {
                HttpContext = new DefaultHttpContext(),
            },
        };

        // Act
        var act = model.OnGet;

        // Assert
        act.Should().NotThrow();
    }
}
