using FluentAssertions;
using OdysseyMvc4.Controllers;
using OdysseyMvc4.Models;
using OdysseyMvc4.ViewData.CoachesTrainingRegistration;

namespace OdysseyMvc4.Tests.Unit.Controllers;

/// <summary>
/// Tests for the CoachesTrainingRegistrationController business logic that must be preserved
/// during the migration from .NET Framework 4.8 to .NET 10.
/// </summary>
public class CoachesTrainingRegistrationControllerTests
{
    private readonly TestableCoachesTrainingRegistrationController _controller;

    public CoachesTrainingRegistrationControllerTests()
    {
        _controller = new TestableCoachesTrainingRegistrationController();
    }

    #region GenerateEmailBody Tests

    [Fact]
    public void GenerateEmailBody_AllFieldsPopulated_ReplacesAllPlaceholders()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("Region 9");
        result.Should().Contain("<a href=\"http://location.example.com\" target=\"_blank\">Test Location, 123 Main St, Springfield, VA</a>");
        result.Should().Contain("Saturday, March 15, 2025");
        result.Should().Contain("9:00 AM");
        result.Should().Contain("2025 - 2026");
        result.Should().Contain("<a href=\"http://example.com/guide\" target=\"_blank\">http://example.com/guide</a>");
        result.Should().Contain("$50");
        result.Should().Contain("NoVA North Odyssey");
        result.Should().Contain("<a href=\"mailto:director@novanorth.org\">director@novanorth.org</a>");
    }

    [Fact]
    public void GenerateEmailBody_LocationURLNull_LocationNotWrappedInAnchorTag()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.LocationURL = null;

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("Test Location, 123 Main St, Springfield, VA");
        result.Should().NotContain("<a href=\"http://location.example.com\"");
        result.Should().NotContain("Test Location, 123 Main St, Springfield, VA</a>");
    }

    [Fact]
    public void GenerateEmailBody_LocationURLEmpty_LocationNotWrappedInAnchorTag()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.LocationURL = "";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("Test Location, 123 Main St, Springfield, VA");
        result.Should().NotContain("<a href=\"\" target=\"_blank\">");
    }

    [Fact]
    public void GenerateEmailBody_LocationURLWhitespace_LocationNotWrappedInAnchorTag()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.LocationURL = "   ";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("Test Location, 123 Main St, Springfield, VA");
        result.Should().NotContain("<a href=\"   \" target=\"_blank\">");
    }

    [Fact]
    public void GenerateEmailBody_OnlyLocationProvided_BuildsLocationStringWithoutCommas()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.LocationURL = null;
        viewData.CoachesTrainingInfo.LocationAddress = null;
        viewData.CoachesTrainingInfo.LocationCity = null;
        viewData.CoachesTrainingInfo.LocationState = null;

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("Test Location");
        result.Should().NotContain("Test Location,");
    }

    [Fact]
    public void GenerateEmailBody_LocationWithAddress_IncludesAddress()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.LocationCity = null;
        viewData.CoachesTrainingInfo.LocationState = null;

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("Test Location, 123 Main St");
        result.Should().NotContain("Test Location, 123 Main St,");
    }

    [Fact]
    public void GenerateEmailBody_LocationWithAddressAndCity_IncludesBoth()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.LocationState = null;

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("Test Location, 123 Main St, Springfield");
        result.Should().NotContain("Test Location, 123 Main St, Springfield,");
    }

    [Fact]
    public void GenerateEmailBody_LocationAddressEmpty_SkipsAddress()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.LocationAddress = "";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("Test Location, Springfield, VA");
        result.Should().NotContain("Test Location, , Springfield");
    }

    [Fact]
    public void GenerateEmailBody_LocationCityWhitespace_SkipsCity()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.LocationCity = "   ";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("Test Location, 123 Main St, VA");
        result.Should().NotContain("Test Location, 123 Main St,   , VA");
    }

    [Fact]
    public void GenerateEmailBody_LocationStateNull_SkipsState()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.LocationState = null;

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("Test Location, 123 Main St, Springfield");
        result.Should().NotContain(", Springfield,");
    }

    [Fact]
    public void GenerateEmailBody_StartDateNull_ShowsTBA()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.StartDate = null;

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("TBA");
        result.Should().NotContain("Saturday, March 15, 2025");
    }

    [Fact]
    public void GenerateEmailBody_StartDateProvided_FormatsAsLongDateString()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.StartDate = new DateTime(2025, 12, 25);

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("Thursday, December 25, 2025");
    }

    [Fact]
    public void GenerateEmailBody_TimeNull_ShowsTBA()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.Time = null;

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("TBA");
    }

    [Fact]
    public void GenerateEmailBody_TimeEmpty_ShowsTBA()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.Time = "";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("TBA");
    }

    [Fact]
    public void GenerateEmailBody_TimeWhitespace_ShowsTBA()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.Time = "   ";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("TBA");
    }

    [Fact]
    public void GenerateEmailBody_TimeProvided_UsesProvidedTime()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.Time = "2:30 PM";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("2:30 PM");
    }

    [Fact]
    public void GenerateEmailBody_CoachesHandbookURLProvided_IncludesHandbookLink()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.Config["CoachesHandbookURL"] = "http://virginia.example.com/handbook";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("<li>\ta copy of the Coaches Handbook from the Virginia state website (<a href=\"http://virginia.example.com/handbook\" target=\"blank\">http://virginia.example.com/handbook</a>),</li>");
    }

    [Fact]
    public void GenerateEmailBody_CoachesHandbookURLEmpty_DoesNotIncludeHandbookLink()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.Config["CoachesHandbookURL"] = "";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().NotContain("Coaches Handbook from the Virginia state website");
    }

    [Fact]
    public void GenerateEmailBody_CoachesHandbookURLWhitespace_DoesNotIncludeHandbookLink()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.Config["CoachesHandbookURL"] = "   ";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().NotContain("Coaches Handbook from the Virginia state website");
    }

    [Fact]
    public void GenerateEmailBody_CoachesHandbookURLKeyMissing_DoesNotIncludeHandbookLink()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        // Key not added to Config dictionary

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().NotContain("Coaches Handbook from the Virginia state website");
    }

    [Fact]
    public void GenerateEmailBody_CoordinatorsDoNotPayTrue_IncludesMessage()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.Config["CoordinatorsDoNotPayCoachesTrainingRegistrationFee"] = "true";
        viewData.Config["SchoolCoordinatorsDoNotPayMessage"] = "School coordinators are exempt from paying the registration fee.";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("School coordinators are exempt from paying the registration fee.");
    }

    [Fact]
    public void GenerateEmailBody_CoordinatorsDoNotPayTrueUpperCase_IncludesMessage()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.Config["CoordinatorsDoNotPayCoachesTrainingRegistrationFee"] = "TRUE";
        viewData.Config["SchoolCoordinatorsDoNotPayMessage"] = "School coordinators are exempt from paying the registration fee.";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("School coordinators are exempt from paying the registration fee.");
    }

    [Fact]
    public void GenerateEmailBody_CoordinatorsDoNotPayTrueMixedCase_IncludesMessage()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.Config["CoordinatorsDoNotPayCoachesTrainingRegistrationFee"] = "TrUe";
        viewData.Config["SchoolCoordinatorsDoNotPayMessage"] = "School coordinators are exempt from paying the registration fee.";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("School coordinators are exempt from paying the registration fee.");
    }

    [Fact]
    public void GenerateEmailBody_CoordinatorsDoNotPayFalse_DoesNotIncludeMessage()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.Config["CoordinatorsDoNotPayCoachesTrainingRegistrationFee"] = "false";
        viewData.Config["SchoolCoordinatorsDoNotPayMessage"] = "School coordinators are exempt from paying the registration fee.";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().NotContain("School coordinators are exempt from paying the registration fee.");
    }

    [Fact]
    public void GenerateEmailBody_CoordinatorsDoNotPayOtherValue_DoesNotIncludeMessage()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.Config["CoordinatorsDoNotPayCoachesTrainingRegistrationFee"] = "yes";
        viewData.Config["SchoolCoordinatorsDoNotPayMessage"] = "School coordinators are exempt from paying the registration fee.";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().NotContain("School coordinators are exempt from paying the registration fee.");
    }

    [Fact]
    public void GenerateEmailBody_CoordinatorsDoNotPayKeyMissing_DoesNotIncludeMessage()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.Config["SchoolCoordinatorsDoNotPayMessage"] = "School coordinators are exempt from paying the registration fee.";
        // CoordinatorsDoNotPayCoachesTrainingRegistrationFee key not added

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().NotContain("School coordinators are exempt from paying the registration fee.");
    }

    [Fact]
    public void GenerateEmailBody_ReplacesRegionSpan_WithRegionNumber()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.Config["RegionNumber"] = "12";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("Region 12");
        result.Should().NotContain("<span>Region</span>");
    }

    [Fact]
    public void GenerateEmailBody_ReplacesYearsSpan_WithYearRange()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.Config["Year"] = "2024";
        viewData.Config["EndYear"] = "2025";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("2024 - 2025");
        result.Should().NotContain("<span>Years</span>");
    }

    [Fact]
    public void GenerateEmailBody_ReplacesProgramGuideSpan_WithProgramGuideURL()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.Config["ProgramGuideURL"] = "http://test.com/program";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("<a href=\"http://test.com/program\" target=\"_blank\">http://test.com/program</a>");
        result.Should().NotContain("<span>ProgramGuide</span>");
    }

    [Fact]
    public void GenerateEmailBody_ReplacesFeeSpan_WithEventCost()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.EventCost = "$75.00";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("$75.00");
        result.Should().NotContain("<span>Fee</span>");
    }

    [Fact]
    public void GenerateEmailBody_ReplacesMakeChecksOutToSpan_WithEventMakeChecksOutTo()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.EventMakeChecksOutTo = "Test Payee Name";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("Test Payee Name");
        result.Should().NotContain("<span>MakeChecksOutTo</span>");
    }

    [Fact]
    public void GenerateEmailBody_ReplacesRegionalDirectorEmailSpan_WithEmailLink()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.Config["RegionalDirectorEmail"] = "test@example.com";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("<a href=\"mailto:test@example.com\">test@example.com</a>");
        result.Should().NotContain("<span>RegionalDirectorEmail</span>");
    }

    [Fact]
    public void GenerateEmailBody_MinimalConfig_ReplacesOnlyAvailablePlaceholders()
    {
        // Arrange
        var viewData = new Page02ViewData
        {
            Config = new Dictionary<string, string>
            {
                ["RegionNumber"] = "5",
                ["Year"] = "2023",
                ["EndYear"] = "2024",
                ["ProgramGuideURL"] = "http://guide.com",
                ["RegionalDirectorEmail"] = "director@region.org"
            },
            CoachesTrainingInfo = new Event
            {
                EventMailBody = "<span>Region</span> <span>Location</span> <span>Date</span> <span>Time</span> <span>Years</span> <span>ProgramGuide</span> <span>VirginiaHandbook</span> <span>Fee</span> <span>MakeChecksOutTo</span> <span>CoordinatorsDoNotPay</span> <span>RegionalDirectorEmail</span>",
                Location = "Minimal Location",
                EventCost = "Free",
                EventMakeChecksOutTo = "Test Org"
            }
        };

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("Region 5");
        result.Should().Contain("Minimal Location");
        result.Should().Contain("TBA"); // Date is null
        result.Should().Contain("2023 - 2024");
        result.Should().Contain("Free");
        result.Should().Contain("Test Org");
    }

    [Fact]
    public void GenerateEmailBody_ComplexLocationWithURL_BuildsCompleteAnchorTag()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.CoachesTrainingInfo.LocationURL = "https://maps.google.com/complex-location";
        viewData.CoachesTrainingInfo.Location = "Complex Training Center";
        viewData.CoachesTrainingInfo.LocationAddress = "456 Oak Street, Suite 200";
        viewData.CoachesTrainingInfo.LocationCity = "Alexandria";
        viewData.CoachesTrainingInfo.LocationState = "Virginia";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().Contain("<a href=\"https://maps.google.com/complex-location\" target=\"_blank\">");
        result.Should().Contain("Complex Training Center, 456 Oak Street, Suite 200, Alexandria, Virginia");
        result.Should().Contain("</a>");
    }

    [Fact]
    public void GenerateEmailBody_AllPlaceholdersPresent_AllGetReplaced()
    {
        // Arrange
        var viewData = CreatePage02ViewDataWithAllFields();
        viewData.Config["CoachesHandbookURL"] = "http://handbook.va.gov";
        viewData.Config["CoordinatorsDoNotPayCoachesTrainingRegistrationFee"] = "true";
        viewData.Config["SchoolCoordinatorsDoNotPayMessage"] = "Coordinators exempt";

        // Act
        var result = _controller.GenerateEmailBodyPublic(viewData);

        // Assert
        result.Should().NotContain("<span>Region</span>");
        result.Should().NotContain("<span>Location</span>");
        result.Should().NotContain("<span>Date</span>");
        result.Should().NotContain("<span>Time</span>");
        result.Should().NotContain("<span>Years</span>");
        result.Should().NotContain("<span>ProgramGuide</span>");
        result.Should().NotContain("<span>VirginiaHandbook</span>");
        result.Should().NotContain("<span>Fee</span>");
        result.Should().NotContain("<span>MakeChecksOutTo</span>");
        result.Should().NotContain("<span>CoordinatorsDoNotPay</span>");
        result.Should().NotContain("<span>RegionalDirectorEmail</span>");
    }

    #endregion

    #region Helper Methods

    private Page02ViewData CreatePage02ViewDataWithAllFields()
    {
        return new Page02ViewData
        {
            Config = new Dictionary<string, string>
            {
                ["RegionNumber"] = "9",
                ["Year"] = "2025",
                ["EndYear"] = "2026",
                ["ProgramGuideURL"] = "http://example.com/guide",
                ["RegionalDirectorEmail"] = "director@novanorth.org"
            },
            CoachesTrainingInfo = new Event
            {
                EventMailBody = "<span>Region</span> <span>Location</span> <span>Date</span> <span>Time</span> <span>Years</span> <span>ProgramGuide</span> <span>VirginiaHandbook</span> <span>Fee</span> <span>MakeChecksOutTo</span> <span>CoordinatorsDoNotPay</span> <span>RegionalDirectorEmail</span>",
                Location = "Test Location",
                LocationURL = "http://location.example.com",
                LocationAddress = "123 Main St",
                LocationCity = "Springfield",
                LocationState = "VA",
                StartDate = new DateTime(2025, 3, 15),
                Time = "9:00 AM",
                EventCost = "$50",
                EventMakeChecksOutTo = "NoVA North Odyssey"
            }
        };
    }

    #endregion

    #region Test Helper Classes

    /// <summary>
    /// Testable subclass that exposes protected methods as public.
    /// </summary>
    private class TestableCoachesTrainingRegistrationController : CoachesTrainingRegistrationController
    {
        public string GenerateEmailBodyPublic(Page02ViewData viewData)
        {
            return GenerateEmailBody(viewData);
        }
    }

    #endregion
}
