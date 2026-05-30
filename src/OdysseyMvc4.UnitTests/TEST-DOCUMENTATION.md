# OdysseyMvc4 Migration Test Suite

## Overview

This document describes the unit test suite created to support the migration of **OdysseyMvc4** (a .NET Framework 4.8 ASP.NET MVC 5 application for Odyssey of the Mind regional tournament registration) to **OdysseyMvc2024** (a .NET 10 ASP.NET Core MVC application).

The test suite serves as a **safety net** to ensure that all business logic, computed properties, and data model shapes are preserved during the migration. Every test targets the OdysseyMvc2024 codebase so that, as migration work proceeds, tests can be run continuously to verify correctness.

---

## Project Structure

```
OdysseyMvc4.Tests.Unit/
├── OdysseyMvc4.Tests.Unit.csproj   # xUnit test project targeting net10.0
├── Helpers/
│   └── TestHelper.cs               # Shared mocks, default data, FakeUrlHelper
├── Controllers/
│   ├── BaseRegistrationControllerTests.cs       # 28 tests
│   ├── TournamentRegistrationControllerTests.cs # 24 tests
│   └── JudgesRegistrationControllerTests.cs     # 30 tests
├── ViewData/
│   └── ViewDataTests.cs            # 46 tests
└── Models/
    └── ModelTests.cs               # 8 tests (+ 7 remaining from above)
```

**Total: 143 tests, all passing.**

---

## Technology Stack

| Component | Version | Purpose |
|-----------|---------|---------|
| .NET | 10.0 (preview) | Target framework matching OdysseyMvc2024 |
| xUnit | 2.9.3 | Test framework |
| Moq | 4.20.72 | Mocking `IOdysseyRepository` and other dependencies |
| FluentAssertions | 8.3.0 | Readable assertion syntax |
| coverlet.collector | 6.0.4 | Code coverage collection |
| Microsoft.NET.Test.Sdk | 17.14.1 | Visual Studio / `dotnet test` integration |

---

## Test Categories and What They Cover

### 1. BaseRegistrationControllerTests (28 tests)

Tests the core business logic inherited by all registration controllers.

#### GetFriendlyRegistrationName (5 tests)
Verifies the human-readable registration name for each `RegistrationType` enum value:
- `None` → empty string
- `Tournament` → `"Tournament Registration"`
- `Judges` → `"Judges Registration"`
- `CoachesTraining` → `"Coaches Training Registration"` (special case with space)
- `Volunteer` → `"Volunteer Registration"`

#### Enum Value Tests (2 tests)
Locks down the integer values of `RegistrationType` (0–4) and `RegistrationState` (0–3) to prevent accidental reordering during migration, which could break database values or switch/case logic.

#### IsRegistrationClosed (4 tests)
- Close date in the future → not closed
- Close date in the past → closed
- Invalid date string in config → closed (fail-safe)
- Uses the correct config key per registration type (e.g., `JudgesRegistrationCloseDateTime`)

