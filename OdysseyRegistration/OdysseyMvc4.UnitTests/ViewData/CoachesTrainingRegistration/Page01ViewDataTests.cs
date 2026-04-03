using FluentAssertions;
using OdysseyMvc4.Models;
using OdysseyMvc4.ViewData.CoachesTrainingRegistration;

namespace OdysseyMvc4.UnitTests.ViewData.CoachesTrainingRegistration;

/// <summary>
/// Tests for Page01ViewData computed properties.
/// </summary>
public class Page01ViewDataTests
{
    #region CoordinatorsDoNotPayCoachesTrainingRegistrationFeeMessage Tests

    [Fact]
    public void CoordinatorsDoNotPayCoachesTrainingRegistrationFeeMessage_WithTrueValue_ReturnsMessage()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>
            {
                { "CoordinatorsDoNotPayCoachesTrainingRegistrationFee", "true" }
            }
        };

        // Act
        var result = viewData.CoordinatorsDoNotPayCoachesTrainingRegistrationFeeMessage;

        // Assert
        result.Should().Be(" &nbsp;We invite School Coordinators to attend at no charge.");
    }

    [Fact]
    public void CoordinatorsDoNotPayCoachesTrainingRegistrationFeeMessage_WithTrueUppercase_ReturnsMessage()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>
            {
                { "CoordinatorsDoNotPayCoachesTrainingRegistrationFee", "TRUE" }
            }
        };

        // Act
        var result = viewData.CoordinatorsDoNotPayCoachesTrainingRegistrationFeeMessage;

        // Assert
        result.Should().Be(" &nbsp;We invite School Coordinators to attend at no charge.");
    }

    [Fact]
    public void CoordinatorsDoNotPayCoachesTrainingRegistrationFeeMessage_WithTrueMixedCase_ReturnsMessage()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>
            {
                { "CoordinatorsDoNotPayCoachesTrainingRegistrationFee", "TrUe" }
            }
        };

        // Act
        var result = viewData.CoordinatorsDoNotPayCoachesTrainingRegistrationFeeMessage;

        // Assert
        result.Should().Be(" &nbsp;We invite School Coordinators to attend at no charge.");
    }

    [Fact]
    public void CoordinatorsDoNotPayCoachesTrainingRegistrationFeeMessage_WithFalseValue_ReturnsEmpty()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>
            {
                { "CoordinatorsDoNotPayCoachesTrainingRegistrationFee", "false" }
            }
        };

        // Act
        var result = viewData.CoordinatorsDoNotPayCoachesTrainingRegistrationFeeMessage;

        // Assert
        result.Should().Be(string.Empty);
    }

    [Fact]
    public void CoordinatorsDoNotPayCoachesTrainingRegistrationFeeMessage_WithInvalidValue_ReturnsEmpty()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>
            {
                { "CoordinatorsDoNotPayCoachesTrainingRegistrationFee", "invalid" }
            }
        };

        // Act
        var result = viewData.CoordinatorsDoNotPayCoachesTrainingRegistrationFeeMessage;

        // Assert
        result.Should().Be(string.Empty);
    }

    [Fact]
    public void CoordinatorsDoNotPayCoachesTrainingRegistrationFeeMessage_WithNullValue_ReturnsEmpty()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>
            {
                { "CoordinatorsDoNotPayCoachesTrainingRegistrationFee", null! }
            }
        };

        // Act
        Action act = () => _ = viewData.CoordinatorsDoNotPayCoachesTrainingRegistrationFeeMessage;

        // Assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void CoordinatorsDoNotPayCoachesTrainingRegistrationFeeMessage_WithMissingKey_ReturnsEmpty()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>()
        };

        // Act
        var result = viewData.CoordinatorsDoNotPayCoachesTrainingRegistrationFeeMessage;

        // Assert
        result.Should().Be(string.Empty);
    }

    #endregion

    #region CoachesTrainingDate Tests

    [Fact]
    public void CoachesTrainingDate_WithValidDate_ReturnsLongDateString()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            CoachesTrainingInfo = new Event { StartDate = new DateTime(2025, 3, 15) }
        };

        // Act
        var result = viewData.CoachesTrainingDate;

        // Assert
        result.Should().Be(new DateTime(2025, 3, 15).ToLongDateString());
    }

    [Fact]
    public void CoachesTrainingDate_WithNullDate_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            CoachesTrainingInfo = new Event { StartDate = null }
        };

        // Act
        var result = viewData.CoachesTrainingDate;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void CoachesTrainingDate_WithNullCoachesTrainingInfo_ThrowsNullReferenceException()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            CoachesTrainingInfo = null
        };

        // Act
        Action act = () => _ = viewData.CoachesTrainingDate;

        // Assert
        act.Should().Throw<NullReferenceException>();
    }

    #endregion

    #region CoachesTrainingTime Tests

    [Fact]
    public void CoachesTrainingTime_WithValidTime_ReturnsTime()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            CoachesTrainingInfo = new Event { Time = "9:00 AM - 5:00 PM" }
        };

        // Act
        var result = viewData.CoachesTrainingTime;

        // Assert
        result.Should().Be("9:00 AM - 5:00 PM");
    }

    [Fact]
    public void CoachesTrainingTime_WithNullTime_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            CoachesTrainingInfo = new Event { Time = null }
        };

        // Act
        var result = viewData.CoachesTrainingTime;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void CoachesTrainingTime_WithEmptyTime_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            CoachesTrainingInfo = new Event { Time = string.Empty }
        };

        // Act
        var result = viewData.CoachesTrainingTime;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void CoachesTrainingTime_WithWhitespaceTime_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            CoachesTrainingInfo = new Event { Time = "   " }
        };

        // Act
        var result = viewData.CoachesTrainingTime;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void CoachesTrainingTime_WithNullCoachesTrainingInfo_ThrowsNullReferenceException()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            CoachesTrainingInfo = null
        };

        // Act
        Action act = () => _ = viewData.CoachesTrainingTime;

        // Assert
        act.Should().Throw<NullReferenceException>();
    }

    #endregion
}
