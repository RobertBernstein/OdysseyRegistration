# OdysseyMvc2024 Architecture Reference

> Comprehensive documentation for the OdysseyMvc2024 project — the .NET 10.0 migration target for the Odyssey of the Mind registration system.

## 1. Project Overview

**OdysseyMvc2024** is an ASP.NET Core MVC application targeting **.NET 10.0**, currently under active migration from the production OdysseyMvc4 application (.NET Framework 4.8). It provides online registration for Odyssey of the Mind regional tournaments in Northern Virginia (NoVA North / NoVA South).

### Key Characteristics
- **Framework**: ASP.NET Core MVC (.NET 10.0 / `net10.0`)
- **ORM**: Entity Framework Core 10.0.2 with SQL Server provider (Code First with Migrations)
- **Dependency Injection**: Full ASP.NET Core DI — `IOdysseyRepository` registered as Scoped
- **Error Logging**: ElmahCore 2.1.2 (replaces classic ELMAH from OdysseyMvc4)
- **Migration Compatibility**: Uses `Microsoft.AspNetCore.SystemWebAdapters` for bridging legacy patterns
- **Origin**: Controllers and models were initially decompiled from the OdysseyMvc4 production DLL using JetBrains decompiler, then manually cleaned up and modernized
- **User Secrets ID**: `4fc61b4d-cbaa-46ed-b907-349d6beff28e`

### Migration Status
| Controller | Status | Notes |
|---|---|---|
| BaseRegistrationController | ✅ Complete | Abstract base, constructor injection, `required` properties |
| HomeController | ✅ Complete | Simple landing page controller |
| JudgesRegistrationController | ✅ Complete | Full 3-page wizard with `GeneratedRegex` |
| TournamentRegistrationController | ✅ Complete (code present) | Full 10-page wizard, but per instructions.md "stopped at line 144" — code exists beyond that and appears fully migrated |
| VolunteerRegistrationController | ❌ Not started | Commented-out references in repository |
| CoachesTrainingRegistrationController | ❌ Not started | Commented-out references in repository |

---

## 2. Project Structure

```
OdysseyMvc2024/
├── OdysseyMvc2024.csproj          # SDK-style project file
├── Program.cs                      # Minimal hosting entry point
├── appsettings.json                # Base configuration (logging only)
├── appsettings.Development.json    # Development overrides
├── Web.config                      # IIS/SystemWebAdapters config
├── launchSettings.json             # Launch profiles (IIS Express, Kestrel)
│
├── Controllers/
│   ├── BaseRegistrationController.cs   # Abstract base (617 lines)
│   ├── HomeController.cs               # Landing page
│   ├── JudgesRegistrationController.cs # 3-page judges wizard (671 lines)
│   └── TournamentRegistrationController.cs # 10-page tournament wizard (934 lines)
│
├── Models/
│   ├── IOdysseyEntities.cs         # DbContext interface (8 DbSets)
│   ├── IOdysseyRepository.cs       # Repository interface (all data operations)
│   ├── OdysseyEntities.cs          # EF Core DbContext (Code First)
│   ├── OdysseyRepository.cs        # Repository implementation (~1170 lines)
│   ├── Config.cs                   # Key-value configuration entity
│   ├── Event.cs                    # Tournament/training event entity
│   ├── Judge.cs                    # Judge registration entity
│   ├── TournamentRegistration.cs   # Team registration entity
│   ├── Problem.cs                  # Odyssey problem/challenge entity
│   ├── School.cs                   # School/organization entity
│   ├── ContactUsRecipient.cs       # Contact form recipient entity
│   ├── ContactUsSenderRole.cs      # Contact form sender role entity
│   └── JudgesExport.cs            # DTO for judges data export
│
├── ViewData/
│   ├── BaseViewData.cs             # Base view model for all pages
│   ├── JudgesRegistration/
│   │   ├── Page01ViewData.cs       # Judges welcome/info
│   │   ├── Page02ViewData.cs       # Judges personal info + preferences
│   │   └── Page03ViewData.cs       # Judges confirmation
│   └── TournamentRegistration/
│       ├── Page01ViewData.cs       # Tournament welcome/fees
│       ├── Page02ViewData.cs       # School selection
│       ├── Page03ViewData.cs       # Judge assignment
│       ├── Page04ViewData.cs       # Volunteer assignment (deprecated)
│       ├── Page05ViewData.cs       # Coach info
│       ├── Page06ViewData.cs       # Team members
│       ├── Page07ViewData.cs       # Problem/division selection
│       ├── Page08ViewData.cs       # Special considerations
│       ├── Page09ViewData.cs       # Review/summary
│       ├── Page10ViewData.cs       # Confirmation + email
│       └── ResendEmailViewData.cs  # Resend confirmation email
│
├── Views/
│   ├── _ViewImports.cshtml         # @using + Tag Helpers
│   ├── _ViewStart.cshtml           # Default layout assignment
│   ├── Home/
│   │   └── Index.cshtml            # Registration landing page
│   ├── JudgesRegistration/
│   │   ├── Page01.cshtml
│   │   ├── Page02.cshtml
│   │   └── Page03.cshtml
│   ├── TournamentRegistration/
│   │   ├── Page01.cshtml through Page10.cshtml
│   │   ├── BadAltCoachEmail.cshtml
│   │   ├── BadCoachEmail.cshtml
│   │   ├── Error.cshtml
│   │   └── ResendEmail.cshtml
│   └── Shared/
│       ├── _Layout.cshtml          # Master layout
│       ├── BadEmail.cshtml
│       ├── Closed.cshtml           # Registration closed state
│       ├── Down.cshtml             # Registration down state
│       ├── Error.cshtml            # General error
│       └── Soon.cshtml             # Registration coming soon state
│
├── wwwroot/
│   ├── css/
│   │   ├── NovaNorth.css           # NoVA North region stylesheet
│   │   └── NovaSouth.css           # NoVA South region stylesheet
│   ├── images/
│   ├── Scripts/
│   └── themes/
│
└── Migrations/
    ├── 20241005151813_InitialCreate.cs
    ├── 20241005151813_InitialCreate.Designer.cs
    └── OdysseyEntitiesModelSnapshot.cs
```