#### IsRegistrationComingSoon (3 tests)
- Open date in the future → coming soon (true)
- Open date in the past → not coming soon (false)
- Invalid date string → not coming soon (fail-safe: assumes we're past the opening date)

#### IsRegistrationDown (4 tests)
- Config value `"true"` → down
- Config value `"false"` → not down
- Invalid boolean string → not down (fail-safe)
- Uses the correct config key per type (e.g., `IsJudgesRegistrationDown`)

#### CurrentRegistrationState (5 tests)
Tests the state machine priority order:
1. **Soon** (highest priority — if open date hasn't arrived, nothing else matters)
2. **Down** (if registration is administratively disabled)
3. **Closed** (if past the close date)
4. **Available** (default when none of the above apply)

Also verifies that `Soon` takes priority over `Down` when both conditions are true.

#### BuildMessage (8 tests)
Tests email message construction:
- Valid inputs produce a `MailMessage` with correct From, Subject, Body, and HTML flag
- Multiple comma-separated recipients are split correctly
- BCC and CC are set when provided
- Empty/whitespace BCC and CC are ignored (no empty addresses added)
- Invalid email format returns `null` (not an exception)
- Priority is always set to `Normal`

---

### 2. TournamentRegistrationControllerTests (24 tests)

Tests tournament-specific business logic.

#### GetDivisionOfTeamMember (13 tests via Theory)
Maps individual student grades to Odyssey of the Mind divisions:

| Grade | Division |
|-------|----------|
| Kindergarten, 0, 1, 2 | Primary (0) |
| 3, 4, 5 | Division 1 (1) |
| 6, 7, 8 | Division 2 (2) |
| 9, 10, 11, 12 | Division 3 (3) |

#### DetermineDivisionOfTeam (8 tests)
Determines the overall team division based on the highest-grade member:
- All Kindergarten → Division 0
- Mixed Primary and Division 1 → Division 1
- Highest grade wins across all members
- Empty strings and null grades are skipped
- Empty list or all-empty grades → returns -1 (undefined)
- Single member and 7-member teams

#### GetProblemsAsHtmlList (3 tests)
- Non-primary teams get problems wrapped in `<ol><li>` tags without the Primary problem
- Primary teams additionally include the Primary problem with `"(The Primary Problem)"` suffix
- Empty problem list produces `<ol>\n</ol>\n`

#### Constructor and Utility Tests (3 tests)
- Constructor sets `CurrentRegistrationType` to `Tournament`
- Constructor sets `FriendlyRegistrationName` to `"Tournament Registration"`
- Null repository throws `ArgumentNullException`

---

### 3. JudgesRegistrationControllerTests (30 tests)

Tests judges-specific business logic.

#### BuildMailRegionalDirectorHyperLink (6 tests)
Generates a `mailto:` link with URI-encoded subject and body:
- Returns a link starting with `mailto:{RegionalDirectorEmail}`
- Subject contains the region number
- Body contains volunteer offer text ("I cannot be a judge this year...")
- Null viewData → `ArgumentNullException`
- Null Config → `ArgumentNullException`
- Missing RegionalDirectorEmail key → `ArgumentException`

#### GetPreviousPositions (7 tests, via reflection)
This method is `private static`, so tests use `System.Reflection` to invoke it.
Concatenates checked judge positions with semicolons:
- All 7 positions selected → `"Head Judge;Problem Judge;Style Judge;Staging Judge;Timekeeper;Scorechecker;Weigh-In Judge"`
- No positions → `null` (written as NULL to SQL Server)
- Only Head Judge → `"Head Judge"` (no leading semicolon)
- Only Problem Judge → `"Problem Judge"` (leading semicolon trimmed)
- Only Weigh-In Judge → `"Weigh-In Judge"` (leading semicolon trimmed)
- Various combinations verify semicolon placement

#### GenerateEmailBody (14 tests)
Tests the email template placeholder replacement engine. The email body template contains `<span>Placeholder</span>` tokens that are replaced with actual judge and event data using compiled `GeneratedRegex` patterns:
- `<span>JudgeID</span>` → judge's numeric ID
- `<span>FirstName</span>` → judge's first name
- `<span>LastName</span>` → judge's last name
- `<span>Region</span>` → `"Region 9"`
- `<span>JudgesTrainingLocation</span>` → training venue with optional hyperlink and address
- `<span>JudgesTrainingDate</span>` → long date string or "TBA"
- `<span>JudgesTrainingTime</span>` → time string or "TBA"
- `<span>TournamentLocation</span>` → tournament venue with optional hyperlink and address
- `<span>TournamentDate</span>` → long date string or "TBA"
- `<span>TournamentTime</span>` → time string or "TBA"
- `<span>ContactUsURL</span>` → HomePage + ContactUsURL from config
- With LocationURL → output includes `<a href="..." target="_blank">` hyperlink
- Without LocationURL → no hyperlink tag in output

#### Constructor Tests (3 tests)
- Registration type set to Judges
- Friendly name set to `"Judges Registration"`
- Null repository throws

---

### 4. ViewDataTests (46 tests)

Tests computed properties on ViewData classes that drive the registration UI.

#### BaseViewData TBA Fallbacks (12 tests)
These properties return `"TBA"` when the underlying data is null, empty, or whitespace:
- `TournamentDate`: Valid date → long date string; null date or null TournamentInfo → `"TBA"`
- `TournamentLocation`: Valid → location string; null/empty/whitespace → `"TBA"`
- `TournamentTime`: Valid → time string; null/empty/whitespace → `"TBA"`

#### Judges Page01ViewData TBA Fallbacks (7 tests)
- `JudgesTrainingDate`: Valid date → long date; null → `"TBA"`
- `JudgesTrainingLocation`: Valid → location; null/empty → `"TBA"`
- `JudgesTrainingTime`: Valid → time; null → `"TBA"`

#### Tournament Page01ViewData Fee Logic (14 tests)
Critical business logic for registration fees:
- `TeamRegistrationFee`:
  - Before late fee date → regular fee (e.g., `"$100"`)
  - After late fee date → late fee (e.g., `"$125"`)
  - No `LateEventCostStartDate` → always regular fee
  - Null/empty `EventCost` → `"TBA"`
- `LateTeamRegistrationFee`: Value → `"$125"`; null → empty string
- `PaymentDueDate`: Valid date → long date; null → `"TBA"`
- `LateEventCostStartDate`: Returns day BEFORE the actual start date (e.g., March 1 → February 28)
- `AcceptingPayPal`: Parses boolean from Config; invalid → false
- `TournamentRegistrationCloseDateTime`: Parses date from Config → long date string

#### ProblemConflictList Transform (6 tests)
The judges registration Page02 has a "Problem Conflict" dropdown that re-labels `"No Preference"` as `"I Don't Know"`:
- All three conflict lists (`ProblemConflictList1/2/3`) perform the same transform
- Items without `"No Preference"` text are left unchanged
- Other items in the list are preserved alongside the renamed item

---

### 5. ModelTests (8 tests)

Verifies that model class shapes match what the database and controllers expect.

#### Judge Model (2 tests)
- All 30+ properties can be set and retrieved with correct types
- Nullable properties (`InformationMailed_`, `AttendedJT_`, `Active`, timestamps) default to null

#### TournamentRegistration Model (3 tests)
- All properties including 7 team member slots (FirstName/LastName/Grade × 7)
- Nullable properties (`ProblemID`, `SchoolID`, `Spontaneous`, `Paid`, `JudgeID`, `VolunteerID`, timestamps) default to null

#### Problem Model (1 test)
- Core properties: `ProblemID`, `ProblemCategory`, `ProblemName`, `Divisions`, `CostLimit`

#### Event Model (2 tests)
- All properties including financial fields (`EventCost`, `LateEventCost`)
- Nullable dates (`StartDate`, `EndDate`, `LateEventCostStartDate`, `PaymentDueDate`) default to null

---

## Test Infrastructure

### Helpers/TestHelper.cs

Provides shared test setup to avoid duplication:

- **`CreateDefaultConfig()`**: Returns a `Dictionary<string, string>` with all 25+ config keys needed by the controllers, pre-populated with valid defaults (registration dates in the past/future to ensure "Available" state).

- **`CreateDefaultTournamentInfo()`**: Returns an `Event` object with typical tournament data (Springfield High School, $100/$125 fees, March 2025 dates).

- **`CreateDefaultJudgesInfo()`**: Returns an `Event` object with judges training data, including an email body template with all 11 `<span>Placeholder</span>` tokens.

- **`CreateMockRepository()`**: Creates a `Mock<IOdysseyRepository>` pre-configured with default config, tournament info, region name ("NoVA North"), and region number ("9").

- **`SetupControllerContext()`**: Configures `HttpContext`, `Request.Host`, and `IUrlHelper` on a controller instance so that `DetermineSiteName()` and `DetermineSiteCssFile()` work in tests.

- **`FakeUrlHelper`**: A concrete implementation of `IUrlHelper` (not a mock) because `PageLink()` is an extension method that cannot be intercepted by Moq. Returns predictable values for `Page()`, `Content()`, and `Link()`.

### Testable Subclasses

Since `BaseRegistrationController` is abstract and some methods are `protected`:

- **`TestableBaseRegistrationController`**: Exposes `BuildMessage()` and `SetBaseViewData()` as public methods.
- **`TestableJudgesRegistrationController`**: Exposes `GenerateEmailBody()` as a public method.

Both use `[SetsRequiredMembers]` to satisfy the `required string FriendlyRegistrationName` constraint.

---

## Production Code Changes

Two minimal changes were made to OdysseyMvc2024 production code to enable testability:

### 1. `[SetsRequiredMembers]` Attribute

**Files modified:**
- `OdysseyMvc2024/Controllers/TournamentRegistrationController.cs`
- `OdysseyMvc2024/Controllers/JudgesRegistrationController.cs`

**Change:** Added `[SetsRequiredMembers]` attribute to constructors and `using System.Diagnostics.CodeAnalysis;` import.

**Reason:** The `FriendlyRegistrationName` property on `BaseRegistrationController` is marked with the C# `required` keyword, which means any code creating an instance must initialize it. The constructors DO set this property (`FriendlyRegistrationName = GetFriendlyRegistrationName()`), but without `[SetsRequiredMembers]`, the compiler only enforces the constraint within the same assembly. When the test project (a separate assembly) tries to instantiate these controllers, the compiler correctly flags the missing initialization. The `[SetsRequiredMembers]` attribute tells the compiler "this constructor satisfies all `required` members."

### 2. ASP.NET Core Framework Reference

**File modified:** `OdysseyMvc4.Tests.Unit/OdysseyMvc4.Tests.Unit.csproj`

**Change:** Added `<FrameworkReference Include="Microsoft.AspNetCore.App" />`.

**Reason:** The test project needs access to ASP.NET Core types (`Controller`, `IUrlHelper`, `DefaultHttpContext`, etc.) that OdysseyMvc2024 depends on.

---

## What Is NOT Tested (and Why)

### Volunteer Registration Controller
The `VolunteerRegistrationController` has **not yet been migrated** from OdysseyMvc4 to OdysseyMvc2024. Tests should be added when that migration occurs.

### CoachesTraining Registration Controller
The `CoachesTrainingRegistrationController` has **not yet been migrated** from OdysseyMvc4 to OdysseyMvc2024. Tests should be added when that migration occurs.

### Database Operations
All tests use `Mock<IOdysseyRepository>`. Actual database calls (`AddJudge`, `UpdateTournamentRegistration`, etc.) are not tested because:
- The tests are unit tests, not integration tests
- The `IOdysseyRepository` interface provides the seam for mocking
- Entity Framework Core operations should be tested separately if needed

### Email Sending (SmtpClient)
The `SendMessage()` method is not unit tested because it makes a real SMTP connection. The `BuildMessage()` method IS tested to verify correct message construction.

### View Rendering
Razor views are not tested because they require a full ASP.NET Core pipeline. The ViewData objects that feed data to views ARE tested.

### HTTP Request/Response Pipeline
Controller action methods (GET/POST handlers) are not deeply tested because they primarily orchestrate database calls and view rendering. The business logic methods they call ARE tested.

---

## Running the Tests

```powershell
# From the solution directory
cd G:\GitHub\RobertBernstein\OdysseyRegistration\OdysseyRegistration

# Run all tests
dotnet test OdysseyMvc4.Tests.Unit

# Run tests with verbose output
dotnet test OdysseyMvc4.Tests.Unit --verbosity normal

# Run a specific test class
dotnet test OdysseyMvc4.Tests.Unit --filter "FullyQualifiedName~BaseRegistrationControllerTests"

# Run tests matching a pattern
dotnet test OdysseyMvc4.Tests.Unit --filter "GetDivisionOfTeamMember"

# Run with code coverage
dotnet test OdysseyMvc4.Tests.Unit --collect:"XPlat Code Coverage"
```

---

## Adding Tests for Future Migrations

When migrating additional controllers (Volunteer, CoachesTraining), follow this pattern:

1. Add a new test file in `Controllers/` (e.g., `VolunteerRegistrationControllerTests.cs`)
2. Use `TestHelper.CreateMockRepository()` for mock setup
3. Use `TestHelper.SetupControllerContext()` for HTTP context
4. If the controller constructor sets `FriendlyRegistrationName`, add `[SetsRequiredMembers]` to it
5. For `protected` methods, create a testable subclass (see `TestableJudgesRegistrationController`)
6. For `private static` methods, use `System.Reflection` (see `InvokeGetPreviousPositions`)
7. Run `dotnet test` to verify all existing tests still pass
