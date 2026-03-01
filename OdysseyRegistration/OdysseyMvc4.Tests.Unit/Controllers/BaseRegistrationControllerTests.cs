using FluentAssertions;
using Moq;
using OdysseyMvc2024.Controllers;
using OdysseyMvc2024.Models;
using OdysseyMvc4.Tests.Unit.Helpers;
using System.Diagnostics.CodeAnalysis;
using static OdysseyMvc2024.Controllers.BaseRegistrationController;

namespace OdysseyMvc4.Tests.Unit.Controllers;

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

    #region GetFriendlyRegistrationName Tests

    [Fact]
    public void GetFriendlyRegistrationName_WhenNone_ReturnsEmptyString()
    {
        var controller = CreateController(RegistrationType.None);
        controller.GetFriendlyRegistrationName().Should().BeEmpty();
    }

    [Fact]
    public void GetFriendlyRegistrationName_WhenTournament_ReturnsTournamentRegistration()
    {
        var controller = CreateController(RegistrationType.Tournament);
        controller.GetFriendlyRegistrationName().Should().Be("Tournament Registration");
    }

    [Fact]
    public void GetFriendlyRegistrationName_WhenJudges_ReturnsJudgesRegistration()
    {
        var controller = CreateController(RegistrationType.Judges);
        controller.GetFriendlyRegistrationName().Should().Be("Judges Registration");
    }

    [Fact]
    public void GetFriendlyRegistrationName_WhenCoachesTraining_ReturnsCoachesTrainingRegistration()
    {
        var controller = CreateController(RegistrationType.CoachesTraining);
        controller.GetFriendlyRegistrationName().Should().Be("Coaches Training Registration");
    }

    [Fact]
    public void GetFriendlyRegistrationName_WhenVolunteer_ReturnsVolunteerRegistration()
    {
        var controller = CreateController(RegistrationType.Volunteer);
        controller.GetFriendlyRegistrationName().Should().Be("Volunteer Registration");
    }

    #endregion

    #region RegistrationType Enum Tests

    [Fact]
    public void RegistrationType_HasExpectedValues()
    {
        Enum.GetValues<RegistrationType>().Should().HaveCount(5);
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
        Enum.GetValues<RegistrationState>().Should().HaveCount(4);
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
        config["TournamentRegistrationCloseDateTime"] = DateTime.Now.AddDays(30).ToString();
        _mockRepo.Setup(r => r.Config).Returns(config);

        var controller = CreateController(RegistrationType.Tournament);
        controller.IsRegistrationClosed(RegistrationType.Tournament).Should().BeFalse();
    }

    [Fact]
    public void IsRegistrationClosed_WhenCloseDateInPast_ReturnsTrue()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationCloseDateTime"] = DateTime.Now.AddDays(-1).ToString();
        _mockRepo.Setup(r => r.Config).Returns(config);

        var controller = CreateController(RegistrationType.Tournament);
        controller.IsRegistrationClosed(RegistrationType.Tournament).Should().BeTrue();
    }

    [Fact]
    public void IsRegistrationClosed_WhenConfigValueInvalid_ReturnsTrue()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationCloseDateTime"] = "not-a-date";
        _mockRepo.Setup(r => r.Config).Returns(config);

        var controller = CreateController(RegistrationType.Tournament);
        controller.IsRegistrationClosed(RegistrationType.Tournament).Should().BeTrue();
    }

    [Fact]
    public void IsRegistrationClosed_ForJudges_UsesJudgesConfig()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["JudgesRegistrationCloseDateTime"] = DateTime.Now.AddDays(30).ToString();
        _mockRepo.Setup(r => r.Config).Returns(config);

        var controller = CreateController(RegistrationType.Judges);
        controller.IsRegistrationClosed(RegistrationType.Judges).Should().BeFalse();
    }

    #endregion

    #region IsRegistrationComingSoon Tests

    [Fact]
    public void IsRegistrationComingSoon_WhenOpenDateInFuture_ReturnsTrue()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(30).ToString();
        _mockRepo.Setup(r => r.Config).Returns(config);

        var controller = CreateController(RegistrationType.Tournament);
        controller.IsRegistrationComingSoon(RegistrationType.Tournament).Should().BeTrue();
    }

    [Fact]
    public void IsRegistrationComingSoon_WhenOpenDateInPast_ReturnsFalse()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(-30).ToString();
        _mockRepo.Setup(r => r.Config).Returns(config);

        var controller = CreateController(RegistrationType.Tournament);
        controller.IsRegistrationComingSoon(RegistrationType.Tournament).Should().BeFalse();
    }

    [Fact]
    public void IsRegistrationComingSoon_WhenConfigValueInvalid_ReturnsFalse()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = "invalid-date";
        _mockRepo.Setup(r => r.Config).Returns(config);

        var controller = CreateController(RegistrationType.Tournament);
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

        var controller = CreateController(RegistrationType.Tournament);
        controller.IsRegistrationDown(RegistrationType.Tournament).Should().BeTrue();
    }

    [Fact]
    public void IsRegistrationDown_WhenConfigIsFalse_ReturnsFalse()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["IsTournamentRegistrationDown"] = "false";
        _mockRepo.Setup(r => r.Config).Returns(config);

        var controller = CreateController(RegistrationType.Tournament);
        controller.IsRegistrationDown(RegistrationType.Tournament).Should().BeFalse();
    }

    [Fact]
    public void IsRegistrationDown_WhenConfigIsInvalid_ReturnsFalse()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["IsTournamentRegistrationDown"] = "not-a-boolean";
        _mockRepo.Setup(r => r.Config).Returns(config);

        var controller = CreateController(RegistrationType.Tournament);
        controller.IsRegistrationDown(RegistrationType.Tournament).Should().BeFalse();
    }

    [Fact]
    public void IsRegistrationDown_ForJudges_UsesJudgesConfig()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["IsJudgesRegistrationDown"] = "true";
        _mockRepo.Setup(r => r.Config).Returns(config);

        var controller = CreateController(RegistrationType.Judges);
        controller.IsRegistrationDown(RegistrationType.Judges).Should().BeTrue();
    }

    #endregion

    #region CurrentRegistrationState Tests

    [Fact]
    public void CurrentRegistrationState_WhenAllOpen_ReturnsAvailable()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(-30).ToString();
        config["TournamentRegistrationCloseDateTime"] = DateTime.Now.AddDays(30).ToString();
        config["IsTournamentRegistrationDown"] = "false";
        _mockRepo.Setup(r => r.Config).Returns(config);

        var controller = CreateController(RegistrationType.Tournament);
        controller.CurrentRegistrationState.Should().Be(RegistrationState.Available);
    }

    [Fact]
    public void CurrentRegistrationState_WhenComingSoon_ReturnsSoon()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(30).ToString();
        _mockRepo.Setup(r => r.Config).Returns(config);

        var controller = CreateController(RegistrationType.Tournament);
        controller.CurrentRegistrationState.Should().Be(RegistrationState.Soon);
    }

    [Fact]
    public void CurrentRegistrationState_WhenDown_ReturnsDown()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(-30).ToString();
        config["IsTournamentRegistrationDown"] = "true";
        _mockRepo.Setup(r => r.Config).Returns(config);

        var controller = CreateController(RegistrationType.Tournament);
        controller.CurrentRegistrationState.Should().Be(RegistrationState.Down);
    }

    [Fact]
    public void CurrentRegistrationState_WhenClosed_ReturnsClosed()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(-60).ToString();
        config["TournamentRegistrationCloseDateTime"] = DateTime.Now.AddDays(-1).ToString();
        config["IsTournamentRegistrationDown"] = "false";
        _mockRepo.Setup(r => r.Config).Returns(config);

        var controller = CreateController(RegistrationType.Tournament);
        controller.CurrentRegistrationState.Should().Be(RegistrationState.Closed);
    }

    [Fact]
    public void CurrentRegistrationState_SoonTakesPriorityOverDown()
    {
        var config = TestHelper.CreateDefaultConfig();
        config["TournamentRegistrationOpenDateTime"] = DateTime.Now.AddDays(30).ToString();
        config["IsTournamentRegistrationDown"] = "true";
        _mockRepo.Setup(r => r.Config).Returns(config);

        var controller = CreateController(RegistrationType.Tournament);
        controller.CurrentRegistrationState.Should().Be(RegistrationState.Soon);
    }

    #endregion

    #region BuildMessage Tests

    [Fact]
    public void BuildMessage_WithValidInputs_ReturnsMailMessage()
    {
        var controller = CreateController(RegistrationType.Tournament);
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
        var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "to1@test.com,to2@test.com", null!, null!);

        message.Should().NotBeNull();
        message!.To.Should().HaveCount(2);
        message.To[0].Address.Should().Be("to1@test.com");
        message.To[1].Address.Should().Be("to2@test.com");
    }

    [Fact]
    public void BuildMessage_WithBcc_SetsBcc()
    {
        var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "to@test.com", "bcc@test.com", null!);

        message.Should().NotBeNull();
        message!.Bcc.Should().HaveCount(1);
        message.Bcc[0].Address.Should().Be("bcc@test.com");
    }

    [Fact]
    public void BuildMessage_WithCc_SetsCc()
    {
        var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "to@test.com", null!, "cc@test.com");

        message.Should().NotBeNull();
        message!.CC.Should().HaveCount(1);
        message.CC[0].Address.Should().Be("cc@test.com");
    }

    [Fact]
    public void BuildMessage_WithEmptyBccAndCc_DoesNotSetBccOrCc()
    {
        var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "to@test.com", "", "");

        message.Should().NotBeNull();
        message!.Bcc.Should().BeEmpty();
        message.CC.Should().BeEmpty();
    }

    [Fact]
    public void BuildMessage_WithWhitespaceBccAndCc_DoesNotSetBccOrCc()
    {
        var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "to@test.com", "   ", "   ");

        message.Should().NotBeNull();
        message!.Bcc.Should().BeEmpty();
        message.CC.Should().BeEmpty();
    }

    [Fact]
    public void BuildMessage_WithInvalidRecipient_ReturnsNull()
    {
        var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "not-an-email", null!, null!);

        message.Should().BeNull();
    }

    [Fact]
    public void BuildMessage_SetsNormalPriority()
    {
        var controller = CreateController(RegistrationType.Tournament);
        var message = InvokeBuildMessage(controller, "from@test.com", "Subject", "Body", "to@test.com", null!, null!);

        message.Should().NotBeNull();
        message!.Priority.Should().Be(System.Net.Mail.MailPriority.Normal);
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
        controller.FriendlyRegistrationName = controller.GetFriendlyRegistrationName();
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
    // [SetsRequiredMembers] is needed because BaseRegistrationController.FriendlyRegistrationName is
    // a 'required' property. Without this attribute, the compiler prevents instantiation from external
    // assemblies even though the constructor sets the property.
    [SetsRequiredMembers]
    public TestableBaseRegistrationController(IOdysseyRepository repository)
        : base(repository)
    {
        FriendlyRegistrationName = "";
    }

    public System.Net.Mail.MailMessage? TestBuildMessage(string from, string subject, string body, string to, string bcc, string cc)
        => BuildMessage(from, subject, body, to, bcc, cc);

    public void TestSetBaseViewData(OdysseyMvc2024.ViewData.BaseViewData viewData)
        => SetBaseViewData(viewData);
}