---

## 3. Application Bootstrap (Program.cs)

The application uses ASP.NET Core minimal hosting:

```csharp
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSystemWebAdapters();
builder.Services.AddControllersWithViews();

// EF Core DbContext registration
builder.Services.AddDbContext<OdysseyEntities>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OdysseyConnection")));

// Repository pattern DI
// builder.Services.AddScoped<IOdysseyEntities, OdysseyEntities>();  // COMMENTED OUT
builder.Services.AddScoped<IOdysseyRepository, OdysseyRepository>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();  // serves from wwwroot
app.UseRouting();
app.UseAuthorization();
app.UseSystemWebAdapters();
app.MapDefaultControllerRoute();  // {controller=Home}/{action=Index}/{id?}
app.Run();
```

### Key Notes
- `IOdysseyEntities` registration is **commented out** — the DbContext is injected directly into `OdysseyRepository`
- `MapDefaultControllerRoute()` uses the default `{controller=Home}/{action=Index}/{id?}` pattern
- `UseSystemWebAdapters()` provides migration compatibility shims
- No explicit error handling middleware (HSTS only in non-development)
- Connection string comes from User Secrets in development

---

## 4. NuGet Dependencies

From `OdysseyMvc2024.csproj`:

| Package | Version | Purpose |
|---|---|---|
| Azure.Identity | 1.14.2 | Future Azure deployment (AAD auth) |
| ElmahCore | 2.1.2 | Error logging (replaces classic ELMAH) |
| Microsoft.AspNetCore.SystemWebAdapters | 1.7.0 | Migration compatibility bridge |
| Microsoft.EntityFrameworkCore | 10.0.2 | ORM framework |
| Microsoft.EntityFrameworkCore.Design | 10.0.2 | EF Core tooling (migrations) |
| Microsoft.EntityFrameworkCore.SqlServer | 10.0.2 | SQL Server provider |
| Microsoft.EntityFrameworkCore.Tools | 10.0.2 | Package Manager Console commands |

---

## 5. Controller Architecture

### 5.1 BaseRegistrationController (Abstract)

All registration controllers inherit from this abstract class. Key responsibilities:

```
BaseRegistrationController (abstract) : Controller
├── HomeController
├── JudgesRegistrationController
└── TournamentRegistrationController
```

#### Constructor Injection
```csharp
public abstract class BaseRegistrationController : Controller
{
    protected readonly IOdysseyRepository Repository;

    protected BaseRegistrationController(IOdysseyRepository repository)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
}
```

This is a **major improvement over OdysseyMvc4**, which used `new OdysseyRepository()` directly.

#### Registration State Machine
The base controller implements a state machine that controls registration availability:

```csharp
public enum RegistrationState { Soon, Down, Closed, Available }
public enum RegistrationType { None, Tournament, Judges, Volunteer, CoachesTraining }
```

`CurrentRegistrationState` is determined by reading Config dictionary keys:
- `{Type}RegistrationIsAvailable` → "True"/"False"
- `{Type}RegistrationIsDown` → "True"/"False"

Priority: Down > Closed > Soon > Available

Every page action checks `CurrentRegistrationState` and redirects to the appropriate state view (Soon/Down/Closed) if not Available.

#### Registration Type Detection
The `CurrentRegistrationType` property is set by each derived controller:
- `RegistrationType.Tournament` → checks keys with "Tournament" prefix
- `RegistrationType.Judges` → checks keys with "Judges" prefix
- etc.

#### Key Base Methods
- `SetBaseViewData(BaseViewData)` — Populates common view data (Config, RegionName, RegionNumber, PathToSiteCssFile, SiteName, FriendlyRegistrationName, TournamentInfo)
- `BuildMessage(from, subject, body, to, bcc, cc)` — Constructs `MailMessage` with HTML body; returns `null` on invalid email
- `SendMessage(BaseViewData, MailMessage)` — Sends email via SMTP using Config["WebmasterEmail"] and Config["WebmasterEmailPassword"]
- `GetFriendlyRegistrationName()` — Maps RegistrationType enum to display strings
- `GetPathToSiteCssFile()` — Returns `~/css/NovaNorth.css` or `~/css/NovaSouth.css` based on `RegionName`
- `BadEmail()` — Shared action for invalid email addresses

#### CSS Path (Multi-Region)
```csharp
// Unlike OdysseyMvc4 which used ~/Content/, this uses ~/css/
return "~/css/NovaNorth.css";  // or NovaSouth.css based on RegionName
```

