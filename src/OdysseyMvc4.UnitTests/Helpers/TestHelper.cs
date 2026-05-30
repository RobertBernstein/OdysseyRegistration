using Moq;
using OdysseyMvc4.Models;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OdysseyMvc4.UnitTests.Helpers;

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
    public static Event CreateDefaultTournamentInfo() => new Event
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
    public static Event CreateDefaultJudgesInfo() => new Event
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
    /// </summary>
    public static void SetupControllerContext(Controller controller, string host = "www.novanorth.org")
    {
        var requestMock = new Mock<HttpRequestBase>();
        requestMock.Setup(r => r.Url).Returns(new Uri($"http://{host}/"));
        requestMock.Setup(r => r.ApplicationPath).Returns("/");
        requestMock.Setup(r => r.ServerVariables).Returns(new NameValueCollection());

        var responseMock = new Mock<HttpResponseBase>();
        responseMock.Setup(r => r.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

        var httpContextMock = new Mock<HttpContextBase>();
        httpContextMock.Setup(c => c.Request).Returns(requestMock.Object);
        httpContextMock.Setup(c => c.Response).Returns(responseMock.Object);

        var routeData = new RouteData();
        controller.ControllerContext = new ControllerContext(httpContextMock.Object, routeData, controller);

        var requestContext = new RequestContext(httpContextMock.Object, routeData);
        controller.Url = new UrlHelper(requestContext);
    }
}
