# GitHub Copilot Instructions for Odyssey Registration System

## Project Overview
This is an ASP.NET Core 10.0 MVC application for managing Odyssey of the Mind tournament and judges registrations. The system handles multi-page registration workflows for tournaments and judges, with email verification and database-driven configuration.

## Architecture
- **Framework**: ASP.NET Core 10.0 MVC
- **Database**: Entity Framework Core 10.0 with SQL Server
- **Pattern**: Repository Pattern (`IOdysseyRepository` interface)
- **Structure**: Traditional MVC with separate ViewData classes for strongly-typed views
- **Error Logging**: ElmahCore 2.1.2

## Coding Standards

### Naming Conventions
- **Controllers**: End with "Controller" (e.g., `TournamentRegistrationController`, `JudgesRegistrationController`)
- **Models**: PascalCase, singular nouns (e.g., `Judge`, `Event`, `School`, `TournamentRegistration`)
- **ViewData classes**: End with "ViewData" in dedicated folders (e.g., `Page01ViewData`, `Page02ViewData`)
- **Database context**: `OdysseyEntities` implementing `IOdysseyEntities`
- **Repository**: `OdysseyRepository` implementing `IOdysseyRepository`

### Controller Guidelines
- **Inheritance**: All registration controllers inherit from `BaseRegistrationController`
- **Dependency**: Use `OdysseyRepository` (protected field in base controller) for all database access
- **Registration Types**: Set `CurrentRegistrationType` enum (Tournament, Judges, CoachesTraining, Volunteer)
- **Page Flow**: Follow sequential page-based pattern (Page01, Page02, ..., Page10)
- **State Management**: Check `CurrentRegistrationState` (Available, Closed, Down, Soon) before allowing registration
- **Session Data**: Store intermediate registration data in session/TempData between pages
- **Email Handling**: Use `BuildMessage()` and `SendMessage()` methods from base controller
- **Error Handling**: Use ElmahCore for logging exceptions

### Registration State Logic
The base controller provides methods to determine registration availability:
- `IsRegistrationClosed()`: Checks against configured close date/time
- `IsRegistrationComingSoon()`: Checks against configured open date/time
- `IsRegistrationDown()`: Checks administrative down flag
- All date/time comparisons use Eastern Standard Time zone

### View Guidelines
- **Strongly-typed views**: Always use ViewData classes that inherit from `BaseViewData`
- **CSS files**: Three available - `Site.css` (base), `NovaNorth.css`, `NovaSouth.css`
- **Site detection**: System automatically determines site (NovaNorth vs NovaSouth) from URL
- **JavaScript**: jQuery 3.7.1 and jQuery UI 1.13.2 available globally
- **Shared views**: Error and status pages in `Views/Shared/` (Error.cshtml, BadEmail.cshtml, Closed.cshtml, Down.cshtml, Soon.cshtml)
- **Layout**: Use `_Layout.cshtml` in Shared folder

### Database Access Pattern
- **Never use `OdysseyEntities` directly** in controllers
- **Always use `IOdysseyRepository` interface** through the `Repository` property
- **Async/await**: Use async methods for all database operations when possible
- **Entity models**: Located in `Models/` folder
- **Migrations**: Entity Framework Core migrations in `Migrations/` folder

### Repository Interface Methods
Key methods available in `IOdysseyRepository`:
- `AddJudge()`, `UpdateJudge()`, `GetJudgeById()`, `GetJudgeByIdAndName()`
- `AddTournamentRegistration()`, `UpdateTournamentRegistration()`, `GetTournamentRegistrationById()`
- `GetProblemNameFromProblemId()`, `GetSchoolNameFromSchoolId()`
- `GetMemberGradesByRegistration()`, `ClearTeamIdFromJudgeRecord()`
- Collections: `Judges`, `Problems`, `Schools`, `TournamentRegistrations`, `Config`

## Project-Specific Patterns

### Multi-Page Registration Flow
1. Each registration type (Tournament/Judges) has multiple pages (Page01, Page02, etc.)
2. Each page has corresponding ViewData class in `ViewData/{RegistrationType}/` folder
3. ViewData classes inherit from `BaseViewData` for common properties
4. Page number passed to repository Update methods to handle page-specific validation
5. Sequential progression through pages with data validation at each step
6. Email verification required before completion

### Email System
- **Email configuration**: Stored in Config dictionary (EmailServer, WebmasterEmail, WebmasterEmailPassword)
- **SMTP handling**: Built-in retry logic for mailbox busy/unavailable
- **Error views**: Separate views for different email errors
  - `BadEmail.cshtml`: Generic bad email
  - `BadCoachEmail.cshtml`: Invalid coach email in tournament registration
  - `BadAltCoachEmail.cshtml`: Invalid alternate coach email
- **Email methods**: `BuildMessage()` constructs MailMessage, `SendMessage()` handles sending with error handling

### Configuration System
- **Database-driven**: All configuration in Config dictionary from database
- **Region settings**: `RegionName` and `RegionNumber` from repository
- **Event information**: `TournamentInfo` and `JudgesInfo` properties
- **Date/time settings**: Registration open/close times per registration type
- **Feature flags**: "Is{RegistrationType}RegistrationDown" administrative toggles

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
- **Microsoft.AspNetCore.SystemWebAdapters.CoreServices**: 2.2.1
- **jQuery**: 3.7.1
- **jQuery UI**: 1.13.2

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
  css/               - Site.css, NovaNorth.css, NovaSouth.css
  Scripts/           - jQuery and jQuery UI
```

## Common Tasks

### Adding a New Registration Page
1. Create ViewData class in `ViewData/{RegistrationType}/Page{N}ViewData.cs`
2. Inherit from `BaseViewData`
3. Create corresponding view in `Views/{RegistrationType}/Page{N}.cshtml`
4. Add controller action methods (GET and POST) to handle the page
5. Update repository `Update{RegistrationType}` method to handle new page number
6. Store intermediate data in TempData/Session for next page

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
- Multi-page workflows require session state testing

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
