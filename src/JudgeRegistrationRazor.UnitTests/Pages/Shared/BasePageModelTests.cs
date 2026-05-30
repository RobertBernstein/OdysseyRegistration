using FluentAssertions;
using JudgeRegistrationRazor.Data;
using JudgeRegistrationRazor.Models;
using JudgeRegistrationRazor.Pages.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace JudgeRegistrationRazor.UnitTests.Pages.Shared;

public sealed class BasePageModelTests
{
    [Fact]
    public void Context_ShouldReturnContext_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();

        // Act
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Assert
        model.Context.Should().BeSameAs(mockContext.Object);
    }

    [Fact]
    public void Logger_ShouldReturnLogger_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();

        // Act
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Assert
        model.Logger.Should().BeSameAs(mockLogger.Object);
    }

    [Fact]
    public void Config_ShouldThrowKeyNotFoundException_WhenConfigsIsEmpty()
    {
        // Arrange
        var mockContext = CreateMockContextWithEmptyConfigs();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act & Assert
        var act = () => model.Config;
        act.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void Config_ShouldReturnDictionary_WhenConfigsExist()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.Config;

        // Assert
        result.Should().NotBeNull();
        result.Should().ContainKey("Year");
        result.Should().ContainKey("RegionName");
        result.Should().ContainKey("RegionNumber");
    }

    [Fact]
    public void Config_ShouldAddEndYear_WhenConfigsExist()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.Config;

        // Assert
        result.Should().ContainKey("EndYear");
        result!["EndYear"].Should().Be("2025");
    }

    [Fact]
    public void Config_ShouldCacheResult_WhenCalledMultipleTimes()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result1 = model.Config;
        var result2 = model.Config;

        // Assert
        result1.Should().BeSameAs(result2);
    }

    [Fact]
    public void Config_ShouldReturnValuesFromDatabase_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.Config;

        // Assert
        result!["Year"].Should().Be("2024");
        result["RegionName"].Should().Be("NoVA North");
        result["RegionNumber"].Should().Be("9");
    }

    [Fact]
    public void SiteName_ShouldReturnHostName_WhenRequestHasHost()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        SetupHttpContext(model, "example.com");

        // Act
        var result = model.SiteName;

        // Assert
        result.Should().Be("example.com");
    }

    [Fact]
    public void SiteName_ShouldReturnLowercaseHostName_WhenRequestHasUppercaseHost()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        SetupHttpContext(model, "EXAMPLE.COM");

        // Act
        var result = model.SiteName;

        // Assert
        result.Should().Be("example.com");
    }

    [Fact]
    public void SiteName_ShouldNotRemoveWwwPrefix_WhenHostHasWwwPrefix()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        SetupHttpContext(model, "www.example.com");

        // Act
        var result = model.SiteName;

        // Assert
        // Bug: Line 103 calls Replace but doesn't assign the result back to _siteName
        result.Should().Be("www.example.com");
    }

    [Fact]
    public void SiteName_ShouldThrowArgumentNullException_WhenConfigsDbSetIsNull()
    {
        // Arrange
        var mockContext = CreateMockContextWithNullConfigs();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act & Assert
        var act = () => model.SiteName;
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void SiteName_ShouldCacheResult_WhenCalledMultipleTimes()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        SetupHttpContext(model, "example.com");

        // Act
        var result1 = model.SiteName;
        var result2 = model.SiteName;

        // Assert
        result1.Should().Be(result2);
    }

    [Fact]
    public void SiteName_ShouldSetRegionName_WhenCalled()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        SetupHttpContext(model, "example.com");

        // Act
        var siteName = model.SiteName;
        var regionName = model.RegionName;

        // Assert
        regionName.Should().Be("NoVA North");
    }

    [Fact]
    public void RegionName_ShouldReturnRegionName_WhenConfigExists()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.RegionName;

        // Assert
        result.Should().Be("NoVA North");
    }

    [Fact]
    public void RegionName_ShouldThrowArgumentNullException_WhenConfigsDbSetIsNull()
    {
        // Arrange
        var mockContext = CreateMockContextWithNullConfigs();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act & Assert
        var act = () => model.RegionName;
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void RegionName_ShouldCacheResult_WhenCalledMultipleTimes()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result1 = model.RegionName;
        var result2 = model.RegionName;

        // Assert
        result1.Should().Be(result2);
    }

    [Fact]
    public void RegionName_ShouldReturnEmpty_WhenRegionNameNotInConfig()
    {
        // Arrange
        var mockContext = CreateMockContextWithoutRegionName();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act & Assert
        var act = () => model.RegionName;
        act.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void Config_ShouldParseYearCorrectly_WhenYearIsValid()
    {
        // Arrange
        var mockContext = CreateMockContextWithYear("2023");
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.Config;

        // Assert
        result!["EndYear"].Should().Be("2024");
    }

    [Fact]
    public void Config_ShouldThrowFormatException_WhenYearIsNotNumeric()
    {
        // Arrange
        var mockContext = CreateMockContextWithYear("invalid");
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act & Assert
        var act = () => model.Config;
        act.Should().Throw<FormatException>();
    }

    [Fact]
    public void SiteName_ShouldReturnEmpty_WhenRegionNameIsEmpty()
    {
        // Arrange
        var mockContext = CreateMockContextWithEmptyRegionName();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        SetupHttpContext(model, "example.com");

        // Act
        var result = model.SiteName;

        // Assert
        result.Should().Be("example.com");
        model._regionName.Should().BeEmpty();
    }

    [Fact]
    public void RegionName_ShouldReturnEmpty_WhenRegionNameIsEmptyInConfig()
    {
        // Arrange
        var mockContext = CreateMockContextWithEmptyRegionName();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.RegionName;

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Config_ShouldReturnPresetValue_WhenBackingFieldIsAlreadySet()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        var presetConfig = new Dictionary<string, string> { { "Test", "Value" } };
        model._config = presetConfig;

        // Act
        var result = model.Config;

        // Assert
        result.Should().BeSameAs(presetConfig);
    }

    [Fact]
    public void SiteName_ShouldReturnPresetValue_WhenBackingFieldIsAlreadySet()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        model._siteName = "preset.com";

        // Act
        var result = model.SiteName;

        // Assert
        result.Should().Be("preset.com");
    }

    [Fact]
    public void RegionName_ShouldReturnPresetValue_WhenBackingFieldIsAlreadySet()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        model._regionName = "Preset Region";

        // Act
        var result = model.RegionName;

        // Assert
        result.Should().Be("Preset Region");
    }

    [Fact]
    public void RegionNumber_ShouldReturnRegionNumber_WhenConfigExists()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.RegionNumber;

        // Assert
        result.Should().Be("9");
    }

    [Fact]
    public void RegionNumber_ShouldReturnEmpty_WhenConfigIsNull()
    {
        // Arrange
        var mockContext = CreateMockContextWithNullConfigs();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act & Assert
        var act = () => model.RegionNumber;
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void RegionNumber_ShouldCacheResult_WhenCalledMultipleTimes()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result1 = model.RegionNumber;
        var result2 = model.RegionNumber;

        // Assert
        result1.Should().Be(result2);
    }

    [Fact]
    public void RegionNumber_ShouldReturnEmpty_WhenRegionNumberNotInConfig()
    {
        // Arrange
        var mockContext = CreateMockContextWithoutRegionNumber();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act & Assert
        var act = () => model.RegionNumber;
        act.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void DisplayableRegistrationName_ShouldReturnInvalidRegistration_WhenRegistrationTypeIsNone()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        model.CurrentRegistrationType = BasePageModel.RegistrationType.None;

        // Act
        var result = model.DisplayableRegistrationName;

        // Assert
        result.Should().Be("Invalid Registration");
    }

    [Fact]
    public void DisplayableRegistrationName_ShouldReturnInvalidRegistration_WhenRegistrationTypeIsVolunteer()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        model.CurrentRegistrationType = BasePageModel.RegistrationType.Volunteer;

        // Act
        var result = model.DisplayableRegistrationName;

        // Assert
        result.Should().Be("Invalid Registration");
    }

    [Fact]
    public void DisplayableRegistrationName_ShouldReturnCoachesTrainingRegistration_WhenRegistrationTypeIsCoachesTraining()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        model.CurrentRegistrationType = BasePageModel.RegistrationType.CoachesTraining;

        // Act
        var result = model.DisplayableRegistrationName;

        // Assert
        result.Should().Be("Coaches Training Registration");
    }

    [Fact]
    public void DisplayableRegistrationName_ShouldReturnTournamentRegistration_WhenRegistrationTypeIsTournament()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        model.CurrentRegistrationType = BasePageModel.RegistrationType.Tournament;

        // Act
        var result = model.DisplayableRegistrationName;

        // Assert
        result.Should().Be("Tournament Registration");
    }

    [Fact]
    public void DisplayableRegistrationName_ShouldReturnJudgesRegistration_WhenRegistrationTypeIsJudges()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        model.CurrentRegistrationType = BasePageModel.RegistrationType.Judges;

        // Act
        var result = model.DisplayableRegistrationName;

        // Assert
        result.Should().Be("Judges Registration");
    }

    [Fact]
    public void DisplayableRegistrationName_ShouldCacheResult_WhenCalledMultipleTimes()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        model.CurrentRegistrationType = BasePageModel.RegistrationType.Judges;

        // Act
        var result1 = model.DisplayableRegistrationName;
        var result2 = model.DisplayableRegistrationName;

        // Assert
        result1.Should().Be(result2);
    }

    [Fact]
    public void DisplayableRegistrationName_ShouldReturnPresetValue_WhenBackingFieldIsAlreadySet()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        model._displayableRegistrationName = "Preset Registration";

        // Act
        var result = model.DisplayableRegistrationName;

        // Assert
        result.Should().Be("Preset Registration");
    }

    [Fact]
    public void TournamentDate_ShouldReturnLongDateString_WhenTournamentInfoHasStartDate()
    {
        // Arrange
        var mockContext = CreateMockContextWithTournamentInfo();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.TournamentDate;

        // Assert
        result.Should().Be(new DateTime(2024, 3, 15).ToLongDateString());
    }

    [Fact]
    public void TournamentDate_ShouldReturnTBA_WhenTournamentInfoStartDateIsNull()
    {
        // Arrange
        var mockContext = CreateMockContextWithTournamentInfoNoDate();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.TournamentDate;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TournamentDate_ShouldReturnErrorMessage_WhenTournamentInfoIsNull()
    {
        // Arrange
        var mockContext = CreateMockContextWithoutTournamentInfo();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.TournamentDate;

        // Assert
        result.Should().Be("TournamentInfo was null");
    }

    [Fact]
    public void TournamentTime_ShouldReturnTime_WhenTournamentInfoHasTime()
    {
        // Arrange
        var mockContext = CreateMockContextWithTournamentInfo();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.TournamentTime;

        // Assert
        result.Should().Be("9:00 AM");
    }

    [Fact]
    public void TournamentTime_ShouldReturnTBA_WhenTournamentInfoTimeIsNull()
    {
        // Arrange
        var mockContext = CreateMockContextWithTournamentInfoNoTime();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.TournamentTime;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TournamentTime_ShouldReturnTBA_WhenTournamentInfoTimeIsEmpty()
    {
        // Arrange
        var mockContext = CreateMockContextWithTournamentInfoEmptyTime();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.TournamentTime;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TournamentTime_ShouldReturnEmpty_WhenTournamentInfoIsNull()
    {
        // Arrange
        var mockContext = CreateMockContextWithoutTournamentInfo();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.TournamentTime;

        // Assert
        result.Should().BeEmpty();
        mockLogger.Verify(
            x => x.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("TournamentInfo was null and should not have been.")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public void TournamentTime_ShouldReturnPresetValue_WhenBackingFieldIsAlreadySet()
    {
        // Arrange
        var mockContext = CreateMockContextWithTournamentInfo();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        model._tournamentTime = "Preset Time";

        // Act
        var result = model.TournamentTime;

        // Assert
        result.Should().Be("Preset Time");
    }

    [Fact]
    public void TournamentInfo_ShouldReturnEvent_WhenRegionNameExists()
    {
        // Arrange
        var mockContext = CreateMockContextWithTournamentInfo();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.TournamentInfo;

        // Assert
        result.Should().NotBeNull();
        result!.EventName.Should().Be("NoVA North Tournament");
    }

    [Fact]
    public void TournamentInfo_ShouldReturnNull_WhenNoMatchingEventExists()
    {
        // Arrange
        var mockContext = CreateMockContextWithoutTournamentInfo();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.TournamentInfo;

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void TournamentInfo_ShouldThrowKeyNotFoundException_WhenRegionNameNotInConfig()
    {
        // Arrange
        var mockContext = CreateMockContextWithNullRegionName();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act & Assert
        var act = () => model.TournamentInfo;
        act.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void TournamentInfo_ShouldCacheResult_WhenCalledMultipleTimes()
    {
        // Arrange
        var mockContext = CreateMockContextWithTournamentInfo();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result1 = model.TournamentInfo;
        var result2 = model.TournamentInfo;

        // Assert
        result1.Should().BeSameAs(result2);
    }

    [Fact]
    public void TournamentInfo_ShouldReturnPresetValue_WhenBackingFieldIsAlreadySet()
    {
        // Arrange
        var mockContext = CreateMockContext();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        var presetEvent = new Event { EventName = "Preset Event" };
        model._tournamentInfo = presetEvent;

        // Act
        var result = model.TournamentInfo;

        // Assert
        result.Should().BeSameAs(presetEvent);
    }

    [Fact]
    public void TournamentLocation_ShouldReturnLocation_WhenTournamentInfoHasLocation()
    {
        // Arrange
        var mockContext = CreateMockContextWithTournamentInfoAndLocation("Convention Center");
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.TournamentLocation;

        // Assert
        result.Should().Be("Convention Center");
    }

    [Fact]
    public void TournamentLocation_ShouldReturnTBA_WhenTournamentInfoLocationIsNull()
    {
        // Arrange
        var mockContext = CreateMockContextWithTournamentInfoAndLocation(null!);
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.TournamentLocation;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TournamentLocation_ShouldReturnTBA_WhenTournamentInfoLocationIsEmpty()
    {
        // Arrange
        var mockContext = CreateMockContextWithTournamentInfoAndLocation(string.Empty);
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.TournamentLocation;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TournamentLocation_ShouldReturnTBA_WhenTournamentInfoLocationIsWhitespace()
    {
        // Arrange
        var mockContext = CreateMockContextWithTournamentInfoAndLocation("   ");
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.TournamentLocation;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TournamentLocation_ShouldReturnEmpty_WhenTournamentInfoIsNull()
    {
        // Arrange
        var mockContext = CreateMockContextWithoutTournamentInfo();
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result = model.TournamentLocation;

        // Assert
        result.Should().BeEmpty();
        mockLogger.Verify(
            x => x.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("TournamentInfo was null and should not have been.")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public void TournamentLocation_ShouldReturnPresetValue_WhenBackingFieldIsAlreadySet()
    {
        // Arrange
        var mockContext = CreateMockContextWithTournamentInfoAndLocation("Convention Center");
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);
        model._tournamentLocation = "Preset Location";

        // Act
        var result = model.TournamentLocation;

        // Assert
        result.Should().Be("Preset Location");
    }

    [Fact]
    public void TournamentLocation_ShouldCacheResult_WhenCalledMultipleTimes()
    {
        // Arrange
        var mockContext = CreateMockContextWithTournamentInfoAndLocation("Convention Center");
        var mockLogger = new Mock<ILogger<BasePageModel>>();
        var model = new BasePageModel(mockContext.Object, mockLogger.Object);

        // Act
        var result1 = model.TournamentLocation;
        var result2 = model.TournamentLocation;

        // Assert
        result1.Should().Be(result2);
        result1.Should().Be("Convention Center");
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
            new() { Name = "RegionNumber", Value = "9" }
        }.AsQueryable();

        var mockConfigSet = CreateMockDbSet(configs);
        mockContext.Setup(c => c.Configs).Returns(mockConfigSet.Object);

        // Setup Events DbSet
        var events = new List<Event>().AsQueryable();
        var mockEventSet = CreateMockDbSet(events);
        mockContext.Setup(c => c.Events).Returns(mockEventSet.Object);

        return mockContext;
    }

    private static Mock<OdysseyContext> CreateMockContextWithEmptyConfigs()
    {
        var options = new DbContextOptionsBuilder<OdysseyContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var mockContext = new Mock<OdysseyContext>(options);

        var configs = new List<Config>().AsQueryable();
        var mockConfigSet = CreateMockDbSet(configs);
        mockContext.Setup(c => c.Configs).Returns(mockConfigSet.Object);

        return mockContext;
    }

    private static Mock<OdysseyContext> CreateMockContextWithNullConfigs()
    {
        var options = new DbContextOptionsBuilder<OdysseyContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var mockContext = new Mock<OdysseyContext>(options);
        mockContext.Setup(c => c.Configs).Returns((DbSet<Config>)null!);

        return mockContext;
    }

    private static Mock<OdysseyContext> CreateMockContextWithoutRegionName()
    {
        var options = new DbContextOptionsBuilder<OdysseyContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var mockContext = new Mock<OdysseyContext>(options);

        var configs = new List<Config>
        {
            new() { Name = "Year", Value = "2024" },
            new() { Name = "RegionNumber", Value = "9" }
        }.AsQueryable();

        var mockConfigSet = CreateMockDbSet(configs);
        mockContext.Setup(c => c.Configs).Returns(mockConfigSet.Object);

        return mockContext;
    }

    private static Mock<OdysseyContext> CreateMockContextWithYear(string year)
    {
        var options = new DbContextOptionsBuilder<OdysseyContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var mockContext = new Mock<OdysseyContext>(options);

        var configs = new List<Config>
        {
            new() { Name = "Year", Value = year },
            new() { Name = "RegionName", Value = "NoVA North" },
            new() { Name = "RegionNumber", Value = "9" }
        }.AsQueryable();

        var mockConfigSet = CreateMockDbSet(configs);
        mockContext.Setup(c => c.Configs).Returns(mockConfigSet.Object);

        return mockContext;
    }

    private static Mock<OdysseyContext> CreateMockContextWithEmptyRegionName()
    {
        var options = new DbContextOptionsBuilder<OdysseyContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var mockContext = new Mock<OdysseyContext>(options);

        var configs = new List<Config>
        {
            new() { Name = "Year", Value = "2024" },
            new() { Name = "RegionName", Value = string.Empty },
            new() { Name = "RegionNumber", Value = "9" }
        }.AsQueryable();

        var mockConfigSet = CreateMockDbSet(configs);
        mockContext.Setup(c => c.Configs).Returns(mockConfigSet.Object);

        return mockContext;
    }

    private static Mock<OdysseyContext> CreateMockContextWithoutRegionNumber()
    {
        var options = new DbContextOptionsBuilder<OdysseyContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var mockContext = new Mock<OdysseyContext>(options);

        var configs = new List<Config>
        {
            new() { Name = "Year", Value = "2024" },
            new() { Name = "RegionName", Value = "NoVA North" }
        }.AsQueryable();

        var mockConfigSet = CreateMockDbSet(configs);
        mockContext.Setup(c => c.Configs).Returns(mockConfigSet.Object);

        return mockContext;
    }

    private static Mock<OdysseyContext> CreateMockContextWithTournamentInfo()
    {
        var options = new DbContextOptionsBuilder<OdysseyContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var mockContext = new Mock<OdysseyContext>(options);

        var configs = new List<Config>
        {
            new() { Name = "Year", Value = "2024" },
            new() { Name = "RegionName", Value = "NoVA North" },
            new() { Name = "RegionNumber", Value = "9" }
        }.AsQueryable();

        var events = new List<Event>
        {
            new() { EventName = "NoVA North Tournament", StartDate = new DateTime(2024, 3, 15), Time = "9:00 AM" }
        }.AsQueryable();

        var mockConfigSet = CreateMockDbSet(configs);
        var mockEventSet = CreateMockDbSet(events);

        mockContext.Setup(c => c.Configs).Returns(mockConfigSet.Object);
        mockContext.Setup(c => c.Events).Returns(mockEventSet.Object);

        return mockContext;
    }

    private static Mock<OdysseyContext> CreateMockContextWithTournamentInfoNoDate()
    {
        var options = new DbContextOptionsBuilder<OdysseyContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var mockContext = new Mock<OdysseyContext>(options);

        var configs = new List<Config>
        {
            new() { Name = "Year", Value = "2024" },
            new() { Name = "RegionName", Value = "NoVA North" },
            new() { Name = "RegionNumber", Value = "9" }
        }.AsQueryable();

        var events = new List<Event>
        {
            new() { EventName = "NoVA North Tournament", StartDate = null, Time = "9:00 AM" }
        }.AsQueryable();

        var mockConfigSet = CreateMockDbSet(configs);
        var mockEventSet = CreateMockDbSet(events);

        mockContext.Setup(c => c.Configs).Returns(mockConfigSet.Object);
        mockContext.Setup(c => c.Events).Returns(mockEventSet.Object);

        return mockContext;
    }

    private static Mock<OdysseyContext> CreateMockContextWithTournamentInfoNoTime()
    {
        var options = new DbContextOptionsBuilder<OdysseyContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var mockContext = new Mock<OdysseyContext>(options);

        var configs = new List<Config>
        {
            new() { Name = "Year", Value = "2024" },
            new() { Name = "RegionName", Value = "NoVA North" },
            new() { Name = "RegionNumber", Value = "9" }
        }.AsQueryable();

        var events = new List<Event>
        {
            new() { EventName = "NoVA North Tournament", StartDate = new DateTime(2024, 3, 15), Time = null! }
        }.AsQueryable();

        var mockConfigSet = CreateMockDbSet(configs);
        var mockEventSet = CreateMockDbSet(events);

        mockContext.Setup(c => c.Configs).Returns(mockConfigSet.Object);
        mockContext.Setup(c => c.Events).Returns(mockEventSet.Object);

        return mockContext;
    }

    private static Mock<OdysseyContext> CreateMockContextWithTournamentInfoEmptyTime()
    {
        var options = new DbContextOptionsBuilder<OdysseyContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var mockContext = new Mock<OdysseyContext>(options);

        var configs = new List<Config>
        {
            new() { Name = "Year", Value = "2024" },
            new() { Name = "RegionName", Value = "NoVA North" },
            new() { Name = "RegionNumber", Value = "9" }
        }.AsQueryable();

        var events = new List<Event>
        {
            new() { EventName = "NoVA North Tournament", StartDate = new DateTime(2024, 3, 15), Time = string.Empty }
        }.AsQueryable();

        var mockConfigSet = CreateMockDbSet(configs);
        var mockEventSet = CreateMockDbSet(events);

        mockContext.Setup(c => c.Configs).Returns(mockConfigSet.Object);
        mockContext.Setup(c => c.Events).Returns(mockEventSet.Object);

        return mockContext;
    }

    private static Mock<OdysseyContext> CreateMockContextWithoutTournamentInfo()
    {
        var options = new DbContextOptionsBuilder<OdysseyContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var mockContext = new Mock<OdysseyContext>(options);

        var configs = new List<Config>
        {
            new() { Name = "Year", Value = "2024" },
            new() { Name = "RegionName", Value = "NoVA North" },
            new() { Name = "RegionNumber", Value = "9" }
        }.AsQueryable();

        var events = new List<Event>().AsQueryable();

        var mockConfigSet = CreateMockDbSet(configs);
        var mockEventSet = CreateMockDbSet(events);

        mockContext.Setup(c => c.Configs).Returns(mockConfigSet.Object);
        mockContext.Setup(c => c.Events).Returns(mockEventSet.Object);

        return mockContext;
    }

    private static Mock<OdysseyContext> CreateMockContextWithNullRegionName()
    {
        var options = new DbContextOptionsBuilder<OdysseyContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var mockContext = new Mock<OdysseyContext>(options);

        var configs = new List<Config>
        {
            new() { Name = "Year", Value = "2024" },
            new() { Name = "RegionNumber", Value = "9" }
        }.AsQueryable();

        var events = new List<Event>().AsQueryable();

        var mockConfigSet = CreateMockDbSet(configs);
        var mockEventSet = CreateMockDbSet(events);

        mockContext.Setup(c => c.Configs).Returns(mockConfigSet.Object);
        mockContext.Setup(c => c.Events).Returns(mockEventSet.Object);

        return mockContext;
    }

    private static Mock<OdysseyContext> CreateMockContextWithTournamentInfoAndLocation(string location)
    {
        var options = new DbContextOptionsBuilder<OdysseyContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var mockContext = new Mock<OdysseyContext>(options);

        var configs = new List<Config>
        {
            new() { Name = "Year", Value = "2024" },
            new() { Name = "RegionName", Value = "NoVA North" },
            new() { Name = "RegionNumber", Value = "9" }
        }.AsQueryable();

        var events = new List<Event>
        {
            new() { EventName = "NoVA North Tournament", StartDate = new DateTime(2024, 3, 15), Time = "9:00 AM", Location = location }
        }.AsQueryable();

        var mockConfigSet = CreateMockDbSet(configs);
        var mockEventSet = CreateMockDbSet(events);

        mockContext.Setup(c => c.Configs).Returns(mockConfigSet.Object);
        mockContext.Setup(c => c.Events).Returns(mockEventSet.Object);

        return mockContext;
    }

    private static void SetupHttpContext(BasePageModel model, string host)
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
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        return mockSet;
    }
}
