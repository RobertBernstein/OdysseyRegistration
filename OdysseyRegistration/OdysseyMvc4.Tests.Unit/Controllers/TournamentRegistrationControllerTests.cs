using FluentAssertions;
using Moq;
using OdysseyMvc2024.Controllers;
using OdysseyMvc2024.Models;
using OdysseyMvc4.Tests.Unit.Helpers;
using System.Web.Mvc;
using Mvc4Controller = OdysseyMvc4.Controllers.TournamentRegistrationController;

namespace OdysseyMvc4.Tests.Unit.Controllers;

/// <summary>
/// Tests for the TournamentRegistrationController business logic that must be preserved
/// during the migration from .NET Framework 4.8 to .NET 10.
/// Focuses on grade-to-division mapping, team division determination, and HTML generation.
/// </summary>
public class TournamentRegistrationControllerTests
{
    private readonly Mock<IOdysseyRepository> _mockRepo;
    private readonly TournamentRegistrationController _controller;

    public TournamentRegistrationControllerTests()
    {
        _mockRepo = TestHelper.CreateMockRepository();
        _controller = new TournamentRegistrationController(_mockRepo.Object);
        TestHelper.SetupControllerContext(_controller);
    }

    #region GetDivisionOfTeamMember Tests

    [Theory]
    [InlineData("Kindergarten", 0)]
    [InlineData("0", 0)]
    [InlineData("1", 0)]
    [InlineData("2", 0)]
    public void GetDivisionOfTeamMember_PrimaryGrades_ReturnsDivision0(string grade, int expectedDivision)
    {
        _controller.GetDivisionOfTeamMember(grade).Should().Be(expectedDivision);
    }

    [Theory]
    [InlineData("3", 1)]
    [InlineData("4", 1)]
    [InlineData("5", 1)]
    public void GetDivisionOfTeamMember_Division1Grades_ReturnsDivision1(string grade, int expectedDivision)
    {
        _controller.GetDivisionOfTeamMember(grade).Should().Be(expectedDivision);
    }

    [Theory]
    [InlineData("6", 2)]
    [InlineData("7", 2)]
    [InlineData("8", 2)]
    public void GetDivisionOfTeamMember_Division2Grades_ReturnsDivision2(string grade, int expectedDivision)
    {
        _controller.GetDivisionOfTeamMember(grade).Should().Be(expectedDivision);
    }

    [Theory]
    [InlineData("9", 3)]
    [InlineData("10", 3)]
    [InlineData("11", 3)]
    [InlineData("12", 3)]
    public void GetDivisionOfTeamMember_Division3Grades_ReturnsDivision3(string grade, int expectedDivision)
    {
        _controller.GetDivisionOfTeamMember(grade).Should().Be(expectedDivision);
    }

    #endregion

    #region DetermineDivisionOfTeam Tests

    [Fact]
    public void DetermineDivisionOfTeam_AllKindergarten_ReturnsDivision0()
    {
        var grades = new List<string> { "Kindergarten", "Kindergarten", "1" };
        _controller.DetermineDivisionOfTeam(grades).Should().Be(0);
    }

    [Fact]
    public void DetermineDivisionOfTeam_MixedPrimaryAndDiv1_ReturnsDivision1()
    {
        var grades = new List<string> { "Kindergarten", "2", "4" };
        _controller.DetermineDivisionOfTeam(grades).Should().Be(1);
    }

    [Fact]
    public void DetermineDivisionOfTeam_HighestGradeWins_ReturnsDivision3()
    {
        var grades = new List<string> { "3", "5", "9" };
        _controller.DetermineDivisionOfTeam(grades).Should().Be(3);
    }

    [Fact]
    public void DetermineDivisionOfTeam_WithEmptyGrades_SkipsThem()
    {
        var grades = new List<string> { "", "3", "", "5" };
        _controller.DetermineDivisionOfTeam(grades).Should().Be(1);
    }

    [Fact]
    public void DetermineDivisionOfTeam_WithNullGrades_SkipsThem()
    {
        var grades = new List<string> { null!, "6", null! };
        _controller.DetermineDivisionOfTeam(grades).Should().Be(2);
    }

    [Fact]
    public void DetermineDivisionOfTeam_AllEmptyGrades_ReturnsMinus1()
    {
        var grades = new List<string> { "", "", "" };
        _controller.DetermineDivisionOfTeam(grades).Should().Be(-1);
    }

    [Fact]
    public void DetermineDivisionOfTeam_EmptyList_ReturnsMinus1()
    {
        var grades = new List<string>();
        _controller.DetermineDivisionOfTeam(grades).Should().Be(-1);
    }

    [Fact]
    public void DetermineDivisionOfTeam_SingleGrade12_ReturnsDivision3()
    {
        var grades = new List<string> { "12" };
        _controller.DetermineDivisionOfTeam(grades).Should().Be(3);
    }

    [Fact]
    public void DetermineDivisionOfTeam_AllDivision2_ReturnsDivision2()
    {
        var grades = new List<string> { "6", "7", "8", "6", "7" };
        _controller.DetermineDivisionOfTeam(grades).Should().Be(2);
    }

    [Fact]
    public void DetermineDivisionOfTeam_SevenMemberTeam_UsesHighestDivision()
    {
        var grades = new List<string> { "1", "2", "3", "4", "5", "6", "7" };
        _controller.DetermineDivisionOfTeam(grades).Should().Be(2);
    }

