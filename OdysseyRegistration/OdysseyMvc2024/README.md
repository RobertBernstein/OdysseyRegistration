# Odyssey Registration System

An ASP.NET Core 9.0 MVC application for managing Odyssey of the Mind tournament and judges registrations for regional tournaments.

## Overview

This system provides multi-page registration workflows for:
- **Tournament Registration**: Teams, coaches, and school information (10-page workflow)
- **Judges Registration**: Judge volunteer information (3-page workflow)

The system supports multiple regions (NOVA North and NOVA South) with site-specific branding and configuration.

## Project Structure

### Core Architecture
```
Controllers/         - MVC controllers inheriting from BaseRegistrationController
Models/              - Entity models and repository pattern interfaces
ViewData/            - Strongly-typed view data classes organized by registration type
Views/               - Razor views organized by controller
Migrations/          - Entity Framework Core database migrations
wwwroot/             - Static files (CSS, JavaScript, images)
```

### Key Design Patterns

#### Repository Pattern
All database access goes through the repository pattern:
- **Interface**: `IOdysseyRepository` defines all data access methods
- **Implementation**: `OdysseyRepository` implements the interface
- **Usage**: Controllers access via `Repository` property from `BaseRegistrationController`

**Never access `OdysseyEntities` DbContext directly from controllers.**

#### ViewData Pattern
Each view uses strongly-typed ViewData classes:
- `BaseViewData`: Common properties (Config, RegionName, TournamentInfo, etc.)
- Page-specific classes: `Page01ViewData`, `Page02ViewData`, etc.
- Organized in subfolders: `ViewData/TournamentRegistration/`, `ViewData/JudgesRegistration/`

#### Multi-Page Registration Workflow
- Sequential pages (Page01, Page02, ..., Page10)
- Each page validates specific data before proceeding
- Session/TempData maintains state between pages
- Page number passed to repository Update methods for page-specific handling

### Controllers

#### BaseRegistrationController
Base class for all registration controllers providing:
- Registration state management (Available, Closed, Down, Soon)
- Email sending functionality with retry logic
- Site detection (NovaNorth vs NovaSouth)
- CSS file determination based on site
- Common ViewData population via `SetBaseViewData()`
- Registration type enumeration and state checking

#### Registration Controllers
- `TournamentRegistrationController`: 10-page team registration workflow
- `JudgesRegistrationController`: 3-page judge volunteer workflow
- `HomeController`: Landing page

### Models

#### Entity Models
- `TournamentRegistration`: Team registration data
- `Judge`: Judge volunteer data
- `Event`: Tournament and judges event information
- `Problem`: Odyssey of the Mind problem categories
- `School`: School information
- `Config`: Database-driven configuration key-value pairs
- `ContactUsRecipient`, `ContactUsSenderRole`: Email configuration

#### Repository
- `IOdysseyRepository`: Interface defining all data access methods
- `OdysseyEntities`: EF Core DbContext
- `IOdysseyEntities`: DbContext interface

### Registration State Management

The system enforces registration availability based on:
1. **Open Date/Time**: Configured per registration type
2. **Close Date/Time**: Configured per registration type
3. **Administrative Down Flag**: Manual override per registration type
4. **Time Zone**: All comparisons use Eastern Standard Time

State enum values:
- `Available`: Registration is open and accepting submissions
- `Soon`: Registration opens in the future
- `Closed`: Registration deadline has passed
- `Down`: Administratively disabled

### Email System

Email functionality includes:
- **BuildMessage()**: Constructs MailMessage with to/cc/bcc support
- **SendMessage()**: Sends email with retry logic for transient failures
- **Configuration**: SMTP server, credentials from Config dictionary
- **Error Handling**: ElmahCore logging and user-friendly error views
- **Error Views**: BadEmail.cshtml, BadCoachEmail.cshtml, BadAltCoachEmail.cshtml

## Technologies

### Backend
- **.NET 9.0**: Latest .NET framework
- **ASP.NET Core MVC**: Web framework
- **Entity Framework Core 8.0.8**: ORM for SQL Server
- **ElmahCore 2.1.2**: Error logging and management
- **System.Web.Adapters**: Compatibility helpers

