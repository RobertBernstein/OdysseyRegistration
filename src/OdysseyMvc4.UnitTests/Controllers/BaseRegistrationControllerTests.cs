using FluentAssertions;
using Moq;
using OdysseyMvc4.Controllers;
using OdysseyMvc4.Models;
using OdysseyMvc4.UnitTests.Helpers;
using System.Web.Mvc;
using static OdysseyMvc4.Controllers.BaseRegistrationController;
using System.Globalization;

namespace OdysseyMvc4.UnitTests.Controllers;

/// <summary>
/// Tests for the business logic in BaseRegistrationController that must be preserved
/// during the migration from .NET Framework 4.8 (OdysseyMvc4) to .NET 10 (OdysseyMvc2024).
/// </summary>
public class BaseRegistrationControllerTests
{
    private readonly Mock<IOdysseyRepository> _mockRepo;

    public BaseRegistrationControllerTests()
    {
        _mockRepo = TestHelper.CreateMockRepository();
    }

    #region GetDisplayableRegistrationName Tests

    [Fact]
    public void GetDisplayableRegistrationName_WhenNone_ReturnsEmptyString()
    {
        using var controller = CreateController(RegistrationType.None);
        controller.GetDisplayableRegistrationName().Should().BeEmpty();
    }

    [Fact]
    public void GetDisplayableRegistrationName_WhenTournament_ReturnsTournamentRegistration()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        controller.GetDisplayableRegistrationName().Should().Be("Tournament Registration");
    }

    [Fact]
    public void GetDisplayableRegistrationName_WhenJudges_ReturnsJudgesRegistration()
    {
        using var controller = CreateController(RegistrationType.Judges);
        controller.GetDisplayableRegistrationName().Should().Be("Judges Registration");
    }

    [Fact]
    public void GetDisplayableRegistrationName_WhenCoachesTraining_ReturnsCoachesTrainingRegistration()
    {
        using var controller = CreateController(RegistrationType.CoachesTraining);
        controller.GetDisplayableRegistrationName().Should().Be("Coaches Training Registration");
    }

    [Fact]
    public void GetDisplayableRegistrationName_WhenVolunteer_ReturnsVolunteerRegistration()
    {
        using var controller = CreateController(RegistrationType.Volunteer);
        controller.GetDisplayableRegistrationName().Should().Be("Volunteer Registration");
    }

    #endregion

    #region RegistrationType Enum Tests

    [Fact]
    public void RegistrationType_HasExpectedValues()
    {
        ((RegistrationType[])Enum.GetValues(typeof(RegistrationType))).Should().HaveCount(5);
        ((int)RegistrationType.None).Should().Be(0);
        ((int)RegistrationType.Tournament).Should().Be(1);
        ((int)RegistrationType.Judges).Should().Be(2);
        ((int)RegistrationType.CoachesTraining).Should().Be(3);
        ((int)RegistrationType.Volunteer).Should().Be(4);
    }

    #endregion

    #region RegistrationState Enum Tests

    [Fact]
    public void RegistrationState_HasExpectedValues()
    {
        ((RegistrationState[])Enum.GetValues(typeof(RegistrationState))).Should().HaveCount(4);
        ((int)RegistrationState.Available).Should().Be(0);
        ((int)RegistrationState.Closed).Should().Be(1);
        ((int)RegistrationState.Down).Should().Be(2);
        ((int)RegistrationState.Soon).Should().Be(3);
    }

    #endregion

    #region IsRegistrationClosed Tests

    [Fact]
    public void IsRegistrationClosed_WhenCloseDateInFuture_ReturnsFalse()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationCloseDateTime"] =
            DateTime.Now.AddDays(30).ToString(CultureInfo.InvariantCulture);

        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Tournament);
        controller.IsRegistrationClosed(RegistrationType.Tournament).Should().BeFalse();
    }

    [Fact]
    public void IsRegistrationClosed_WhenCloseDateInPast_ReturnsTrue()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationCloseDateTime"] = DateTime.Now.AddDays(-1).ToString(CultureInfo.InvariantCulture);
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Tournament);
        controller.IsRegistrationClosed(RegistrationType.Tournament).Should().BeTrue();
    }

    [Fact]
    public void IsRegistrationClosed_WhenConfigValueInvalid_ReturnsTrue()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationCloseDateTime"] = "not-a-date";
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Tournament);
        controller.IsRegistrationClosed(RegistrationType.Tournament).Should().BeTrue();
    }

    [Fact]
    public void IsRegistrationClosed_ForJudges_UsesJudgesConfig()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["JudgesRegistrationCloseDateTime"] = DateTime.Now.AddDays(30).ToString(CultureInfo.InvariantCulture);
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Judges);
        controller.IsRegistrationClosed(RegistrationType.Judges).Should().BeFalse();
    }

    #endregion

    #region IsRegistrationComingSoon Tests

    [Fact]
    public void IsRegistrationComingSoon_WhenOpenDateInFuture_ReturnsTrue()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(30).ToString(CultureInfo.InvariantCulture);
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Tournament);
        controller.IsRegistrationComingSoon(RegistrationType.Tournament).Should().BeTrue();
    }

    [Fact]
    public void IsRegistrationComingSoon_WhenOpenDateInPast_ReturnsFalse()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(-30).ToString(CultureInfo.InvariantCulture);
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Tournament);
        controller.IsRegistrationComingSoon(RegistrationType.Tournament).Should().BeFalse();
    }

    [Fact]
    public void IsRegistrationComingSoon_WhenConfigValueInvalid_ReturnsFalse()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = "invalid-date";
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Tournament);
        controller.IsRegistrationComingSoon(RegistrationType.Tournament).Should().BeFalse();
    }

    #endregion

    #region IsRegistrationDown Tests

    [Fact]
    public void IsRegistrationDown_WhenConfigIsTrue_ReturnsTrue()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["IsTournamentRegistrationDown"] = "true";
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Tournament);
        controller.IsRegistrationDown(RegistrationType.Tournament).Should().BeTrue();
    }

    [Fact]
    public void IsRegistrationDown_WhenConfigIsFalse_ReturnsFalse()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["IsTournamentRegistrationDown"] = "false";
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Tournament);
        controller.IsRegistrationDown(RegistrationType.Tournament).Should().BeFalse();
    }

    [Fact]
    public void IsRegistrationDown_WhenConfigIsInvalid_ReturnsFalse()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["IsTournamentRegistrationDown"] = "not-a-boolean";
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Tournament);
        controller.IsRegistrationDown(RegistrationType.Tournament).Should().BeFalse();
    }

    [Fact]
    public void IsRegistrationDown_ForJudges_UsesJudgesConfig()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["IsJudgesRegistrationDown"] = "true";
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Judges);
        controller.IsRegistrationDown(RegistrationType.Judges).Should().BeTrue();
    }

    #endregion

    #region CurrentRegistrationState Tests

    [Fact]
    public void CurrentRegistrationState_WhenAllOpen_ReturnsAvailable()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(-30).ToString(CultureInfo.InvariantCulture);
        config["TournamentRegistrationCloseDateTime"] = DateTime.Now.AddDays(30).ToString(CultureInfo.InvariantCulture);
        config["IsTournamentRegistrationDown"] = "false";
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Tournament);
        controller.CurrentRegistrationState.Should().Be(RegistrationState.Available);
    }

    [Fact]
    public void CurrentRegistrationState_WhenComingSoon_ReturnsSoon()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(30).ToString(CultureInfo.InvariantCulture);
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Tournament);
        controller.CurrentRegistrationState.Should().Be(RegistrationState.Soon);
    }

    [Fact]
    public void CurrentRegistrationState_WhenDown_ReturnsDown()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(-30).ToString(CultureInfo.InvariantCulture);
        config["IsTournamentRegistrationDown"] = "true";
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Tournament);
        controller.CurrentRegistrationState.Should().Be(RegistrationState.Down);
    }

    [Fact]
    public void CurrentRegistrationState_WhenClosed_ReturnsClosed()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(-60).ToString(CultureInfo.InvariantCulture);
        config["TournamentRegistrationCloseDateTime"] = DateTime.Now.AddDays(-1).ToString(CultureInfo.InvariantCulture);
        config["IsTournamentRegistrationDown"] = "false";
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Tournament);
        controller.CurrentRegistrationState.Should().Be(RegistrationState.Closed);
    }

    [Fact]
    public void CurrentRegistrationState_SoonTakesPriorityOverDown()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(30).ToString(CultureInfo.InvariantCulture);
        config["IsTournamentRegistrationDown"] = "true";
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Tournament);
        controller.CurrentRegistrationState.Should().Be(RegistrationState.Soon);
    }

    [Fact]
    public void CurrentRegistrationState_DownTakesPriorityOverClosed()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(-60).ToString(CultureInfo.InvariantCulture);
        config["TournamentRegistrationCloseDateTime"] = DateTime.Now.AddDays(-1).ToString(CultureInfo.InvariantCulture);
        config["IsTournamentRegistrationDown"] = "true";
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Tournament);
        controller.CurrentRegistrationState.Should().Be(RegistrationState.Down);
    }

    [Fact]
    public void CurrentRegistrationState_CallsSetBaseViewData()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(-30).ToString(CultureInfo.InvariantCulture);
        config["TournamentRegistrationCloseDateTime"] = DateTime.Now.AddDays(30).ToString(CultureInfo.InvariantCulture);
        config["IsTournamentRegistrationDown"] = "false";
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Tournament);
        var state = controller.CurrentRegistrationState;

        state.Should().Be(RegistrationState.Available);
        _mockRepo.Verify(r => r.Config, Times.AtLeastOnce);
        _mockRepo.Verify(r => r.TournamentInfo, Times.AtLeastOnce);
    }

    [Fact]
    public void CurrentRegistrationState_ForJudges_UsesJudgesConfiguration()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["JudgesRegistrationOpenDateTime"] = DateTime.Now.AddDays(-30).ToString(CultureInfo.InvariantCulture);
        config["JudgesRegistrationCloseDateTime"] = DateTime.Now.AddDays(30).ToString(CultureInfo.InvariantCulture);
        config["IsJudgesRegistrationDown"] = "false";
        _mockRepo.Setup(r => r.Config).Returns(config);

        using var controller = CreateController(RegistrationType.Judges);
        controller.CurrentRegistrationState.Should().Be(RegistrationState.Available);
    }

    #endregion

    #region BuildMessage Tests

    [Fact]
    public void BuildMessage_WithValidInputs_ReturnsMailMessage()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "to@test.com", null!, null!);

        message.Should().NotBeNull();
        message!.From!.Address.Should().Be("from@test.com");
        message.Subject.Should().Be("Subject");
        message.Body.Should().Be("Body");
        message.IsBodyHtml.Should().BeTrue();
        message.To.Should().HaveCount(1);
        message.To[0].Address.Should().Be("to@test.com");
    }

    [Fact]
    public void BuildMessage_WithMultipleRecipients_SplitsOnComma()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "to1@test.com,to2@test.com", null!, null!);

        message.Should().NotBeNull();
        message!.To.Should().HaveCount(2);
        message.To[0].Address.Should().Be("to1@test.com");
        message.To[1].Address.Should().Be("to2@test.com");
    }

    [Fact]
    public void BuildMessage_WithBcc_SetsBcc()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "to@test.com", "bcc@test.com", null!);

        message.Should().NotBeNull();
        message!.Bcc.Should().HaveCount(1);
        message.Bcc[0].Address.Should().Be("bcc@test.com");
    }

    [Fact]
    public void BuildMessage_WithCc_SetsCc()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "to@test.com", null!, "cc@test.com");

        message.Should().NotBeNull();
        message!.CC.Should().HaveCount(1);
        message.CC[0].Address.Should().Be("cc@test.com");
    }

    [Fact]
    public void BuildMessage_WithEmptyBccAndCc_DoesNotSetBccOrCc()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "to@test.com", "", "");

        message.Should().NotBeNull();
        message!.Bcc.Should().BeEmpty();
        message.CC.Should().BeEmpty();
    }

    [Fact]
    public void BuildMessage_WithWhitespaceBccAndCc_DoesNotSetBccOrCc()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "to@test.com", "   ", "   ");

        message.Should().NotBeNull();
        message!.Bcc.Should().BeEmpty();
        message.CC.Should().BeEmpty();
    }

    [Fact]
    public void BuildMessage_WithInvalidRecipient_ReturnsNull()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "not-an-email", null!, null!);

        message.Should().BeNull();
    }

    [Fact]
    public void BuildMessage_SetsNormalPriority()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "to@test.com", null!, null!);

        message.Should().NotBeNull();
        message!.Priority.Should().Be(System.Net.Mail.MailPriority.Normal);
    }

    [Fact]
    public void BuildMessage_WithThreeRecipients_AddsAllRecipients()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "to1@test.com,to2@test.com,to3@test.com", null!, null!);

        message.Should().NotBeNull();
        message!.To.Should().HaveCount(3);
        message.To[0].Address.Should().Be("to1@test.com");
        message.To[1].Address.Should().Be("to2@test.com");
        message.To[2].Address.Should().Be("to3@test.com");
    }

    [Fact]
    public void BuildMessage_WithMixOfValidAndInvalidRecipients_ReturnsNull()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "valid@test.com,invalid-email", null!, null!);

        message.Should().BeNull();
    }

    [Fact]
    public void BuildMessage_WithBothBccAndCc_SetsBothFields()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "to@test.com", "bcc@test.com", "cc@test.com");

        message.Should().NotBeNull();
        message!.Bcc.Should().HaveCount(1);
        message.Bcc[0].Address.Should().Be("bcc@test.com");
        message.CC.Should().HaveCount(1);
        message.CC[0].Address.Should().Be("cc@test.com");
    }

    [Fact]
    public void BuildMessage_WithNullBccAndNullCc_DoesNotSetBccOrCc()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "to@test.com", null!, null!);

        message.Should().NotBeNull();
        message!.Bcc.Should().BeEmpty();
        message.CC.Should().BeEmpty();
    }

    #endregion

    #region BadEmail Tests

    [Fact]
    public void BadEmail_ReturnsViewResultWithCorrectPath()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.BadEmail();

        result.Should().NotBeNull();
        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.ViewName.Should().Be("~/Views/Shared/BadEmail.cshtml");
    }

    [Fact]
    public void BadEmail_SetsBaseViewData()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.BadEmail();

        result.Should().NotBeNull();
        _mockRepo.Verify(r => r.Config, Times.AtLeastOnce);
        _mockRepo.Verify(r => r.TournamentInfo, Times.AtLeastOnce);
    }

    #endregion

    #region Closed Tests

    [Fact]
    public void Closed_ReturnsViewResult()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.Closed();

        result.Should().NotBeNull();
        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public void Closed_ReturnsViewDataAsModel()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.Closed();

        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().NotBeNull();
        viewResult.Model.Should().BeOfType<OdysseyMvc4.ViewData.BaseViewData>();
    }

    [Fact]
    public void Closed_SetsBaseViewData()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.Closed();

        result.Should().NotBeNull();
        result.Should().BeOfType<ViewResult>();
        _mockRepo.Verify(r => r.Config, Times.AtLeastOnce);
        _mockRepo.Verify(r => r.TournamentInfo, Times.AtLeastOnce);
    }

    #endregion

    #region ClosedPost Tests

    [Fact]
    public void ClosedPost_RedirectsToHomePage()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.ClosedPost();

        result.Should().NotBeNull();
        var redirectResult = result.Should().BeOfType<RedirectResult>().Subject;
        redirectResult.Url.Should().Be("http://www.novanorth.org");
    }

    [Fact]
    public void ClosedPost_SetsBaseViewData()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.ClosedPost();

        result.Should().NotBeNull();
        result.Should().BeOfType<ViewResult>();
        _mockRepo.Verify(r => r.Config, Times.AtLeastOnce);
        _mockRepo.Verify(r => r.TournamentInfo, Times.AtLeastOnce);
    }

    #endregion

    #region Down Tests

    [Fact]
    public void Down_ReturnsViewResult()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.Down();

        result.Should().NotBeNull();
        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public void Down_ReturnsViewDataAsModel()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.Down();

        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().NotBeNull();
        viewResult.Model.Should().BeOfType<OdysseyMvc4.ViewData.BaseViewData>();
    }

    [Fact]
    public void Down_SetsBaseViewData()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.Down();

        result.Should().NotBeNull();
        result.Should().BeOfType<ViewResult>();
        _mockRepo.Verify(r => r.Config, Times.AtLeastOnce);
        _mockRepo.Verify(r => r.TournamentInfo, Times.AtLeastOnce);
    }

    #endregion

    #region DownPost Tests

    [Fact]
    public void DownPost_RedirectsToHomePage()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.DownPost();

        result.Should().NotBeNull();
        var redirectResult = result.Should().BeOfType<RedirectResult>().Subject;
        redirectResult.Url.Should().Be("http://www.novanorth.org");
    }

    [Fact]
    public void DownPost_SetsBaseViewData()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.DownPost();

        result.Should().NotBeNull();
        result.Should().BeOfType<ViewResult>();
        _mockRepo.Verify(r => r.Config, Times.AtLeastOnce);
        _mockRepo.Verify(r => r.TournamentInfo, Times.AtLeastOnce);
    }

    #endregion

    #region Error Tests

    [Fact]
    public void Error_ReturnsViewResult()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.Error();

        result.Should().NotBeNull();
        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public void Error_ReturnsViewDataAsModel()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.Error();

        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().NotBeNull();
        viewResult.Model.Should().BeOfType<OdysseyMvc4.ViewData.BaseViewData>();
    }

    [Fact]
    public void Error_SetsBaseViewData()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.Error();

        result.Should().NotBeNull();
        result.Should().BeOfType<ViewResult>();
        _mockRepo.Verify(r => r.Config, Times.AtLeastOnce);
        _mockRepo.Verify(r => r.TournamentInfo, Times.AtLeastOnce);
    }

    #endregion

    #region SendMessage Tests
    // Note: SendMessage creates SmtpClient internally and cannot be fully unit tested without:
    // 1. A test SMTP server (integration test)
    // 2. Refactoring to inject SmtpClient via dependency injection
    // The tests below verify the method signature and error handling structure.

    [Fact(Skip = "Requires SMTP server infrastructure - integration test needed")]
    public void SendMessage_WithValidConfiguration_ReturnsNullOnSuccess()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var viewData = new OdysseyMvc4.ViewData.BaseViewData();
        controller.TestSetBaseViewData(viewData);

        using var mailMessage = new System.Net.Mail.MailMessage("from@test.com", "to@test.com")
        {
            Subject = "Test Subject",
            Body = "Test Body"
        };

        var result = controller.TestSendMessage(viewData, mailMessage);

        result.Should().BeNull();
    }

    #endregion

    #region SetBaseViewData Tests

    [Fact]
    public void SetBaseViewData_SetsConfig()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var viewData = new OdysseyMvc4.ViewData.BaseViewData();

        controller.TestSetBaseViewData(viewData);

        viewData.Config.Should().NotBeNull();
        viewData.Config.Should().BeSameAs(_mockRepo.Object.Config);
    }

    [Fact]
    public void SetBaseViewData_SetsRegionName()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var viewData = new OdysseyMvc4.ViewData.BaseViewData();

        controller.TestSetBaseViewData(viewData);

        viewData.RegionName.Should().Be("NoVA North");
    }

    [Fact]
    public void SetBaseViewData_SetsRegionNumber()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var viewData = new OdysseyMvc4.ViewData.BaseViewData();

        controller.TestSetBaseViewData(viewData);

        viewData.RegionNumber.Should().Be("9");
    }

    [Fact]
    public void SetBaseViewData_SetsTournamentInfo()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var viewData = new OdysseyMvc4.ViewData.BaseViewData();

        controller.TestSetBaseViewData(viewData);

        viewData.TournamentInfo.Should().NotBeNull();
        viewData.TournamentInfo.Should().BeSameAs(_mockRepo.Object.TournamentInfo);
    }

    [Fact]
    public void SetBaseViewData_SetsFriendlyRegistrationName()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var viewData = new OdysseyMvc4.ViewData.BaseViewData();

        controller.TestSetBaseViewData(viewData);

        viewData.FriendlyRegistrationName.Should().Be("Tournament Registration");
    }

    [Fact]
    public void SetBaseViewData_SetsSiteName()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var viewData = new OdysseyMvc4.ViewData.BaseViewData();

        controller.TestSetBaseViewData(viewData);

        viewData.SiteName.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void SetBaseViewData_SetsPathToSiteCssFile()
    {
        using var controller = CreateController(RegistrationType.Tournament);
        var viewData = new OdysseyMvc4.ViewData.BaseViewData();

        controller.TestSetBaseViewData(viewData);

        viewData.PathToSiteCssFile.Should().NotBeNullOrEmpty();
    }

    #endregion

    #region Soon Tests

    [Fact]
    public void Soon_ReturnsViewResult()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.Soon();

        result.Should().NotBeNull();
        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public void Soon_ReturnsViewDataAsModel()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.Soon();

        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().NotBeNull();
        viewResult.Model.Should().BeOfType<OdysseyMvc4.ViewData.BaseViewData>();
    }

    [Fact]
    public void Soon_SetsBaseViewData()
    {
        using var controller = CreateController(RegistrationType.Tournament);

        var result = controller.Soon();

        result.Should().NotBeNull();
        result.Should().BeOfType<ViewResult>();
        _mockRepo.Verify(r => r.Config, Times.AtLeastOnce);
        _mockRepo.Verify(r => r.TournamentInfo, Times.AtLeastOnce);
    }

    #endregion

    #region Helper Methods

    /// <summary>
    /// Creates a concrete test controller that inherits from BaseRegistrationController.
    /// </summary>
    private TestableBaseRegistrationController CreateController(RegistrationType registrationType)
    {
        var controller = new TestableBaseRegistrationController(_mockRepo.Object)
        {
            CurrentRegistrationType = registrationType,
            FriendlyRegistrationName = ""
        };
        TestHelper.SetupControllerContext(controller);
        controller.FriendlyRegistrationName = controller.GetDisplayableRegistrationName();
        return controller;
    }

    /// <summary>
    /// Invokes the protected BuildMessage method via the testable subclass.
    /// </summary>
    private static System.Net.Mail.MailMessage? InvokeBuildMessage(
        TestableBaseRegistrationController controller,
        string from, string subject, string body, string to, string bcc, string cc)
    {
        return controller.TestBuildMessage(from, subject, body, to, bcc, cc);
    }

    #endregion
}

/// <summary>
/// Concrete subclass of BaseRegistrationController for testing, since the base class is abstract.
/// </summary>
public class TestableBaseRegistrationController : BaseRegistrationController
{
    public TestableBaseRegistrationController(IOdysseyRepository repository)
        : base(repository)
    {
        FriendlyRegistrationName = "";
    }

    public System.Net.Mail.MailMessage TestBuildMessage(string from, string subject, string body, string to, string bcc, string cc)
        => BuildMessage(from, subject, body, to, bcc, cc);

    public void TestSetBaseViewData(OdysseyMvc4.ViewData.BaseViewData viewData)
        => SetBaseViewData(viewData);

    public string TestSendMessage(OdysseyMvc4.ViewData.BaseViewData viewData, System.Net.Mail.MailMessage mailMessage)
        => SendMessage(viewData, mailMessage);
}