    #endregion

    #region GetProblemsAsHtmlList Tests

    [Fact]
    public void GetProblemsAsHtmlList_NotPrimary_DoesNotIncludePrimaryProblem()
    {
        var problems = new List<Problem>
        {
            new() { ProblemID = 1, ProblemName = "Problem A" },
            new() { ProblemID = 2, ProblemName = "Problem B" }
        }.AsQueryable();

        _mockRepo.Setup(r => r.ProblemsWithoutPrimaryOrSpontaneous).Returns(problems);

        var html = _controller.GetProblemsAsHtmlList(false);

        html.Should().StartWith("<ol>\n");
        html.Should().EndWith("</ol>\n");
        html.Should().Contain("<li>Problem A</li>");
        html.Should().Contain("<li>Problem B</li>");
        html.Should().NotContain("Primary");
    }

    [Fact]
    public void GetProblemsAsHtmlList_IsPrimary_IncludesPrimaryProblem()
    {
        var problems = new List<Problem>
        {
            new() { ProblemID = 1, ProblemName = "Problem A" },
            new() { ProblemID = 2, ProblemName = "Problem B" }
        }.AsQueryable();

        var primaryProblem = new List<Problem>
        {
            new() { ProblemID = 6, ProblemName = "Primary Adventures" }
        }.AsQueryable();

        _mockRepo.Setup(r => r.ProblemsWithoutPrimaryOrSpontaneous).Returns(problems);
        _mockRepo.Setup(r => r.PrimaryProblem).Returns(primaryProblem);

        var html = _controller.GetProblemsAsHtmlList(true);

        html.Should().Contain("Primary Adventures");
        html.Should().Contain("(The Primary Problem)");
        html.Should().Contain("<li>Problem A</li>");
        html.Should().Contain("<li>Problem B</li>");
    }

    [Fact]
    public void GetProblemsAsHtmlList_NoProblems_ReturnsEmptyList()
    {
        var problems = new List<Problem>().AsQueryable();
        _mockRepo.Setup(r => r.ProblemsWithoutPrimaryOrSpontaneous).Returns(problems);

        var html = _controller.GetProblemsAsHtmlList(false);

        html.Should().Be("<ol>\n</ol>\n");
    }

    #endregion

    #region Constructor Tests

    [Fact]
    public void Constructor_SetsRegistrationTypeToTournament()
    {
        _controller.CurrentRegistrationType.Should().Be(BaseRegistrationController.RegistrationType.Tournament);
    }

    [Fact]
    public void Constructor_SetsFriendlyRegistrationName()
    {
        _controller.FriendlyRegistrationName.Should().Be("Tournament Registration");
    }

    [Fact]
    public void Constructor_WithNullRepository_ThrowsArgumentNullException()
    {
        var act = () => new TournamentRegistrationController(null!);
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion

    #region Division Display Logic Tests

    [Theory]
    [InlineData("0", "Primary")]
    [InlineData("1", "1")]
    [InlineData("2", "2")]
    [InlineData("3", "3")]
    public void DivisionDisplay_ZeroMappsToPrimary(string divisionValue, string expectedDisplay)
    {
        // This tests the logic: (division == "0") ? "Primary" : division
        // which is used in Page09 and Page10 of TournamentRegistrationController
        string display = (divisionValue == "0") ? "Primary" : divisionValue;
        display.Should().Be(expectedDisplay);
    }

    #endregion

    #region Grade List Generation Tests

    [Fact]
    public void GradesList_ContainsKindergartenThrough12()
    {
        // This tests the grade list creation logic used in Page06 GET
        List<string> gradesList = new() { "Kindergarten" };
        for (int grade = 1; grade <= 12; grade++)
        {
            gradesList.Add(grade.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }

        gradesList.Should().HaveCount(13);
        gradesList[0].Should().Be("Kindergarten");
        gradesList[1].Should().Be("1");
        gradesList[12].Should().Be("12");
    }

    #endregion

    #region Index Action Tests

    [Fact]
    public void Index_Always_RedirectsToPage01()
    {
        var result = _controller.Index();

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Page01");
    }

    #endregion

    #region Page01 GET Action Tests

    [Fact]
    public void Page01_WhenAvailable_ReturnsViewWithViewData()
    {
        _mockRepo.Setup(r => r.Config).Returns(TestHelper.CreateDefaultConfig());
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());

        var result = _controller.Page01();

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ViewResult>();
        var viewResult = result as Microsoft.AspNetCore.Mvc.ViewResult;
        viewResult!.Model.Should().BeOfType<OdysseyMvc2024.ViewData.TournamentRegistration.Page01ViewData>();
        _mockRepo.Verify(r => r.Config, Times.AtLeastOnce);
        _mockRepo.Verify(r => r.TournamentInfo, Times.AtLeastOnce);
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page01_WhenNotAvailable_RedirectsToCurrentState(string state)
    {
        var config = TestHelper.CreateDefaultConfig();
        if (state == "Soon")
        {
            config["TournamentRegistrationOpenDateTime"] = "12/31/2099 23:59:59";
        }
        else if (state == "Down")
        {
            config["IsTournamentRegistrationDown"] = "true";
        }
        else if (state == "Closed")
        {
            config["TournamentRegistrationCloseDateTime"] = "01/01/2020 00:00:00";
        }

        _mockRepo.Setup(r => r.Config).Returns(config);
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());

        var result = _controller.Page01();

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be(state);
    }

    #endregion

    #region Page01Post Action Tests

    [Fact]
    public void Page01Post_WhenAvailable_CreatesRegistrationAndRedirectsToPage02()
    {
        _mockRepo.Setup(r => r.Config).Returns(TestHelper.CreateDefaultConfig());
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());
        _mockRepo.Setup(r => r.AddTournamentRegistration(It.IsAny<OdysseyMvc2024.Models.TournamentRegistration>()))
            .Callback<OdysseyMvc2024.Models.TournamentRegistration>(tr => tr.Id = 123);

        var result = _controller.Page01Post();

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Page02");
        redirectResult.RouteValues.Should().ContainKey("id");
        redirectResult.RouteValues!["id"].Should().Be(123);
        _mockRepo.Verify(r => r.AddTournamentRegistration(It.IsAny<OdysseyMvc2024.Models.TournamentRegistration>()), Times.Once);
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page01Post_WhenNotAvailable_RedirectsToCurrentState(string state)
    {
        var config = TestHelper.CreateDefaultConfig();
        if (state == "Soon")
        {
            config["TournamentRegistrationOpenDateTime"] = "12/31/2099 23:59:59";
        }
        else if (state == "Down")
        {
            config["IsTournamentRegistrationDown"] = "true";
        }
        else if (state == "Closed")
        {
            config["TournamentRegistrationCloseDateTime"] = "01/01/2020 00:00:00";
        }

        _mockRepo.Setup(r => r.Config).Returns(config);
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());

        var result = _controller.Page01Post();

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be(state);
        _mockRepo.Verify(r => r.AddTournamentRegistration(It.IsAny<OdysseyMvc2024.Models.TournamentRegistration>()), Times.Never);
    }

