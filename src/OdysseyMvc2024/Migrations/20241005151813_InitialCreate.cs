using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OdysseyMvc2024.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Config",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "ContactUsRecipient",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint", nullable: false),
                    contact_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email_address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUsRecipient", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContactUsSenderRole",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint", nullable: false),
                    role_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUsSenderRole", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationURLColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCoordinatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCoordinatorEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCoordinatorPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InformationURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationMapURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventPayeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventPayeeAddress1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventPayeeAddress2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventPayeeCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventPayeeState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventPayeeZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventPayeePhone1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventPayeeEmail1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LateEventCost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LateEventCostStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentDueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EventMakeChecksOutTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventVolunteerInformationMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamsVolunteerWantsToSeeMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventMailBody = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Judge",
                columns: table => new
                {
                    JudgeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DaytimePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EveningPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemChoice1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemChoice2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemChoice3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasChildrenCompeting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemCOI1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemCOI2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemCOI3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemAssigned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InformationMailed_ = table.Column<bool>(type: "bit", nullable: true),
                    AttendedJT_ = table.Column<bool>(type: "bit", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    WillingToBeScorechecker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TshirtSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WantsCEUCredit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearsOfLongTermJudgingExperience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearsOfSpontaneousJudgingExperience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousPositions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeRegistered = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeAssignedToTeam = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeRegistrationStarted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Judge", x => x.JudgeID);
                });

            migrationBuilder.CreateTable(
                name: "Problem",
                columns: table => new
                {
                    ProblemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProblemCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Divisions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostLimit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemCaptainID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PCFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PCLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PCAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PCCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PCStateOrProvince = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PCPostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PCWorkPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PCHomePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PCMobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PCFaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PCEmail1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PCEmail2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problem", x => x.ProblemID);
                });

            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Membership_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Membership_1seen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Membership_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Membership_2seen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Membership_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Membership_3seen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Membership_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Membership_4seen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordNew_ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordPostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordAltPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordMobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordFaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordEmailName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Share_ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TournamentRegistration",
                columns: table => new
                {
                    TeamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MembershipName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MembershipNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemID = table.Column<int>(type: "int", nullable: true),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchoolID = table.Column<int>(type: "int", nullable: true),
                    CoachFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoachLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoachAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoachCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoachState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoachZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoachEveningPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoachDaytimePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoachMobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoachEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AltCoachFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AltCoachLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AltCoachEveningPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AltCoachDaytimePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AltCoachMobilePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AltCoachEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberFirstName1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberLastName1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberGrade1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberFirstName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberLastName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberGrade2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberFirstName3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberLastName3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberGrade3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberFirstName4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberLastName4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberGrade4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberFirstName5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberLastName5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberGrade5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberFirstName6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberLastName6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberGrade6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberFirstName7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberLastName7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberGrade7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Spontaneous = table.Column<bool>(type: "bit", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialConsiderations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchedulingIssues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Paid = table.Column<short>(type: "smallint", nullable: true),
                    JudgeID = table.Column<short>(type: "smallint", nullable: true),
                    TeamRegistrationFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VolunteerID = table.Column<int>(type: "int", nullable: true),
                    TimeRegistrationStarted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeRegistered = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentRegistration", x => x.TeamID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Config");

            migrationBuilder.DropTable(
                name: "ContactUsRecipient");

            migrationBuilder.DropTable(
                name: "ContactUsSenderRole");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Judge");

            migrationBuilder.DropTable(
                name: "Problem");

            migrationBuilder.DropTable(
                name: "School");

            migrationBuilder.DropTable(
                name: "TournamentRegistration");
        }
    }
}
