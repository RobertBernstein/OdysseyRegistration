using FluentAssertions;
using OdysseyMvc2024.Models;

namespace OdysseyMvc4.Tests.Unit.Models;

/// <summary>
/// Tests for model classes to ensure property types and behaviors are preserved during migration.
/// These tests verify that model shapes match what the database and controllers expect.
/// </summary>
public class ModelTests
{
    #region Judge Model Tests

    [Fact]
    public void Judge_AllProperties_CanBeSetAndRetrieved()
    {
        var judge = new Judge
        {
            JudgeID = 42,
            TeamID = "T001",
            FirstName = "John",
            LastName = "Doe",
            Address = "123 Main St",
            AddressLine2 = "Apt 4",
            City = "Springfield",
            State = "VA",
            ZipCode = "22150",
            DaytimePhone = "703-555-0100",
            EveningPhone = "703-555-0101",
            MobilePhone = "703-555-0102",
            EmailAddress = "john@example.com",
            Notes = "Test notes",
            ProblemChoice1 = "1",
            ProblemChoice2 = "2",
            ProblemChoice3 = "3",
            HasChildrenCompeting = "Yes",
            COI = "Problem A",
            ProblemCOI1 = "1",
            ProblemCOI2 = "2",
            ProblemCOI3 = "3",
            ProblemAssigned = "Problem B",
            InformationMailed_ = true,
            AttendedJT_ = false,
            Active = true,
            WillingToBeScorechecker = "Yes",
            TshirtSize = "L",
            WantsCEUCredit = "Yes",
            YearsOfLongTermJudgingExperience = "5",
            YearsOfSpontaneousJudgingExperience = "3",
            PreviousPositions = "Head Judge;Problem Judge",
            ProblemID = "1",
            TimeRegistered = new DateTime(2025, 1, 15, 10, 0, 0),
            TimeAssignedToTeam = new DateTime(2025, 1, 20),
            TimeRegistrationStarted = new DateTime(2025, 1, 15, 9, 55, 0),
            UserAgent = "Mozilla/5.0"
        };

        judge.JudgeID.Should().Be(42);
        judge.TeamID.Should().Be("T001");
        judge.FirstName.Should().Be("John");
        judge.LastName.Should().Be("Doe");
        judge.Address.Should().Be("123 Main St");
        judge.AddressLine2.Should().Be("Apt 4");
        judge.City.Should().Be("Springfield");
        judge.State.Should().Be("VA");
        judge.ZipCode.Should().Be("22150");
        judge.DaytimePhone.Should().Be("703-555-0100");
        judge.EveningPhone.Should().Be("703-555-0101");
        judge.MobilePhone.Should().Be("703-555-0102");
        judge.EmailAddress.Should().Be("john@example.com");
        judge.Notes.Should().Be("Test notes");
        judge.ProblemChoice1.Should().Be("1");
        judge.ProblemChoice2.Should().Be("2");
        judge.ProblemChoice3.Should().Be("3");
        judge.HasChildrenCompeting.Should().Be("Yes");
        judge.PreviousPositions.Should().Be("Head Judge;Problem Judge");
        judge.TshirtSize.Should().Be("L");
        judge.WantsCEUCredit.Should().Be("Yes");
        judge.Active.Should().BeTrue();
        judge.TimeRegistered.Should().Be(new DateTime(2025, 1, 15, 10, 0, 0));
        judge.UserAgent.Should().Be("Mozilla/5.0");
    }

    [Fact]
    public void Judge_NullableProperties_DefaultToNull()
    {
        var judge = new Judge();

        judge.InformationMailed_.Should().BeNull();
        judge.AttendedJT_.Should().BeNull();
        judge.Active.Should().BeNull();
        judge.TimeRegistered.Should().BeNull();
        judge.TimeAssignedToTeam.Should().BeNull();
        judge.TimeRegistrationStarted.Should().BeNull();
    }

    #endregion

    #region TournamentRegistration Model Tests

    [Fact]
    public void TournamentRegistration_AllProperties_CanBeSetAndRetrieved()
    {
        var reg = new TournamentRegistration
        {
            Id = 1,
            MembershipName = "Test Team",
            MembershipNumber = "12345",
            ProblemID = 3,
            Division = "2",
            SchoolID = 10,
            CoachFirstName = "Jane",
            CoachLastName = "Smith",
            CoachAddress = "456 Oak Ave",
            CoachCity = "Fairfax",
            CoachState = "VA",
            CoachZipCode = "22030",
            CoachEveningPhone = "703-555-0200",
            CoachDaytimePhone = "703-555-0201",
            CoachMobilePhone = "703-555-0202",
            CoachEmailAddress = "jane@example.com",
            AltCoachFirstName = "Bob",
            AltCoachLastName = "Jones",
            MemberFirstName1 = "Alice",
            MemberLastName1 = "Brown",
            MemberGrade1 = "5",
            MemberFirstName2 = "Charlie",
            MemberLastName2 = "Davis",
            MemberGrade2 = "6",
            Spontaneous = true,
            Notes = "Test notes",
            SpecialConsiderations = "None",
            SchedulingIssues = "None",
            Paid = 1,
            JudgeID = 42,
            TeamRegistrationFee = "$100",
            VolunteerID = 5,
            TimeRegistrationStarted = new DateTime(2025, 1, 10),
            TimeRegistered = new DateTime(2025, 1, 10, 14, 30, 0),
            UserAgent = "Chrome/120"
        };

        reg.Id.Should().Be(1);
        reg.MembershipName.Should().Be("Test Team");
        reg.Division.Should().Be("2");
        reg.CoachFirstName.Should().Be("Jane");
        reg.MemberFirstName1.Should().Be("Alice");
        reg.MemberGrade1.Should().Be("5");
        reg.MemberFirstName2.Should().Be("Charlie");
        reg.MemberGrade2.Should().Be("6");
        reg.Spontaneous.Should().BeTrue();
        reg.JudgeID.Should().Be(42);
        reg.TeamRegistrationFee.Should().Be("$100");
    }