#### T-Shirt Size Choices
```csharp
protected static readonly string[] TshirtSizeChoices = ["S", "M", "L", "XL", "XXL", "XXXL"];
```

#### Email SMTP Configuration
- SMTP credentials from Config table: `Config["WebmasterEmail"]` and `Config["WebmasterEmailPassword"]`
- SMTP Host: `Config["SmtpHost"]`
- Uses `System.Net.Mail.SmtpClient` (not yet migrated to MailKit)
- Error handling uses ElmahCore: `ElmahExtensions.RaiseError(exception)`

#### `required` Property
```csharp
public required string FriendlyRegistrationName { get; set; }
```
Uses C# 11's `required` modifier — must be set before use.

### 5.2 HomeController

Minimal controller that serves the registration landing page:
```csharp
public class HomeController : BaseRegistrationController
{
    public HomeController(IOdysseyRepository repository) : base(repository) { }

    [HttpGet]
    public ActionResult Index()
    {
        // Sets up BaseViewData with Config and TournamentInfo
        // ViewData["Message"] = welcome text with RegionName and RegionNumber
    }
}
```

The Home/Index view displays links to:
- Judges Registration
- Tournament Registration
- Volunteer Registration (conditional on Config["WillHaveVolunteerRegistration"])
- Coaches Training Registration (commented out)

### 5.3 JudgesRegistrationController (3 Pages)

Complete migration with modern C# features including `GeneratedRegexAttribute`.

| Page | GET | POST | Purpose |
|---|---|---|---|
| Page01 | Display welcome + training info | Create new Judge record (Page01Post) | Welcome, training schedule |
| Page02 | Show form with dropdowns | Validate + update judge record | Personal info, problem preferences, experience |
| Page03 | Show confirmation + send email | — | Confirmation, email delivery |

**Key Modernizations:**
- Uses `[GeneratedRegex]` for compile-time regex (SYSLIB1045 fix):
  ```csharp
  [GeneratedRegex(@"^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")]
  private static partial Regex PhoneRegex();
  ```
- Constructor injection of `IOdysseyRepository`
- `ElmahExtensions.RaiseError(exception)` instead of `ErrorSignal.FromCurrentContext().Raise()`
- Eastern time zone: `TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")`

### 5.4 TournamentRegistrationController (10 Pages)

Full 10-page registration wizard, the most complex controller (934 lines):

| Page | Purpose | Data Collected |
|---|---|---|
| Page01 | Welcome + fee information | Creates new TournamentRegistration record (TimeRegistrationStarted, TeamRegistrationFee, UserAgent) |
| Page02 | School selection | SchoolID (from dropdown of verified schools) |
| Page03 | Judge assignment | JudgeID (lookup by ID + first/last name) |
| Page04 | Volunteer assignment | **Deprecated** — volunteer logic commented out, just passes through |
| Page05 | Coach information | Coach + alternate coach: name, address, phones, email |
| Page06 | Team members | Up to 7 members: first name, last name, grade (K-12) |
| Page07 | Problem & division selection | ProblemID, Division (auto-calculated from grades), Spontaneous flag |
| Page08 | Special considerations | SchedulingIssues, SpecialConsiderations |
| Page09 | Review/summary | Read-only display of all entered data |
| Page10 | Confirmation | Assigns judge to team, sets TimeRegistered, sends confirmation email |

**Additional Actions:**
- `ResendEmail` — Allows resending confirmation to coach/alt-coach
- `BadCoachEmail` / `BadAltCoachEmail` — Error views for invalid emails
- Named judge-clearing actions (hardcoded): `Carolina()`, `Joyce()`, `Margaret()`, `Rob()`, `Ron()`, `Sarah()` — each clears a specific judge's TeamID

**Division Calculation Logic:**
```
Grade K, 1, 2     → Division 0 (Primary)
Grade 3, 4, 5     → Division 1
Grade 6, 7, 8     → Division 2
Grade 9, 10, 11, 12 → Division 3
Team Division = highest member division
```

**Email Generation:**
- Uses `ICompositeViewEngine` to render `TournamentRegistration/EmailPartial` view as HTML string
- Sends via `BuildMessage()` + `SendMessage()` from base controller

---

## 6. Data Access Layer

### 6.1 OdysseyEntities (DbContext)

EF Core DbContext with Code First configuration:

```csharp
public class OdysseyEntities : DbContext
{
    public OdysseyEntities(DbContextOptions<OdysseyEntities> options) : base(options) { }

    public DbSet<Config> Configs { get; set; }
    public DbSet<ContactUsRecipient> ContactUsRecipients { get; set; }
    public DbSet<ContactUsSenderRole> ContactUsSenderRoles { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Judge> Judges { get; set; }
    public DbSet<Problem> Problems { get; set; }
    public DbSet<School> Schools { get; set; }
    public DbSet<TournamentRegistration> TournamentRegistrations { get; set; }
}
```

**Commented-out DbSets** (not yet migrated):
- `CoachesTrainingDivision`, `CoachesTrainingRegion`, `CoachesTrainingRegistration`, `CoachesTrainingRole`
- `Volunteer`

