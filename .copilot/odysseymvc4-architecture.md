# OdysseyMvc4 — Complete Architecture Reference

## Overview

OdysseyMvc4 is the **production** ASP.NET MVC web application for the NoVA North Odyssey of the Mind regional registration system. It handles registration for four event types: Tournament, Judges, Volunteer, and Coaches Training. The application is built on **.NET Framework 4.8** using **ASP.NET MVC 5.3** with **Entity Framework 6.5.1** (Database First via EDMX) and is hosted on **WinHost** (IIS). The codebase was originally written in 2010 (ASP.NET MVC 1), rebuilt in 2013 as MVC 4, and has been maintained since. Copyright is held by **Tardis Technologies**.

**CRITICAL: This is the production application. Never make changes that could break it.**

## Technology Stack

| Component            | Version / Detail                                      |
|----------------------|-------------------------------------------------------|
| Framework            | .NET Framework 4.8                                    |
| Web Framework        | ASP.NET MVC 5.3 (Microsoft.AspNet.Mvc 5.3.0)         |
| Web API              | ASP.NET Web API 5.3 (coexists in same project)        |
| ORM                  | Entity Framework 6.5.1 (Database First, EDMX)         |
| View Engine          | Razor (.cshtml)                                       |
| JavaScript Libraries | jQuery 3.7.1, jQuery UI 1.14.1, jQuery Validate 1.21, Knockout 3.5.1, Inputmask 5.0.9, Modernizr 2.8.3 |
| Error Logging        | ELMAH (elmah.corelibrary 1.2.2 + Elmah.Mvc 2.1.2)    |
| JSON                 | Newtonsoft.Json 13.0.4                                |
| CSS                  | Site.css (base), NovaNorth.css, NovaSouth.css (region-specific) |
| Bundling             | System.Web.Optimization 1.1.3                         |
| Configuration        | Microsoft.Configuration.ConfigurationBuilders.UserSecrets 3.0.0 |
| Hosting              | WinHost (IIS), SQL Server 2008 R2                     |
| Dev Server           | IIS Express (port 58363)                              |
| Project Type         | ASP.NET Web Application (.csproj, non-SDK style)      |
| Namespace            | `OdysseyMvc4`                                         |

## Project Structure