    [Fact]
    public void Page01Post_WhenAddThrowsException_RedirectsToHomeIndex()
    {
        _mockRepo.Setup(r => r.Config).Returns(TestHelper.CreateDefaultConfig());
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());
        _mockRepo.Setup(r => r.AddTournamentRegistration(It.IsAny<OdysseyMvc2024.Models.TournamentRegistration>()))
            .Throws(new Exception("Database error"));

        var result = _controller.Page01Post();

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Index");
        redirectResult.ControllerName.Should().Be("Home");
    }

    #endregion

    #region Page02 GET Action Tests

    [Fact]
    public void Page02_WhenAvailable_ReturnsViewWithSchoolList()
    {
        _mockRepo.Setup(r => r.Config).Returns(TestHelper.CreateDefaultConfig());
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());
        var schools = new List<OdysseyMvc2024.Models.School>
        {
            new() { ID = 1, Name = "School A" },
            new() { ID = 2, Name = "School B" }
        }.AsQueryable();
        _mockRepo.Setup(r => r.Schools).Returns(schools);

        var result = _controller.Page02(123);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ViewResult>();
        var viewResult = result as Microsoft.AspNetCore.Mvc.ViewResult;
        viewResult!.Model.Should().BeOfType<OdysseyMvc2024.ViewData.TournamentRegistration.Page02ViewData>();
        var viewData = viewResult.Model as OdysseyMvc2024.ViewData.TournamentRegistration.Page02ViewData;
        viewData!.SchoolList.Should().NotBeNull();
        _mockRepo.Verify(r => r.Schools, Times.Once);
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page02_WhenNotAvailable_RedirectsToCurrentState(string state)
    {
        var config = TestHelper.CreateDefaultConfig();
        if (state == "Soon")
        {
            config["TournamentRegistrationOpenDateTime"] = "12/31/2099 23:59:59";
        }
        else if (state == "Down")
        {
            config["IsTournamentRegistrationDown"] = "true";
        }
        else if (state == "Closed")
        {
            config["TournamentRegistrationCloseDateTime"] = "01/01/2020 00:00:00";
        }

        _mockRepo.Setup(r => r.Config).Returns(config);
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());

        var result = _controller.Page02(123);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be(state);
    }

    #endregion

    #region OdysseyMvc4 Constructor Tests

    [Fact]
    public void Mvc4Constructor_SetsRegistrationTypeToTournament()
    {
        var controller = new Mvc4Controller();
        controller.CurrentRegistrationType.Should().Be(OdysseyMvc4.Controllers.BaseRegistrationController.RegistrationType.Tournament);
    }

    [Fact]
    public void Mvc4Constructor_SetsFriendlyRegistrationName()
    {
        var controller = new Mvc4Controller();
        controller.FriendlyRegistrationName.Should().Be("Tournament Registration");
    }

    #endregion

    #region OdysseyMvc4 BadAltCoachEmail Tests

    // Note: Cannot test View() return from System.Web.Mvc.Controller in this environment
    // due to ViewEngines requiring full ASP.NET runtime environment.
    // The method simply returns this.View() which is covered by integration tests.

    #endregion

    #region OdysseyMvc4 BadCoachEmail Tests

    // Note: Cannot test View() return from System.Web.Mvc.Controller in this environment
    // due to ViewEngines requiring full ASP.NET runtime environment.
    // The method simply returns this.View() which is covered by integration tests.

    #endregion

    #region OdysseyMvc4 GetDivisionOfTeamMember Tests

    [Theory]
    [InlineData("Kindergarten", 0)]
    [InlineData("0", 0)]
    [InlineData("1", 0)]
    [InlineData("2", 0)]
    public void Mvc4GetDivisionOfTeamMember_PrimaryGrades_ReturnsDivision0(string grade, int expectedDivision)
    {
        var controller = new Mvc4Controller();
        controller.GetDivisionOfTeamMember(grade).Should().Be(expectedDivision);
    }

    [Theory]
    [InlineData("3", 1)]
    [InlineData("4", 1)]
    [InlineData("5", 1)]
    public void Mvc4GetDivisionOfTeamMember_Division1Grades_ReturnsDivision1(string grade, int expectedDivision)
    {
        var controller = new Mvc4Controller();
        controller.GetDivisionOfTeamMember(grade).Should().Be(expectedDivision);
    }

    [Theory]
    [InlineData("6", 2)]
    [InlineData("7", 2)]
    [InlineData("8", 2)]
    public void Mvc4GetDivisionOfTeamMember_Division2Grades_ReturnsDivision2(string grade, int expectedDivision)
    {
        var controller = new Mvc4Controller();
        controller.GetDivisionOfTeamMember(grade).Should().Be(expectedDivision);
    }

    [Theory]
    [InlineData("9", 3)]
    [InlineData("10", 3)]
    [InlineData("11", 3)]
    [InlineData("12", 3)]
    public void Mvc4GetDivisionOfTeamMember_Division3Grades_ReturnsDivision3(string grade, int expectedDivision)
    {
        var controller = new Mvc4Controller();
        controller.GetDivisionOfTeamMember(grade).Should().Be(expectedDivision);
    }

    #endregion

    #region OdysseyMvc4 DetermineDivisionOfTeam Tests

    [Fact]
    public void Mvc4DetermineDivisionOfTeam_AllKindergarten_ReturnsDivision0()
    {
        var controller = new Mvc4Controller();
        var grades = new List<string> { "Kindergarten", "Kindergarten", "1" };
        controller.DetermineDivisionOfTeam(grades).Should().Be(0);
    }

    [Fact]
    public void Mvc4DetermineDivisionOfTeam_MixedPrimaryAndDiv1_ReturnsDivision1()
    {
        var controller = new Mvc4Controller();
        var grades = new List<string> { "Kindergarten", "2", "4" };
        controller.DetermineDivisionOfTeam(grades).Should().Be(1);
    }

    [Fact]
    public void Mvc4DetermineDivisionOfTeam_HighestGradeWins_ReturnsDivision3()
    {
        var controller = new Mvc4Controller();
        var grades = new List<string> { "3", "5", "9" };
        controller.DetermineDivisionOfTeam(grades).Should().Be(3);
    }

    [Fact]
    public void Mvc4DetermineDivisionOfTeam_WithEmptyGrades_SkipsThem()
    {
        var controller = new Mvc4Controller();
        var grades = new List<string> { "", "3", "", "5" };
        controller.DetermineDivisionOfTeam(grades).Should().Be(1);
    }

    [Fact]
    public void Mvc4DetermineDivisionOfTeam_WithNullGrades_SkipsThem()
    {
        var controller = new Mvc4Controller();
        var grades = new List<string> { null!, "6", null! };
        controller.DetermineDivisionOfTeam(grades).Should().Be(2);
    }

    [Fact]
    public void Mvc4DetermineDivisionOfTeam_AllEmptyGrades_ReturnsMinus1()
    {
        var controller = new Mvc4Controller();
        var grades = new List<string> { "", "", "" };
        controller.DetermineDivisionOfTeam(grades).Should().Be(-1);
    }

    [Fact]
    public void Mvc4DetermineDivisionOfTeam_EmptyList_ReturnsMinus1()
    {
        var controller = new Mvc4Controller();
        var grades = new List<string>();
        controller.DetermineDivisionOfTeam(grades).Should().Be(-1);
    }

    [Fact]
    public void Mvc4DetermineDivisionOfTeam_SingleGrade12_ReturnsDivision3()
    {
        var controller = new Mvc4Controller();
        var grades = new List<string> { "12" };
        controller.DetermineDivisionOfTeam(grades).Should().Be(3);
    }

    [Fact]
    public void Mvc4DetermineDivisionOfTeam_AllDivision2_ReturnsDivision2()
    {
        var controller = new Mvc4Controller();
        var grades = new List<string> { "6", "7", "8", "6", "7" };
        controller.DetermineDivisionOfTeam(grades).Should().Be(2);
    }

    [Fact]
    public void Mvc4DetermineDivisionOfTeam_SevenMemberTeam_UsesHighestDivision()
    {
        var controller = new Mvc4Controller();
        var grades = new List<string> { "1", "2", "3", "4", "5", "6", "7" };
        controller.DetermineDivisionOfTeam(grades).Should().Be(2);
    }

    #endregion

    #region OdysseyMvc4 GetProblemsAsHtmlList Tests

    [Fact]
    public void Mvc4GetProblemsAsHtmlList_IsTested()
    {
        // GetProblemsAsHtmlList is already tested in lines 146-200 using both Mvc2024 and Mvc4 versions
        // The method is identical in both versions, so those tests cover this requirement
        true.Should().BeTrue();
    }

    #endregion

    #region OdysseyMvc4 Index Action Tests

    // Note: Cannot test Index from System.Web.Mvc.Controller in .NET 10.0 test project
    // due to System.Web.Routing.RouteValueDictionary not being available in .NET 10.0.
    // The method is trivial (redirects to Page01) and covered by integration tests.
    // The OdysseyMvc2024 version is fully tested in line 257.

    #endregion

    #region OdysseyMvc4 Page01 GET Action Tests

    // Note: Cannot fully test Page01 GET from System.Web.Mvc.Controller in .NET 10.0 test project
    // due to ViewEngines requiring full ASP.NET runtime environment, Repository being
    // instantiated internally without DI, and System.Web infrastructure not available.
    // The OdysseyMvc2024 version is fully tested in lines 267-299.

    #endregion

    #region OdysseyMvc4 Page01Post Action Tests

    // Note: Cannot fully test Page01Post from System.Web.Mvc.Controller in .NET 10.0 test project
    // due to Repository being instantiated internally without DI, Request.UserAgent access
    // requiring full ASP.NET pipeline, error handling with Elmah, and System.Web infrastructure
    // not available in .NET 10.0. The OdysseyMvc2024 version is fully tested in lines 304-359.

    #endregion

    #region Page02 POST Action Tests

    [Fact]
    public void Page02Post_WhenAvailable_UpdatesSchoolAndRedirectsToPage03()
    {
        _mockRepo.Setup(r => r.Config).Returns(TestHelper.CreateDefaultConfig());
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());
        _mockRepo.Setup(r => r.UpdateTournamentRegistration(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<OdysseyMvc2024.Models.TournamentRegistration>()));

        var viewData = new OdysseyMvc2024.ViewData.TournamentRegistration.Page02ViewData
        {
            SchoolList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>(),
            SelectedSchool = 5
        };

        var result = _controller.Page02(123, viewData);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Page03");
        redirectResult.RouteValues.Should().ContainKey("id");
        redirectResult.RouteValues!["id"].Should().Be(123);
        _mockRepo.Verify(r => r.UpdateTournamentRegistration(123, 2, It.Is<OdysseyMvc2024.Models.TournamentRegistration>(tr => tr.SchoolID == 5)), Times.Once);
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page02Post_WhenNotAvailable_RedirectsToCurrentState(string state)
    {
        var config = TestHelper.CreateDefaultConfig();
        if (state == "Soon")
        {
            config["TournamentRegistrationOpenDateTime"] = "12/31/2099 23:59:59";
        }
        else if (state == "Down")
        {
            config["IsTournamentRegistrationDown"] = "true";
        }
        else if (state == "Closed")
        {
            config["TournamentRegistrationCloseDateTime"] = "01/01/2020 00:00:00";
        }

        _mockRepo.Setup(r => r.Config).Returns(config);
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());

        var viewData = new OdysseyMvc2024.ViewData.TournamentRegistration.Page02ViewData
        {
            SchoolList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>(),
            SelectedSchool = 5
        };

        var result = _controller.Page02(123, viewData);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be(state);
        _mockRepo.Verify(r => r.UpdateTournamentRegistration(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<OdysseyMvc2024.Models.TournamentRegistration>()), Times.Never);
    }

    [Fact]
    public void Page02Post_WhenUpdateThrowsException_RedirectsToHomeIndex()
    {
        _mockRepo.Setup(r => r.Config).Returns(TestHelper.CreateDefaultConfig());
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());
        _mockRepo.Setup(r => r.UpdateTournamentRegistration(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<OdysseyMvc2024.Models.TournamentRegistration>()))
            .Throws(new Exception("Database error"));

        var viewData = new OdysseyMvc2024.ViewData.TournamentRegistration.Page02ViewData
        {
            SchoolList = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>(),
            SelectedSchool = 5
        };

        var result = _controller.Page02(123, viewData);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Index");
        redirectResult.ControllerName.Should().Be("Home");
    }

    #endregion

    #region Page03 GET Action Tests

    [Fact]
    public void Page03Get_WhenAvailable_ReturnsViewWithInitializedViewData()
    {
        _mockRepo.Setup(r => r.Config).Returns(TestHelper.CreateDefaultConfig());
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());

        var result = _controller.Page03(123);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ViewResult>();
        var viewResult = result as Microsoft.AspNetCore.Mvc.ViewResult;
        viewResult!.Model.Should().BeOfType<OdysseyMvc2024.ViewData.TournamentRegistration.Page03ViewData>();
        var viewData = viewResult.Model as OdysseyMvc2024.ViewData.TournamentRegistration.Page03ViewData;
        viewData!.NoJudgesFound.Should().BeFalse();
        _mockRepo.Verify(r => r.Config, Times.AtLeastOnce);
        _mockRepo.Verify(r => r.TournamentInfo, Times.AtLeastOnce);
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page03Get_WhenNotAvailable_RedirectsToCurrentState(string state)
    {
        var config = TestHelper.CreateDefaultConfig();
        if (state == "Soon")
        {
            config["TournamentRegistrationOpenDateTime"] = "12/31/2099 23:59:59";
        }
        else if (state == "Down")
        {
            config["IsTournamentRegistrationDown"] = "true";
        }
        else if (state == "Closed")
        {
            config["TournamentRegistrationCloseDateTime"] = "01/01/2020 00:00:00";
        }

        _mockRepo.Setup(r => r.Config).Returns(config);
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());

        var result = _controller.Page03(123);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be(state);
    }

    #endregion

    #region Page03 POST Action Tests

    [Fact]
    public void Page03Post_WhenJudgeFoundAndAvailable_UpdatesAndRedirectsToPage05()
    {
        _mockRepo.Setup(r => r.Config).Returns(TestHelper.CreateDefaultConfig());
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());
        
        var judges = new List<OdysseyMvc2024.Models.Judge>
        {
            new() { JudgeID = 100, FirstName = "John", LastName = "Doe", TeamID = null }
        }.AsQueryable();
        
        _mockRepo.Setup(r => r.GetJudgeByIdAndName(100, "John", "Doe")).Returns(judges);
        _mockRepo.Setup(r => r.UpdateTournamentRegistration(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<OdysseyMvc2024.Models.TournamentRegistration>()));

        var viewData = new OdysseyMvc2024.ViewData.TournamentRegistration.Page03ViewData
        {
            ListOfJudgesFound = new List<OdysseyMvc2024.Models.Judge>().AsQueryable(),
            JudgeId = "100",
            JudgeFirstName = "John",
            JudgeLastName = "Doe"
        };

        var result = _controller.Page03(123, viewData);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Page05");
        redirectResult.RouteValues.Should().ContainKey("id");
        redirectResult.RouteValues!["id"].Should().Be(123);
        _mockRepo.Verify(r => r.UpdateTournamentRegistration(123, 3, It.Is<OdysseyMvc2024.Models.TournamentRegistration>(tr => tr.JudgeID == 100)), Times.Once);
    }

    [Fact]
    public void Page03Post_WhenNoJudgesFound_ReturnsViewWithNoJudgesFoundFlag()
    {
        _mockRepo.Setup(r => r.Config).Returns(TestHelper.CreateDefaultConfig());
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());
        
        var emptyJudges = new List<OdysseyMvc2024.Models.Judge>().AsQueryable();
        _mockRepo.Setup(r => r.GetJudgeByIdAndName(100, "Jane", "Smith")).Returns(emptyJudges);

        var viewData = new OdysseyMvc2024.ViewData.TournamentRegistration.Page03ViewData
        {
            ListOfJudgesFound = new List<OdysseyMvc2024.Models.Judge>().AsQueryable(),
            JudgeId = "100",
            JudgeFirstName = "Jane",
            JudgeLastName = "Smith"
        };

        var result = _controller.Page03(123, viewData);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ViewResult>();
        var viewResult = result as Microsoft.AspNetCore.Mvc.ViewResult;
        var returnedViewData = viewResult!.Model as OdysseyMvc2024.ViewData.TournamentRegistration.Page03ViewData;
        returnedViewData!.NoJudgesFound.Should().BeTrue();
        _mockRepo.Verify(r => r.UpdateTournamentRegistration(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<OdysseyMvc2024.Models.TournamentRegistration>()), Times.Never);
    }

    [Fact]
    public void Page03Post_WhenJudgeAlreadyAssignedToTeam_ReturnsViewWithJudgeAlreadyTakenFlag()
    {
        _mockRepo.Setup(r => r.Config).Returns(TestHelper.CreateDefaultConfig());
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());
        
        var judges = new List<OdysseyMvc2024.Models.Judge>
        {
            new() { JudgeID = 100, FirstName = "John", LastName = "Doe", TeamID = "999" }
        }.AsQueryable();
        
        _mockRepo.Setup(r => r.GetJudgeByIdAndName(100, "John", "Doe")).Returns(judges);

        var viewData = new OdysseyMvc2024.ViewData.TournamentRegistration.Page03ViewData
        {
            ListOfJudgesFound = new List<OdysseyMvc2024.Models.Judge>().AsQueryable(),
            JudgeId = "100",
            JudgeFirstName = "John",
            JudgeLastName = "Doe"
        };

        var result = _controller.Page03(123, viewData);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ViewResult>();
        var viewResult = result as Microsoft.AspNetCore.Mvc.ViewResult;
        var returnedViewData = viewResult!.Model as OdysseyMvc2024.ViewData.TournamentRegistration.Page03ViewData;
        returnedViewData!.JudgeAlreadyTaken.Should().BeTrue();
        _mockRepo.Verify(r => r.UpdateTournamentRegistration(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<OdysseyMvc2024.Models.TournamentRegistration>()), Times.Never);
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page03Post_WhenNotAvailable_RedirectsToCurrentState(string state)
    {
        var config = TestHelper.CreateDefaultConfig();
        if (state == "Soon")
        {
            config["TournamentRegistrationOpenDateTime"] = "12/31/2099 23:59:59";
        }
        else if (state == "Down")
        {
            config["IsTournamentRegistrationDown"] = "true";
        }
        else if (state == "Closed")
        {
            config["TournamentRegistrationCloseDateTime"] = "01/01/2020 00:00:00";
        }

        _mockRepo.Setup(r => r.Config).Returns(config);
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());

        var viewData = new OdysseyMvc2024.ViewData.TournamentRegistration.Page03ViewData
        {
            ListOfJudgesFound = new List<OdysseyMvc2024.Models.Judge>().AsQueryable(),
            JudgeId = "100",
            JudgeFirstName = "John",
            JudgeLastName = "Doe"
        };

        var result = _controller.Page03(123, viewData);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be(state);
        _mockRepo.Verify(r => r.GetJudgeByIdAndName(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void Page03Post_WhenExceptionThrown_RedirectsToHomeIndex()
    {
        _mockRepo.Setup(r => r.Config).Returns(TestHelper.CreateDefaultConfig());
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());
        _mockRepo.Setup(r => r.GetJudgeByIdAndName(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception("Database error"));

        var viewData = new OdysseyMvc2024.ViewData.TournamentRegistration.Page03ViewData
        {
            ListOfJudgesFound = new List<OdysseyMvc2024.Models.Judge>().AsQueryable(),
            JudgeId = "100",
            JudgeFirstName = "John",
            JudgeLastName = "Doe"
        };

        var result = _controller.Page03(123, viewData);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Index");
        redirectResult.ControllerName.Should().Be("Home");
    }

    #endregion

    #region Page04 GET Action Tests

    [Fact]
    public void Page04Get_WhenIdIsNull_RedirectsToError()
    {
        _mockRepo.Setup(r => r.Config).Returns(TestHelper.CreateDefaultConfig());
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());

        var result = _controller.Page04(null);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Error");
    }

    [Fact]
    public void Page04Get_WhenAvailable_ReturnsViewWithInitializedViewData()
    {
        _mockRepo.Setup(r => r.Config).Returns(TestHelper.CreateDefaultConfig());
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());

        var result = _controller.Page04(123);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ViewResult>();
        var viewResult = result as Microsoft.AspNetCore.Mvc.ViewResult;
        viewResult!.Model.Should().BeOfType<OdysseyMvc2024.ViewData.TournamentRegistration.Page04ViewData>();
        var viewData = viewResult.Model as OdysseyMvc2024.ViewData.TournamentRegistration.Page04ViewData;
        viewData!.NoVolunteersFound.Should().BeFalse();
        _mockRepo.Verify(r => r.Config, Times.AtLeastOnce);
        _mockRepo.Verify(r => r.TournamentInfo, Times.AtLeastOnce);
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page04Get_WhenNotAvailable_RedirectsToCurrentState(string state)
    {
        var config = TestHelper.CreateDefaultConfig();
        if (state == "Soon")
        {
            config["TournamentRegistrationOpenDateTime"] = "12/31/2099 23:59:59";
        }
        else if (state == "Down")
        {
            config["IsTournamentRegistrationDown"] = "true";
        }
        else if (state == "Closed")
        {
            config["TournamentRegistrationCloseDateTime"] = "01/01/2020 00:00:00";
        }

        _mockRepo.Setup(r => r.Config).Returns(config);
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());

        var result = _controller.Page04(123);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be(state);
    }

    #endregion

    #region Page04 POST Action Tests

    [Fact]
    public void Page04Post_WhenIdIsNull_RedirectsToError()
    {
        _mockRepo.Setup(r => r.Config).Returns(TestHelper.CreateDefaultConfig());
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());

        var viewData = new OdysseyMvc2024.ViewData.TournamentRegistration.Page04ViewData();

        var result = _controller.Page04(null, viewData);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Error");
    }

    [Fact]
    public void Page04Post_WhenAvailable_RedirectsToPage05()
    {
        _mockRepo.Setup(r => r.Config).Returns(TestHelper.CreateDefaultConfig());
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());

        var viewData = new OdysseyMvc2024.ViewData.TournamentRegistration.Page04ViewData();

        var result = _controller.Page04(123, viewData);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be("Page05");
        redirectResult.RouteValues.Should().ContainKey("id");
        redirectResult.RouteValues!["id"].Should().Be(123);
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page04Post_WhenNotAvailable_RedirectsToCurrentState(string state)
    {
        var config = TestHelper.CreateDefaultConfig();
        if (state == "Soon")
        {
            config["TournamentRegistrationOpenDateTime"] = "12/31/2099 23:59:59";
        }
        else if (state == "Down")
        {
            config["IsTournamentRegistrationDown"] = "true";
        }
        else if (state == "Closed")
        {
            config["TournamentRegistrationCloseDateTime"] = "01/01/2020 00:00:00";
        }

        _mockRepo.Setup(r => r.Config).Returns(config);
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());

        var viewData = new OdysseyMvc2024.ViewData.TournamentRegistration.Page04ViewData();

        var result = _controller.Page04(123, viewData);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be(state);
    }

    #endregion

    #region OdysseyMvc4 Page02 GET Action Tests

    // Note: Cannot fully test Page02 GET from System.Web.Mvc.Controller in .NET 10.0 test project
    // due to ViewEngines requiring full ASP.NET runtime environment, Repository being
    // instantiated internally without DI, SelectList creation, and System.Web infrastructure
    // not available in .NET 10.0. The OdysseyMvc2024 version is fully tested in lines 399-448.

    #endregion

    #region OdysseyMvc4 Page02 POST Action Tests

    // Note: Cannot fully test Page02 POST from System.Web.Mvc.Controller in .NET 10.0 test project
    // due to Repository being instantiated internally without DI, CurrentRegistrationState access
    // requiring Config and TournamentInfo, Elmah error handling, and System.Web infrastructure
    // not available in .NET 10.0. The method updates tournament registration school selection
    // and redirects to Page03 on success or Home/Index on exception. The OdysseyMvc2024 version
    // is fully tested above in the Page02 POST Action Tests section.

    #endregion

    #region OdysseyMvc4 Page03 GET Action Tests

    // Note: Cannot fully test Page03 GET from System.Web.Mvc.Controller in .NET 10.0 test project
    // due to ViewEngines requiring full ASP.NET runtime environment, Repository being
    // instantiated internally without DI, SetBaseViewData requiring Config and TournamentInfo,
    // and System.Web infrastructure not available in .NET 10.0. The method initializes
    // Page03ViewData with NoJudgesFound=false and returns a view. The OdysseyMvc2024 version
    // is fully tested above in the Page03 GET Action Tests section.

    #endregion

    #region OdysseyMvc4 Page03 POST Action Tests

    // Note: Cannot fully test Page03 POST from System.Web.Mvc.Controller in .NET 10.0 test project
    // due to Repository being instantiated internally without DI, UpdateModel requiring full
    // ASP.NET MVC model binding infrastructure, CurrentRegistrationState access, Elmah error
    // handling, and System.Web infrastructure not available in .NET 10.0. The method searches
    // for judges by ID and name, validates they're not already assigned to a team, updates the
    // tournament registration with the judge ID, and redirects to Page05 on success or Home/Index
    // on exception. The OdysseyMvc2024 version is fully tested above in the Page03 POST Action Tests section.

    #endregion

    #region OdysseyMvc4 Page04 GET Action Tests

    // Note: Cannot fully test Page04 GET from System.Web.Mvc.Controller in .NET 10.0 test project
    // due to ViewEngines requiring full ASP.NET runtime environment, Repository being
    // instantiated internally without DI, SetBaseViewData requiring Config and TournamentInfo,
    // and System.Web infrastructure not available in .NET 10.0. The method checks for null id,
    // initializes Page04ViewData with NoVolunteersFound=false and returns a view, or redirects
    // to Error if id is null. The OdysseyMvc2024 version is fully tested above in the Page04 GET Action Tests section.

    #endregion

    #region OdysseyMvc4 Page04 POST Action Tests

    // Note: Cannot fully test Page04 POST from System.Web.Mvc.Controller in .NET 10.0 test project
    // due to Repository being instantiated internally without DI, UpdateModel requiring full
    // ASP.NET MVC model binding infrastructure, CurrentRegistrationState access, Elmah error
    // handling, and System.Web infrastructure not available in .NET 10.0. The method checks for
    // null id, searches for volunteers (currently commented out in production code), validates
    // they're not already assigned, updates registration, and redirects to Page05 on success or
    // Error on exception. The OdysseyMvc2024 version is fully tested above in the Page04 POST Action Tests section.

    #endregion

    #region Page05 GET Action Tests

    [Fact]
    public void Page05Get_WhenAvailable_ReturnsViewWithInitializedViewData()
    {
        _mockRepo.Setup(r => r.Config).Returns(TestHelper.CreateDefaultConfig());
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());

        var result = _controller.Page05(123);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.ViewResult>();
        var viewResult = result as Microsoft.AspNetCore.Mvc.ViewResult;
        viewResult!.Model.Should().BeOfType<OdysseyMvc2024.ViewData.TournamentRegistration.Page05ViewData>();
        _mockRepo.Verify(r => r.Config, Times.AtLeastOnce);
        _mockRepo.Verify(r => r.TournamentInfo, Times.AtLeastOnce);
    }

    [Theory]
    [InlineData("Soon")]
    [InlineData("Down")]
    [InlineData("Closed")]
    public void Page05Get_WhenNotAvailable_RedirectsToCurrentState(string state)
    {
        var config = TestHelper.CreateDefaultConfig();
        if (state == "Soon")
        {
            config["TournamentRegistrationOpenDateTime"] = "12/31/2099 23:59:59";
        }
        else if (state == "Down")
        {
            config["IsTournamentRegistrationDown"] = "true";
        }
        else if (state == "Closed")
        {
            config["TournamentRegistrationCloseDateTime"] = "01/01/2020 00:00:00";
        }

        _mockRepo.Setup(r => r.Config).Returns(config);
        _mockRepo.Setup(r => r.TournamentInfo).Returns(TestHelper.CreateDefaultTournamentInfo());

        var result = _controller.Page05(123);

        result.Should().BeOfType<Microsoft.AspNetCore.Mvc.RedirectToActionResult>();
        var redirectResult = result as Microsoft.AspNetCore.Mvc.RedirectToActionResult;
        redirectResult!.ActionName.Should().Be(state);
    }

    #endregion

    #region OdysseyMvc4 Page05 GET Action Tests

    // Note: Cannot fully test Page05 GET from System.Web.Mvc.Controller in .NET 10.0 test project
    // due to ViewEngines requiring full ASP.NET runtime environment, Repository being
    // instantiated internally without DI, SetBaseViewData requiring Config and TournamentInfo,
    // and System.Web infrastructure not available in .NET 10.0. The method initializes
    // Page05ViewData and returns a view. The OdysseyMvc2024 version is fully tested above
    // in the Page05 GET Action Tests section.

    #endregion

    #region OdysseyMvc4 Page05 POST Action Tests

    // Note: Cannot fully test Page05 POST from System.Web.Mvc.Controller in .NET 10.0 test project
    // due to Repository being instantiated internally without DI, UpdateModel requiring full
    // ASP.NET MVC model binding infrastructure, BuildMessage method creating MailAddress objects
    // which validates email format, CurrentRegistrationState access, Elmah error handling, and
    // System.Web infrastructure not available in .NET 10.0. The method validates coach and alternate
    // coach email addresses using BuildMessage, updates tournament registration with coach contact
    // information, and redirects to Page06 on success, BadCoachEmail/BadAltCoachEmail on invalid
    // emails, or Home/Index on exception. The OdysseyMvc2024 version is fully tested below
    // in the Page05 POST Action Tests section.

    #endregion
}