### Frontend
- **jQuery 3.7.1**: JavaScript library
- **jQuery UI 1.13.2**: UI components
- **Custom CSS**: NovaNorth.css, NovaSouth.css for site-specific branding

### Database
- **SQL Server**: Primary data store
- **Code-First Migrations**: Schema management via EF Core

## Configuration

### Database-Driven Config
All configuration stored in Config table as key-value pairs:
- **Registration Times**: `{Type}RegistrationOpenDateTime`, `{Type}RegistrationCloseDateTime`
- **Admin Flags**: `Is{Type}RegistrationDown`
- **Email Settings**: `EmailServer`, `WebmasterEmail`, `WebmasterEmailPassword`
- **Region Info**: `RegionName`, `RegionNumber`
- **Testing**: `TestGuid` for bypassing state checks

### App Settings
- `appsettings.json`: Production configuration
- `appsettings.Development.json`: Development overrides

## Development Guidelines

### Adding a New Registration Type
1. Add enum value to `BaseRegistrationController.RegistrationType`
2. Create controller inheriting from `BaseRegistrationController`
3. Set `CurrentRegistrationType` in controller
4. Create ViewData folder and classes
5. Create Views folder and Razor files
6. Add Config table entries for open/close dates and down flag
7. Create Event record if needed

### Adding a Page to Existing Registration
1. Create `Page{N}ViewData.cs` in appropriate ViewData subfolder
2. Create `Page{N}.cshtml` in appropriate Views subfolder
3. Add GET and POST action methods to controller
4. Update repository Update method to handle new page number
5. Update previous page to navigate to new page
6. Handle session state for data passed between pages

### Modifying Database Schema
1. Update entity model(s) in Models folder
2. Run: `dotnet ef migrations add MigrationName`
3. Review generated migration in Migrations folder
4. Update repository methods if needed
5. Update ViewData classes with new properties
6. Update views to display/collect new data
7. Run: `dotnet ef database update`

### Working with Repository
```csharp
// Good: Use repository interface
var registration = Repository.GetTournamentRegistrationById(id);
Repository.UpdateTournamentRegistration(id, pageNumber, data);

// Bad: Direct DbContext access
// var registration = context.TournamentRegistrations.Find(id);
```

### Error Handling
```csharp
try
{
    // Your code
}
catch (Exception ex)
{
    // Log with ElmahCore
    ElmahExtensions.RaiseError(ex);
    
    // Return error view or message
    return View("Error");
}
```

## Building and Running

### Prerequisites
- Visual Studio 2022 or later
- .NET 9.0 SDK
- SQL Server (Express or higher)

### Build
```powershell
dotnet build
```

### Run
```powershell
dotnet run
```

Or press F5 in Visual Studio 2022

### Database Migrations
```powershell
# Add new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Rollback
dotnet ef database update PreviousMigrationName

# Remove last migration (if not applied)
dotnet ef migrations remove
```

## Testing

### Test Registration States
1. Modify Config table dates to test Soon/Closed states
2. Toggle `Is{Type}RegistrationDown` flags to test Down state
3. Use TestGuid query parameter to bypass checks (if enabled in code)

### Test Email Flow
1. Configure test SMTP server in Config table
2. Test BuildMessage with invalid addresses
3. Verify retry logic for transient failures
4. Check ElmahCore logs for email errors

## Deployment

### Publish with Visual Studio
1. Right-click project → Publish
2. Configure publish profile
3. Build configuration: Release
4. Deploy to IIS or Azure App Service

### Manual Publish
```powershell
dotnet publish -c Release -o ./publish
```

### Web.config
Included for IIS deployment with ASP.NET Core Module configuration.

## Support

For issues or questions:
- Check ElmahCore logs at `/elmah`
- Review database Config table for configuration issues
- Verify registration state dates and flags
- Check SMTP server connectivity for email issues

## Copyright

Copyright © 2014-2025 Tardis Technologies. All rights reserved.