```
OdysseyMvc4/
├── App_Start/                          # Application startup configuration
│   ├── BundleConfig.cs                 # JavaScript/CSS bundle definitions
│   ├── FilterConfig.cs                 # Global MVC filters (HandleErrorAttribute)
│   ├── RouteConfig.cs                  # MVC routing (default: {controller}/{action}/{id})
│   ├── WebApiConfig.cs                 # Web API routing (api/{controller}/{id})
│   └── MvcApplication.cs              # (listed in csproj, may be Global.asax.cs alias)
├── Controllers/                        # MVC Controllers
│   ├── BaseRegistrationController.cs   # Abstract base for all registration controllers
│   ├── HomeController.cs               # Home/landing page
│   ├── TournamentRegistrationController.cs  # Tournament team registration (10 pages)
│   ├── JudgesRegistrationController.cs      # Judge registration (3 pages)
│   ├── VolunteerRegistrationController.cs   # Volunteer registration (3 pages)
│   ├── CoachesTrainingRegistrationController.cs  # Coaches training registration (2 pages)
│   └── CoachesTrainingRegistrationController.dll.cs  # (compiled/duplicate)
├── Models/                             # Entity Framework models + repository
│   ├── OdysseyModel.edmx               # EF Database First model (EDMX)
│   ├── OdysseyModel.edmx.diagram       # Visual diagram of the EDMX
│   ├── OdysseyModel.tt                 # T4 template generating entity classes
│   ├── OdysseyModel.Context.tt         # T4 template generating DbContext
│   ├── OdysseyModel.Context.cs         # Generated DbContext (OdysseyEntities)
│   ├── OdysseyModel.cs                 # Generated model (unused boilerplate)
│   ├── OdysseyModel.Designer.cs        # Designer-generated code
│   ├── OdysseyRepository.cs            # Repository pattern - all DB access (~800+ lines)
│   ├── Config.cs                       # Config entity (Name/Value key-value pairs)
│   ├── Event.cs                        # Event entity (tournaments, training, etc.)
│   ├── Judge.cs                        # Judge entity
│   ├── TournamentRegistration.cs       # Tournament registration entity
│   ├── Volunteer.cs                    # Volunteer entity
│   ├── CoachesTrainingRegistration.cs  # Coaches training registration entity
│   ├── Problem.cs                      # Odyssey problem/challenge entity
│   ├── School.cs                       # School entity
│   ├── CoachesTrainingDivision.cs      # Division lookup entity
│   ├── CoachesTrainingRegion.cs        # Region lookup entity
│   ├── CoachesTrainingRole.cs          # Role lookup entity
│   ├── ContactUsRecipient.cs           # Contact form recipient entity
│   ├── ContactUsSenderRole.cs          # Contact form sender role entity
│   ├── JudgesExport.cs                 # DTO for judge data export
│   └── QueryInfo.cs                    # Utility class for SQL query metadata
├── ViewData/                           # View model classes (passed to Razor views)
│   ├── BaseViewData.cs                 # Base view model with Config, Region, Tournament info
│   ├── TournamentRegistration/         # Tournament-specific view models
│   │   ├── Page01ViewData.cs           # Registration fees, dates, PayPal status
│   │   ├── Page02ViewData.cs through Page10ViewData.cs
│   │   └── ResendEmailViewData.cs      # For resending confirmation emails
│   ├── JudgesRegistration/             # Judge-specific view models
│   │   ├── Page01ViewData.cs           # Training date/location/time
│   │   ├── Page02ViewData.cs
│   │   └── Page03ViewData.cs
│   ├── VolunteerRegistration/          # Volunteer-specific view models
│   │   ├── Page01ViewData.cs
│   │   ├── Page02ViewData.cs
│   │   └── Page03ViewData.cs
│   └── CoachesTrainingRegistration/    # Coaches training view models
│       ├── Page01ViewData.cs
│       └── Page02ViewData.cs
├── Views/                              # Razor views
│   ├── _ViewStart.cshtml               # Sets default layout
│   ├── Web.config                      # View-specific web config
│   ├── Shared/
│   │   ├── _Layout.cshtml              # Master layout (header, navigation, footer)
│   │   ├── Error.cshtml                # Error page
│   │   ├── Closed.cshtml               # "Registration closed" page
│   │   ├── Down.cshtml                 # "Registration temporarily down" page
│   │   ├── Soon.cshtml                 # "Registration coming soon" page
│   │   ├── BadEmail.cshtml             # Invalid email error page
│   │   ├── EmailPartial.cshtml         # Shared email partial view
│   │   └── TournamentRegistration/
│   │       └── EmailPartial.cshtml     # Tournament-specific email partial
│   ├── Home/
│   │   └── Index.cshtml                # Home page with registration links
│   ├── TournamentRegistration/
│   │   ├── Page01.cshtml through Page10.cshtml  # 10-page wizard
│   │   ├── ResendEmail.cshtml
│   │   ├── BadCoachEmail.cshtml
│   │   ├── BadAltCoachEmail.cshtml
│   │   └── Error.cshtml
│   ├── JudgesRegistration/
│   │   ├── Page01.cshtml through Page03.cshtml  # 3-page wizard
│   ├── VolunteerRegistration/
│   │   ├── Page01.cshtml through Page03.cshtml  # 3-page wizard
│   └── CoachesTrainingRegistration/
│       ├── Page01.cshtml and Page02.cshtml      # 2-page wizard
├── Content/                            # Static CSS and images
│   ├── Site.css                        # Base site styles
│   ├── NovaNorth.css                   # NoVA North region-specific styles
│   ├── NovaSouth.css                   # NoVA South region-specific styles
│   ├── images/                         # Bullet and navigation images
│   └── themes/base/                    # jQuery UI theme (Redmond)
├── Scripts/                            # Client-side JavaScript
│   ├── jquery-3.7.1.js (+min, slim)    # jQuery core
│   ├── jquery-ui-1.14.1.js (+min)      # jQuery UI
│   ├── jquery.validate.js (+min)       # Form validation
│   ├── jquery.unobtrusive-ajax.js      # Unobtrusive AJAX
│   ├── jquery.validate.unobtrusive.js  # Unobtrusive validation
│   ├── knockout-3.5.1.js               # Knockout.js MVVM
│   ├── modernizr-2.8.3.js             # Feature detection
│   ├── PrintThisPage.js               # Print helper
│   ├── inputmask/                      # jQuery inputmask plugin
│   └── _references.js                  # Script references for IntelliSense
├── Properties/
│   ├── AssemblyInfo.cs                 # Assembly metadata (Tardis Technologies)
│   └── PublishProfiles/                # Deployment profiles
│       ├── novanorth.org - FTP.pubxml
│       ├── novanorth.org - Web Deploy.pubxml
│       ├── novasouth.org - FTP.pubxml
│       ├── novasouth.org - Web Deploy.pubxml
│       ├── Test-Server.pubxml
│       └── test2.pubxml
├── Documentation/                      # Word documents
│   ├── Registration - Configuration (In Progress).docx
│   ├── Registration - Configuring Open and Close Dates and Times.docx
│   ├── Registration - Connecting to the SQL Server Database.docx
│   ├── Website - Configuration Information.docx
│   └── Website - Fixing WordPress Theme Problems.docx
├── Global.asax / Global.asax.cs        # Application entry point
├── OdysseyMvc4.csproj                  # MSBuild project file (non-SDK, ToolsVersion 12.0)
├── packages.config                     # NuGet package references
├── Web.config                          # Main configuration (connection strings, ELMAH, EF)
├── Web.Debug.config                    # Debug transform
└── Web.Release.config                  # Release transform
```

