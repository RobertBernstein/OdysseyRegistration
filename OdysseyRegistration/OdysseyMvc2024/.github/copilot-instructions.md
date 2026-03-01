# GitHub Copilot Instructions for Odyssey Registration System

## Project Overview
This is an ASP.NET Core MVC application targeting **.NET 10.0** for managing Odyssey of the Mind tournament and judges registrations for Northern Virginia (NoVA North / NoVA South). The system handles multi-page registration workflows for tournaments and judges, with email verification and database-driven configuration.

This project is a **migration target** — controllers and models were initially decompiled from the production OdysseyMvc4 .NET Framework 4.8 DLL using JetBrains decompiler, then manually cleaned up and modernized for ASP.NET Core.

> **Comprehensive architecture reference**: See `../../.copilot/odysseymvc2024-architecture.md` for the full 40KB architecture document covering all entity models, controller details, ViewData classes, database schema, and known issues.

## Architecture
- **Framework**: ASP.NET Core MVC (.NET 10.0 / `net10.0`)
- **Database**: Entity Framework Core 10.0.2 with SQL Server provider (Code First with Migrations)
- **Pattern**: Repository Pattern (`IOdysseyRepository` interface) with constructor-injected DI
- **Structure**: Traditional MVC with separate ViewData classes for strongly-typed views
- **Error Logging**: ElmahCore 2.1.2
- **Migration Compatibility**: `Microsoft.AspNetCore.SystemWebAdapters` 1.7.0 for bridging legacy patterns
- **User Secrets ID**: `4fc61b4d-cbaa-46ed-b907-349d6beff28e`

## Migration Status
| Controller | Status | Notes |
|---|---|---|
| BaseRegistrationController | ✅ Complete | Abstract base, constructor injection, `required` properties |
| HomeController | ✅ Complete | Simple landing page |
| JudgesRegistrationController | ✅ Complete | Full 3-page wizard with `GeneratedRegex` |
| TournamentRegistrationController | ✅ Complete (code present) | Full 10-page wizard in code |
| VolunteerRegistrationController | ❌ Not started | References commented out in repository |
| CoachesTrainingRegistrationController | ❌ Not started | References commented out in repository |

## Coding Standards

### Naming Conventions
- **Controllers**: End with "Controller" (e.g., `TournamentRegistrationController`, `JudgesRegistrationController`)
- **Models**: PascalCase, singular nouns (e.g., `Judge`, `Event`, `School`, `TournamentRegistration`)
- **ViewData classes**: End with "ViewData" in dedicated folders (e.g., `Page01ViewData`, `Page02ViewData`)
- **Database context**: `OdysseyEntities` implementing `IOdysseyEntities`
- **Repository**: `OdysseyRepository` implementing `IOdysseyRepository`

