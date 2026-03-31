using FluentAssertions;
using OdysseyMvc4.Models;
using OdysseyMvc4.ViewData;

namespace OdysseyMvc4.Tests.Unit.ViewData;

/// <summary>
/// Tests for BaseViewData properties in OdysseyMvc4.
/// </summary>
public class BaseViewDataTests
{
    #region TournamentDate Tests

    [Fact]
    public void TournamentDate_WithValidDate_ReturnsLongDateString()
    {
        // Arrange
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { StartDate = new DateTime(2025, 3, 15) }
        };

        // Act
        var result = viewData.TournamentDate;

        // Assert
        result.Should().Be(new DateTime(2025, 3, 15).ToLongDateString());
    }

    [Fact]
    public void TournamentDate_WithNullDate_ReturnsTBA()
    {
        // Arrange
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { StartDate = null }
        };

        // Act
        var result = viewData.TournamentDate;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TournamentDate_WithNullTournamentInfo_ThrowsNullReferenceException()
    {
        // Arrange
        var viewData = new BaseViewData
        {
            TournamentInfo = null
        };

        // Act
        Action act = () => _ = viewData.TournamentDate;

        // Assert
        act.Should().Throw<NullReferenceException>();
    }

    #endregion

    #region TournamentLocation Tests

    [Fact]
    public void TournamentLocation_WithValidLocation_ReturnsLocation()
    {
        // Arrange
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { Location = "Springfield High School" }
        };

        // Act
        var result = viewData.TournamentLocation;

        // Assert
        result.Should().Be("Springfield High School");
    }

    [Fact]
    public void TournamentLocation_WithNullLocation_ReturnsTBA()
    {
        // Arrange
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { Location = null }
        };

        // Act
        var result = viewData.TournamentLocation;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TournamentLocation_WithEmptyLocation_ReturnsTBA()
    {
        // Arrange
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { Location = string.Empty }
        };

        // Act
        var result = viewData.TournamentLocation;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TournamentLocation_WithWhitespaceLocation_ReturnsTBA()
    {
        // Arrange
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { Location = "   " }
        };

        // Act
        var result = viewData.TournamentLocation;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TournamentLocation_WithNullTournamentInfo_ThrowsNullReferenceException()
    {
        // Arrange
        var viewData = new BaseViewData
        {
            TournamentInfo = null
        };

        // Act
        Action act = () => _ = viewData.TournamentLocation;

        // Assert
        act.Should().Throw<NullReferenceException>();
    }

    #endregion

    #region TournamentTime Tests

    [Fact]
    public void TournamentTime_WithValidTime_ReturnsTime()
    {
        // Arrange
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { Time = "8:00 AM" }
        };

        // Act
        var result = viewData.TournamentTime;

        // Assert
        result.Should().Be("8:00 AM");
    }

    [Fact]
    public void TournamentTime_WithNullTime_ReturnsTBA()
    {
        // Arrange
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { Time = null }
        };

        // Act
        var result = viewData.TournamentTime;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TournamentTime_WithEmptyTime_ReturnsTBA()
    {
        // Arrange
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { Time = string.Empty }
        };

        // Act
        var result = viewData.TournamentTime;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TournamentTime_WithWhitespaceTime_ReturnsTBA()
    {
        // Arrange
        var viewData = new BaseViewData
        {
            TournamentInfo = new Event { Time = "   " }
        };

        // Act
        var result = viewData.TournamentTime;

        // Assert
        result.Should().Be("TBA");
    }

    [Fact]
    public void TournamentTime_WithNullTournamentInfo_ThrowsNullReferenceException()
    {
        // Arrange
        var viewData = new BaseViewData
        {
            TournamentInfo = null
        };

        // Act
        Action act = () => _ = viewData.TournamentTime;

        // Assert
        act.Should().Throw<NullReferenceException>();
    }

    #endregion
}