## Architecture

### Pattern: MVC + Repository + ViewData

The application follows a classic ASP.NET MVC pattern with three key layers:

1. **Controllers** → Handle HTTP requests, orchestrate registration flow
2. **Repository** (`OdysseyRepository`) → Encapsulates all database access via EF6
3. **ViewData classes** → Strongly-typed view models passed to Razor views

There is no dependency injection. The repository is instantiated directly (`new OdysseyRepository()`) in controllers. The DbContext (`OdysseyEntities`) is instantiated directly in the repository.

### Controller Hierarchy

```
Controller (System.Web.Mvc)
  └── BaseRegistrationController
        ├── HomeController
        ├── TournamentRegistrationController
        ├── JudgesRegistrationController
        ├── VolunteerRegistrationController
        └── CoachesTrainingRegistrationController
```

**BaseRegistrationController** provides:
- `OdysseyRepository Repository` — shared database access
- `SetBaseViewData(BaseViewData)` — populates Config, RegionName, RegionNumber, TournamentInfo, SiteName, CSS path
- Registration state management: `CurrentRegistrationState` (Available/Closed/Down/Soon)
- Registration type: `CurrentRegistrationType` (Tournament/Judges/CoachesTraining/Volunteer)
- `IsRegistrationClosed()`, `IsRegistrationComingSoon()`, `IsRegistrationDown()` — checks against Config table date/time values
- `BuildMessage()` — constructs email `MailMessage` objects
- `SendMessage()` — sends emails via SMTP (credentials from Config table)
- Shared action methods: `Closed()`, `Down()`, `Soon()`, `Error()`, `BadEmail()` — with GET/POST variants
- `DetermineSiteName()` — returns host name (novanorth.org or novasouth.org)
- `DetermineSiteCssFile()` — returns NovaNorth.css or NovaSouth.css based on URL

### Registration Flow Pattern

Each registration type follows a **multi-page wizard** pattern:

1. **Page01 (GET)** — Check registration state (Soon/Down/Closed/Available), display info page
2. **Page01 (POST)** → Redirect to **Page02**
3. **Page02 (GET)** — Display data entry form
4. **Page02 (POST)** — Validate, save to database, send confirmation email → Redirect to final page
5. **Final Page (GET)** — Display confirmation/summary

Tournament Registration has **10 pages** (the most complex flow), collecting:
- Team membership info (name, number)
- Problem selection and division
- School selection
- Coach and alternate coach contact info
- Up to 7 team member names/grades
- Spontaneous preference, scheduling issues, special considerations
- Volunteer registration
- Payment and confirmation

Each controller constructor sets `CurrentRegistrationType` and `FriendlyRegistrationName`.

### Multi-Region Support

The application serves **two regions from the same codebase**:
- **NoVA North** (novanorth.org) — Region 9
- **NoVA South** (novasouth.org) — Region 11 (historically)

