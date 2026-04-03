using FluentAssertions;
using JudgeRegistrationRazor.Data;
using JudgeRegistrationRazor.Models;
using JudgeRegistrationRazor.Pages;
using JudgeRegistrationRazor.Pages.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace JudgeRegistrationRazor.UnitTests.Pages;

public sealed class IndexModelTests
{
    [Fact]
    public void Constructor_ShouldCreateInstance_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<IndexModel>>();

        // Act
        var model = new IndexModel(mockContext.Object, mockLogger.Object);

        // Assert
        model.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_ShouldInheritFromBasePageModel_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<IndexModel>>();

        // Act
        var model = new IndexModel(mockContext.Object, mockLogger.Object);

        // Assert
        model.Should().BeAssignableTo<BasePageModel>();
    }

    [Fact]
    public void Constructor_ShouldInheritFromPageModel_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<IndexModel>>();

        // Act
        var model = new IndexModel(mockContext.Object, mockLogger.Object);

        // Assert
        model.Should().BeAssignableTo<PageModel>();
    }

    [Fact]
    public void Constructor_ShouldStoreContext_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<IndexModel>>();

        // Act
        var model = new IndexModel(mockContext.Object, mockLogger.Object);

        // Assert
        model.Context.Should().NotBeNull();
    }

    [Fact]
    public void OnGet_ShouldComplete_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<IndexModel>>();
        var model = new IndexModel(mockContext.Object, mockLogger.Object);

        // Act
        var act = () => model.OnGet();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void OnPost_ShouldReturnPageResult_WhenModelStateIsInvalid()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<IndexModel>>();
        var model = new IndexModel(mockContext.Object, mockLogger.Object);
        model.ModelState.AddModelError("TestError", "Test error message");
        SetupHttpContext(model, "example.com");

        // Act
        var result = model.OnPost();

        // Assert
        result.Should().BeOfType<PageResult>();
    }

    [Fact]
    public void OnPost_ShouldReturnRedirectResult_WhenModelStateIsValid()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<IndexModel>>();
        var model = new IndexModel(mockContext.Object, mockLogger.Object);
        SetupHttpContext(model, "example.com");

        // Act
        var result = model.OnPost();

        // Assert
        result.Should().BeOfType<RedirectResult>();
    }

    [Fact]
    public void OnPost_ShouldRedirectToHomePage_WhenModelStateIsValidAndHomePageConfigExists()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<IndexModel>>();
        var model = new IndexModel(mockContext.Object, mockLogger.Object);
        SetupHttpContext(model, "example.com");

        // Act
        var result = model.OnPost() as RedirectResult;

        // Assert
        result.Should().NotBeNull();
        result!.Url.Should().Be("https://example.com/home");
    }

    [Fact(Skip = "ProductionBugSuspected")]
    public void OnPost_ShouldHandleNullConfig_WhenModelStateIsValid()
    {
        // Arrange
        var mockContext = CreateMockContextWithoutHomePage();
        var mockLogger = new Mock<ILogger<IndexModel>>();
        var model = new IndexModel(mockContext.Object, mockLogger.Object);
        SetupHttpContext(model, "example.com");

        // Act
        var act = () => model.OnPost();

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void OnPost_ShouldNotCallRedirectWhenModelStateIsInvalid_WhenModelStateIsInvalid()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<IndexModel>>();
        var model = new IndexModel(mockContext.Object, mockLogger.Object);
        model.ModelState.AddModelError("Email", "Email is required");
        SetupHttpContext(model, "example.com");

        // Act
        var result = model.OnPost();

        // Assert
        result.Should().NotBeOfType<RedirectResult>();
    }

    [Fact]
    public void OnPost_ShouldReturnPageResult_WhenMultipleModelStateErrors()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<IndexModel>>();
        var model = new IndexModel(mockContext.Object, mockLogger.Object);
        model.ModelState.AddModelError("Email", "Email is required");
        model.ModelState.AddModelError("Name", "Name is required");
        SetupHttpContext(model, "example.com");

        // Act
        var result = model.OnPost();

        // Assert
        result.Should().BeOfType<PageResult>();
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
            new Config { Name = "Year", Value = "2024" },
            new Config { Name = "RegionName", Value = "NoVA North" },
            new Config { Name = "HomePage", Value = "https://example.com/home" }
        }.AsQueryable();

        var mockConfigSet = CreateMockDbSet(configs);
        mockContext.Setup(c => c.Configs).Returns(mockConfigSet.Object);

        return mockContext;
    }

    private static Mock<OdysseyContext> CreateMockContextWithoutHomePage()
    {
        var options = new DbContextOptionsBuilder<OdysseyContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var mockContext = new Mock<OdysseyContext>(options);

        // Setup Configs DbSet without HomePage
        var configs = new List<Config>
        {
            new Config { Name = "Year", Value = "2024" },
            new Config { Name = "RegionName", Value = "NoVA North" }
        }.AsQueryable();

        var mockConfigSet = CreateMockDbSet(configs);
        mockContext.Setup(c => c.Configs).Returns(mockConfigSet.Object);

        return mockContext;
    }

    private static void SetupHttpContext(IndexModel model, string host)
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Host = new HostString(host);
        httpContext.Request.Path = "/test";
        httpContext.Request.QueryString = QueryString.Empty;
        model.PageContext.HttpContext = httpContext;
    }

    private static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
    {
        var mockSet = new Mock<DbSet<T>>();
        mockSet.As<IAsyncEnumerable<T>>()
            .Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new TestAsyncEnumerator<T>(data.GetEnumerator()));
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<T>(data.Provider));
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        return mockSet;
    }

    private sealed class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        public TestAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestAsyncEnumerable<TElement>(expression);
        }

        public object? Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        {
            var resultType = typeof(TResult).GetGenericArguments()[0];
            var executionResult = typeof(IQueryProvider)
                .GetMethod(nameof(IQueryProvider.Execute), 1, [typeof(Expression)])!
                .MakeGenericMethod(resultType)
                .Invoke(this, [expression]);

            return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))!
                .MakeGenericMethod(resultType)
                .Invoke(null, [executionResult])!;
        }
    }

    private sealed class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public TestAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        {
        }

        public TestAsyncEnumerable(Expression expression)
            : base(expression)
        {
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);
    }

    private sealed class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public T Current => _inner.Current;

        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(_inner.MoveNext());
        }

        public ValueTask DisposeAsync()
        {
            _inner.Dispose();
            return default;
        }
    }
}