**Table Mappings** (OnModelCreating):
| Entity | Table Name | Key |
|---|---|---|
| Config | Config | Name (string PK) |
| ContactUsRecipient | ContactUsRecipient | ID (byte) |
| ContactUsSenderRole | ContactUsSenderRole | ID (byte) |
| Event | Events | ID (int) |
| Judge | Judges | JudgeID (int, auto-increment) |
| Problem | Problem | ProblemID (int) |
| School | School | ID (int) |
| TournamentRegistration | TournamentRegistration | TeamID → mapped to `Id` property |

**Legacy Column Name Mappings:**
```csharp
entity.Property(j => j.InformationMailed_).HasColumnName("InformationMailed?");
entity.Property(j => j.AttendedJT_).HasColumnName("AttendedJT?");
```
The legacy database has `?` characters in column names — these are mapped to underscore-suffixed properties.

**Fallback Connection String:**
```csharp
if (!optionsBuilder.IsConfigured)
{
    // Falls back to: Server=localhost;Database=DB_12824_registration;Trusted_Connection=True;...
    optionsBuilder.UseSqlServer(connectionString, sql =>
    {
        sql.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), ...);
        sql.CommandTimeout(60);
        sql.MigrationsAssembly(typeof(OdysseyEntities).Assembly.FullName);
    });
}
```

**Debug Configuration:**
```csharp
#if DEBUG
    optionsBuilder.EnableDetailedErrors();
    optionsBuilder.EnableSensitiveDataLogging();
#endif
```

### 6.2 IOdysseyRepository Interface

Defines all data access operations. All controllers use this interface (never DbContext directly):

**Properties:**
| Property | Type | Description |
|---|---|---|
| Config | `Dictionary<string, string>?` | Key-value configuration from Config table |
| Judges | `IEnumerable<Judge>?` | All registered judges |
| JudgesInfo | `Event?` | Event record for judges training |
| PrimaryProblem | `IQueryable<Problem>?` | Problem with ID 6 |
| ProblemChoices | `IEnumerable<Problem>` | All problems for dropdowns |
| ProblemChoicesWithoutSpontaneous | `IEnumerable<Problem>` | Problems minus Spontaneous |
| ProblemConflicts | `IEnumerable<Problem>` | Problems for COI dropdowns |
| Problems | `IEnumerable<Problem>?` | All problems with categories |
| ProblemsWithoutPrimaryOrSpontaneous | `IQueryable<Problem>?` | Problems minus IDs 0, 6, 7 |
| ProblemsWithoutSpontaneous | `IQueryable<Problem>?` | Problems minus IDs 0, 7 |
| RegionName | `string` | From Config["RegionName"] |
| RegionNumber | `string` | From Config["RegionNumber"] |
| Schools | `IEnumerable?` | Schools with Membership_1seen = "yes" |
| TournamentInfo | `Event` | Event record for tournament |
| TournamentRegistration | `IQueryable` | All registrations |
| TournamentRegistrations | `IEnumerable<TournamentRegistration>` | All registrations ordered by Id |

**Methods:**
| Method | Return | Description |
|---|---|---|
| AddJudge(Judge) | int | Insert new judge, returns SaveChanges count |
| AddTournamentRegistration(TournamentRegistration) | int | Insert new registration |
| ClearTeamIdFromJudgeRecord(id, first, last) | void | Clears judge's TeamID assignment |
| GetJudgeById(id) | IQueryable\<Judge\> | Find judge by ID |
| GetJudgeByIdAndName(id, first, last) | IQueryable\<Judge\> | Find judge by ID + name (case-insensitive) |
| GetJudgeIdFromTournamentRegistrationId(id) | short? | Get judge assigned to team |
| GetJudgeNameFromJudgeId(id, out first, out last) | void | Lookup judge name from ID |
| GetMemberGradesByRegistration(id) | List\<string\> | Get all 7 member grades for division calc |
| GetProblemNameFromProblemId(id) | string? | Lookup problem name |
| GetSchoolNameFromSchoolId(id) | string? | Lookup school name |
| GetTournamentRegistrationById(id) | TournamentRegistration? | Find registration by TeamID |
| UpdateJudge(id, pageNumber, data) | int | Update judge fields by page |
| UpdateJudgeEmail(id, email) | int | Update judge email only |
| UpdateJudgeRecordWithTournamentRegistrationId(judgeId, regId, out error) | int | Assign judge to team (with conflict detection) |
| UpdateTournamentRegistration(id, pageNumber, data) | int | Update registration fields by page |

### 6.3 OdysseyRepository Implementation

Key patterns in the repository:

**Lazy-Loading with Null-Coalescing:**
```csharp
// Same pattern as OdysseyMvc4 but with nullable reference types
public IEnumerable<Judge>? Judges
{
    get
    {
        _judges ??= [.. (from c in _context.Judges select c)];
        return _judges;
    }
}
```

**Constructor Config Initialization:**
```csharp
public OdysseyRepository(OdysseyEntities context)
{
    _context = context ?? throw new ArgumentNullException(nameof(context));
    Config ??= (from c in _context.Configs select c).ToDictionary(d => d.Name, d => d.Value);
    Config.Add("EndYear", (int.Parse(Config["Year"]) + 1).ToString(CultureInfo.InvariantCulture));
}
```

**Problem ID Constants:**
```csharp
private const int TheNotSpecifiedProblemNumber = 0;
private const int ThePrimaryProblemNumber = 6;
private const int TheSpontaneousProblemNumber = 7;
```