Region detection is URL-based:
- `DetermineSiteName()` checks `Request.Url.Host`
- `DetermineSiteCssFile()` checks if URL contains "novasouth" → uses `NovaSouth.css`, otherwise `NovaNorth.css`
- Configuration values (region name, number, etc.) come from the database `Config` table, which is region-specific per database

### Data Access Layer

#### OdysseyEntities (DbContext)

Generated from `OdysseyModel.edmx` via T4 templates. Connects using the `OdysseyEntities` connection string (Entity Framework metadata format). DbSets:

| DbSet                          | Entity Type                  | Database Table             |
|--------------------------------|------------------------------|----------------------------|
| `CoachesTrainingDivisions`     | `CoachesTrainingDivision`    | Lookup: divisions          |
| `CoachesTrainingRegions`       | `CoachesTrainingRegion`      | Lookup: regions            |
| `CoachesTrainingRegistrations` | `CoachesTrainingRegistration`| Registration records       |
| `CoachesTrainingRoles`         | `CoachesTrainingRole`        | Lookup: roles              |
| `Configs`                      | `Config`                     | Key-value configuration    |
| `ContactUsRecipients`          | `ContactUsRecipient`         | Contact form recipients    |
| `ContactUsSenderRoles`         | `ContactUsSenderRole`        | Contact form sender roles  |
| `Events`                       | `Event`                      | Event info (tournament, judges training, etc.) |
| `Judges`                       | `Judge`                      | Judge registrations        |
| `Problems`                     | `Problem`                    | OotM problem definitions   |
| `Schools`                      | `School`                     | School directory           |
| `TournamentRegistrations`      | `TournamentRegistration`     | Tournament team registrations |
| `Volunteers`                   | `Volunteer`                  | Volunteer registrations    |

#### OdysseyRepository

The repository (`~800+ lines`) provides:

**Read Properties (lazy-loaded with null-coalescing pattern):**
- `Config` → `Dictionary<string, string>` from Config table (adds computed `EndYear` key)
- `RegionName`, `RegionNumber` → from Config dictionary
- `TournamentInfo`, `JudgesInfo`, `CoachesTrainingInfo`, `VolunteerInfo` → Event records (queried by EventName)
- `Problems`, `ProblemChoices`, `ProblemChoicesWithoutSpontaneous`, `ProblemConflicts`, `ProblemsWithoutPrimaryOrSpontaneous`, `ProblemsWithoutSpontaneous`, `PrimaryProblem` → Problem queries with various filters
- `Judges` → All judge records
- `Schools` → Schools with `Membership_1seen == "yes"`, projected to `{ID, Name}`
- `Divisions`, `Regions`, `Roles` → Coaches training lookups
- `CoachesTrainingRegistrations`, `TournamentRegistrations` → Registration records
- `Volunteers`, `TournamentRegistration` → As IQueryable

**Write Methods:**
- `AddCoachesTrainingRegistration(CoachesTrainingRegistration)` → Insert + SaveChanges
- `AddJudge(Judge)` → Insert + SaveChanges
- `AddTournamentRegistration(TournamentRegistration)` → Insert + SaveChanges
- `AddVolunteer(Volunteer, int?)` → Insert with optional team association + SaveChanges
- `ClearTeamIdFromJudgeRecord(int, string, string)` → Nulls out judge's TeamID

**Export Methods:**
- `ExportJudges()` → Joins Judges with Problems, projects to `JudgesExport` DTO

**Important implementation details:**
- `TournamentInfo` query filters by `EventName.StartsWith(RegionName)` AND `Contains("Tournament")` — the EventName **must** match the RegionName or it throws
- `PrimaryProblem` is hardcoded as `ProblemID == 6`
- `ProblemChoices` appends " (The Primary Problem)" to the primary problem's name
- `Config` setter computes `EndYear` from `Year` value
- Problem ID 0 = "Not Specified", ID 6 = Primary, ID 7 = Spontaneous

### Entity Models (Database Schema)

All entity classes are **auto-generated** from `OdysseyModel.tt` (T4 template against the EDMX). They are `partial` classes.

**Config** — Application configuration (key-value store)
- `Name` (string, PK) — Config key (e.g., "RegionName", "TournamentRegistrationCloseDateTime")
- `Value` (string) — Config value

