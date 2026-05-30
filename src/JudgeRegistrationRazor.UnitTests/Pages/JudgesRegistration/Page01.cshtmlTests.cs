using FluentAssertions;
using JudgeRegistrationRazor.Data;
using JudgeRegistrationRazor.Models;
using JudgeRegistrationRazor.Pages.JudgesRegistration;
using JudgeRegistrationRazor.Pages.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace JudgeRegistrationRazor.UnitTests.Pages.JudgesRegistration;

public sealed class Page01ModelTests
{
    [Fact]
    public void Constructor_ShouldSetCurrentRegistrationType_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();

        // Act
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

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
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Assert
        model.Title.Should().Be("Judges Registration Page 1 of 3");
    }

    [Fact]
    public void Constructor_ShouldSetMessage_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();

        // Act
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Assert
        model.Message.Should().Contain("Welcome to the NoVA North Region 9 2024-2025 Odyssey of the Mind Judges Registration");
    }

    [Fact]
    public void JudgesTrainingDate_ShouldReturnTBA_WhenJudgesInfoIsNull()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object)
        {
            JudgesInfo = null
        };

        // Act
        var result = model.JudgesTrainingDate;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void JudgesTrainingDate_ShouldReturnTBA_WhenStartDateIsNull()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object)
        {
            JudgesInfo = new Event { StartDate = null }
        };

        // Act
        var result = model.JudgesTrainingDate;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void JudgesTrainingDate_ShouldReturnLongDateString_WhenStartDateHasValue()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var testDate = new DateTime(2024, 3, 15);
        var model = new Page01Model(mockContext.Object, mockLogger.Object)
        {
            JudgesInfo = new Event { StartDate = testDate }
        };

        // Act
        var result = model.JudgesTrainingDate;

        // Assert
        result.Should().Be(testDate.ToLongDateString());
    }

    [Fact]
    public void JudgesTrainingLocation_ShouldReturnTBA_WhenJudgesInfoIsNull()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object)
        {
            JudgesInfo = null
        };

        // Act
        var result = model.JudgesTrainingLocation;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void JudgesTrainingLocation_ShouldReturnTBA_WhenLocationIsNull()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object)
        {
            JudgesInfo = new Event { Location = null }
        };

        // Act
        var result = model.JudgesTrainingLocation;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void JudgesTrainingLocation_ShouldReturnTBA_WhenLocationIsEmpty()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object)
        {
            JudgesInfo = new Event { Location = string.Empty }
        };

        // Act
        var result = model.JudgesTrainingLocation;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void JudgesTrainingLocation_ShouldReturnTBA_WhenLocationIsWhitespace()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object)
        {
            JudgesInfo = new Event { Location = "   " }
        };

        // Act
        var result = model.JudgesTrainingLocation;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void JudgesTrainingLocation_ShouldReturnLocation_WhenLocationHasValue()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var testLocation = "Community Center";
        var model = new Page01Model(mockContext.Object, mockLogger.Object)
        {
            JudgesInfo = new Event { Location = testLocation }
        };

        // Act
        var result = model.JudgesTrainingLocation;

        // Assert
        result.Should().Be(testLocation);
    }

    [Fact]
    public void JudgesTrainingTime_ShouldReturnTBA_WhenJudgesInfoIsNull()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object)
        {
            JudgesInfo = null
        };

        // Act
        var result = model.JudgesTrainingTime;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void JudgesTrainingTime_ShouldReturnTBA_WhenTimeIsNull()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object)
        {
            JudgesInfo = new Event { Time = null }
        };

        // Act
        var result = model.JudgesTrainingTime;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void JudgesTrainingTime_ShouldReturnTBA_WhenTimeIsEmpty()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object)
        {
            JudgesInfo = new Event { Time = string.Empty }
        };

        // Act
        var result = model.JudgesTrainingTime;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void JudgesTrainingTime_ShouldReturnTBA_WhenTimeIsWhitespace()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object)
        {
            JudgesInfo = new Event { Time = "   " }
        };

        // Act
        var result = model.JudgesTrainingTime;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void JudgesTrainingTime_ShouldReturnTime_WhenTimeHasValue()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var testTime = "9:00 AM - 12:00 PM";
        var model = new Page01Model(mockContext.Object, mockLogger.Object)
        {
            JudgesInfo = new Event { Time = testTime }
        };

        // Act
        var result = model.JudgesTrainingTime;

        // Assert
        result.Should().Be(testTime);
    }

    [Fact]
    public void MailRegionalDirectorHyperLinkText_ShouldContainMailto_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.MailRegionalDirectorHyperLinkText;

        // Assert
        result.Should().StartWith("mailto:");
    }

    [Fact]
    public void MailRegionalDirectorHyperLinkText_ShouldContainRegionalDirectorEmail_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.MailRegionalDirectorHyperLinkText;

        // Assert
        result.Should().Contain("director@example.com");
    }

    [Fact]
    public void MailRegionalDirectorHyperLinkText_ShouldContainSubject_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.MailRegionalDirectorHyperLinkText;

        // Assert
        result.Should().Contain("?subject=");
        result.Should().Contain("Region%209%20Tournament");
    }

    [Fact]
    public void MailRegionalDirectorHyperLinkText_ShouldContainBody_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.MailRegionalDirectorHyperLinkText;

        // Assert
        result.Should().Contain("&body=");
        result.Should().Contain("I%20cannot%20be%20a%20judge%20this%20year");
    }

    [Fact]
    public void MailRegionalDirectorHyperLinkText_ShouldReplaceSpacesWithPercent20_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.MailRegionalDirectorHyperLinkText;

        // Assert
        result.Should().NotContain(" ");
        result.Should().Contain("%20");
    }

    [Fact]
    public void MailRegionalDirectorHyperLinkText_ShouldIncludeRegionNumber_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.MailRegionalDirectorHyperLinkText;

        // Assert
        result.Should().Contain("Region%209%20Tournament");
    }

    [Fact]
    public async Task OnGetAsync_ShouldLoadJudgesInfo_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContextWithEvents();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        await model.OnGetAsync();

        // Assert
        model.JudgesInfo.Should().NotBeNull();
        model.JudgesInfo!.EventName.Should().Contain("Judges");
        model.JudgesInfo.EventName.Should().Contain("Training");
    }

    [Fact]
    public async Task OnGetAsync_ShouldLoadJudgesList_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContextWithEvents();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        await model.OnGetAsync();

        // Assert
        model.Judge.Should().NotBeNull();
        model.Judge.Should().HaveCount(2);
    }

    [Fact]
    public async Task OnGetAsync_ShouldQueryEventsTable_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContextWithEvents();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        await model.OnGetAsync();

        // Assert
        mockContext.Verify(c => c.Events, Times.AtLeastOnce);
    }

    [Fact]
    public async Task OnGetAsync_ShouldQueryJudgesTable_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContextWithEvents();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        await model.OnGetAsync();

        // Assert
        mockContext.Verify(c => c.Judges, Times.Once);
    }

    [Fact]
    public async Task OnGetAsync_ShouldSetJudgesInfo_WhenMultipleEventsExist()
    {
        // Arrange
        var mockContext = CreateMockContextWithEvents();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        await model.OnGetAsync();

        // Assert
        model.JudgesInfo.Should().NotBeNull();
        model.JudgesInfo!.EventName.Should().Be("Judges Training Session");
    }

    [Fact]
    public async Task OnGetAsync_ShouldLoadAllJudges_WhenJudgesExist()
    {
        // Arrange
        var mockContext = CreateMockContextWithEvents();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        await model.OnGetAsync();

        // Assert
        model.Judge.Should().ContainSingle(j => j.FirstName == "John" && j.LastName == "Doe");
        model.Judge.Should().ContainSingle(j => j.FirstName == "Jane" && j.LastName == "Smith");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_ShouldStartWithMailto_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.BuildMailRegionalDirectorHyperLink();

        // Assert
        result.Should().StartWith("mailto:");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_ShouldContainRegionalDirectorEmail_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.BuildMailRegionalDirectorHyperLink();

        // Assert
        result.Should().Contain("director@example.com");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_ShouldContainSubject_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.BuildMailRegionalDirectorHyperLink();

        // Assert
        result.Should().Contain("?subject=");
        result.Should().Contain("I%20would%20like%20to%20help%20at%20the%20Region%209%20Tournament");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_ShouldContainBody_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.BuildMailRegionalDirectorHyperLink();

        // Assert
        result.Should().Contain("&body=");
        result.Should().Contain("I%20cannot%20be%20a%20judge%20this%20year");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_ShouldReplaceSpacesWithPercent20_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.BuildMailRegionalDirectorHyperLink();

        // Assert
        result.Should().NotContain(" ");
        result.Should().Contain("%20");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_ShouldIncludeRegionNumber_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.BuildMailRegionalDirectorHyperLink();

        // Assert
        result.Should().Contain("Region%209%20Tournament");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_ShouldReturnCompleteMailtoString_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<Page01Model>>();
        var model = new Page01Model(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.BuildMailRegionalDirectorHyperLink();

        // Assert
        result.Should().Be("mailto:director@example.com?subject=I%20would%20like%20to%20help%20at%20the%20Region%209%20Tournament&body=I%20cannot%20be%20a%20judge%20this%20year,%20but%20would%20like%20to%20help%20in%20some%20other%20way.%0A%0AMy%20name%20is%20______________________.%0A%0AMy%20phone%20number%20is%20______________________.%0A%0A");
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
            new Config { Name = "RegionNumber", Value = "9" },
            new Config { Name = "RegionalDirectorEmail", Value = "director@example.com" }
        }.AsQueryable();

        var mockConfigSet = CreateMockDbSet(configs);
        mockContext.Setup(c => c.Configs).Returns(mockConfigSet.Object);

        return mockContext;
    }

    private static Mock<OdysseyContext> CreateMockContextWithEvents()
    {
        var mockContext = CreateMockContext();

        // Setup Events DbSet
        var events = new List<Event>
        {
            new Event
            {
                Id = 1,
                EventName = "Judges Training Session",
                StartDate = new DateTime(2024, 11, 15),
                Location = "Community Center",
                Time = "9:00 AM - 12:00 PM"
            },
            new Event
            {
                Id = 2,
                EventName = "Regional Tournament",
                StartDate = new DateTime(2024, 12, 10),
                Location = "High School Gym",
                Time = "8:00 AM - 5:00 PM"
            }
        }.AsQueryable();

        var mockEventSet = CreateMockDbSet(events);
        mockContext.Setup(c => c.Events).Returns(mockEventSet.Object);

        // Setup Judges DbSet
        var judges = new List<Judge>
        {
            new Judge
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            },
            new Judge
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith"
            }
        }.AsQueryable();

        var mockJudgeSet = CreateMockDbSet(judges);
        mockContext.Setup(c => c.Judges).Returns(mockJudgeSet.Object);

        return mockContext;
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
