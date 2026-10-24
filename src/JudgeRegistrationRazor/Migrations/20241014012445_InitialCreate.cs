using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JudgeRegistrationRazor.Migrations
{
    internal partial class InitialCreate : Migration
    {
        private static readonly string[] ConfigColumns = ["Id", "Name", "Value"];

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoachesTrainingDivisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_coaches_training_divisions", x => x.Id));

            migrationBuilder.CreateTable(
                name: "CoachesTrainingRegions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table => table.PrimaryKey("PK_coaches_training_regions", x => x.Id));

            migrationBuilder.CreateTable(
                name: "CoachesTrainingRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SchoolName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Division = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SelectedProblem = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    YearsInvolved = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    RegionNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TimeRegistered = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table => table.PrimaryKey("PK_coaches_training", x => x.Id));

            migrationBuilder.CreateTable(
                name: "CoachesTrainingRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_coaches_training_roles", x => x.Id));

            migrationBuilder.CreateTable(
                name: "Config",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true)
                },
                constraints: table => table.PrimaryKey("PK_config", x => x.Id));

            migrationBuilder.CreateTable(
                name: "ContactUsRecipients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contact_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email_address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_contact_us_recipients", x => x.Id));

            migrationBuilder.CreateTable(
                name: "ContactUsSenderRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table => table.PrimaryKey("PK_contact_us_sender_roles", x => x.Id));

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LocationURL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LocationURLColor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LocationAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LocationCity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LocationState = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    LocationPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Time = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EventCoordinatorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EventCoordinatorEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EventCoordinatorPhone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InformationURL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LocationMapURL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EventPayeeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EventPayeeAddress1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EventPayeeAddress2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EventPayeeCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EventPayeeState = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EventPayeeZipCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    EventPayeePhone1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EventPayeeEmail1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EventCost = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LateEventCost = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LateEventCostStartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PaymentDueDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EventMakeChecksOutTo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    EventVolunteerInformationMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamsVolunteerWantsToSeeMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventMailBody = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table => table.PrimaryKey("PK_events", x => x.Id));

            migrationBuilder.CreateTable(
                name: "Judges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DaytimePhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EveningPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemChoice1 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    ProblemChoice2 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    ProblemChoice3 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    HasChildrenCompeting = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    COI = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProblemCOI1 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    ProblemCOI2 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    ProblemCOI3 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    ProblemAssigned = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InformationMailed = table.Column<bool>(type: "bit", name: "InformationMailed?", nullable: true),
                    AttendedJT = table.Column<bool>(type: "bit", name: "AttendedJT?", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    WillingToBeScorechecker = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    TshirtSize = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WantsCEUCredit = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    YearsOfLongTermJudgingExperience = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YearsOfSpontaneousJudgingExperience = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PreviousPositions = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProblemID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TimeRegistered = table.Column<DateTime>(type: "datetime", nullable: true),
                    TimeAssignedToTeam = table.Column<DateTime>(type: "datetime", nullable: true),
                    TimeRegistrationStarted = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table => table.PrimaryKey("PK_judges", x => x.Id));

            migrationBuilder.CreateTable(
                name: "Problem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProblemCategory = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ProblemName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProblemDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Divisions = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CostLimit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProblemCaptainID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PCFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PCLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PCAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PCCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PCStateOrProvince = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PCPostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PCWorkPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PCHomePhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PCMobilePhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PCFaxNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PCEmail1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PCEmail2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table => table.PrimaryKey("PK_problem", x => x.Id));

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: static table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegionNumber = table.Column<short>(type: "smallint", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Membership1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, name: "Membership#1", nullable: true),
                    Membership1seen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, name: "Membership#1seen", nullable: true),
                    Membership2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, name: "Membership#2", nullable: true),
                    Membership2seen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, name: "Membership#2seen", nullable: true),
                    Membership3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, name: "Membership#3", nullable: true),
                    Membership3seen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, name: "Membership#3seen", nullable: true),
                    Membership4 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, name: "Membership#4", nullable: true),
                    Membership4seen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, name: "Membership#4seen", nullable: true),
                    CoordNew = table.Column<string>(type: "nvarchar(50)", maxLength: 50, name: "CoordNew?", nullable: true),
                    CoordFirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CoordLastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CoordAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CoordCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CoordState = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CoordPostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CoordPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CoordAltPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CoordMobilePhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CoordFaxNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CoordEmailName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Share = table.Column<string>(type: "nvarchar(50)", maxLength: 50, name: "Share?", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table => table.PrimaryKey("PK_Schools", x => x.Id));

            CreateTournamentRegistrationTable(migrationBuilder);
            AddRegistrationDetails(migrationBuilder);
            AddCoachDetails(migrationBuilder);
            AddAlternateCoachDetails(migrationBuilder);
            AddMemberDetails(migrationBuilder);
            AddRegistrationState(migrationBuilder);

            migrationBuilder.CreateTable(
                name: "Volunteers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamID = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DaytimePhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EveningPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    MobilePhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VolunteerWantsToSee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeRegistrationStarted = table.Column<DateTime>(type: "datetime", nullable: true),
                    TimeRegistered = table.Column<DateTime>(type: "datetime", nullable: true),
                    TimeAssignedToTeam = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table => table.PrimaryKey("PK_Volunteers", x => x.Id));

            migrationBuilder.InsertData(
                table: "Config",
                columns: ConfigColumns,
                values: new object[,]
                {
                    { 1, "AcceptingPayPal", "True" },
                    { 2, "CoachesHandbookURL", "" },
                    { 3, "CoachesTrainingRegistrationCloseDateTime", "2013-11-08 22:00" },
                    { 4, "CoachesTrainingRegistrationOpenDateTime", "2013-08-01 00:00" },
                    { 5, "ContactUsURL", "/wp/?page_id=129" },
                    { 6, "CoordinatorsDoNotPayCoachesTrainingRegistrationFee", "True" },
                    { 7, "EmailServer", "mail.novanorth.org" },
                    { 8, "EventVolunteerInformationMessage", "Each team is required to provide one volunteer. &nbsp;As that volunteer, you will be asked to work about 2 hours at morning registration for judges and teams, souvenir sales, the spontaneous waiting area, or other similar jobs. &nbsp;Volunteers will be informed by e-mail of their assignment." },
                    { 9, "HomePage", "https://www.novanorth.org" },
                    { 10, "IsCoachesTrainingRegistrationDown", "False" },
                    { 11, "IsJudgesRegistrationDown", "False" },
                    { 12, "IsTournamentRegistrationDown", "False" },
                    { 13, "IsVolunteerRegistrationDown", "False" },
                    { 14, "JudgesRegistrationCloseDateTime", "2024-02-29 23:59" },
                    { 15, "JudgesRegistrationOpenDateTime", "2023-11-28 00:00" },
                    { 16, "LinkToSynopses", "https://www.odysseyofthemind.com/wp-content/uploads/2023/06/2024-SYNOPSIS.pdf" },
                    { 17, "PrimaryTeamsMayDoSpontaneous", "True" },
                    { 18, "ProgramGuideURL", "https://www.odysseyofthemind.com/program-guide" },
                    { 19, "RegionalDirectorEmail", "director@novanorth.org" },
                    { 20, "RegionalDirectorText", "our Regional Director" },
                    { 21, "RegionName", "NoVA North and NoVA South" },
                    { 22, "RegionNumber", "9  & 12" },
                    { 23, "SchoolCoordinatorsDoNotPayMessage", "<ul><li>Note that School Coordinators do not pay this fee.</li></ul>" },
                    { 24, "TeamsVolunteerWantsToSeeMessage", "Please list the teams that you would like to see perform during the day so we may do our best to schedule around this conflict. &nbsp;For each team, list the school, division, problem name, and coach's name so that we know which team you mean. &nbsp;Also let us know if there is any time the day of the tournament that you will not be available to serve as a volunteer: " },
                    { 25, "TournamentRegistrationCloseDateTime", "2024-1-25 23: 59" },
                    { 26, "TournamentRegistrationOpenDateTime", "2023-12-15 00: 00" },
                    { 27, "VolunteerNotesMessage", "Please provide us with any other information you would like to share as part of your registration.&nbsp; For example, this is a good place to let us know if you have any previous experience volunteering at Odyssey tournaments, and if so, what that was. &nbsp;Also let us know if you would be interested in doing that again or if you would prefer to do something else. &nbsp;Please keep your comments to 500 characters or less: " },
                    { 28, "VolunteerRegistrationCloseDateTime", "2014-1-18 14: 00" },
                    { 29, "VolunteerRegistrationOpenDateTime", "2013-11-10 00: 00" },
                    { 30, "WebmasterEmail", "webmaster@novanorth.org" },
                    { 31, "WebmasterEmailPassword", "" },
                    { 32, "WillHaveCoachesTrainingRegistration", "False" },
                    { 33, "WillHaveVolunteerRegistration", "False" },
                    { 34, "Year", "2023" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoachesTrainingDivisions");

            migrationBuilder.DropTable(
                name: "CoachesTrainingRegions");

            migrationBuilder.DropTable(
                name: "CoachesTrainingRegistrations");

            migrationBuilder.DropTable(
                name: "CoachesTrainingRoles");

            migrationBuilder.DropTable(
                name: "Config");

            migrationBuilder.DropTable(
                name: "ContactUsRecipients");

            migrationBuilder.DropTable(
                name: "ContactUsSenderRoles");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Judges");

            migrationBuilder.DropTable(
                name: "Problem");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "TournamentRegistration");

            migrationBuilder.DropTable(
                name: "Volunteers");
        }

        private static void CreateTournamentRegistrationTable(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TournamentRegistration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table => table.PrimaryKey("PK_TournamentRegistration", x => x.Id));
        }

        private static void AddRegistrationDetails(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MembershipName",
                table: "TournamentRegistration",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MembershipNumber",
                table: "TournamentRegistration",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProblemID",
                table: "TournamentRegistration",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Division",
                table: "TournamentRegistration",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SchoolID",
                table: "TournamentRegistration",
                type: "int",
                nullable: true);
        }

        private static void AddCoachDetails(MigrationBuilder migrationBuilder)
        {
            AddTextColumn(migrationBuilder, "CoachFirstName", 50);
            AddTextColumn(migrationBuilder, "CoachLastName", 50);
            AddTextColumn(migrationBuilder, "CoachAddress", 255);
            AddTextColumn(migrationBuilder, "CoachCity", 50);
            AddTextColumn(migrationBuilder, "CoachState", 20);
            AddTextColumn(migrationBuilder, "CoachZipCode", 20);
            AddTextColumn(migrationBuilder, "CoachEveningPhone", 30);
            AddTextColumn(migrationBuilder, "CoachDaytimePhone", 30);
            AddTextColumn(migrationBuilder, "CoachMobilePhone", 30);
            AddTextColumn(migrationBuilder, "CoachEmailAddress", 50);
        }

        private static void AddAlternateCoachDetails(MigrationBuilder migrationBuilder)
        {
            AddTextColumn(migrationBuilder, "AltCoachFirstName", 50);
            AddTextColumn(migrationBuilder, "AltCoachLastName", 50);
            AddTextColumn(migrationBuilder, "AltCoachEveningPhone", 50);
            AddTextColumn(migrationBuilder, "AltCoachDaytimePhone", 50);
            AddTextColumn(migrationBuilder, "AltCoachMobilePhone", 30);
            AddTextColumn(migrationBuilder, "AltCoachEmailAddress", 50);
        }

        private static void AddMemberDetails(MigrationBuilder migrationBuilder)
        {
            AddMemberFields(migrationBuilder, "1");
            AddMemberFields(migrationBuilder, "2");
            AddMemberFields(migrationBuilder, "3");
            AddMemberFields(migrationBuilder, "4");
            AddMemberFields(migrationBuilder, "5");
            AddMemberFields(migrationBuilder, "6");
            AddMemberFields(migrationBuilder, "7");
        }

        private static void AddMemberFields(MigrationBuilder migrationBuilder, string memberNumber)
        {
            migrationBuilder.AddColumn<string>(
                name: $"MemberFirstName{memberNumber}",
                table: "TournamentRegistration",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: $"MemberLastName{memberNumber}",
                table: "TournamentRegistration",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: $"MemberGrade{memberNumber}",
                table: "TournamentRegistration",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        private static void AddRegistrationState(MigrationBuilder migrationBuilder)
        {
            AddBooleanRegistrationStateFields(migrationBuilder);
            AddTextRegistrationStateFields(migrationBuilder);
            AddNumericRegistrationStateFields(migrationBuilder);
            AddDateTimeRegistrationStateFields(migrationBuilder);
        }

        private static void AddBooleanRegistrationStateFields(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Spontaneous",
                table: "TournamentRegistration",
                type: "bit",
                nullable: true);
        }

        private static void AddTextRegistrationStateFields(MigrationBuilder migrationBuilder)
        {
            AddTextColumn(migrationBuilder, "Notes");
            AddTextColumn(migrationBuilder, "SpecialConsiderations");
            AddTextColumn(migrationBuilder, "SchedulingIssues");
            AddTextColumn(migrationBuilder, "TeamRegistrationFee", 20);
            AddTextColumn(migrationBuilder, "UserAgent");
        }

        private static void AddNumericRegistrationStateFields(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Paid",
                table: "TournamentRegistration",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "JudgeID",
                table: "TournamentRegistration",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VolunteerID",
                table: "TournamentRegistration",
                type: "int",
                nullable: true);
        }

        private static void AddDateTimeRegistrationStateFields(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimeRegistrationStarted",
                table: "TournamentRegistration",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeRegistered",
                table: "TournamentRegistration",
                type: "datetime",
                nullable: true);
        }

        private static void AddTextColumn(MigrationBuilder migrationBuilder, string columnName, int? maxLength = null)
        {
            migrationBuilder.AddColumn<string>(
                name: columnName,
                table: "TournamentRegistration",
                type: maxLength.HasValue ? $"nvarchar({maxLength.Value})" : "nvarchar(max)",
                maxLength: maxLength,
                nullable: true);
        }
    }
}