**Key Config entries used in code:**
- `RegionName`, `RegionNumber`, `Year`, `HomePage`
- `{RegistrationType}RegistrationCloseDateTime` — closing time for each registration
- `{RegistrationType}RegistrationOpenDateTime` — opening time for each registration
- `Is{RegistrationType}RegistrationDown` — admin kill switch (bool as string)
- `EmailServer`, `WebmasterEmail`, `WebmasterEmailPassword` — SMTP settings
- `RegionalDirectorEmail` — contact email
- `AcceptingPayPal`, `WillHaveVolunteerRegistration` — feature flags
- `TestGuid` — bypass key for registration state checks (commented out)

**Event** — Event details (tournament, judges training, coaches training, volunteer)
- `ID` (int), `EventName` (string) — identifies the event type and region
- Location fields: `Location`, `LocationURL`, `LocationAddress`, `LocationCity`, `LocationState`, `LocationPhone`, `LocationMapURL`
- Date/time: `StartDate`, `EndDate`, `Time`
- Coordinator: `EventCoordinatorName`, `EventCoordinatorEmail`, `EventCoordinatorPhone`
- Fees: `EventCost`, `LateEventCost`, `LateEventCostStartDate`, `PaymentDueDate`
- Payee: `EventPayeeName`, `EventPayeeAddress1`/`2`, `City`, `State`, `ZipCode`, `Phone1`, `Email1`
- `EventMakeChecksOutTo`, `EventMailBody` — for confirmation emails
- `EventVolunteerInformationMessage`, `TeamsVolunteerWantsToSeeMessage`

**TournamentRegistration** — Team registration records
- `TeamID` (int, PK, identity)
- Membership: `MembershipName`, `MembershipNumber`
- `ProblemID` (int?), `Division` (string), `SchoolID` (int?)
- Coach: `CoachFirstName`, `CoachLastName`, `CoachAddress`, `CoachCity`, `CoachState`, `CoachZipCode`, `CoachEveningPhone`, `CoachDaytimePhone`, `CoachMobilePhone`, `CoachEmailAddress`
- Alt Coach: `AltCoachFirstName`, `AltCoachLastName`, `AltCoachEveningPhone`, `AltCoachDaytimePhone`, `AltCoachMobilePhone`, `AltCoachEmailAddress`
- Members 1-7: `MemberFirstName{N}`, `MemberLastName{N}`, `MemberGrade{N}`
- `Spontaneous` (bool?), `Notes`, `SpecialConsiderations`, `SchedulingIssues`
- `Paid` (short?), `JudgeID` (short?), `VolunteerID` (int?)
- `TeamRegistrationFee`, `TimeRegistrationStarted`, `TimeRegistered`, `UserAgent`

**Judge** — Judge registration records
- `JudgeID` (int, PK, identity)
- `TeamID` (string) — assigned team
- Personal info: `FirstName`, `LastName`, `Address`, `AddressLine2`, `City`, `State`, `ZipCode`
- Contact: `DaytimePhone`, `EveningPhone`, `MobilePhone`, `EmailAddress`
- Preferences: `ProblemChoice1`/`2`/`3`
- Conflicts: `HasChildrenCompeting`, `COI`, `ProblemCOI1`/`2`/`3`
- Assignment: `ProblemAssigned`, `ProblemID`
- Status: `InformationMailed_` (bool?), `AttendedJT_` (bool?), `Active` (bool?)
- Other: `WillingToBeScorechecker`, `TshirtSize`, `WantsCEUCredit`
- Experience: `YearsOfLongTermJudgingExperience`, `YearsOfSpontaneousJudgingExperience`, `PreviousPositions`
- Timestamps: `TimeRegistered`, `TimeAssignedToTeam`, `TimeRegistrationStarted`, `UserAgent`

**Volunteer** — Volunteer registration records
- `VolunteerID` (int, PK, identity), `TeamID` (int?) — associated team
- `FirstName`, `LastName`, `DaytimePhone`, `EveningPhone`, `MobilePhone`, `EmailAddress`
- `Notes`, `VolunteerWantsToSee`
- `TimeRegistrationStarted`, `TimeRegistered`, `TimeAssignedToTeam`, `UserAgent`

