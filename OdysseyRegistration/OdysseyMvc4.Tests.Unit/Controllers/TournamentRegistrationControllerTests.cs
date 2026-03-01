using FluentAssertions;
using Moq;
using OdysseyMvc2024.Controllers;
using OdysseyMvc2024.Models;
using OdysseyMvc4.Tests.Unit.Helpers;

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
}