**TournamentInfo Query (Critical — Known Issue):**
```csharp
Event tournamentInfo = _tournamentInfo ??
    (TournamentInfo = Events.AsQueryable()
        .Where(e => e.EventName.StartsWith(RegionName) && e.EventName.Contains("Tournament"))
        .First());
```
⚠️ This throws if no matching event exists. EventName must start with RegionName (e.g., "NoVA North Regional Tournament").

**Page-Based Update Pattern:**
```csharp
public int UpdateTournamentRegistration(int id, int pageNumber, TournamentRegistration? newRegistrationData)
{
    switch (pageNumber)
    {
        case 2:  // SchoolID
        case 3:  // JudgeID
        case 4:  // VolunteerID
        case 5:  // Coach info (16 fields)
        case 6:  // Team members (21 fields) — uses ThePrimaryProblemNumber (6) as case value
        case 7:  // Problem/Division/Spontaneous — uses TheSpontaneousProblemNumber (7) as case value
        case 8:  // Scheduling/Special
        case 10: // TimeRegistered
    }
    return _context.SaveChanges();
}
```

**Schools Query:**
```csharp
Schools = _context.Schools
    .Where(s => s.Membership_1seen == "yes")  // Only verified schools
    .OrderBy(s => s.Name ?? string.Empty)
    .Select(s => new { s.ID, s.Name });
```

---

## 7. Entity Models

### 7.1 Config
Key-value store driving all registration behavior. **Primary key is `Name` (string)**, not auto-increment.
```csharp
public partial class Config
{
    public string Name { get; set; }
    public string Value { get; set; }
}
```

Critical Config keys include:
- `RegionName`, `RegionNumber`, `Year`, `HomePage`
- `TournamentRegistrationIsAvailable`, `JudgesRegistrationIsAvailable`
- `TournamentRegistrationIsDown`, `JudgesRegistrationIsDown`
- `TournamentRegistrationCloseDateTime`
- `WillHaveVolunteerRegistration`
- `AcceptingPayPal`
- `WebmasterEmail`, `WebmasterEmailPassword`, `SmtpHost`

### 7.2 Event
Stores tournament, judges training, and other event details:
- **Key fields**: ID, EventName, StartDate, EndDate, Location, Time
- **Coordinator**: EventCoordinatorName, EventCoordinatorEmail, EventCoordinatorPhone
- **Venue**: LocationURL, LocationAddress, LocationCity, LocationState, LocationPhone, LocationMapURL
- **Payment**: EventCost, LateEventCost, LateEventCostStartDate, PaymentDueDate, EventMakeChecksOutTo
- **Payee**: EventPayeeName, EventPayeeAddress1/2, EventPayeeCity/State/ZipCode/Phone1/Email1

### 7.3 Judge (59 properties)
Complete judge registration record:
- **Identity**: JudgeID (PK, auto-increment), FirstName, LastName
- **Contact**: Address, AddressLine2, City, State, ZipCode, DaytimePhone, EveningPhone, MobilePhone, EmailAddress
- **Preferences**: ProblemChoice1/2/3, HasChildrenCompeting, COI, ProblemCOI1/2/3
- **Experience**: YearsOfLongTermJudgingExperience, YearsOfSpontaneousJudgingExperience, PreviousPositions
- **Assignment**: TeamID, ProblemAssigned, ProblemID
- **Status**: Active, InformationMailed_, AttendedJT_ (mapped from `InformationMailed?` and `AttendedJT?`)
- **Other**: WillingToBeScorechecker, TshirtSize, WantsCEUCredit, Notes
- **Audit**: TimeRegistered, TimeAssignedToTeam, TimeRegistrationStarted, UserAgent

### 7.4 TournamentRegistration (34 properties)
Complete team registration record:
- **Identity**: Id (PK, mapped to `TeamID` column), MembershipName, MembershipNumber
- **Team**: ProblemID, Division, SchoolID, Spontaneous
- **Coach**: CoachFirstName/LastName/Address/City/State/ZipCode/EveningPhone/DaytimePhone/MobilePhone/EmailAddress
- **Alternate Coach**: AltCoachFirstName/LastName/EveningPhone/DaytimePhone/MobilePhone/EmailAddress
- **Members** (up to 7): MemberFirstName1-7, MemberLastName1-7, MemberGrade1-7
- **Other**: Notes, SpecialConsiderations, SchedulingIssues
- **Financials**: Paid, TeamRegistrationFee
- **Linked**: JudgeID, VolunteerID
- **Audit**: TimeRegistrationStarted, TimeRegistered, UserAgent

### 7.5 Problem (18 properties)
Odyssey of the Mind challenges/problems:
- **Identity**: ProblemID (PK), ProblemCategory, ProblemName, ProblemDescription
- **Details**: Divisions, CostLimit
- **Problem Captain**: ProblemCaptainID, PCFirstName/LastName/Address/City/StateOrProvince/PostalCode/WorkPhone/HomePhone/MobilePhone/FaxNumber/PCEmail1/PCEmail2
- **Notes**: Notes

**Special Problem IDs:**
- `0` = Not Specified
- `6` = Primary (hardcoded constant)
- `7` = Spontaneous (hardcoded constant)