**Problem** — Odyssey of the Mind problem/challenge definitions
- `ProblemID` (int, PK), `ProblemCategory`, `ProblemName`, `ProblemDescription`
- `Divisions`, `CostLimit`
- Problem Captain: `ProblemCaptainID`, `PCFirstName`, `PCLastName`, `PCAddress`, `PCCity`, `PCStateOrProvince`, `PCPostalCode`, `PCWorkPhone`, `PCHomePhone`, `PCMobilePhone`, `PCFaxNumber`, `PCEmail1`, `PCEmail2`
- `Notes`

**School** — School directory
- `ID` (int, PK), `Name`, `Address`, `City`, `State`, `PostalCode`, `Phone`
- Memberships 1-4: `Membership_{N}`, `Membership_{N}seen` — membership tracking
- Coordinator: `CoordNew_`, `CoordFirstName`, `CoordLastName`, `CoordAddress`, `CoordCity`, `CoordState`, `CoordPostalCode`, `CoordPhone`, `CoordAltPhone`, `CoordMobilePhone`, `CoordFaxNumber`, `CoordEmailName`
- `Share_`, `Notes`

**CoachesTrainingRegistration** — Coaches training registration
- `RegistrationID` (int, PK, identity)
- `FirstName`, `LastName`, `SchoolName`, `Role`, `Division`, `SelectedProblem`
- `EmailAddress`, `YearsInvolved`, `RegionNumber`
- `TimeRegistered`, `UserAgent`

**Lookup Tables:**
- `CoachesTrainingDivision` — `ID` (byte), `Name`
- `CoachesTrainingRegion` — `ID` (int), `Name`
- `CoachesTrainingRole` — `ID` (byte), `Name`
- `ContactUsRecipient` — `ID` (byte), `contact_name`, `email_address`
- `ContactUsSenderRole` — `ID` (byte), `role_name`

### ViewData (View Models)

All view models inherit from `BaseViewData`, which provides:
- `Config` — `Dictionary<string, string>` (all config key-value pairs)
- `RegionName`, `RegionNumber` — region identification
- `FriendlyRegistrationName` — display name (e.g., "Tournament Registration")
- `TournamentInfo` — `Event` object with tournament details
- `TournamentDate`, `TournamentLocation`, `TournamentTime` — computed properties (returns "TBA" if not set)
- `SiteName` — host domain name
- `PathToSiteCssFile` — region-specific CSS path

**Tournament ViewData (Page01-Page10, ResendEmail):**
- Page01: `TeamRegistrationFee` (handles late fees), `LateTeamRegistrationFee`, `LateEventCostStartDate`, `PaymentDueDate`, `TournamentRegistrationCloseDateTime`, `AcceptingPayPal`

**Judges ViewData (Page01-Page03):**
- Page01: `JudgesInfo` (Event), `JudgesTrainingDate`, `JudgesTrainingLocation`, `JudgesTrainingTime`, `MailRegionalDirectorHyperLink`/`Text`

### Email System

Confirmation emails are sent after successful registration:
- SMTP server and credentials come from `Config` table (`EmailServer`, `WebmasterEmail`, `WebmasterEmailPassword`)
- `BuildMessage()` constructs `MailMessage` with HTML body, supports multiple recipients, CC, BCC
- `SendMessage()` sends via `SmtpClient` with retry logic for `MailboxBusy`/`MailboxUnavailable`
- Email body templates are stored in `Event.EventMailBody` with placeholder tokens (`<span>JudgeID</span>`, `<span>FirstName</span>`, `<span>LastName</span>`, `<span>Region</span>`) replaced via Regex
- Errors are logged to ELMAH

### Error Handling

- **ELMAH** is configured for error logging, accessible at `/elmah` route
- ELMAH logs to a separate SQL Server database (`DB_12824_elmah` on WinHost)
- `ErrorSignal.FromCurrentContext().Raise(exception)` is used to manually signal errors to ELMAH
- Global `HandleErrorAttribute` filter catches unhandled exceptions
- Controllers use try/catch around email sending and date parsing
- Registration state checks (`IsRegistrationClosed`/`ComingSoon`/`Down`) have catch-all exception handlers that default to safe states

### Routing

