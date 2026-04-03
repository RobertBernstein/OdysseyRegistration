using FluentAssertions;
using JudgeRegistrationRazor.Data;
using JudgeRegistrationRazor.Models;
using JudgeRegistrationRazor.Pages.JudgesRegistration;
using JudgeRegistrationRazor.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace JudgeRegistrationRazor.UnitTests.Pages.JudgesRegistration;

public sealed class Page02ModelTests
{
    [Fact]
    public void Constructor_ShouldSetCurrentRegistrationType_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();

        // Act
        var model = new Page02Model(mockContext.Object, mockLogger.Object);

        // Assert
        model.CurrentRegistrationType.Should().Be(BasePageModel.RegistrationType.Judges);
    }

    [Fact]
    public void Constructor_ShouldSetTitle_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();

        // Act
        var model = new Page02Model(mockContext.Object, mockLogger.Object);

        // Assert
        model.Title.Should().Be("Judges Registration Page 2 of 3");
    }

    [Fact]
    public void OnGet_ShouldReturnPageResult_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page02Model(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.OnGet();

        // Assert
        result.Should().BeOfType<PageResult>();
    }

    [Fact]
    public async Task OnPostAsync_ShouldReturnPageResult_WhenModelStateIsInvalid()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page02Model(mockContext.Object, mockLogger.Object);
        model.ModelState.AddModelError("TestError", "Test error message");
        model.Judge = new Judge();

        // Act
        var result = await model.OnPostAsync();

        // Assert
        result.Should().BeOfType<PageResult>();
    }

    [Fact]
    public async Task OnPostAsync_ShouldAddJudgeToContext_WhenModelStateIsValid()
    {
        // Arrange
        var mockContext = CreateMockContextWithJudges();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page02Model(mockContext.Object, mockLogger.Object);
        var judge = new Judge
        {
            FirstName = "John",
            LastName = "Doe"
        };
        model.Judge = judge;

        // Act
        await model.OnPostAsync();

        // Assert
        mockContext.Verify(c => c.Judges.Add(It.IsAny<Judge>()), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_ShouldCallSaveChangesAsync_WhenModelStateIsValid()
    {
        // Arrange
        var mockContext = CreateMockContextWithJudges();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page02Model(mockContext.Object, mockLogger.Object);
        var judge = new Judge
        {
            FirstName = "John",
            LastName = "Doe"
        };
        model.Judge = judge;

        // Act
        await model.OnPostAsync();

        // Assert
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task OnPostAsync_ShouldRedirectToPage03_WhenModelStateIsValid()
    {
        // Arrange
        var mockContext = CreateMockContextWithJudges();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page02Model(mockContext.Object, mockLogger.Object);
        var judge = new Judge
        {
            FirstName = "John",
            LastName = "Doe"
        };
        model.Judge = judge;

        // Act
        var result = await model.OnPostAsync();

        // Assert
        result.Should().BeOfType<RedirectToPageResult>();
        var redirectResult = result as RedirectToPageResult;
        redirectResult!.PageName.Should().Be("Page03");
    }

    [Fact]
    public async Task OnPostAsync_ShouldNotCallSaveChangesAsync_WhenModelStateIsInvalid()
    {
        // Arrange
        var mockContext = CreateMockContextWithJudges();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page02Model(mockContext.Object, mockLogger.Object);
        model.ModelState.AddModelError("TestError", "Test error message");
        model.Judge = new Judge();

        // Act
        await model.OnPostAsync();

        // Assert
        mockContext.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task OnPostAsync_ShouldNotAddJudgeToContext_WhenModelStateIsInvalid()
    {
        // Arrange
        var mockContext = CreateMockContextWithJudges();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page02Model(mockContext.Object, mockLogger.Object);
        model.ModelState.AddModelError("TestError", "Test error message");
        model.Judge = new Judge();

        // Act
        await model.OnPostAsync();

        // Assert
        mockContext.Verify(c => c.Judges.Add(It.IsAny<Judge>()), Times.Never);
    }

    private static Mock<OdysseyContext> CreateMockContext()
    {
        var options = new DbContextOptionsBuilder<OdysseyContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var mockContext = new Mock<OdysseyContext>(options);

        // Setup Configs DbSet
        var configs = new List<Config>
        {
            new() { Name = "Year", Value = "2024" },
            new() { Name = "RegionName", Value = "NoVA North" },
            new() { Name = "RegionNumber", Value = "9" },
            new() { Name = "RegionalDirectorEmail", Value = "director@example.com" }
        }.AsQueryable();

        var mockConfigSet = CreateMockDbSet(configs);
        mockContext.Setup(c => c.Configs).Returns(mockConfigSet.Object);

        return mockContext;
    }

    private static Mock<OdysseyContext> CreateMockContextWithJudges()
    {
        var mockContext = CreateMockContext();

        // Setup Judges DbSet
        var judges = new List<Judge>().AsQueryable();
        var mockJudgeSet = CreateMockDbSet(judges);
        mockContext.Setup(c => c.Judges).Returns(mockJudgeSet.Object);

        return mockContext;
    }

    private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
    {
        var mockSet = new Mock<DbSet<T>>();
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        return mockSet;
    }
}