    [Fact]
    public void TournamentRegistration_AllSevenMembers_CanBeSet()
    {
        var reg = new TournamentRegistration
        {
            MemberFirstName1 = "M1", MemberLastName1 = "L1", MemberGrade1 = "1",
            MemberFirstName2 = "M2", MemberLastName2 = "L2", MemberGrade2 = "2",
            MemberFirstName3 = "M3", MemberLastName3 = "L3", MemberGrade3 = "3",
            MemberFirstName4 = "M4", MemberLastName4 = "L4", MemberGrade4 = "4",
            MemberFirstName5 = "M5", MemberLastName5 = "L5", MemberGrade5 = "5",
            MemberFirstName6 = "M6", MemberLastName6 = "L6", MemberGrade6 = "6",
            MemberFirstName7 = "M7", MemberLastName7 = "L7", MemberGrade7 = "7"
        };

        reg.MemberFirstName7.Should().Be("M7");
        reg.MemberLastName7.Should().Be("L7");
        reg.MemberGrade7.Should().Be("7");
    }

    [Fact]
    public void TournamentRegistration_NullableProperties_DefaultToNull()
    {
        var reg = new TournamentRegistration();

        reg.ProblemID.Should().BeNull();
        reg.SchoolID.Should().BeNull();
        reg.Spontaneous.Should().BeNull();
        reg.Paid.Should().BeNull();
        reg.JudgeID.Should().BeNull();
        reg.VolunteerID.Should().BeNull();
        reg.TimeRegistrationStarted.Should().BeNull();
        reg.TimeRegistered.Should().BeNull();
    }

    #endregion

    #region Problem Model Tests

    [Fact]
    public void Problem_AllProperties_CanBeSetAndRetrieved()
    {
        var problem = new Problem
        {
            ProblemID = 1,
            ProblemCategory = "Vehicle",
            ProblemName = "Problem A: It's How You Look At It",
            ProblemDescription = "Teams will create a performance...",
            Divisions = "1,2,3",
            CostLimit = "$145",
            ProblemCaptainID = "PC001"
        };

        problem.ProblemID.Should().Be(1);
        problem.ProblemCategory.Should().Be("Vehicle");
        problem.ProblemName.Should().Be("Problem A: It's How You Look At It");
        problem.Divisions.Should().Be("1,2,3");
        problem.CostLimit.Should().Be("$145");
    }

    #endregion

    #region Event Model Tests

    [Fact]
    public void Event_AllProperties_CanBeSetAndRetrieved()
    {
        var evt = new Event
        {
            ID = 1,
            EventName = "Regional Tournament",
            Location = "Springfield High School",
            LocationURL = "http://www.springfield.edu",
            LocationAddress = "123 Main St",
            LocationCity = "Springfield",
            LocationState = "VA",
            LocationPhone = "703-555-0300",
            StartDate = new DateTime(2025, 3, 15),
            EndDate = new DateTime(2025, 3, 15),
            Time = "8:00 AM",
            EventCoordinatorName = "Coordinator",
            EventCoordinatorEmail = "coord@example.com",
            EventCost = "100",
            LateEventCost = "125",
            LateEventCostStartDate = new DateTime(2025, 3, 1),
            PaymentDueDate = new DateTime(2025, 3, 10),
            EventMakeChecksOutTo = "NoVA North Odyssey",
            EventMailBody = "Thank you"
        };

        evt.ID.Should().Be(1);
        evt.EventName.Should().Be("Regional Tournament");
        evt.Location.Should().Be("Springfield High School");
        evt.EventCost.Should().Be("100");
        evt.LateEventCost.Should().Be("125");
        evt.LateEventCostStartDate.Should().Be(new DateTime(2025, 3, 1));
        evt.PaymentDueDate.Should().Be(new DateTime(2025, 3, 10));
    }

    [Fact]
    public void Event_NullableDates_DefaultToNull()
    {
        var evt = new Event();

        evt.StartDate.Should().BeNull();
        evt.EndDate.Should().BeNull();
        evt.LateEventCostStartDate.Should().BeNull();
        evt.PaymentDueDate.Should().BeNull();
    }

    #endregion
}
