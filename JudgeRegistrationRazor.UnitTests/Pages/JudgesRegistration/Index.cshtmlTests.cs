using FluentAssertions;
using JudgeRegistrationRazor.Pages.JudgesRegistration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JudgeRegistrationRazor.UnitTests.Pages.JudgesRegistration;

public class IndexModelTests
{
    [Fact]
    public void Constructor_ShouldCreateInstance_WhenCalled()
    {
        // Arrange & Act
        var model = new IndexModel();

        // Assert
        model.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_ShouldInheritFromPageModel_WhenCalled()
    {
        // Arrange & Act
        var model = new IndexModel();

        // Assert
        model.Should().BeAssignableTo<PageModel>();
    }

    [Fact]
    public void OnGet_ShouldReturnRedirectToPageResult_WhenCalled()
    {
        // Arrange
        var model = new IndexModel();

        // Act
        var result = model.OnGet();

        // Assert
        result.Should().BeOfType<RedirectToPageResult>();
    }

    [Fact]
    public void OnGet_ShouldRedirectToPage01_WhenCalled()
    {
        // Arrange
        var model = new IndexModel();

        // Act
        var result = model.OnGet() as RedirectToPageResult;

        // Assert
        result.Should().NotBeNull();
        result!.PageName.Should().Be("Page01");
    }
}