**MVC Routes** (RouteConfig.cs):
- Default: `{controller}/{action}/{id}` → defaults to `Home/Index`
- Registration URLs follow pattern: `/TournamentRegistration/Page01`, `/JudgesRegistration/Page02`, etc.

**Web API Routes** (WebApiConfig.cs):
- `api/{controller}/{id}` — standard Web API route (no API controllers are visible in this project)

### Client-Side Architecture

The `_Layout.cshtml` master layout loads all JavaScript from CDNs:
- jQuery 3.7.1, jQuery Validate 1.21, jQuery Validation Unobtrusive 4.0, jQuery UI 1.14.1, jQuery Inputmask 5.0.9
- jQuery UI uses the "Redmond" theme from CDN
- Region-specific CSS is loaded dynamically based on `@Model.PathToSiteCssFile`
- Bundle config defines bundles but they are commented out in the layout in favor of CDN references
- Knockout.js is included but its usage is limited
- `PrintThisPage.js` provides print functionality for confirmation pages

### Configuration & Connection Strings

**Web.config** defines multiple connection strings (Entity Framework metadata format):
- `OdysseyEntities` — Local development (localhost, sa)
- `NovaNorthOdysseyEntities` — Production NoVA North (s06.winhost.com, DB_12824_registration)
- `NovaSouthOdysseyEntities` — Production NoVA South (s06.winhost.com, DB_27415_registration)
- `TestOdysseyEntities` — Test database (s06.winhost.com, DB_12824_test)
- `DefaultConnection` — ASP.NET membership (LocalDb)

The `OdysseyEntities` DbContext uses `name=OdysseyEntities` — the active connection string must be named `OdysseyEntities`. To switch regions/environments, comment/uncomment the appropriate connection string.

**User Secrets** are enabled via `Microsoft.Configuration.ConfigurationBuilders.UserSecrets` with `userSecretsId="345b550c-60fc-4d0d-9ea2-646b6bcd6b76"`.

### Deployment

Publish profiles exist for:
- **novanorth.org** — FTP and Web Deploy
- **novasouth.org** — FTP and Web Deploy
- **Test-Server** and **test2** — test environments

The application is deployed to **WinHost** shared hosting via FTP or Web Deploy.

### Key Business Rules

1. **Division determination**: Team division is determined by the highest grade among team members
2. **Registration state machine**: Each registration type has independent open/close dates and admin kill switches
3. **Late fees**: Tournament registration has a standard fee and a late fee that activates after `LateEventCostStartDate`
4. **Problem ID 6 is Primary**: Hardcoded as "The Primary Problem" — appended to display name
5. **Problem ID 7 is Spontaneous**: Excluded from various problem lists
6. **Problem ID 0 is "Not Specified"**: Excluded from most queries
7. **Schools filter**: Only schools with `Membership_1seen == "yes"` appear in dropdowns
8. **EventName must match RegionName**: `TournamentInfo` query filters by `EventName.StartsWith(RegionName)` — mismatch causes runtime exception
9. **UserAgent tracking**: All registrations store the browser's User-Agent string
10. **Time tracking**: Registrations track both `TimeRegistrationStarted` and `TimeRegistered`

### Known Issues & Technical Debt

- `OdysseyRepository` is a large class (~800+ lines) with no interface — makes testing difficult
- No dependency injection — controllers directly instantiate repositories
- `BaseRegistrationController.SetBaseViewData()` is called repeatedly, creating new `BaseViewData` instances
- Problem queries have a known bug where " (The Primary Problem)" can appear twice in dropdowns
- The `ExportJudges()` LINQ query has a known issue with `ToString()` in LINQ-to-Entities
- Several TODO comments remain in code (date validation, timezone handling, testing)
- No timezone handling — assumes Eastern Time (`DateTime.Now` with commented-out adjustment)
- `Thread.Sleep(5000)` is used for email retry — blocks the request thread
- `SmtpClient` is not disposed properly (should use `using` statement)
- Connection string passwords are empty in Web.config — populated via User Secrets or server config
- ELMAH `allowRemoteAccess="true"` with no authentication — potential security concern in production
- `problemChoicesWithoutSpontaneous` field is declared `readonly` but never initialized via field initializer
- `MvcApplication.cs` (alias for `App_Start\MvcApplication.cs`) may conflict with `Global.asax.cs`
