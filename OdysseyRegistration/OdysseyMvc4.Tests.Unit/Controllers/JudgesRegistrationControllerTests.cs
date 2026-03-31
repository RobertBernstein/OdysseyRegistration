using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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

    #region Index Tests

    [Fact]
    public void Index_ReturnsRedirectToAction()
    {
        var result = _controller.Index();

        result.Should().BeOfType<RedirectToActionResult>();
    }

    [Fact]
    public void Index_RedirectsToPage01()
    {
        var result = _controller.Index() as RedirectToActionResult;

        result!.ActionName.Should().Be("Page01");
    }

    #endregion

    #region Page01 Tests

    [Fact]
    public void Page01_WhenRegistrationAvailable_ReturnsViewResult()
    {
        var result = _controller.Page01();

        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public void Page01_WhenRegistrationAvailable_ReturnsPage01ViewData()
    {
        var result = _controller.Page01() as ViewResult;

        result!.Model.Should().BeOfType<Page01ViewData>();
    }

    [Fact]
    public void Page01_WhenRegistrationAvailable_SetsMailRegionalDirectorHyperLink()
    {
        var result = _controller.Page01() as ViewResult;
        var viewData = result!.Model as Page01ViewData;

        viewData!.MailRegionalDirectorHyperLink.Should().NotBeNullOrEmpty();
        viewData.MailRegionalDirectorHyperLink.Should().StartWith("mailto:");
    }

    [Fact]
    public void Page01_WhenRegistrationAvailable_SetsMailRegionalDirectorHyperLinkText()
    {
        var result = _controller.Page01() as ViewResult;
        var viewData = result!.Model as Page01ViewData;

        viewData!.MailRegionalDirectorHyperLinkText.Should().NotBeNullOrEmpty();
        viewData.MailRegionalDirectorHyperLinkText.Should().Contain("send an e-mail to");
    }

    [Fact]
    public void Page01_WhenRegistrationAvailable_RetrievesJudgesInfoFromRepository()
    {
        _controller.Page01();

        _mockRepo.Verify(r => r.JudgesInfo, Times.AtLeastOnce);
    }

    [Fact]
    public void Page01_WhenRegistrationAvailable_CallsSetBaseViewData()
    {
        _controller.Page01();

        _mockRepo.Verify(r => r.Config, Times.AtLeastOnce);
        _mockRepo.Verify(r => r.TournamentInfo, Times.AtLeastOnce);
    }

    [Fact]
    public void Page01_WhenRegistrationClosed_RedirectsToClosedPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithClosedRegistration());

        var result = _controller.Page01();

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Closed");
    }

    [Fact]
    public void Page01_WhenRegistrationDown_RedirectsToDownPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithDownRegistration());

        var result = _controller.Page01();

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Down");
    }

    [Fact]
    public void Page01_WhenRegistrationComingSoon_RedirectsToSoonPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithComingSoonRegistration());

        var result = _controller.Page01();

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Soon");
    }

    #endregion

    #region Page01Post Tests

    [Fact]
    public void Page01Post_WhenRegistrationAvailable_CreatesNewJudge()
    {
        var result = _controller.Page01Post();

        _mockRepo.Verify(r => r.AddJudge(It.IsAny<Judge>()), Times.Once);
    }

    [Fact]
    public void Page01Post_WhenRegistrationAvailable_RedirectsToPage02()
    {
        var result = _controller.Page01Post();

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Page02");
    }

    [Fact]
    public void Page01Post_WhenRegistrationAvailable_PassesJudgeIdToPage02()
    {
        var newJudge = new Judge { JudgeID = 123 };
        _mockRepo.Setup(r => r.AddJudge(It.IsAny<Judge>()))
            .Callback<Judge>(j => j.JudgeID = newJudge.JudgeID);

        var result = _controller.Page01Post() as RedirectToActionResult;

        result!.RouteValues.Should().ContainKey("id");
        result.RouteValues!["id"].Should().Be(123);
    }

    [Fact]
    public void Page01Post_WhenRegistrationAvailable_SetsTimeRegistrationStarted()
    {
        Judge? capturedJudge = null;
        _mockRepo.Setup(r => r.AddJudge(It.IsAny<Judge>()))
            .Callback<Judge>(j => capturedJudge = j);

        _controller.Page01Post();

        capturedJudge.Should().NotBeNull();
        capturedJudge!.TimeRegistrationStarted.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void Page01Post_WhenRegistrationAvailable_SetsUserAgent()
    {
        Judge? capturedJudge = null;
        _mockRepo.Setup(r => r.AddJudge(It.IsAny<Judge>()))
            .Callback<Judge>(j => capturedJudge = j);

        _controller.Page01Post();

        capturedJudge.Should().NotBeNull();
        capturedJudge!.UserAgent.Should().NotBeNull();
    }

    [Fact]
    public void Page01Post_WhenRegistrationClosed_RedirectsToClosedPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithClosedRegistration());

        var result = _controller.Page01Post();

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Closed");
    }

    [Fact]
    public void Page01Post_WhenRegistrationClosed_DoesNotCreateJudge()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithClosedRegistration());

        _controller.Page01Post();

        _mockRepo.Verify(r => r.AddJudge(It.IsAny<Judge>()), Times.Never);
    }

    [Fact]
    public void Page01Post_WhenRegistrationDown_RedirectsToDownPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithDownRegistration());

        var result = _controller.Page01Post();

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Down");
    }

    [Fact]
    public void Page01Post_WhenRegistrationComingSoon_RedirectsToSoonPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithComingSoonRegistration());

        var result = _controller.Page01Post();

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Soon");
    }

    [Fact]
    public void Page01Post_WhenAddJudgeThrowsException_RedirectsToHomeIndex()
    {
        _mockRepo.Setup(r => r.AddJudge(It.IsAny<Judge>()))
            .Throws(new InvalidOperationException("Database error"));

        var result = _controller.Page01Post();

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Index");
        redirectResult.ControllerName.Should().Be("Home");
    }

    #endregion

    #region Page02 GET Tests

    [Fact]
    public void Page02Get_WhenRegistrationAvailable_ReturnsViewResult()
    {
        var result = _controller.Page02(1);

        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public void Page02Get_WhenRegistrationAvailable_ReturnsPage02ViewData()
    {
        var result = _controller.Page02(1) as ViewResult;

        result!.Model.Should().BeOfType<Page02ViewData>();
    }

    [Fact]
    public void Page02Get_WhenRegistrationAvailable_PopulatesTshirtSizes()
    {
        var result = _controller.Page02(1) as ViewResult;
        var viewData = result!.Model as Page02ViewData;

        viewData!.TshirtSizes.Should().NotBeNull();
    }

    [Fact]
    public void Page02Get_WhenRegistrationAvailable_PopulatesProblemChoices()
    {
        var result = _controller.Page02(1) as ViewResult;
        var viewData = result!.Model as Page02ViewData;

        viewData!.ProblemChoices.Should().NotBeNull();
    }

    [Fact]
    public void Page02Get_WhenRegistrationAvailable_CallsSetBaseViewData()
    {
        _controller.Page02(1);

        _mockRepo.Verify(r => r.Config, Times.AtLeastOnce);
        _mockRepo.Verify(r => r.TournamentInfo, Times.AtLeastOnce);
    }

    [Fact]
    public void Page02Get_WhenRegistrationClosed_RedirectsToClosedPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithClosedRegistration());

        var result = _controller.Page02(1);

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Closed");
    }

    [Fact]
    public void Page02Get_WhenRegistrationDown_RedirectsToDownPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithDownRegistration());

        var result = _controller.Page02(1);

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Down");
    }

    [Fact]
    public void Page02Get_WhenRegistrationComingSoon_RedirectsToSoonPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithComingSoonRegistration());

        var result = _controller.Page02(1);

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Soon");
    }

    #endregion

    #region Page02 POST Tests

    [Fact]
    public void Page02Post_WhenRegistrationAvailable_AndModelStateValid_UpdatesJudge()
    {
        var viewData = CreateValidPage02ViewData();

        _controller.Page02(1, viewData);

        _mockRepo.Verify(r => r.UpdateJudge(1, 2, It.IsAny<Judge>()), Times.Once);
    }

    [Fact]
    public void Page02Post_WhenRegistrationAvailable_AndModelStateValid_RedirectsToPage03()
    {
        var viewData = CreateValidPage02ViewData();

        var result = _controller.Page02(1, viewData);

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Page03");
        redirectResult.RouteValues.Should().ContainKey("id");
        redirectResult.RouteValues!["id"].Should().Be(1);
    }

    [Fact]
    public void Page02Post_WhenRegistrationAvailable_AndModelStateValid_MapsAllFields()
    {
        var viewData = CreateValidPage02ViewData();
        Judge? capturedJudge = null;
        _mockRepo.Setup(r => r.UpdateJudge(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Judge>()))
            .Callback<int, int, Judge>((id, page, j) => capturedJudge = j);

        _controller.Page02(1, viewData);

        capturedJudge.Should().NotBeNull();
        capturedJudge!.FirstName.Should().Be(viewData.FirstName);
        capturedJudge.LastName.Should().Be(viewData.LastName);
        capturedJudge.Address.Should().Be(viewData.Address);
        capturedJudge.AddressLine2.Should().Be(viewData.AddressLine2);
        capturedJudge.City.Should().Be(viewData.City);
        capturedJudge.State.Should().Be(viewData.State);
        capturedJudge.ZipCode.Should().Be(viewData.ZipCode);
        capturedJudge.EveningPhone.Should().Be(viewData.EveningPhone);
        capturedJudge.DaytimePhone.Should().Be(viewData.DaytimePhone);
        capturedJudge.MobilePhone.Should().Be(viewData.MobilePhone);
        capturedJudge.EmailAddress.Should().Be(viewData.EmailAddress);
    }

    [Fact]
    public void Page02Post_WhenEmailIsEmpty_SetsEmailToNone()
    {
        var viewData = CreateValidPage02ViewData();
        viewData.EmailAddress = "";
        Judge? capturedJudge = null;
        _mockRepo.Setup(r => r.UpdateJudge(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Judge>()))
            .Callback<int, int, Judge>((id, page, j) => capturedJudge = j);

        _controller.Page02(1, viewData);

        capturedJudge!.EmailAddress.Should().Be("None");
    }

    [Fact]
    public void Page02Post_WhenEmailIsWhitespace_SetsEmailToNone()
    {
        var viewData = CreateValidPage02ViewData();
        viewData.EmailAddress = "   ";
        Judge? capturedJudge = null;
        _mockRepo.Setup(r => r.UpdateJudge(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Judge>()))
            .Callback<int, int, Judge>((id, page, j) => capturedJudge = j);

        _controller.Page02(1, viewData);

        capturedJudge!.EmailAddress.Should().Be("None");
    }

    [Fact]
    public void Page02Post_WhenEmailIsNull_SetsEmailToNone()
    {
        var viewData = CreateValidPage02ViewData();
        viewData.EmailAddress = null;
        Judge? capturedJudge = null;
        _mockRepo.Setup(r => r.UpdateJudge(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Judge>()))
            .Callback<int, int, Judge>((id, page, j) => capturedJudge = j);

        _controller.Page02(1, viewData);

        capturedJudge!.EmailAddress.Should().Be("None");
    }

    [Fact]
    public void Page02Post_WhenModelStateInvalid_ReturnsViewWithSameData()
    {
        var viewData = CreateValidPage02ViewData();
        _controller.ModelState.AddModelError("FirstName", "Required");

        var result = _controller.Page02(1, viewData) as ViewResult;

        result.Should().NotBeNull();
        result!.Model.Should().Be(viewData);
    }

    [Fact]
    public void Page02Post_WhenModelStateInvalid_DoesNotUpdateJudge()
    {
        var viewData = CreateValidPage02ViewData();
        _controller.ModelState.AddModelError("FirstName", "Required");

        _controller.Page02(1, viewData);

        _mockRepo.Verify(r => r.UpdateJudge(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Judge>()), Times.Never);
    }

    [Fact]
    public void Page02Post_WhenModelStateInvalid_RepopulatesTshirtSizes()
    {
        var viewData = CreateValidPage02ViewData();
        _controller.ModelState.AddModelError("FirstName", "Required");

        var result = _controller.Page02(1, viewData) as ViewResult;
        var returnedData = result!.Model as Page02ViewData;

        returnedData!.TshirtSizes.Should().NotBeNull();
    }

    [Fact]
    public void Page02Post_WhenModelStateInvalid_RepopulatesProblemChoices()
    {
        var viewData = CreateValidPage02ViewData();
        _controller.ModelState.AddModelError("FirstName", "Required");

        var result = _controller.Page02(1, viewData) as ViewResult;
        var returnedData = result!.Model as Page02ViewData;

        returnedData!.ProblemChoices.Should().NotBeNull();
    }

    [Fact]
    public void Page02Post_WhenRegistrationClosed_RedirectsToClosedPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithClosedRegistration());
        var viewData = CreateValidPage02ViewData();

        var result = _controller.Page02(1, viewData);

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Closed");
    }

    [Fact]
    public void Page02Post_WhenRegistrationDown_RedirectsToDownPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithDownRegistration());
        var viewData = CreateValidPage02ViewData();

        var result = _controller.Page02(1, viewData);

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Down");
    }

    [Fact]
    public void Page02Post_WhenRegistrationComingSoon_RedirectsToSoonPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithComingSoonRegistration());
        var viewData = CreateValidPage02ViewData();

        var result = _controller.Page02(1, viewData);

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Soon");
    }

    [Fact]
    public void Page02Post_WhenUpdateJudgeThrowsException_RedirectsToHomeIndex()
    {
        var viewData = CreateValidPage02ViewData();
        _mockRepo.Setup(r => r.UpdateJudge(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Judge>()))
            .Throws(new InvalidOperationException("Database error"));

        var result = _controller.Page02(1, viewData);

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Index");
        redirectResult.ControllerName.Should().Be("Home");
    }

    #endregion

    #region Page03 GET Tests

    [Fact]
    public void Page03Get_WhenRegistrationAvailable_ReturnsViewResult()
    {
        SetupValidJudge(1);

        var result = _controller.Page03(1);

        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public void Page03Get_WhenRegistrationAvailable_ReturnsPage03ViewData()
    {
        SetupValidJudge(1);

        var result = _controller.Page03(1) as ViewResult;

        result!.Model.Should().BeOfType<Page03ViewData>();
    }

    [Fact]
    public void Page03Get_WhenRegistrationAvailable_LoadsJudgeById()
    {
        SetupValidJudge(1);

        _controller.Page03(1);

        _mockRepo.Verify(r => r.GetJudgeById(1), Times.Once);
    }

    [Fact]
    public void Page03Get_WhenRegistrationAvailable_UpdatesJudgePage()
    {
        SetupValidJudge(1);

        _controller.Page03(1);

        _mockRepo.Verify(r => r.UpdateJudge(1, 3, It.IsAny<Judge>()), Times.Once);
    }

    [Fact]
    public void Page03Get_WhenRegistrationAvailable_CallsSetBaseViewData()
    {
        SetupValidJudge(1);

        _controller.Page03(1);

        _mockRepo.Verify(r => r.Config, Times.AtLeastOnce);
        _mockRepo.Verify(r => r.TournamentInfo, Times.AtLeastOnce);
    }

    [Fact]
    public void Page03Get_WhenJudgeNotFound_ReturnsViewWithError()
    {
        _mockRepo.Setup(r => r.GetJudgeById(It.IsAny<int>()))
            .Returns(new List<Judge>().AsQueryable());

        var result = _controller.Page03(999) as ViewResult;
        var viewData = result!.Model as Page03ViewData;

        viewData!.ErrorMessage.Should().Contain("registration failed");
    }

    [Fact]
    public void Page03Get_WhenJudgeNotFound_DoesNotUpdateJudge()
    {
        _mockRepo.Setup(r => r.GetJudgeById(It.IsAny<int>()))
            .Returns(new List<Judge>().AsQueryable());

        _controller.Page03(999);

        _mockRepo.Verify(r => r.UpdateJudge(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Judge>()), Times.Never);
    }

    [Fact]
    public void Page03Get_WhenEmailProvided_SetsEmailAddressWasSpecifiedTrue()
    {
        SetupValidJudge(1, "test@example.com");

        var result = _controller.Page03(1) as ViewResult;
        var viewData = result!.Model as Page03ViewData;

        viewData!.EmailAddressWasSpecified.Should().BeTrue();
    }

    [Fact]
    public void Page03Get_WhenEmailIsNone_SetsEmailAddressWasSpecifiedFalse()
    {
        SetupValidJudge(1, "None");

        var result = _controller.Page03(1) as ViewResult;
        var viewData = result!.Model as Page03ViewData;

        viewData!.EmailAddressWasSpecified.Should().BeFalse();
    }

    [Fact]
    public void Page03Get_WhenEmailIsEmpty_SetsEmailAddressWasSpecifiedFalse()
    {
        SetupValidJudge(1, "");

        var result = _controller.Page03(1) as ViewResult;
        var viewData = result!.Model as Page03ViewData;

        viewData!.EmailAddressWasSpecified.Should().BeFalse();
    }

    [Fact]
    public void Page03Get_WhenEmailIsWhitespace_SetsEmailAddressWasSpecifiedFalse()
    {
        SetupValidJudge(1, "   ");

        var result = _controller.Page03(1) as ViewResult;
        var viewData = result!.Model as Page03ViewData;

        viewData!.EmailAddressWasSpecified.Should().BeFalse();
    }

    [Fact]
    public void Page03Get_WhenRegistrationClosed_RedirectsToClosedPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithClosedRegistration());

        var result = _controller.Page03(1);

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Closed");
    }

    [Fact]
    public void Page03Get_WhenRegistrationDown_RedirectsToDownPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithDownRegistration());

        var result = _controller.Page03(1);

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Down");
    }

    [Fact]
    public void Page03Get_WhenRegistrationComingSoon_RedirectsToSoonPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithComingSoonRegistration());

        var result = _controller.Page03(1);

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Soon");
    }

    #endregion

    #region Page03 POST Tests

    [Fact]
    public void Page03Post_WhenRestartButtonClicked_RedirectsToPage01()
    {
        var formCollection = new Microsoft.AspNetCore.Http.FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>());

        var result = _controller.Page03(1, null!, null!, null!, "restart", formCollection);

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Page01");
    }

    [Fact]
    public void Page03Post_WhenSubmitButtonClicked_UpdatesJudgeEmail()
    {
        var formData = new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
        {
            { "NewEmailTextBox", "newemail@example.com" }
        };
        var formCollection = new Microsoft.AspNetCore.Http.FormCollection(formData);

        _controller.Page03(1, "submit", null!, null!, null!, formCollection);

        _mockRepo.Verify(r => r.UpdateJudgeEmail(1, "newemail@example.com"), Times.Once);
    }

    [Fact]
    public void Page03Post_WhenSubmitButtonClicked_CallsPage03GetAgain()
    {
        SetupValidJudge(1);
        var formData = new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
        {
            { "NewEmailTextBox", "newemail@example.com" }
        };
        var formCollection = new Microsoft.AspNetCore.Http.FormCollection(formData);

        var result = _controller.Page03(1, "submit", null!, null!, null!, formCollection);

        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public void Page03Post_WhenNoSpecialButtonClicked_RedirectsToHomePage()
    {
        var formCollection = new Microsoft.AspNetCore.Http.FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>());

        var result = _controller.Page03(1, null!, null!, null!, null!, formCollection);

        result.Should().BeOfType<RedirectResult>();
        var redirectResult = result as RedirectResult;
        redirectResult!.Url.Should().Be("http://www.novanorth.org");
    }

    [Fact]
    public void Page03Post_WhenNoSpecialButtonClicked_LoadsHomePageFromConfig()
    {
        var formCollection = new Microsoft.AspNetCore.Http.FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>());

        _controller.Page03(1, null!, null!, null!, null!, formCollection);

        _mockRepo.Verify(r => r.Config, Times.AtLeastOnce);
    }

    [Fact]
    public void Page03Post_WhenRegistrationClosed_RedirectsToClosedPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithClosedRegistration());
        var formCollection = new Microsoft.AspNetCore.Http.FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>());

        var result = _controller.Page03(1, null!, null!, null!, null!, formCollection);

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Closed");
    }

    [Fact]
    public void Page03Post_WhenRegistrationDown_RedirectsToDownPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithDownRegistration());
        var formCollection = new Microsoft.AspNetCore.Http.FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>());

        var result = _controller.Page03(1, null!, null!, null!, null!, formCollection);

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Down");
    }

    [Fact]
    public void Page03Post_WhenRegistrationComingSoon_RedirectsToSoonPage()
    {
        _mockRepo.Setup(r => r.Config).Returns(CreateConfigWithComingSoonRegistration());
        var formCollection = new Microsoft.AspNetCore.Http.FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>());

        var result = _controller.Page03(1, null!, null!, null!, null!, formCollection);

        result.Should().BeOfType<RedirectToActionResult>();
        var redirectResult = result as RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Soon");
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

    private static Dictionary<string, string> CreateConfigWithClosedRegistration()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["JudgesRegistrationOpenDateTime"] = "01/01/2020 00:00:00";
        config["JudgesRegistrationCloseDateTime"] = "01/02/2020 00:00:00"; // Closed
        return config;
    }

    private static Dictionary<string, string> CreateConfigWithDownRegistration()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["IsJudgesRegistrationDown"] = "true";
        return config;
    }

    private static Dictionary<string, string> CreateConfigWithComingSoonRegistration()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["JudgesRegistrationOpenDateTime"] = "12/31/2099 23:59:59"; // Far future
        config["JudgesRegistrationCloseDateTime"] = "12/31/2100 23:59:59";
        return config;
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

    private Page02ViewData CreateValidPage02ViewData()
    {
        return new Page02ViewData
        {
            FirstName = "John",
            LastName = "Doe",
            Address = "123 Main St",
            AddressLine2 = "Apt 4B",
            City = "Springfield",
            State = "VA",
            ZipCode = "22150",
            EveningPhone = "703-555-1234",
            DaytimePhone = "703-555-5678",
            MobilePhone = "703-555-9012",
            EmailAddress = "john.doe@example.com",
            ProblemChoice1 = "1",
            ProblemChoice2 = "2",
            ProblemChoice3 = "3",
            HasChildrenCompeting = "false",
            ProblemConflict1 = "0",
            ProblemConflict2 = "0",
            ProblemConflict3 = "0",
            YearsOfLongTermJudgingExperience = "5",
            YearsOfSpontaneousJudgingExperience = "3",
            PreviouslyHeadJudge = false,
            PreviouslyProblemJudge = true,
            PreviouslyStyleJudge = false,
            PreviouslyStagingJudge = false,
            PreviouslyTimekeeper = false,
            PreviouslyScorechecker = false,
            PreviouslyWeighInJudge = false,
            WillingToBeScorechecker = "true",
            WantsCeuCredit = "false",
            Notes = "Looking forward to judging!",
            TshirtSizes = [],
            ProblemChoices = []
        };
    }

    private void SetupValidJudge(int judgeId, string email = "test@example.com")
    {
        var judge = new Judge
        {
            JudgeID = judgeId,
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = email
        };
        _mockRepo.Setup(r => r.GetJudgeById(judgeId))
            .Returns(new List<Judge> { judge }.AsQueryable());
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
