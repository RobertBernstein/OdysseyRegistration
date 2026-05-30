using FluentAssertions;
using OdysseyMvc4.Models;
using OdysseyMvc4.ViewData.JudgesRegistration;

namespace OdysseyMvc4.UnitTests.ViewData.JudgesRegistration;

/// <summary>
/// Tests for Page01ViewData computed properties.
/// </summary>
public class Page01ViewDataTests
{
    #region JudgesTrainingDate Tests

    [Fact]
    public void JudgesTrainingDate_WithValidDate_ReturnsLongDateString()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            JudgesInfo = new Event { StartDate = new DateTime(2025, 3, 15) }
        };

        // Act
        var result = viewData.JudgesTrainingDate;

        // Assert
        result.Should().Be(new DateTime(2025, 3, 15).ToLongDateString());
    }

    [Fact]
    public void JudgesTrainingDate_WithNullDate_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            JudgesInfo = new Event { StartDate = null }
        };

        // Act
        var result = viewData.JudgesTrainingDate;

        // Assert
        result.Should().Be("TBA");
    }

    #endregion

    #region JudgesTrainingLocation Tests

    [Fact]
    public void JudgesTrainingLocation_WithValidLocation_ReturnsLocation()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            JudgesInfo = new Event { Location = "Community Center" }
        };

        // Act
        var result = viewData.JudgesTrainingLocation;

        // Assert
        result.Should().Be("Community Center");
    }

    [Fact]
    public void JudgesTrainingLocation_WithNullLocation_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            JudgesInfo = new Event { Location = null }
        };

        // Act
        var result = viewData.JudgesTrainingLocation;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void JudgesTrainingLocation_WithEmptyLocation_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            JudgesInfo = new Event { Location = string.Empty }
        };

        // Act
        var result = viewData.JudgesTrainingLocation;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void JudgesTrainingLocation_WithWhitespaceLocation_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            JudgesInfo = new Event { Location = "   " }
        };

        // Act
        var result = viewData.JudgesTrainingLocation;

        // Assert
        result.Should().Be("TBA");
    }

    #endregion

    #region JudgesTrainingTime Tests

    [Fact]
    public void JudgesTrainingTime_WithValidTime_ReturnsTime()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            JudgesInfo = new Event { Time = "9:00 AM - 5:00 PM" }
        };

        // Act
        var result = viewData.JudgesTrainingTime;

        // Assert
        result.Should().Be("9:00 AM - 5:00 PM");
    }

    [Fact]
    public void JudgesTrainingTime_WithNullTime_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            JudgesInfo = new Event { Time = null }
        };

        // Act
        var result = viewData.JudgesTrainingTime;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void JudgesTrainingTime_WithEmptyTime_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            JudgesInfo = new Event { Time = string.Empty }
        };

        // Act
        var result = viewData.JudgesTrainingTime;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void JudgesTrainingTime_WithWhitespaceTime_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            JudgesInfo = new Event { Time = "   " }
        };

        // Act
        var result = viewData.JudgesTrainingTime;

        // Assert
        result.Should().Be("TBA");
    }

    #endregion
}