### Controller Guidelines
- **Inheritance**: All registration controllers inherit from `BaseRegistrationController` (which is `abstract`)
- **Dependency Injection**: `IOdysseyRepository` is constructor-injected (registered as Scoped in Program.cs)
- **Repository Access**: Use `Repository` (protected field in base controller) for all database access — **never use OdysseyEntities DbContext directly**
- **Registration Types**: Set `CurrentRegistrationType` enum (Tournament, Judges, CoachesTraining, Volunteer)
- **`required` Property**: `FriendlyRegistrationName` uses C# 11's `required` modifier — must be set in constructor
- **Page Flow**: Follow sequential page-based pattern (Page01, Page02, ..., Page10); team `id` is passed via route parameter between pages (not session/TempData)
- **State Management**: Check `CurrentRegistrationState` (Available, Closed, Down, Soon) before allowing registration
- **Email Handling**: Use `BuildMessage()` and `SendMessage()` methods from base controller
- **Error Handling**: Use `ElmahExtensions.RaiseError(exception)` for exception logging (not `ErrorSignal.FromCurrentContext().Raise()` — that's the old OdysseyMvc4 pattern)

### Registration State Logic
The base controller implements a state machine that controls registration availability:
- **States**: `RegistrationState.Soon`, `Down`, `Closed`, `Available`
- **Config keys**: `{Type}RegistrationIsAvailable` and `{Type}RegistrationIsDown` (True/False strings)
- **Priority**: Down > Closed > Soon > Available
- All date/time comparisons use `TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")`
- Every page action checks state and redirects to the corresponding shared view (Soon.cshtml, Down.cshtml, Closed.cshtml)

### View Guidelines
- **Strongly-typed views**: Always use ViewData classes that inherit from `BaseViewData`
- **CSS files**: Two region-specific stylesheets — `NovaNorth.css` and `NovaSouth.css` in `wwwroot/css/`
- **Region detection**: Determined by `Config["RegionName"]` from Config table (not URL-based like OdysseyMvc4)
- **CSS path**: Use `~/css/NovaNorth.css` (not `~/Content/` — that was the OdysseyMvc4 pattern)
- **JavaScript**: jQuery 3.7.1, jQuery Validate 1.21.0, jQuery UI 1.14.1, InputMask 5.0.9 (all via CDN)
- **Shared views**: Error and status pages in `Views/Shared/` (Error.cshtml, BadEmail.cshtml, Closed.cshtml, Down.cshtml, Soon.cshtml)
- **Layout**: `_Layout.cshtml` in Shared folder, uses `@Model.PathToSiteCssFile` for region CSS and `@Model.Config["HomePage"]` for home link

### Database Access Pattern
- **Never use `OdysseyEntities` directly** in controllers — always use `IOdysseyRepository`
- **DI Registration**: `OdysseyEntities` registered via `AddDbContext<>`, `IOdysseyRepository` registered as `Scoped` in Program.cs
- **Note**: `IOdysseyEntities` registration is **commented out** in Program.cs — the DbContext is injected directly into `OdysseyRepository`
- **Routing**: `MapDefaultControllerRoute()` → `{controller=Home}/{action=Index}/{id?}`
- **Async Note**: Repository methods are currently synchronous (using `SaveChanges()` not `SaveChangesAsync()`) — this is a known technical debt
- **Entity models**: Located in `Models/` folder, decompiled from OdysseyMvc4 with nullable reference types added
- **Migrations**: Single `InitialCreate` migration (2024-10-05) in `Migrations/` folder
- **SQL Server Resilience**: `EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: 10s)` and `CommandTimeout(60)`

### Repository Interface Methods
Key methods available in `IOdysseyRepository`:
- `AddJudge()`, `UpdateJudge()`, `GetJudgeById()`, `GetJudgeByIdAndName()`
- `AddTournamentRegistration()`, `UpdateTournamentRegistration()`, `GetTournamentRegistrationById()`
- `GetProblemNameFromProblemId()`, `GetSchoolNameFromSchoolId()`
- `GetMemberGradesByRegistration()`, `ClearTeamIdFromJudgeRecord()`
- Collections: `Judges`, `Problems`, `Schools`, `TournamentRegistrations`, `Config`

## Project-Specific Patterns

### Critical Constants and Business Rules
- **Problem ID 0** = "Not Specified"
- **Problem ID 6** = Primary (hardcoded as `ThePrimaryProblemNumber`)
- **Problem ID 7** = Spontaneous (hardcoded as `TheSpontaneousProblemNumber`)
- **Config table** uses `Name` (string) as primary key — it's a key-value store, not auto-increment
- **TournamentRegistration.Id** is mapped to the `TeamID` column via `[Column("TeamID")]`
- **Judge column mapping**: `InformationMailed_` → `"InformationMailed?"` and `AttendedJT_` → `"AttendedJT?"` (legacy DB has `?` in column names)
- **Schools filter**: Only schools with `Membership_1seen == "yes"` appear in dropdowns
- **Division calculation**: K-2 → Primary (0), 3-5 → Div 1, 6-8 → Div 2, 9-12 → Div 3; team division = highest member division
- **EventName must match RegionName**: `TournamentInfo` query does `EventName.StartsWith(RegionName) && EventName.Contains("Tournament")` — throws if no match
- **Volunteer functionality is disabled**: Page04 passes through without doing anything (code commented out)

### Multi-Page Registration Flow
1. Each registration type (Tournament/Judges) has multiple pages (Page01, Page02, etc.)
2. Each page has corresponding ViewData class in `ViewData/{RegistrationType}/` folder
3. ViewData classes inherit from `BaseViewData` for common properties
4. Page number passed to repository Update methods to handle page-specific field mapping
5. Sequential progression through pages with data validation at each step; `id` (TeamID/JudgeID) passed via route parameter
6. Email verification/sending on final confirmation page
7. Each page action checks `CurrentRegistrationState` before proceeding

### Tournament Registration Pages (10 Pages)
| Page | Purpose | Data |
|---|---|---|
| Page01 | Welcome + fees | Creates record (TimeRegistrationStarted, TeamRegistrationFee, UserAgent) |
| Page02 | School selection | SchoolID |
| Page03 | Judge assignment | JudgeID (lookup by ID + name) |
| Page04 | Volunteer (disabled) | Passes through — volunteer logic commented out |
| Page05 | Coach info | Coach + alt coach (name, address, phones, email) |
| Page06 | Team members | Up to 7 members (first name, last name, grade K-12) |
| Page07 | Problem/division | ProblemID, Division (auto-calc), Spontaneous flag |
| Page08 | Special considerations | SchedulingIssues, SpecialConsiderations |
| Page09 | Review summary | Read-only display of all data |
| Page10 | Confirmation | Assigns judge→team, sets TimeRegistered, sends email |

### Email System
- **Email configuration**: Stored in Config dictionary (`SmtpHost`, `WebmasterEmail`, `WebmasterEmailPassword`)
- **SMTP handling**: Uses `System.Net.Mail.SmtpClient` (not yet migrated to MailKit — known tech debt)
- **Email body generation**: `GenerateEmailBody()` renders `TournamentRegistration/EmailPartial` view via `ICompositeViewEngine`
- **⚠️ Sync-over-async**: `partialView.View.RenderAsync(viewContext).Wait()` — violates no sync-over-async rule, needs fix
- **Email validation**: `BuildMessage()` returns `null` on invalid email — controller redirects to `BadCoachEmail`/`BadAltCoachEmail`
- **Error views**: Separate views for different email errors
  - `BadEmail.cshtml`: Generic bad email (shared)
  - `BadCoachEmail.cshtml`: Invalid coach email in tournament registration
  - `BadAltCoachEmail.cshtml`: Invalid alternate coach email
- **Email methods**: `BuildMessage()` constructs MailMessage, `SendMessage()` handles sending with error handling

### Configuration System
- **Database-driven**: All configuration in `Config` dictionary loaded from Config table (string key-value pairs)
- **Loaded in constructor**: `OdysseyRepository` constructor loads all Config rows into a `Dictionary<string, string>` and adds computed `EndYear` key
- **Region settings**: `RegionName` and `RegionNumber` from Config dictionary
- **Event information**: `TournamentInfo` and `JudgesInfo` — lazily loaded from Events table
- **Critical Config keys**: `RegionName`, `RegionNumber`, `Year`, `HomePage`, `WebmasterEmail`, `WebmasterEmailPassword`, `SmtpHost`, `AcceptingPayPal`, `WillHaveVolunteerRegistration`, `TournamentRegistrationIsAvailable`, `JudgesRegistrationIsAvailable`, `TournamentRegistrationIsDown`, `JudgesRegistrationIsDown`, `TournamentRegistrationCloseDateTime`
- **Feature flags**: `{Type}RegistrationIsDown` and `{Type}RegistrationIsAvailable` (True/False strings)

### ViewData Pattern
Every ViewData class should:
1. Inherit from `BaseViewData`
2. Be populated using `SetBaseViewData()` method
3. Include Config, RegionName, RegionNumber, TournamentInfo from repository
4. Add page-specific properties as needed
5. Be strongly typed when passed to views

## Dependencies & Versions
- **.NET**: 10.0
- **Entity Framework Core**: 10.0.2
- **Entity Framework Core SQL Server**: 10.0.2
- **ElmahCore**: 2.1.2 (error logging)
- **Microsoft.AspNetCore.SystemWebAdapters.CoreServices**: 1.7.0
- **Azure.Identity**: 1.14.2
- **jQuery**: 3.7.1
- **jQuery Validate**: 1.21.0
- **jQuery UI**: 1.14.1
- **InputMask**: 5.0.9

## When Generating Code

### DO:
- Inherit from `BaseRegistrationController` for new registration controllers
- Use `Repository` property to access database via `IOdysseyRepository`
- Call `SetBaseViewData()` to populate common view data
- Set `CurrentRegistrationType` and `FriendlyRegistrationName` in controller constructors or actions
- Check `CurrentRegistrationState` before allowing registration to proceed
- Use ElmahCore's `ElmahExtensions.RaiseError()` for exception logging
- Follow the page-based workflow pattern (Page01, Page02, etc.)
- Create ViewData classes in appropriate subfolders under `ViewData/`
- Use strongly-typed views with ViewData classes
- Handle email errors with appropriate error views
- Use Eastern Standard Time for all date/time comparisons

### DON'T:
- Use `OdysseyEntities` DbContext directly in controllers
- Create database queries outside the repository pattern
- Forget to set `CurrentRegistrationType` enum value
- Hard-code configuration values (use Config dictionary)
- Assume registration is available (always check state)
- Use time zones other than Eastern Standard Time without justification
- Create views without corresponding ViewData classes
- Bypass email validation in registration flows

## File Organization
```
Controllers/          - BaseRegistrationController + specific registration controllers
Models/              - Entity models + IOdysseyRepository + OdysseyRepository
ViewData/            - BaseViewData + subfolder per registration type
  TournamentRegistration/  - Page01ViewData through Page10ViewData
  JudgesRegistration/      - Page01ViewData through Page03ViewData
Views/
  Shared/            - Common error and status views
  TournamentRegistration/  - Page01 through Page10 + error views
  JudgesRegistration/      - Page01 through Page03
Migrations/          - EF Core migrations
wwwroot/
  css/               - NovaNorth.css, NovaSouth.css (region-specific stylesheets)
```

## Common Tasks

### Adding a New Registration Page
1. Create ViewData class in `ViewData/{RegistrationType}/Page{N}ViewData.cs`
2. Inherit from `BaseViewData`
3. Create corresponding view in `Views/{RegistrationType}/Page{N}.cshtml`
4. Add controller action methods (GET and POST) to handle the page
5. Update repository `Update{RegistrationType}` method to handle new page number
6. Pass the record `id` via route parameter between pages (not TempData/Session)

### Adding a New Field to Registration
1. Update entity model in `Models/`
2. Create EF Core migration
3. Update repository methods to handle new field
4. Update appropriate ViewData class(es)
5. Update view(s) to display/collect the field
6. Update controller validation logic

### Handling Registration State Changes
1. Configuration values control open/close times and down flags
2. Override checks for testing by checking query string "Test" parameter against "TestGuid" config
3. Return appropriate views: Soon.cshtml, Down.cshtml, Closed.cshtml, or proceed with registration
4. All date/time checks use Eastern Standard Time zone

## Testing Considerations
- Registration state checks can be bypassed with Test GUID in query string (commented out but available)
- Date/time parsing should use TryParse to handle invalid dates
- Email sending includes retry logic for transient failures
- All exceptions should be logged with ElmahCore
- Multi-page workflows pass `id` via route parameter — test page transitions with valid IDs

## Docker & Development Environment

### Docker Compose Integration
OdysseyMvc2024 depends on a SQL Server database running in Docker. The solution is configured to start Docker containers automatically when debugging.

### Key Files for Docker Startup
- **`OdysseyRegistration/docker-compose.dcproj`**: Docker Compose project that orchestrates containers
- **`OdysseyRegistration/docker-compose.yml`**: Defines SQL Server and WebAPI services
- **`OdysseyRegistration/docker-compose.override.yml`**: Development environment overrides
- **`OdysseyRegistration/launchSettings.json`**: Solution-level launch profiles for Docker + projects
- **`OdysseyRegistration/OdysseyRegistration.slnlaunch`**: Multiple startup project configurations

### Launch Profiles Available
1. **Docker Compose**: Starts only the WebAPI service with debugging
2. **Docker Compose + OdysseyMvc2024**: Starts WebAPI + SQL Server + OdysseyMvc2024 with debugging
3. **SQL Server + OdysseyMvc2024**: Starts only SQL Server, waits for health check, then starts OdysseyMvc2024

### Starting Development Environment
1. Open `OdysseyRegistration.slnx` in Visual Studio 2022+
2. Select **"SQL Server + OdysseyMvc2024"** from the Debug dropdown or startup configuration
3. Press F5 to start debugging - Docker containers will start first, then OdysseyMvc2024

### SQL Server Container Details
- **Image**: `mcr.microsoft.com/mssql/server:2022-latest`
- **Port**: 1433 (exposed to localhost)
- **Health Check**: 90-second start period with 15-second intervals
- **Data Persistence**: Named volumes `sqlserver_odyssey` and `sqlserver_backup`
- **Initialization**: Scripts in `/init` folder run on first startup

### Connection String Configuration

The application uses different connection string sources depending on the environment:

#### Development (Local Debugging)
**ASP.NET Core User Secrets** provides the connection string with the password:
- User Secrets are stored outside the project at `%APPDATA%\Microsoft\UserSecrets\{UserSecretsId}\secrets.json`
- Never committed to source control
- Automatically loaded in Development environment

**Managing User Secrets:**
```bash
# Navigate to the project directory
cd OdysseyRegistration\OdysseyMvc2024

# View all secrets
dotnet user-secrets list

# Set the connection string (with password)
dotnet user-secrets set "ConnectionStrings:OdysseyConnection" "Server=localhost;Database=DB_12824_registration;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;"

# Remove all secrets
dotnet user-secrets clear
```

#### Docker Environment
The SQL Server container uses **Docker Secrets** for the SA password:
- Password stored in `OdysseyRegistration/sa_password.txt`
- Mounted as `/run/secrets/sa_password` in the container
- Used by SQL Server via `MSSQL_SA_PASSWORD_FILE` environment variable

**Note**: When setting up a new development environment, ensure the User Secrets connection string password matches the password in `sa_password.txt`.

### Documentation
Full Docker setup documentation: `OdysseyMvc2024/Documentation/Docker Compose.md`