### 7.6 School (25 properties)
Schools/organizations eligible for tournament:
- **Identity**: ID (PK), Name, Address, City, State, PostalCode, Phone
- **Memberships**: Membership_1 through Membership_4, each with corresponding `_seen` field
- **Coordinator**: CoordNew_, CoordFirstName/LastName/Address/City/State/PostalCode/Phone/AltPhone/MobilePhone/FaxNumber/CoordEmailName
- **Other**: Share_, Notes

Schools are filtered by `Membership_1seen == "yes"` for dropdown lists.

### 7.7 ContactUsRecipient
```csharp
public byte ID { get; set; }
public string contact_name { get; set; }
public string email_address { get; set; }
```

### 7.8 ContactUsSenderRole
```csharp
public byte ID { get; set; }
public string role_name { get; set; }
```

### 7.9 JudgesExport (DTO)
Read-only projection for exporting judge data. Not a database entity — used for reporting queries (currently commented out in repository due to EF Core migration issues).

---

## 8. ViewData Architecture

ViewData classes serve as strongly-typed view models. All inherit from `BaseViewData`.

### 8.1 BaseViewData
Base class for all page view models:
```csharp
public class BaseViewData
{
    public Dictionary<string, string>? Config { get; set; }
    public string? FriendlyRegistrationName { get; set; }
    public string? PathToSiteCssFile { get; set; }
    public string? RegionName { get; set; }
    public string? RegionNumber { get; set; }
    public string? SiteName { get; set; }
    public Event? TournamentInfo { get; set; }

    // Computed properties with null-safe TBA fallbacks
    public string TournamentDate => TournamentInfo?.StartDate?.ToLongDateString() ?? "TBA";
    public string? TournamentLocation => !string.IsNullOrWhiteSpace(TournamentInfo?.Location) ? ... : "TBA";
    public string? TournamentTime => !string.IsNullOrWhiteSpace(TournamentInfo?.Time) ? ... : "TBA";
}
```

### 8.2 JudgesRegistration ViewData

**Page01ViewData** — Welcome page:
- `JudgesInfo` (required Event?) — training event details
- Computed: `JudgesTrainingDate`, `JudgesTrainingLocation`, `JudgesTrainingTime` (all with "TBA" fallback)
- `MailRegionalDirectorHyperLink`, `MailRegionalDirectorHyperLinkText` (both `required`)

**Page02ViewData** — Personal info form (heavily validated):
- Personal: FirstName, LastName, Address, AddressLine2, City, State (2 chars), ZipCode (5 digits)
- Contact: EveningPhone, DaytimePhone, MobilePhone (regex-validated), EmailAddress, EmailConfirmation
- Preferences: ProblemChoice1/2/3, ProblemConflict1/2/3
- Experience: YearsOfLongTermJudgingExperience, YearsOfSpontaneousJudgingExperience (0-100)
- Checkboxes: HasChildrenCompeting, WantsCeuCredit, WillingToBeScorechecker
- Previous positions: PreviouslyHeadJudge, PreviouslyProblemJudge, PreviouslyScorechecker, PreviouslyStagingJudge, PreviouslyStyleJudge, PreviouslyTimekeeper, PreviouslyWeighInJudge
- Dropdowns: TshirtSizes, ProblemChoices (both `[BindNever]` to prevent over-posting)
- `ProblemConflictList1/2/3` — derived from ProblemChoices with "No Preference" replaced by "I Don't Know"
- Notes (max 500 chars)

**Page03ViewData** — Confirmation:
- `JudgesInfo` (required Event?), `Judge`, `MailBody`, `MailErrorMessage`, `EmailAddressWasSpecified`, `ErrorMessage`

### 8.3 TournamentRegistration ViewData

**Page01ViewData** — Welcome + fees:
- Computed: `AcceptingPayPal` (from Config), `TeamRegistrationFee` (handles late fee logic), `LateTeamRegistrationFee`, `LateEventCostStartDate`, `PaymentDueDate`, `TournamentRegistrationCloseDateTime`
- Late fee logic: compares `LateEventCostStartDate` to `DateTime.Now`

**Page02ViewData** — School selection:
- `SchoolList` (required `IEnumerable<SelectListItem>`)
- `SelectedSchool` (int, required)

**Page03ViewData** — Judge assignment:
- `JudgeId`, `JudgeFirstName`, `JudgeLastName` (validated, required)
- `ListOfJudgesFound` (required `IQueryable<Judge>`)
- `NoJudgesFound`, `JudgeAlreadyTaken` (status flags)

**Page04ViewData** — Volunteer (deprecated):
- `VolunteerId`, `VolunteerFirstName`, `VolunteerLastName`
- `NoVolunteersFound`, `VolunteerAlreadyTaken`
- Volunteer reference (`VolunteerFound`) is commented out

**Page05ViewData** — Coach info (extensively validated):
- Coach: FirstName, LastName, Address, City, State, ZipCode, EveningPhone, DaytimePhone, MobilePhone, EmailAddress, EmailConfirmation
- Alt Coach: same fields with "AltCoach" prefix
- All phone fields use regex validation: `^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$`
- Email confirmation fields use `[Compare]` attribute

**Page06ViewData** — Team members:
- `GradeChoices` (required `IEnumerable<SelectListItem>`) — Kindergarten + grades 1-12
- Member1 through Member7: FirstName (25 chars max), LastName (25 chars max), Grade
- Member1 is required; Members 2-7 are optional

