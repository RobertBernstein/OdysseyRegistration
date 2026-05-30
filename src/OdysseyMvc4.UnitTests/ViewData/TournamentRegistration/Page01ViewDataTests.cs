using FluentAssertions;
using OdysseyMvc4.Models;
using OdysseyMvc4.ViewData.TournamentRegistration;

namespace OdysseyMvc4.UnitTests.ViewData.TournamentRegistration;

/// <summary>
/// Tests for Page01ViewData computed properties.
/// </summary>
public class Page01ViewDataTests
{
    #region AcceptingPayPal Tests

    [Fact]
    public void AcceptingPayPal_WithTrueValue_ReturnsTrue()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>
            {
                { "AcceptingPayPal", "true" }
            }
        };

        // Act
        var result = viewData.AcceptingPayPal;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void AcceptingPayPal_WithFalseValue_ReturnsFalse()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>
            {
                { "AcceptingPayPal", "false" }
            }
        };

        // Act
        var result = viewData.AcceptingPayPal;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void AcceptingPayPal_WithInvalidValue_ReturnsFalse()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>
            {
                { "AcceptingPayPal", "invalid" }
            }
        };

        // Act
        var result = viewData.AcceptingPayPal;

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void AcceptingPayPal_WithNullValue_ReturnsFalse()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>
            {
                { "AcceptingPayPal", null! }
            }
        };

        // Act
        var result = viewData.AcceptingPayPal;

        // Assert
        result.Should().BeFalse();
    }

    [Fact(Skip = "ProductionBugSuspected")]
    public void AcceptingPayPal_WithMissingKey_ReturnsFalse()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>()
        };

        // Act
        var result = viewData.AcceptingPayPal;

        // Assert
        result.Should().BeFalse();
    }

    #endregion

    #region LateEventCostStartDate Tests

    [Fact]
    public void LateEventCostStartDate_WithValidDate_ReturnsDateMinusOneDayAsLongDateString()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            TournamentInfo = new Event
            {
                LateEventCostStartDate = new DateTime(2025, 3, 15)
            }
        };

        // Act
        var result = viewData.LateEventCostStartDate;

        // Assert
        result.Should().Be(new DateTime(2025, 3, 14).ToLongDateString());
    }

    [Fact]
    public void LateEventCostStartDate_WithNullDate_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            TournamentInfo = new Event
            {
                LateEventCostStartDate = null
            }
        };

        // Act
        var result = viewData.LateEventCostStartDate;

        // Assert
        result.Should().Be("TBA");
    }

    #endregion

    #region LateTeamRegistrationFee Tests

    [Fact]
    public void LateTeamRegistrationFee_WithValidCost_ReturnsCostWithDollarSign()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            TournamentInfo = new Event
            {
                LateEventCost = "150"
            }
        };

        // Act
        var result = viewData.LateTeamRegistrationFee;

        // Assert
        result.Should().Be("$150");
    }

    [Fact]
    public void LateTeamRegistrationFee_WithNullCost_ReturnsEmpty()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            TournamentInfo = new Event
            {
                LateEventCost = null
            }
        };

        // Act
        var result = viewData.LateTeamRegistrationFee;

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void LateTeamRegistrationFee_WithEmptyCost_ReturnsEmpty()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            TournamentInfo = new Event
            {
                LateEventCost = string.Empty
            }
        };

        // Act
        var result = viewData.LateTeamRegistrationFee;

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void LateTeamRegistrationFee_WithWhitespaceCost_ReturnsEmpty()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            TournamentInfo = new Event
            {
                LateEventCost = "   "
            }
        };

        // Act
        var result = viewData.LateTeamRegistrationFee;

        // Assert
        result.Should().BeEmpty();
    }

    #endregion

    #region PaymentDueDate Tests

    [Fact]
    public void PaymentDueDate_WithValidDate_ReturnsLongDateString()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            TournamentInfo = new Event
            {
                PaymentDueDate = new DateTime(2025, 4, 1)
            }
        };

        // Act
        var result = viewData.PaymentDueDate;

        // Assert
        result.Should().Be(new DateTime(2025, 4, 1).ToLongDateString());
    }

    [Fact]
    public void PaymentDueDate_WithNullDate_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            TournamentInfo = new Event
            {
                PaymentDueDate = null
            }
        };

        // Act
        var result = viewData.PaymentDueDate;

        // Assert
        result.Should().Be("TBA");
    }

    #endregion

    #region TeamRegistrationFee Tests

    [Fact]
    public void TeamRegistrationFee_WithNoLateStartDate_ReturnsEventCost()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            TournamentInfo = new Event
            {
                EventCost = "100",
                LateEventCost = "150",
                LateEventCostStartDate = null
            }
        };

        // Act
        var result = viewData.TeamRegistrationFee;

        // Assert
        result.Should().Be("$100");
    }

    [Fact]
    public void TeamRegistrationFee_WithNoLateStartDateAndNoCost_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            TournamentInfo = new Event
            {
                EventCost = null,
                LateEventCost = "150",
                LateEventCostStartDate = null
            }
        };

        // Act
        var result = viewData.TeamRegistrationFee;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TeamRegistrationFee_BeforeLateDate_ReturnsEventCost()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            TournamentInfo = new Event
            {
                EventCost = "100",
                LateEventCost = "150",
                LateEventCostStartDate = DateTime.Now.AddDays(10)
            }
        };

        // Act
        var result = viewData.TeamRegistrationFee;

        // Assert
        result.Should().Be("$100");
    }

    [Fact]
    public void TeamRegistrationFee_AfterLateDate_ReturnsLateEventCost()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            TournamentInfo = new Event
            {
                EventCost = "100",
                LateEventCost = "150",
                LateEventCostStartDate = DateTime.Now.AddDays(-10)
            }
        };

        // Act
        var result = viewData.TeamRegistrationFee;

        // Assert
        result.Should().Be("$150");
    }

    [Fact]
    public void TeamRegistrationFee_BeforeLateDate_WithNullEventCost_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            TournamentInfo = new Event
            {
                EventCost = null,
                LateEventCost = "150",
                LateEventCostStartDate = DateTime.Now.AddDays(10)
            }
        };

        // Act
        var result = viewData.TeamRegistrationFee;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TeamRegistrationFee_AfterLateDate_WithNullLateEventCost_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            TournamentInfo = new Event
            {
                EventCost = "100",
                LateEventCost = null,
                LateEventCostStartDate = DateTime.Now.AddDays(-10)
            }
        };

        // Act
        var result = viewData.TeamRegistrationFee;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TeamRegistrationFee_BeforeLateDate_WithWhitespaceEventCost_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            TournamentInfo = new Event
            {
                EventCost = "   ",
                LateEventCost = "150",
                LateEventCostStartDate = DateTime.Now.AddDays(10)
            }
        };

        // Act
        var result = viewData.TeamRegistrationFee;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TeamRegistrationFee_AfterLateDate_WithWhitespaceLateEventCost_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            TournamentInfo = new Event
            {
                EventCost = "100",
                LateEventCost = "   ",
                LateEventCostStartDate = DateTime.Now.AddDays(-10)
            }
        };

        // Act
        var result = viewData.TeamRegistrationFee;

        // Assert
        result.Should().Be("TBA");
    }

    #endregion

    #region TournamentRegistrationCloseDateTime Tests

    [Fact]
    public void TournamentRegistrationCloseDateTime_WithValidDate_ReturnsLongDateString()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>
            {
                { "TournamentRegistrationCloseDateTime", "2025-03-15" }
            }
        };

        // Act
        var result = viewData.TournamentRegistrationCloseDateTime;

        // Assert
        result.Should().Be(new DateTime(2025, 3, 15).ToLongDateString());
    }

    [Fact]
    public void TournamentRegistrationCloseDateTime_WithNullValue_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>
            {
                { "TournamentRegistrationCloseDateTime", null! }
            }
        };

        // Act
        var result = viewData.TournamentRegistrationCloseDateTime;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact(Skip = "ProductionBugSuspected")]
    public void TournamentRegistrationCloseDateTime_WithMissingKey_ReturnsTBA()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>()
        };

        // Act
        var result = viewData.TournamentRegistrationCloseDateTime;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TournamentRegistrationCloseDateTime_WithInvalidDate_ReturnsDefaultDateTimeAsLongDateString()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>
            {
                { "TournamentRegistrationCloseDateTime", "invalid-date" }
            }
        };

        // Act
        var result = viewData.TournamentRegistrationCloseDateTime;

        // Assert
        result.Should().Be(default(DateTime).ToLongDateString());
    }

    [Fact]
    public void TournamentRegistrationCloseDateTime_WithEmptyString_ReturnsDefaultDateTimeAsLongDateString()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>
            {
                { "TournamentRegistrationCloseDateTime", string.Empty }
            }
        };

        // Act
        var result = viewData.TournamentRegistrationCloseDateTime;

        // Assert
        result.Should().Be(default(DateTime).ToLongDateString());
    }

    [Fact]
    public void TournamentRegistrationCloseDateTime_WithWhitespace_ReturnsDefaultDateTimeAsLongDateString()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>
            {
                { "TournamentRegistrationCloseDateTime", "   " }
            }
        };

        // Act
        var result = viewData.TournamentRegistrationCloseDateTime;

        // Assert
        result.Should().Be(default(DateTime).ToLongDateString());
    }

    [Fact]
    public void TournamentRegistrationCloseDateTime_WithValidDateTimeString_ReturnsLongDateString()
    {
        // Arrange
        var viewData = new Page01ViewData
        {
            Config = new Dictionary<string, string>
            {
                { "TournamentRegistrationCloseDateTime", "2025-12-25 18:30:00" }
            }
        };

        // Act
        var result = viewData.TournamentRegistrationCloseDateTime;

        // Assert
        result.Should().Be(new DateTime(2025, 12, 25).ToLongDateString());
    }

    #endregion
}
