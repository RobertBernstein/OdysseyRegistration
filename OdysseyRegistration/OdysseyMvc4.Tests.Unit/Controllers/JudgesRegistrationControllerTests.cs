using FluentAssertions;
using Moq;
using OdysseyMvc2024.Controllers;
using OdysseyMvc2024.Models;
using OdysseyMvc2024.ViewData.JudgesRegistration;
using OdysseyMvc4.Tests.Unit.Helpers;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace OdysseyMvc4.Tests.Unit.Controllers;

/// <summary>
/// Tests for the JudgesRegistrationController business logic that must be preserved
/// during the migration from .NET Framework 4.8 to .NET 10.
/// </summary>
public class JudgesRegistrationControllerTests
{
    private readonly Mock<IOdysseyRepository> _mockRepo;
    private readonly JudgesRegistrationController _controller;

    public JudgesRegistrationControllerTests()
    {
        _mockRepo = TestHelper.CreateMockRepository();
        _mockRepo.Setup(r => r.JudgesInfo).Returns(TestHelper.CreateDefaultJudgesInfo());
        _controller = new JudgesRegistrationController(_mockRepo.Object);
        TestHelper.SetupControllerContext(_controller);
    }

    #region BuildMailRegionalDirectorHyperLink Tests

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_ValidViewData_ReturnsMailtoLink()
    {
        var viewData = CreatePage01ViewData();
        var result = _controller.BuildMailRegionalDirectorHyperLink(viewData);

        result.Should().StartWith("mailto:director@novanorth.org?subject=");
        result.Should().Contain("subject=");
        result.Should().Contain("body=");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_ContainsRegionNumber()
    {
        var viewData = CreatePage01ViewData();
        var result = _controller.BuildMailRegionalDirectorHyperLink(viewData);

        // The subject should contain "Region 9" (URI-encoded)
        var decodedResult = Uri.UnescapeDataString(result);
        decodedResult.Should().Contain("Region 9");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_ContainsExpectedBodyText()
    {
        var viewData = CreatePage01ViewData();
        var result = _controller.BuildMailRegionalDirectorHyperLink(viewData);

        var decodedResult = Uri.UnescapeDataString(result);
        decodedResult.Should().Contain("I cannot be a judge this year");
        decodedResult.Should().Contain("My name is");
        decodedResult.Should().Contain("My phone number is");
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_NullViewData_ThrowsArgumentNullException()
    {
        var act = () => _controller.BuildMailRegionalDirectorHyperLink(null!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_NullConfig_ThrowsArgumentNullException()
    {
        var viewData = CreatePage01ViewData();
        viewData.Config = null;

        var act = () => _controller.BuildMailRegionalDirectorHyperLink(viewData);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void BuildMailRegionalDirectorHyperLink_MissingRegionalDirectorEmail_ThrowsArgumentException()
    {
        var viewData = CreatePage01ViewData();
        viewData.Config!.Remove("RegionalDirectorEmail");

        var act = () => _controller.BuildMailRegionalDirectorHyperLink(viewData);
        act.Should().Throw<ArgumentException>();
    }

    #endregion

    #region GetPreviousPositions Tests (via Reflection since it's private static)

    [Fact]
    public void GetPreviousPositions_AllPositionsSelected_ReturnsAllSemicolonSeparated()
    {
        var viewData = new Page02ViewData
        {
            PreviouslyHeadJudge = true,
            PreviouslyProblemJudge = true,
            PreviouslyStyleJudge = true,
            PreviouslyStagingJudge = true,
            PreviouslyTimekeeper = true,
            PreviouslyScorechecker = true,
            PreviouslyWeighInJudge = true,
            TshirtSizes = [],
            ProblemChoices = []
        };

        var result = InvokeGetPreviousPositions(viewData);

        result.Should().Be("Head Judge;Problem Judge;Style Judge;Staging Judge;Timekeeper;Scorechecker;Weigh-In Judge");
    }

    [Fact]
    public void GetPreviousPositions_NoPositionsSelected_ReturnsNull()
    {
        var viewData = new Page02ViewData
        {
            PreviouslyHeadJudge = false,
            PreviouslyProblemJudge = false,
            PreviouslyStyleJudge = false,
            PreviouslyStagingJudge = false,
            PreviouslyTimekeeper = false,
            PreviouslyScorechecker = false,
            PreviouslyWeighInJudge = false,
            TshirtSizes = [],
            ProblemChoices = []
        };

        var result = InvokeGetPreviousPositions(viewData);

        result.Should().BeNull();
    }

    [Fact]
    public void GetPreviousPositions_OnlyHeadJudge_ReturnsHeadJudge()
    {
        var viewData = new Page02ViewData
        {
            PreviouslyHeadJudge = true,
            PreviouslyProblemJudge = false,
            PreviouslyStyleJudge = false,
            PreviouslyStagingJudge = false,
            PreviouslyTimekeeper = false,
            PreviouslyScorechecker = false,
            PreviouslyWeighInJudge = false,
            TshirtSizes = [],
            ProblemChoices = []
        };

        var result = InvokeGetPreviousPositions(viewData);

        result.Should().Be("Head Judge");
    }

    [Fact]
    public void GetPreviousPositions_OnlyProblemJudge_TrimsLeadingSemicolon()
    {
        var viewData = new Page02ViewData
        {
            PreviouslyHeadJudge = false,
            PreviouslyProblemJudge = true,
            PreviouslyStyleJudge = false,
            PreviouslyStagingJudge = false,
            PreviouslyTimekeeper = false,
            PreviouslyScorechecker = false,
            PreviouslyWeighInJudge = false,
            TshirtSizes = [],
            ProblemChoices = []
        };

        var result = InvokeGetPreviousPositions(viewData);

        result.Should().Be("Problem Judge");
        result.Should().NotStartWith(";");
    }

    [Fact]
    public void GetPreviousPositions_OnlyWeighInJudge_TrimsLeadingSemicolon()
    {
        var viewData = new Page02ViewData
        {
            PreviouslyHeadJudge = false,
            PreviouslyProblemJudge = false,
            PreviouslyStyleJudge = false,
            PreviouslyStagingJudge = false,
            PreviouslyTimekeeper = false,
            PreviouslyScorechecker = false,
            PreviouslyWeighInJudge = true,
            TshirtSizes = [],
            ProblemChoices = []
        };

        var result = InvokeGetPreviousPositions(viewData);

        result.Should().Be("Weigh-In Judge");
    }

    [Fact]
    public void GetPreviousPositions_ProblemJudgeAndTimekeeper_ReturnsBothSemicolonSeparated()
    {
        var viewData = new Page02ViewData
        {
            PreviouslyHeadJudge = false,
            PreviouslyProblemJudge = true,
            PreviouslyStyleJudge = false,
            PreviouslyStagingJudge = false,
            PreviouslyTimekeeper = true,
            PreviouslyScorechecker = false,
            PreviouslyWeighInJudge = false,
            TshirtSizes = [],
            ProblemChoices = []
        };

        var result = InvokeGetPreviousPositions(viewData);

        result.Should().Be("Problem Judge;Timekeeper");
    }

    [Fact]
    public void GetPreviousPositions_HeadJudgeAndScorechecker_ReturnsBoth()
    {
        var viewData = new Page02ViewData
        {
            PreviouslyHeadJudge = true,
            PreviouslyProblemJudge = false,
            PreviouslyStyleJudge = false,
            PreviouslyStagingJudge = false,
            PreviouslyTimekeeper = false,
            PreviouslyScorechecker = true,
            PreviouslyWeighInJudge = false,
            TshirtSizes = [],
            ProblemChoices = []
        };

        var result = InvokeGetPreviousPositions(viewData);

        result.Should().Be("Head Judge;Scorechecker");
    }

    #endregion

    #region GenerateEmailBody Tests

    [Fact]
    public void GenerateEmailBody_ReplacesJudgeId()
    {
        var page03ViewData = CreatePage03ViewData();
        var result = InvokeGenerateEmailBody(page03ViewData);

        result.Should().Contain("42");
        result.Should().NotContain("<span>JudgeID</span>");
    }

    [Fact]
    public void GenerateEmailBody_ReplacesFirstName()
    {
        var page03ViewData = CreatePage03ViewData();
        var result = InvokeGenerateEmailBody(page03ViewData);

        result.Should().Contain("John");
        result.Should().NotContain("<span>FirstName</span>");
    }

    [Fact]
    public void GenerateEmailBody_ReplacesLastName()
    {
        var page03ViewData = CreatePage03ViewData();
        var result = InvokeGenerateEmailBody(page03ViewData);

        result.Should().Contain("Doe");
        result.Should().NotContain("<span>LastName</span>");
    }

    [Fact]
    public void GenerateEmailBody_ReplacesRegion()
    {
        var page03ViewData = CreatePage03ViewData();
        var result = InvokeGenerateEmailBody(page03ViewData);

        result.Should().Contain("Region 9");
        result.Should().NotContain("<span>Region</span>");
    }

    [Fact]
    public void GenerateEmailBody_ReplacesJudgesTrainingLocation()
    {
        var page03ViewData = CreatePage03ViewData();
        var result = InvokeGenerateEmailBody(page03ViewData);

        result.Should().Contain("Community Center");
        result.Should().NotContain("<span>JudgesTrainingLocation</span>");
    }

    [Fact]
    public void GenerateEmailBody_ReplacesJudgesTrainingDate()
    {
        var page03ViewData = CreatePage03ViewData();
        var result = InvokeGenerateEmailBody(page03ViewData);

        result.Should().NotContain("<span>JudgesTrainingDate</span>");
        // The date should be a long date string for Feb 20, 2025
        result.Should().Contain("2025");
    }

    [Fact]
    public void GenerateEmailBody_ReplacesJudgesTrainingTime()
    {
        var page03ViewData = CreatePage03ViewData();
        var result = InvokeGenerateEmailBody(page03ViewData);

        result.Should().Contain("9:00 AM");
        result.Should().NotContain("<span>JudgesTrainingTime</span>");
    }

    [Fact]
    public void GenerateEmailBody_ReplacesTournamentLocation()
    {
        var page03ViewData = CreatePage03ViewData();
        var result = InvokeGenerateEmailBody(page03ViewData);

        result.Should().Contain("Springfield High School");
        result.Should().NotContain("<span>TournamentLocation</span>");
    }

    [Fact]
    public void GenerateEmailBody_ReplacesTournamentDate()
    {
        var page03ViewData = CreatePage03ViewData();
        var result = InvokeGenerateEmailBody(page03ViewData);

        result.Should().NotContain("<span>TournamentDate</span>");
        result.Should().Contain("2025");
    }

    [Fact]
    public void GenerateEmailBody_ReplacesTournamentTime()
    {
        var page03ViewData = CreatePage03ViewData();
        var result = InvokeGenerateEmailBody(page03ViewData);

        result.Should().Contain("8:00 AM");
        result.Should().NotContain("<span>TournamentTime</span>");
    }

    [Fact]
    public void GenerateEmailBody_ReplacesContactUsURL()
    {
        var page03ViewData = CreatePage03ViewData();
        var result = InvokeGenerateEmailBody(page03ViewData);

        result.Should().Contain("http://www.novanorth.org/Home/ContactUs");
        result.Should().NotContain("<span>ContactUsURL</span>");
    }

    [Fact]
    public void GenerateEmailBody_NullStartDate_ShowsTBA()
    {
        var page03ViewData = CreatePage03ViewData();
        page03ViewData.JudgesInfo!.StartDate = null;
        var result = InvokeGenerateEmailBody(page03ViewData);

        result.Should().Contain("TBA");
    }

    [Fact]
    public void GenerateEmailBody_NullTime_ShowsTBA()
    {
        var page03ViewData = CreatePage03ViewData();
        page03ViewData.JudgesInfo!.Time = null;
        var result = InvokeGenerateEmailBody(page03ViewData);

        result.Should().Contain("TBA");
    }

    [Fact]
    public void GenerateEmailBody_WithLocationURL_IncludesHyperlink()
    {
        var page03ViewData = CreatePage03ViewData();
        var result = InvokeGenerateEmailBody(page03ViewData);

        result.Should().Contain("<a href=");
        result.Should().Contain("target=\"_blank\"");
        result.Should().Contain("</a>");
    }

    [Fact]
    public void GenerateEmailBody_WithoutLocationURL_NoHyperlink()
    {
        var page03ViewData = CreatePage03ViewData();
        page03ViewData.JudgesInfo!.LocationURL = null;
        page03ViewData.TournamentInfo!.LocationURL = null;
        var result = InvokeGenerateEmailBody(page03ViewData);

        result.Should().NotContain("<a href=");
    }

    #endregion

    #region Constructor Tests

    [Fact]
    public void Constructor_SetsRegistrationTypeToJudges()
    {
        _controller.CurrentRegistrationType.Should().Be(BaseRegistrationController.RegistrationType.Judges);
    }

    [Fact]
    public void Constructor_SetsFriendlyRegistrationName()
    {
        _controller.FriendlyRegistrationName.Should().Be("Judges Registration");
    }

    [Fact]
    public void Constructor_WithNullRepository_ThrowsArgumentNullException()
    {
        var act = () => new JudgesRegistrationController(null!);
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region Helper Methods

    private Page01ViewData CreatePage01ViewData()
    {
        var config = TestHelper.CreateDefaultConfig();
        return new Page01ViewData
        {
            Config = config,
            TournamentInfo = TestHelper.CreateDefaultTournamentInfo(),
            JudgesInfo = TestHelper.CreateDefaultJudgesInfo(),
            RegionNumber = "9",
            MailRegionalDirectorHyperLink = "",
            MailRegionalDirectorHyperLinkText = ""
        };
    }

    private Page03ViewData CreatePage03ViewData()
    {
        return new Page03ViewData
        {
            Config = TestHelper.CreateDefaultConfig(),
            TournamentInfo = TestHelper.CreateDefaultTournamentInfo(),
            JudgesInfo = TestHelper.CreateDefaultJudgesInfo(),
            RegionNumber = "9",
            Judge = new Judge
            {
                JudgeID = 42,
                FirstName = "John",
                LastName = "Doe"
            }
        };
    }

    /// <summary>
    /// Invokes the private static GetPreviousPositions method via reflection.
    /// </summary>
    private static string? InvokeGetPreviousPositions(Page02ViewData viewData)
    {
        var method = typeof(JudgesRegistrationController)
            .GetMethod("GetPreviousPositions", BindingFlags.NonPublic | BindingFlags.Static);

        if (method == null)
            throw new InvalidOperationException("GetPreviousPositions method not found.");

        return (string?)method.Invoke(null, [viewData]);
    }

    /// <summary>
    /// Invokes the protected GenerateEmailBody method via a testable subclass.
    /// </summary>
    private string InvokeGenerateEmailBody(Page03ViewData viewData)
    {
        var testable = new TestableJudgesRegistrationController(_mockRepo.Object);
        TestHelper.SetupControllerContext(testable);
        return testable.TestGenerateEmailBody(viewData);
    }

    #endregion
}

/// <summary>
/// Testable subclass to expose protected methods.
/// </summary>
public class TestableJudgesRegistrationController : JudgesRegistrationController
{
    // [SetsRequiredMembers] is needed because BaseRegistrationController.FriendlyRegistrationName is
    // a 'required' property. The base JudgesRegistrationController constructor already sets it, but
    // without this attribute the compiler prevents instantiation from external assemblies.
    [SetsRequiredMembers]
    public TestableJudgesRegistrationController(IOdysseyRepository repository) : base(repository) { }

    public string TestGenerateEmailBody(Page03ViewData viewData) => GenerateEmailBody(viewData);
}