**Page07ViewData** — Problem/division:
- `DivisionOfTeam` (int, calculated from grades)
- `Division123ProblemDropDown` (required), `IsDoingSpontaneousDropDown` (required)
- `Division123ProblemChoice`, `SelectedProblem`, `DivisionRadioGroup`
- `PrimaryProblemName`, `Division123ListOfProblemsAsHtmlList`, `Division123AndPrimaryListOfProblemsAsHtmlList`
- `IsDoingSpontaneous` (Yes/No string)

**Page08ViewData** — Special considerations:
- `SchedulingIssues`, `SpecialConsiderations`, `TournamentRegistration`

**Page09ViewData** — Review summary:
- All display fields: Division, IsDoingSpontaneous, JudgeFirstName/LastName, ProblemName, SchoolName, VolunteerFirstName/LastName
- `TournamentRegistration` (full record for display)

**Page10ViewData** — Confirmation:
- Same display fields as Page09 plus: `MailBody`, `MailErrorMessage`, `JudgeErrorMessage`, `VolunteerErrorMessage`
- `AcceptingPayPal`, `PaymentDueDate` (computed from Config/TournamentInfo)
- `TournamentRegistration` (required)

**ResendEmailViewData**:
- `TeamNumber` (int), `CoachCheckbox`, `AltCoachCheckbox` ("true"/"false" strings)
- `ErrorMessage`, `Success`

---

## 9. Views Architecture

### 9.1 Layout

**_Layout.cshtml** — Master layout used by all pages:
- jQuery 3.7.1 (CDN), jQuery Validate 1.21.0, jQuery UI 1.14.1, InputMask 5.0.9
- Region-specific CSS via `@Model.PathToSiteCssFile`
- Site header with region name from Config
- Home page link from `@Model.Config["HomePage"]`
- `@RenderBody()` for page content
- `@RenderSection("scripts", false)` for page-specific scripts

**_ViewImports.cshtml:**
```csharp
@using OdysseyMvc2024
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
```

**_ViewStart.cshtml:**
```csharp
@{ Layout = "~/Views/Shared/_Layout.cshtml"; }
```

### 9.2 Shared Views (Registration States)
- **Soon.cshtml** — "Registration is coming soon"
- **Down.cshtml** — "Registration is temporarily down"
- **Closed.cshtml** — "Registration is closed"
- **Error.cshtml** — General error page
- **BadEmail.cshtml** — Invalid email address

### 9.3 View File Inventory

| Controller | Views |
|---|---|
| Home | Index.cshtml |
| JudgesRegistration | Page01.cshtml, Page02.cshtml, Page03.cshtml |
| TournamentRegistration | Page01-Page10.cshtml, BadAltCoachEmail.cshtml, BadCoachEmail.cshtml, Error.cshtml, ResendEmail.cshtml |
| Shared | _Layout.cshtml, BadEmail.cshtml, Closed.cshtml, Down.cshtml, Error.cshtml, Soon.cshtml |

---

## 10. Static Assets (wwwroot)

```
wwwroot/
├── css/
│   ├── NovaNorth.css     # NoVA North region styles
│   └── NovaSouth.css     # NoVA South region styles
├── images/               # Image assets
├── Scripts/              # JavaScript files
└── themes/               # jQuery UI theme assets
```

**Key difference from OdysseyMvc4:** Static files are served from `wwwroot/` (ASP.NET Core convention) instead of `~/Content/` and `~/Scripts/`. CSS path references use `~/css/NovaNorth.css` instead of `~/Content/NovaNorth.css`.

---

## 11. Configuration

### Connection String
- **Development**: User Secrets (`dotnet user-secrets`)
  ```
  ConnectionStrings:OdysseyConnection = "Server=localhost;Database=DB_12824_registration;User Id=sa;Password=...;TrustServerCertificate=True;"
  ```
- **Fallback** (in OdysseyEntities.OnConfiguring): `Server=localhost;Database=DB_12824_registration;Trusted_Connection=True;TrustServerCertificate=True;`
- **Production**: WinHost environment variables or appsettings.Production.json (not in source control)

### appsettings.json
Minimal — only logging configuration:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Launch Profiles
- **IIS Express**: Port 37149 (HTTP) / 44397 (HTTPS)
- **OdysseyMvc2024** (Kestrel): Port 5153 (HTTP) / 7023 (HTTPS)

---

## 12. Database Migrations

The project uses EF Core Code First migrations:
- **InitialCreate** (2024-10-05): Creates all tables matching the legacy database schema
- Migration assembly configured in `OdysseyEntities.OnConfiguring`

SQL Server resilience is configured:
```csharp
sql.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null);
sql.CommandTimeout(60);
```

---

## 13. Key Differences from OdysseyMvc4

