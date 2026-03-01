using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using OdysseyMvc2024.Models;

namespace OdysseyMvc4.Tests.Unit.Helpers;

/// <summary>
/// Provides reusable helper methods for setting up mocks and test data
/// across all test classes in the OdysseyMvc4 test suite.
/// </summary>
public static class TestHelper
{
    /// <summary>
    /// Creates a standard Config dictionary with all commonly needed keys populated.
    /// </summary>
    public static Dictionary<string, string> CreateDefaultConfig() => new()
    {
        ["RegionName"] = "NoVA North",
        ["RegionNumber"] = "9",
        ["HomePage"] = "http://www.novanorth.org",
        ["ContactUsURL"] = "/Home/ContactUs",
        ["RegionalDirectorEmail"] = "director@novanorth.org",
        ["RegionalDirectorText"] = "the Regional Director",
        ["WebmasterEmail"] = "webmaster@novanorth.org",
        ["WebmasterEmailPassword"] = "password123",
        ["EmailServer"] = "smtp.example.com",
        ["Year"] = "2025",
        ["EndYear"] = "2026",
        ["ProgramGuideURL"] = "http://example.com/guide",
        ["TournamentRegistrationOpenDateTime"] = "01/01/2020 00:00:00",
        ["TournamentRegistrationCloseDateTime"] = "12/31/2099 23:59:59",
        ["IsTournamentRegistrationDown"] = "false",
        ["JudgesRegistrationOpenDateTime"] = "01/01/2020 00:00:00",
        ["JudgesRegistrationCloseDateTime"] = "12/31/2099 23:59:59",
        ["IsJudgesRegistrationDown"] = "false",
        ["CoachesTrainingRegistrationOpenDateTime"] = "01/01/2020 00:00:00",
        ["CoachesTrainingRegistrationCloseDateTime"] = "12/31/2099 23:59:59",
        ["IsCoachesTrainingRegistrationDown"] = "false",
        ["VolunteerRegistrationOpenDateTime"] = "01/01/2020 00:00:00",
        ["VolunteerRegistrationCloseDateTime"] = "12/31/2099 23:59:59",
        ["IsVolunteerRegistrationDown"] = "false",
        ["AcceptingPayPal"] = "false",
        ["TestGuid"] = "test-guid-123"
    };

    /// <summary>
    /// Creates a default Event object suitable for tournament info.
    /// </summary>
    public static Event CreateDefaultTournamentInfo() => new()
    {
        ID = 1,
        EventName = "Regional Tournament",
        Location = "Springfield High School",
        LocationURL = "http://www.springfield.edu",
        LocationAddress = "123 Main St",
        LocationCity = "Springfield",
        LocationState = "VA",
        StartDate = new DateTime(2025, 3, 15),
        Time = "8:00 AM",
        EventCost = "100",
        LateEventCost = "125",
        LateEventCostStartDate = new DateTime(2025, 3, 1),
        PaymentDueDate = new DateTime(2025, 3, 10),
        EventMailBody = "Thank you for registering.",
        EventMakeChecksOutTo = "NoVA North Odyssey"
    };

    /// <summary>
    /// Creates a default Event object suitable for judges training info.
    /// </summary>
    public static Event CreateDefaultJudgesInfo() => new()
    {
        ID = 2,
        EventName = "Judges Training",
        Location = "Community Center",
        LocationURL = "http://www.community.org",
        LocationAddress = "456 Oak Ave",
        LocationCity = "Fairfax",
        LocationState = "VA",
        StartDate = new DateTime(2025, 2, 20),
        Time = "9:00 AM",
        EventMailBody = "Dear <span>FirstName</span> <span>LastName</span>, Thank you for registering as a judge (ID: <span>JudgeID</span>) for <span>Region</span>. " +
                        "Judges Training: <span>JudgesTrainingLocation</span> on <span>JudgesTrainingDate</span> at <span>JudgesTrainingTime</span>. " +
                        "Tournament: <span>TournamentLocation</span> on <span>TournamentDate</span> at <span>TournamentTime</span>. " +
                        "Contact us: <span>ContactUsURL</span>"
    };

    /// <summary>
    /// Creates a mock IOdysseyRepository with standard config and tournament info.
    /// </summary>
    public static Mock<IOdysseyRepository> CreateMockRepository()
    {
        var mockRepo = new Mock<IOdysseyRepository>();
        var config = CreateDefaultConfig();
        var tournamentInfo = CreateDefaultTournamentInfo();

        mockRepo.Setup(r => r.Config).Returns(config);
        mockRepo.Setup(r => r.TournamentInfo).Returns(tournamentInfo);
        mockRepo.Setup(r => r.RegionName).Returns("NoVA North");
        mockRepo.Setup(r => r.RegionNumber).Returns("9");

        return mockRepo;
    }

    /// <summary>
    /// Sets up the HttpContext, UrlHelper, and other MVC infrastructure on a controller
    /// so that methods like DetermineSiteName() and DetermineSiteCssFile() can be tested.
    /// Uses a FakeUrlHelper since PageLink() is an extension method that cannot be mocked.
    /// </summary>
    public static void SetupControllerContext(Controller controller, string host = "www.novanorth.org", string? pageLink = "http://www.novanorth.org/")
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Host = new HostString(host);
        httpContext.Request.Scheme = "http";

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        controller.Url = new FakeUrlHelper(pageLink);
    }
}

/// <summary>
/// A fake IUrlHelper that returns predictable values for testing.
/// This is needed because PageLink() is an extension method on IUrlHelper
/// that cannot be mocked with Moq.
/// </summary>
public class FakeUrlHelper : IUrlHelper
{
    private readonly string? _pageLink;

    public FakeUrlHelper(string? pageLink = "http://www.novanorth.org/")
    {
        _pageLink = pageLink;
    }

    public ActionContext ActionContext { get; } = new ActionContext(new DefaultHttpContext(), new Microsoft.AspNetCore.Routing.RouteData(), new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());

    public string? Action(UrlActionContext actionContext) => null;

    public string? Content(string? contentPath) => contentPath?.Replace("~", "");

    public bool IsLocalUrl(string? url) => false;

    public string? Link(string? routeName, object? values) => _pageLink;

    public string? Page(string? pageName, string? pageHandler, object? values, string? protocol, string? host, string? fragment) => _pageLink;

    public string? RouteUrl(UrlRouteContext routeContext) => null;
}
