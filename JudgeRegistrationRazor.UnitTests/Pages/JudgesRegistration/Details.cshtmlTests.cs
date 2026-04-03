using System.Linq.Expressions;
using FluentAssertions;
using JudgeRegistrationRazor.Data;
using JudgeRegistrationRazor.Models;
using JudgeRegistrationRazor.Pages.JudgesRegistration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Xunit;

namespace JudgeRegistrationRazor.UnitTests.Pages.JudgesRegistration;

public sealed class DetailsModelTests
{
    [Fact]
    public void Constructor_WithContext_InitializesContext()
    {
        // Arrange
        var mockContext = new Mock<OdysseyContext>(
            new DbContextOptionsBuilder<OdysseyContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);

        // Act
        var model = new DetailsModel(mockContext.Object);

        // Assert
        model.Should().NotBeNull();
    }

    [Fact]
    public async Task OnGetAsync_WithNullId_ReturnsNotFound()
    {
        // Arrange
        var mockContext = new Mock<OdysseyContext>(
            new DbContextOptionsBuilder<OdysseyContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);

        var model = new DetailsModel(mockContext.Object);

        // Act
        var result = await model.OnGetAsync(null);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task OnGetAsync_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        var judges = new List<Judge>().AsQueryable();

        var mockSet = CreateMockDbSet(judges);

        var mockContext = new Mock<OdysseyContext>(
            new DbContextOptionsBuilder<OdysseyContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);

        mockContext.Setup(c => c.Judges).Returns(mockSet.Object);

        var model = new DetailsModel(mockContext.Object);

        // Act
        var result = await model.OnGetAsync(999);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task OnGetAsync_WithExistingId_SetsJudgeProperty()
    {
        // Arrange
        var judges = new List<Judge>
        {
            new Judge
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com"
            }
        }.AsQueryable();

        var mockSet = CreateMockDbSet(judges);

        var mockContext = new Mock<OdysseyContext>(
            new DbContextOptionsBuilder<OdysseyContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);

        mockContext.Setup(c => c.Judges).Returns(mockSet.Object);

        var model = new DetailsModel(mockContext.Object);

        // Act
        var result = await model.OnGetAsync(1);

        // Assert
        result.Should().BeOfType<PageResult>();
        model.Judge.Should().NotBeNull();
        model.Judge.Id.Should().Be(1);
        model.Judge.FirstName.Should().Be("John");
        model.Judge.LastName.Should().Be("Doe");
        model.Judge.EmailAddress.Should().Be("john.doe@example.com");
    }

    [Fact]
    public async Task OnGetAsync_WithExistingId_ReturnsPageResult()
    {
        // Arrange
        var judges = new List<Judge>
        {
            new Judge
            {
                Id = 42,
                FirstName = "Jane",
                LastName = "Smith"
            }
        }.AsQueryable();

        var mockSet = CreateMockDbSet(judges);

        var mockContext = new Mock<OdysseyContext>(
            new DbContextOptionsBuilder<OdysseyContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);

        mockContext.Setup(c => c.Judges).Returns(mockSet.Object);

        var model = new DetailsModel(mockContext.Object);

        // Act
        var result = await model.OnGetAsync(42);

        // Assert
        result.Should().BeOfType<PageResult>();
    }

    [Fact]
    public async Task OnGetAsync_WithMultipleJudges_ReturnsCorrectJudge()
    {
        // Arrange
        var judges = new List<Judge>
        {
            new Judge { Id = 1, FirstName = "First", LastName = "Judge" },
            new Judge { Id = 2, FirstName = "Second", LastName = "Judge" },
            new Judge { Id = 3, FirstName = "Third", LastName = "Judge" }
        }.AsQueryable();

        var mockSet = CreateMockDbSet(judges);

        var mockContext = new Mock<OdysseyContext>(
            new DbContextOptionsBuilder<OdysseyContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options);

        mockContext.Setup(c => c.Judges).Returns(mockSet.Object);

        var model = new DetailsModel(mockContext.Object);

        // Act
        var result = await model.OnGetAsync(2);

        // Assert
        result.Should().BeOfType<PageResult>();
        model.Judge.Should().NotBeNull();
        model.Judge.Id.Should().Be(2);
        model.Judge.FirstName.Should().Be("Second");
        model.Judge.LastName.Should().Be("Judge");
    }

    private static Mock<DbSet<Judge>> CreateMockDbSet(IQueryable<Judge> data)
    {
        var mockSet = new Mock<DbSet<Judge>>();
        mockSet.As<IQueryable<Judge>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Judge>(data.Provider));
        mockSet.As<IQueryable<Judge>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Judge>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Judge>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
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
