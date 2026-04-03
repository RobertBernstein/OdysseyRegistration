using FluentAssertions;
using OdysseyMvc4.Controllers;
using OdysseyMvc4.ViewData.VolunteerRegistration;
using OdysseyMvc4.UnitTests.Helpers;

namespace OdysseyMvc4.UnitTests.Controllers;

/// <summary>
/// Tests for the VolunteerRegistrationController business logic.
/// Note: Full action method tests (Index, Page01, Page02, Page03) are limited due to architectural
/// constraints:
/// 1. The .NET Framework controller (System.Web.Mvc) depends on HttpContext infrastructure that's
///    difficult to set up from a .NET 10 test project
/// 2. The OdysseyRepository is instantiated as a concrete field in the base class, preventing mocking
/// 3. Action methods depend on CurrentRegistrationState which requires a configured repository
/// These methods should be fully tested through integration tests or after migration to OdysseyMvc2024
/// where dependency injection is used. This test file focuses on testable business logic (GenerateEmailBody)
/// and constructor behavior.
/// </summary>
public class VolunteerRegistrationControllerTests
{
    #region Constructor Tests

    [Fact]
    public void Constructor_SetsCurrentRegistrationType_ToVolunteer()
    {
        // Arrange & Act
        var controller = new VolunteerRegistrationController();

        // Assert
        controller.CurrentRegistrationType.Should().Be(BaseRegistrationController.RegistrationType.Volunteer);
    }

    [Fact]
    public void Constructor_SetsFriendlyRegistrationName_ToVolunteerRegistration()
    {
        // Arrange & Act
        var controller = new VolunteerRegistrationController();

        // Assert
        controller.FriendlyRegistrationName.Should().Be("Volunteer Registration");
    }

    #endregion