| Aspect | OdysseyMvc4 (.NET Framework 4.8) | OdysseyMvc2024 (.NET 10.0) |
|---|---|---|
| **DI** | `new OdysseyRepository()` directly | Constructor injection via `IOdysseyRepository` |
| **Base Controller** | Regular class | `abstract` class with `required` properties |
| **ORM** | EF6 Database First (EDMX + T4) | EF Core 10.0 Code First (Migrations) |
| **Error Logging** | ELMAH (`ErrorSignal.FromCurrentContext().Raise()`) | ElmahCore (`ElmahExtensions.RaiseError()`) |
| **Null Safety** | No nullable reference types | Nullable reference types, `required` modifier |
| **Static Files** | `~/Content/`, `~/Scripts/` | `wwwroot/css/`, `wwwroot/Scripts/` |
| **CSS Path** | `~/Content/NovaNorth.css` | `~/css/NovaNorth.css` |
| **Routing** | Explicit `RouteConfig.RegisterRoutes()` | `MapDefaultControllerRoute()` |
| **Regex** | Runtime regex | `[GeneratedRegex]` compile-time (SYSLIB1045) |
| **Bundling** | `BundleConfig.cs` (System.Web.Optimization) | CDN links in _Layout.cshtml |
| **Column Names** | Handled by EDMX mapping | `HasColumnName("InformationMailed?")` in Fluent API |
| **Region Detection** | URL-based (`HttpContext.Request.Url.Host`) | Config-based (`RegionName` from Config table) |
| **Time Zones** | `TimeZone.CurrentTimeZone` | `TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")` |
| **Hosting** | IIS + WinHost | Kestrel + `UseSystemWebAdapters()` |
| **Project File** | packages.config / .csproj (legacy) | SDK-style .csproj |

---

## 14. Known Issues and TODOs

### Critical Issues
1. **TournamentInfo query throws** if EventName doesn't match `RegionName + "Tournament"` pattern
2. **IOdysseyEntities registration is commented out** in Program.cs — unclear if this causes issues
3. **Volunteer functionality is disabled** — Page04 passes through without doing anything
4. **ExportJudges is commented out** — needs EF Core migration of complex LINQ join
5. **No error handling middleware** — only HSTS in non-development

### Code Quality TODOs (from source comments)
- Split OdysseyRepository into per-registration-type repositories
- Add try/catch to TournamentInfo getter
- Add logging when TournamentInfo query fails
- Cache TournamentInfo to avoid per-request DB queries
- Test Config dictionary initialization
- Investigate ProblemChoicesWithoutSpontaneous assignment bug (assigns to ProblemChoices)
- Page04 volunteer logic entirely commented out — remove or implement
- Page07 has decompiler artifact: `goto label_8`
- Named judge-clearing actions (Carolina, Joyce, etc.) are hardcoded — should be parameterized
- MailMessage uses obsolete SmtpClient — should migrate to MailKit
- `sync-over-async` in `GenerateEmailBody()`: `partialView.View.RenderAsync(viewContext).Wait()`

### Decompilation Artifacts
Many files still contain JetBrains decompiler headers:
```csharp
// Decompiled with JetBrains decompiler
// Type: OdysseyMvc4.Controllers.TournamentRegistrationController
// Assembly: OdysseyMvc4, Version=1.0.5460.36587
```
These are informational only and don't affect functionality.

---

## 15. Registration Flow Patterns

### State Check Pattern
Every page action follows this pattern:
```csharp
[HttpGet]
public ActionResult PageXX(int id)
{
    if (CurrentRegistrationState != RegistrationState.Available)
        return RedirectToAction(CurrentRegistrationState.ToString());

    var viewData = new PageXXViewData { Config = Repository.Config, TournamentInfo = Repository.TournamentInfo };
    SetBaseViewData(viewData);
    return View(viewData);
}
```

### POST Error Handling Pattern
```csharp
[HttpPost]
public ActionResult PageXX(int id, PageXXViewData viewData)
{
    if (CurrentRegistrationState != RegistrationState.Available)
        return RedirectToAction(CurrentRegistrationState.ToString());
    try
    {
        // Process form data, update database
        Repository.UpdateTournamentRegistration(id, pageNumber, newData);
        return RedirectToAction("NextPage", new { id });
    }
    catch (Exception exception)
    {
        ElmahExtensions.RaiseError(exception);
        return RedirectToAction("Index", "Home");
    }
}
```

### Email Validation Pattern
Before accepting email addresses, the controller tests them:
```csharp
if (BuildMessage(config["WebmasterEmail"], "test", "test", email, null, null) == null)
    return RedirectToAction("BadCoachEmail");
```

---

## 16. Security Considerations

- **No Authentication/Authorization** — The registration system is publicly accessible
- **Email credentials** stored in Config table (database)
- **User Secrets** for connection strings in development
- **`[BindNever]`** attribute on server-side dropdown properties to prevent over-posting
- **Data Annotations** for input validation (Required, StringLength, Range, RegularExpression, Compare)
- **No CSRF protection explicitly configured** — relies on ASP.NET Core defaults
- **SystemWebAdapters** may introduce legacy security patterns
- **Azure.Identity** package present for future Azure Key Vault integration

---

## 17. Multi-Region Support

The system supports multiple Odyssey of the Mind regions (NoVA North / NoVA South):
- **CSS**: `wwwroot/css/NovaNorth.css` and `wwwroot/css/NovaSouth.css`
- **Region Detection**: Based on `Config["RegionName"]` value
- **Event Queries**: Filter by `EventName.StartsWith(RegionName)`
- **Email Subjects**: Include region name and number

Unlike OdysseyMvc4 (which detected region from URL hostname), OdysseyMvc2024 relies on the Config table's `RegionName` value to determine which region is active.
