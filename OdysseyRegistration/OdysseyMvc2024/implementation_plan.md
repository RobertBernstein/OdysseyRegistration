# Implementation Plan

[Overview]
Fix Tournament Registration in OdysseyMvc2024 to make it fully functional by creating the missing EmailPartial view and ensuring the registration workflow works end-to-end.

The OdysseyMvc2024 project is an ASP.NET Core 9.0 port of the original OdysseyMvc4 ASP.NET MVC 4 application. The Tournament Registration feature is a 10-page workflow that allows teams to register for Odyssey of the Mind tournaments. The primary issue is a missing email template view (`EmailPartial.cshtml`) that prevents the final registration page from displaying and sending confirmation emails.

The original OdysseyMvc4 implementation works correctly and serves as the reference for this fix. Volunteer-related functionality has been intentionally disabled in both versions and will remain commented out per the user's request.

[Types]
No new types are required for this implementation.

All necessary types already exist in the project:
- `TournamentRegistration` (entity model with `Id` property mapped to `TeamID` column)
- `Page01ViewData` through `Page10ViewData` (view data classes)
- `BaseViewData` (base class for all view data)
- `IOdysseyRepository` / `OdysseyRepository` (repository pattern implementation)

[Files]
Create the missing EmailPartial view file to enable email generation on the final registration page.

**New Files to Create:**
1. `Views/Shared/TournamentRegistration/EmailPartial.cshtml`
   - Purpose: Email template for tournament registration confirmation
   - Based on: OdysseyMvc4/Views/Shared/TournamentRegistration/EmailPartial.cshtml
   - Changes: Update namespace from `OdysseyMvc4` to `OdysseyMvc2024`, change `TeamID` references to `Id`

**Existing Files - No Modifications Required:**
- `Controllers/TournamentRegistrationController.cs` - Already properly implemented for ASP.NET Core
- `Controllers/BaseRegistrationController.cs` - Fully functional
- `Views/TournamentRegistration/Page01.cshtml` through `Page10.cshtml` - Already use correct namespaces
- `ViewData/TournamentRegistration/*.cs` - All view data classes are complete
- `Models/OdysseyRepository.cs` - Repository methods are implemented

[Functions]
No new functions need to be created.

**Existing Functions - Verification Only:**
- `TournamentRegistrationController.GenerateEmailBody(Page10ViewData)` 
  - Location: `Controllers/TournamentRegistrationController.cs`
  - Status: Already uses ASP.NET Core `ICompositeViewEngine` for view rendering
  - Action: Verify it correctly renders the EmailPartial view

[Classes]
No new classes need to be created.

All required classes already exist:
- `TournamentRegistrationController` - 10-page registration workflow controller
- `BaseRegistrationController` - Base controller with email and state management
- `OdysseyRepository` - Database access via repository pattern
- `Page01ViewData` through `Page10ViewData` - View data classes for each registration page

[Dependencies]
No new dependencies are required.

Current project dependencies are sufficient:
- Microsoft.AspNetCore.Mvc (view rendering)
- Microsoft.EntityFrameworkCore.SqlServer (database access)
- ElmahCore (error logging)
- System.Net.Mail (email sending)

[Testing]
Validate the Tournament Registration workflow from start to finish.

**Manual Testing Steps:**
1. Start Docker SQL Server container (`docker-compose up`)
2. Run OdysseyMvc2024 application
3. Ensure Config table has valid dates:
   - `TournamentRegistrationOpenDateTime` < current time
   - `TournamentRegistrationCloseDateTime` > current time
   - `IsTournamentRegistrationDown` = "False"
4. Navigate to Tournament Registration (Page01)
5. Complete all 10 pages of registration:
   - Page01: Start registration
   - Page02: Select school
   - Page03: Enter judge information
   - Page04: Skip (volunteer disabled)
   - Page05: Enter coach information
   - Page06: Enter team member information
   - Page07: Select division and problem
   - Page08: Enter scheduling issues
   - Page09: Review and confirm
   - Page10: Final confirmation with email
6. Verify Page10 displays correctly with registration summary
7. Verify confirmation email is sent to coach email address
8. Check ELMAH logs for any errors

[Implementation Order]
Execute the implementation in this specific order to minimize issues.

1. Create the `Views/Shared/TournamentRegistration/` directory structure
2. Create `EmailPartial.cshtml` view file with updated namespace and property references
3. Verify the application builds without errors
4. Test the complete registration workflow
5. Verify email content is rendered correctly
6. Confirm email delivery works (if SMTP is configured)