    #region BuildMailRegionalDirectorHyperLink Tests

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_ValidViewData_ReturnsMailtoLink()
    {
        // Arrange
        var controller = new VolunteerRegistrationController();
        var viewData = new Page01ViewData
        {
            Config = TestHelper.CreateDefaultConfig(),
            RegionNumber = "9"
        };

        // Act
        var result = controller.BuildMailRegionalDirectorHyperLink(viewData);

        // Assert
        result.Should().StartWith("mailto:");
        result.Should().Contain("?subject=");
        result.Should().Contain("&body=");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_ContainsRegionalDirectorEmail()
    {
        // Arrange
        var controller = new VolunteerRegistrationController();
        var viewData = new Page01ViewData
        {
            Config = TestHelper.CreateDefaultConfig(),
            RegionNumber = "9"
        };

        // Act
        var result = controller.BuildMailRegionalDirectorHyperLink(viewData);

        // Assert
        result.Should().StartWith("mailto:director@novanorth.org");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_ContainsRegionNumber()
    {
        // Arrange
        var controller = new VolunteerRegistrationController();
        var viewData = new Page01ViewData
        {
            Config = TestHelper.CreateDefaultConfig(),
            RegionNumber = "9"
        };

        // Act
        var result = controller.BuildMailRegionalDirectorHyperLink(viewData);

        // Assert
        var decodedResult = Uri.UnescapeDataString(result);
        decodedResult.Should().Contain("Region 9");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_DifferentRegionNumber_UsesProvidedValue()
    {
        // Arrange
        var controller = new VolunteerRegistrationController();
        var viewData = new Page01ViewData
        {
            Config = TestHelper.CreateDefaultConfig(),
            RegionNumber = "12"
        };

        // Act
        var result = controller.BuildMailRegionalDirectorHyperLink(viewData);

        // Assert
        var decodedResult = Uri.UnescapeDataString(result);
        decodedResult.Should().Contain("Region 12");
        decodedResult.Should().NotContain("Region 9");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_ContainsExpectedBodyText()
    {
        // Arrange
        var controller = new VolunteerRegistrationController();
        var viewData = new Page01ViewData
        {
            Config = TestHelper.CreateDefaultConfig(),
            RegionNumber = "9"
        };

        // Act
        var result = controller.BuildMailRegionalDirectorHyperLink(viewData);

        // Assert
        var decodedResult = Uri.UnescapeDataString(result);
        decodedResult.Should().Contain("I cannot be a volunteer this year");
        decodedResult.Should().Contain("My name is");
        decodedResult.Should().Contain("My phone number is");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_ContainsHelpMessage()
    {
        // Arrange
        var controller = new VolunteerRegistrationController();
        var viewData = new Page01ViewData
        {
            Config = TestHelper.CreateDefaultConfig(),
            RegionNumber = "9"
        };

        // Act
        var result = controller.BuildMailRegionalDirectorHyperLink(viewData);

        // Assert
        var decodedResult = Uri.UnescapeDataString(result);
        decodedResult.Should().Contain("would like to help in some other way");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_SubjectContainsCorrectText()
    {
        // Arrange
        var controller = new VolunteerRegistrationController();
        var viewData = new Page01ViewData
        {
            Config = TestHelper.CreateDefaultConfig(),
            RegionNumber = "9"
        };

        // Act
        var result = controller.BuildMailRegionalDirectorHyperLink(viewData);

        // Assert
        var decodedResult = Uri.UnescapeDataString(result);
        decodedResult.Should().Contain("?subject=I would like to help at the Region 9 Tournament");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_SpacesAreUrlEncoded()
    {
        // Arrange
        var controller = new VolunteerRegistrationController();
        var viewData = new Page01ViewData
        {
            Config = TestHelper.CreateDefaultConfig(),
            RegionNumber = "9"
        };

        // Act
        var result = controller.BuildMailRegionalDirectorHyperLink(viewData);

        // Assert
        result.Should().Contain("%20");
        result.Should().NotContain(" ");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_ContainsPlaceholderForName()
    {
        // Arrange
        var controller = new VolunteerRegistrationController();
        var viewData = new Page01ViewData
        {
            Config = TestHelper.CreateDefaultConfig(),
            RegionNumber = "9"
        };

        // Act
        var result = controller.BuildMailRegionalDirectorHyperLink(viewData);

        // Assert
        var decodedResult = Uri.UnescapeDataString(result);
        // Should contain underscores for the user to fill in
        decodedResult.Should().MatchRegex(@"My name is _+\.");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_ContainsPlaceholderForPhone()
    {
        // Arrange
        var controller = new VolunteerRegistrationController();
        var viewData = new Page01ViewData
        {
            Config = TestHelper.CreateDefaultConfig(),
            RegionNumber = "9"
        };

        // Act
        var result = controller.BuildMailRegionalDirectorHyperLink(viewData);

        // Assert
        var decodedResult = Uri.UnescapeDataString(result);
        // Should contain underscores for the user to fill in
        decodedResult.Should().MatchRegex(@"My phone number is _+\.");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_LineBreaksAreEncoded()
    {
        // Arrange
        var controller = new VolunteerRegistrationController();
        var viewData = new Page01ViewData
        {
            Config = TestHelper.CreateDefaultConfig(),
            RegionNumber = "9"
        };

        // Act
        var result = controller.BuildMailRegionalDirectorHyperLink(viewData);

        // Assert
        // Should contain %0A for line breaks
        result.Should().Contain("%0A");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_CustomEmail_UsesProvidedEmail()
    {
        // Arrange
        var controller = new VolunteerRegistrationController();
        var config = TestHelper.CreateDefaultConfig();
        config["RegionalDirectorEmail"] = "custom@example.com";
        var viewData = new Page01ViewData
        {
            Config = config,
            RegionNumber = "9"
        };

        // Act
        var result = controller.BuildMailRegionalDirectorHyperLink(viewData);

        // Assert
        result.Should().StartWith("mailto:custom@example.com");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_NullViewData_ThrowsNullReferenceException()
    {
        // Arrange
        var controller = new VolunteerRegistrationController();

        // Act
        var act = () => controller.BuildMailRegionalDirectorHyperLink(null!);

        // Assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_NullConfig_ThrowsNullReferenceException()
    {
        // Arrange
        var controller = new VolunteerRegistrationController();
        var viewData = new Page01ViewData
        {
            Config = null,
            RegionNumber = "9"
        };

        // Act
        var act = () => controller.BuildMailRegionalDirectorHyperLink(viewData);

        // Assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_MissingRegionalDirectorEmail_ThrowsKeyNotFoundException()
    {
        // Arrange
        var controller = new VolunteerRegistrationController();
        var config = TestHelper.CreateDefaultConfig();
        config.Remove("RegionalDirectorEmail");
        var viewData = new Page01ViewData
        {
            Config = config,
            RegionNumber = "9"
        };

        // Act
        var act = () => controller.BuildMailRegionalDirectorHyperLink(viewData);

        // Assert
        act.Should().Throw<KeyNotFoundException>();
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_NullRegionNumber_IncludesNullInSubject()
    {
        // Arrange
        var controller = new VolunteerRegistrationController();
        var viewData = new Page01ViewData
        {
            Config = TestHelper.CreateDefaultConfig(),
            RegionNumber = null
        };

        // Act
        var result = controller.BuildMailRegionalDirectorHyperLink(viewData);

        // Assert
        var decodedResult = Uri.UnescapeDataString(result);
        // When RegionNumber is null, it will be concatenated as empty string
        decodedResult.Should().Contain("Region  Tournament"); // Two spaces
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_EmptyRegionNumber_IncludesEmptyInSubject()
    {
        // Arrange
        var controller = new VolunteerRegistrationController();
        var viewData = new Page01ViewData
        {
            Config = TestHelper.CreateDefaultConfig(),
            RegionNumber = string.Empty
        };

        // Act
        var result = controller.BuildMailRegionalDirectorHyperLink(viewData);

        // Assert
        var decodedResult = Uri.UnescapeDataString(result);
        decodedResult.Should().Contain("Region  Tournament"); // Two spaces
    }

    #endregion

    #region GenerateEmailBody Tests

    [Fact]
    public void GenerateEmailBody_WithAllFields_ReplacesAllTokens()
    {
        // Arrange
        var controller = new TestableVolunteerRegistrationController();
        var config = TestHelper.CreateDefaultConfig();
        var page03ViewData = new Page03ViewData
        {
            Config = config,
            Volunteer = new OdysseyMvc4.Models.Volunteer
            {
                VolunteerID = 123,
                FirstName = "John",
                LastName = "Doe"
            },
            VolunteerInfo = new OdysseyMvc4.Models.Event
            {
                EventMailBody = "ID: <span>VolunteerID</span>, Name: <span>FirstName</span> <span>LastName</span>, <span>Region</span>, Location: <span>TournamentLocation</span>, Date: <span>TournamentDate</span>, Time: <span>TournamentTime</span>, Contact: <span>ContactUsURL</span>"
            },
            TournamentInfo = new OdysseyMvc4.Models.Event
            {
                Location = "Test Location",
                LocationAddress = "123 Main St",
                LocationCity = "Test City",
                LocationState = "VA",
                LocationURL = "http://example.com",
                StartDate = new DateTime(2025, 3, 15),
                Time = "9:00 AM"
            }
        };

        // Act
        var result = controller.PublicGenerateEmailBody(page03ViewData);

        // Assert
        result.Should().Contain("ID: 123");
        result.Should().Contain("Name: John Doe");
        result.Should().Contain("Region 9");
        result.Should().Contain("<a href=\"http://example.com\" target=\"_blank\">Test Location, 123 Main St, Test City, VA</a>");
        result.Should().Contain("Date: Saturday, March 15, 2025");
        result.Should().Contain("Time: 9:00 AM");
        result.Should().Contain("Contact: http://www.novanorth.org/Home/ContactUs");
    }

    [Fact]
    public void GenerateEmailBody_WithoutLocationURL_DoesNotIncludeHyperlink()
    {
        // Arrange
        var controller = new TestableVolunteerRegistrationController();
        var config = TestHelper.CreateDefaultConfig();
        var page03ViewData = new Page03ViewData
        {
            Config = config,
            Volunteer = new OdysseyMvc4.Models.Volunteer
            {
                VolunteerID = 123,
                FirstName = "Jane",
                LastName = "Smith"
            },
            VolunteerInfo = new OdysseyMvc4.Models.Event
            {
                EventMailBody = "Location: <span>TournamentLocation</span>"
            },
            TournamentInfo = new OdysseyMvc4.Models.Event
            {
                Location = "Test Location",
                LocationAddress = "456 Oak Ave",
                LocationCity = "Springfield",
                LocationState = "MD",
                LocationURL = null,
                StartDate = null,
                Time = null
            }
        };

        // Act
        var result = controller.PublicGenerateEmailBody(page03ViewData);

        // Assert
        result.Should().Contain("Test Location, 456 Oak Ave, Springfield, MD");
        result.Should().NotContain("<a href=");
        result.Should().NotContain("</a>");
    }

    [Fact]
    public void GenerateEmailBody_WithEmptyLocationURL_DoesNotIncludeHyperlink()
    {
        // Arrange
        var controller = new TestableVolunteerRegistrationController();
        var config = TestHelper.CreateDefaultConfig();
        var page03ViewData = new Page03ViewData
        {
            Config = config,
            Volunteer = new OdysseyMvc4.Models.Volunteer
            {
                VolunteerID = 456,
                FirstName = "Bob",
                LastName = "Johnson"
            },
            VolunteerInfo = new OdysseyMvc4.Models.Event
            {
                EventMailBody = "Location: <span>TournamentLocation</span>"
            },
            TournamentInfo = new OdysseyMvc4.Models.Event
            {
                Location = "Community Center",
                LocationURL = string.Empty,
                StartDate = null,
                Time = null
            }
        };

        // Act
        var result = controller.PublicGenerateEmailBody(page03ViewData);

        // Assert
        result.Should().Contain("Community Center");
        result.Should().NotContain("<a href=");
    }

    [Fact]
    public void GenerateEmailBody_WithoutLocationAddress_OnlyIncludesLocation()
    {
        // Arrange
        var controller = new TestableVolunteerRegistrationController();
        var config = TestHelper.CreateDefaultConfig();
        var page03ViewData = new Page03ViewData
        {
            Config = config,
            Volunteer = new OdysseyMvc4.Models.Volunteer
            {
                VolunteerID = 789,
                FirstName = "Alice",
                LastName = "Williams"
            },
            VolunteerInfo = new OdysseyMvc4.Models.Event
            {
                EventMailBody = "Location: <span>TournamentLocation</span>"
            },
            TournamentInfo = new OdysseyMvc4.Models.Event
            {
                Location = "Library",
                LocationAddress = null,
                LocationCity = "Fairfax",
                LocationState = "VA",
                LocationURL = null,
                StartDate = null,
                Time = null
            }
        };

        // Act
        var result = controller.PublicGenerateEmailBody(page03ViewData);

        // Assert
        result.Should().Contain("Library, Fairfax, VA");
        result.Should().NotContain(", ,");
    }

    [Fact]
    public void GenerateEmailBody_WithNullStartDate_ReplacesWith_TBA()
    {
        // Arrange
        var controller = new TestableVolunteerRegistrationController();
        var config = TestHelper.CreateDefaultConfig();
        var page03ViewData = new Page03ViewData
        {
            Config = config,
            Volunteer = new OdysseyMvc4.Models.Volunteer
            {
                VolunteerID = 111,
                FirstName = "Test",
                LastName = "User"
            },
            VolunteerInfo = new OdysseyMvc4.Models.Event
            {
                EventMailBody = "Date: <span>TournamentDate</span>"
            },
            TournamentInfo = new OdysseyMvc4.Models.Event
            {
                Location = "School",
                StartDate = null,
                Time = null
            }
        };

        // Act
        var result = controller.PublicGenerateEmailBody(page03ViewData);

        // Assert
        result.Should().Contain("Date: TBA");
    }

    [Fact]
    public void GenerateEmailBody_WithNullTime_ReplacesWith_TBA()
    {
        // Arrange
        var controller = new TestableVolunteerRegistrationController();
        var config = TestHelper.CreateDefaultConfig();
        var page03ViewData = new Page03ViewData
        {
            Config = config,
            Volunteer = new OdysseyMvc4.Models.Volunteer
            {
                VolunteerID = 222,
                FirstName = "Sample",
                LastName = "Person"
            },
            VolunteerInfo = new OdysseyMvc4.Models.Event
            {
                EventMailBody = "Time: <span>TournamentTime</span>"
            },
            TournamentInfo = new OdysseyMvc4.Models.Event
            {
                Location = "Gym",
                StartDate = new DateTime(2025, 4, 1),
                Time = null
            }
        };

        // Act
        var result = controller.PublicGenerateEmailBody(page03ViewData);

        // Assert
        result.Should().Contain("Time: TBA");
    }

    [Fact]
    public void GenerateEmailBody_WithEmptyTime_ReplacesWith_TBA()
    {
        // Arrange
        var controller = new TestableVolunteerRegistrationController();
        var config = TestHelper.CreateDefaultConfig();
        var page03ViewData = new Page03ViewData
        {
            Config = config,
            Volunteer = new OdysseyMvc4.Models.Volunteer
            {
                VolunteerID = 333,
                FirstName = "Demo",
                LastName = "User"
            },
            VolunteerInfo = new OdysseyMvc4.Models.Event
            {
                EventMailBody = "Time: <span>TournamentTime</span>"
            },
            TournamentInfo = new OdysseyMvc4.Models.Event
            {
                Location = "Hall",
                StartDate = new DateTime(2025, 5, 1),
                Time = string.Empty
            }
        };

        // Act
        var result = controller.PublicGenerateEmailBody(page03ViewData);

        // Assert
        result.Should().Contain("Time: TBA");
    }

    [Fact]
    public void GenerateEmailBody_WithWhitespaceTime_ReplacesWith_TBA()
    {
        // Arrange
        var controller = new TestableVolunteerRegistrationController();
        var config = TestHelper.CreateDefaultConfig();
        var page03ViewData = new Page03ViewData
        {
            Config = config,
            Volunteer = new OdysseyMvc4.Models.Volunteer
            {
                VolunteerID = 444,
                FirstName = "Example",
                LastName = "Volunteer"
            },
            VolunteerInfo = new OdysseyMvc4.Models.Event
            {
                EventMailBody = "Time: <span>TournamentTime</span>"
            },
            TournamentInfo = new OdysseyMvc4.Models.Event
            {
                Location = "Center",
                StartDate = new DateTime(2025, 6, 1),
                Time = "   "
            }
        };

        // Act
        var result = controller.PublicGenerateEmailBody(page03ViewData);

        // Assert
        result.Should().Contain("Time: TBA");
    }

    [Fact]
    public void GenerateEmailBody_WithOnlyLocation_BuildsSimpleString()
    {
        // Arrange
        var controller = new TestableVolunteerRegistrationController();
        var config = TestHelper.CreateDefaultConfig();
        var page03ViewData = new Page03ViewData
        {
            Config = config,
            Volunteer = new OdysseyMvc4.Models.Volunteer
            {
                VolunteerID = 555,
                FirstName = "Plain",
                LastName = "Name"
            },
            VolunteerInfo = new OdysseyMvc4.Models.Event
            {
                EventMailBody = "Location: <span>TournamentLocation</span>"
            },
            TournamentInfo = new OdysseyMvc4.Models.Event
            {
                Location = "Building",
                LocationAddress = null,
                LocationCity = null,
                LocationState = null,
                LocationURL = null,
                StartDate = null,
                Time = null
            }
        };

        // Act
        var result = controller.PublicGenerateEmailBody(page03ViewData);

        // Assert
        result.Should().Contain("Location: Building");
        result.Should().NotContain(",");
    }

    [Fact]
    public void GenerateEmailBody_WithLocationAndURL_WrapsInHyperlink()
    {
        // Arrange
        var controller = new TestableVolunteerRegistrationController();
        var config = TestHelper.CreateDefaultConfig();
        var page03ViewData = new Page03ViewData
        {
            Config = config,
            Volunteer = new OdysseyMvc4.Models.Volunteer
            {
                VolunteerID = 666,
                FirstName = "Link",
                LastName = "Test"
            },
            VolunteerInfo = new OdysseyMvc4.Models.Event
            {
                EventMailBody = "Visit: <span>TournamentLocation</span>"
            },
            TournamentInfo = new OdysseyMvc4.Models.Event
            {
                Location = "Venue",
                LocationAddress = null,
                LocationCity = null,
                LocationState = null,
                LocationURL = "https://venue.example.com",
                StartDate = null,
                Time = null
            }
        };

        // Act
        var result = controller.PublicGenerateEmailBody(page03ViewData);

        // Assert
        result.Should().Contain("<a href=\"https://venue.example.com\" target=\"_blank\">Venue</a>");
    }

    #endregion

    #region Test Helpers

    /// <summary>
    /// A testable wrapper around VolunteerRegistrationController that exposes protected members.
    /// </summary>
    private class TestableVolunteerRegistrationController : VolunteerRegistrationController
    {
        public string PublicGenerateEmailBody(Page03ViewData page03ViewData)
        {
            return this.GenerateEmailBody(page03ViewData);
        }
    }

    #endregion
}
