# OdysseyRegistration Project Instructions

## What this repo is
- ASP.NET registration system for NoVA North Odyssey of the Mind
- Multiple projects in different stages of migration from .NET Framework to .NET 10.0
- Primary production site: OdysseyMvc4 (.NET Framework 4.8)
- Target migration project: OdysseyMvc2024 (.NET 10.0)
- Supporting WebAPI: OdysseyRegistrationWebApi (.NET 10.0)

## Tech stack
- .NET 10.0 (new projects) / .NET Framework 4.8 (legacy)
- ASP.NET MVC (controllers, not Minimal APIs)
- Entity Framework Core (new) / Entity Framework 6.x (legacy)
- SQL Server (running in Docker for local dev)
- Docker Compose for local development environment
- Hosted on WinHost (production)

## Repo map
```
/                                    → Solution root
├── EFCoreToolReverseEngineeringTest → .NET 10.0 test project
├── JudgeRegistrationRazor          → .NET 10.0 Razor Pages project
├── OdysseyRegistration/            → Main project folder
│   ├── docker-compose              → Docker Compose orchestration (.dcproj)
│   ├── init/                       → SQL Server initialization scripts
│   ├── Odyssey.Database/           → SQL Server database project (.sqlproj)
│   ├── OdysseyCoreMvc/             → .NET 10.0 (unused?)
│   ├── OdysseyMvc4/                → .NET Framework 4.8 (PRODUCTION)
│   ├── OdysseyMvc4.Tests/          → .NET Framework 4.8 tests
│   ├── OdysseyMvc2023/             → .NET Framework 4.8 (migration attempt)
│   ├── OdysseyMvc2024/             → .NET 10.0 (current migration target)
│   ├── OdysseyMvc2025/             → .NET 10.0 (future?)
│   ├── OdysseyRegistrationWebApi/  → .NET 10.0 Web API
│   └── UpdateProblemSynopsesForRegistration/ → .NET Framework 4.8 utility
```

## Hard rules (do not violate)
- NEVER break OdysseyMvc4 (.NET Framework) - it's running in production
- NEVER commit passwords, connection strings, or secrets to git
- The sa_password.txt file must exist in OdysseyRegistration/OdysseyRegistration/ but NEVER commit it
- Always pass CancellationToken through all async calls in .NET 10.0 projects
- No sync-over-async (no .Result/.Wait)
- Problem table IDs start at 1 (not 0) - code must handle this
- EventName must match region name or code will fail (needs better error handling)
- When migrating code from OdysseyMvc4 to OdysseyMvc2024, maintain backward compatibility

## Database rules
- Local dev: SQL Server in Docker (localhost, sa account)
- Production: WinHost SQL Server 2008 R2 (s06.winhost.com)
  - Database: DB_12824_registration
  - User: DB_12824_registration_user
  - Quota: 25 MB
- Connection strings use different formats for Framework vs Core
- Init scripts are in init/ directory (duplicated at root - keep both in sync)
- Database schema managed via Odyssey.Database project

## Security & Secrets
- Use User Secrets for development (right-click project → Manage User Secrets)
- Production secrets go in hosting control panel, not code
- WebmasterEmailPassword comes from Config table in production DB
- Never store passwords in init.sql or novanorth-prod.sql directly
- Use sa_password.txt for local Docker SQL Server password

## Default workflow
1. Ask for missing requirements before changing code
2. Identify which project(s) need changes (Framework vs Core)
3. Propose a plan + list files to touch
4. Implement smallest change that works
5. Test locally with Docker Compose environment
6. Verify OdysseyMvc4 still works if touched

## Commands
### Build & Run
- Build solution: `dotnet build OdysseyRegistration.slnx`
- Run WebAPI: `dotnet run --project OdysseyRegistration\OdysseyRegistrationWebApi`
- Run tests: `dotnet test OdysseyRegistration\OdysseyMvc4.Tests`

### Docker
- Start environment: Set docker-compose as startup project, F5 in VS 2022
- Or: Right-click docker-compose → "Compose Up"
- Connect to SQL: localhost, sa, password from sa_password.txt
- Shell into SQL container: `docker container exec -it sqlserver bash`
- SQL query: `docker container exec sqlserver /opt/mssql-tools18/bin/sqlcmd -U sa -P <password> -Q "query"`

### Database
- Generate schema diagram: `mermerd -c "sqlserver://sa:<password>@localhost:1433?database=DB_12824_registration" -s dbo --useAllTables -o OdysseySchema.mmd`
- EF Core migrations: `dotnet ef migrations add <name>` then `dotnet ef database update`
- Export as JSON: `SELECT * FROM <table> FOR JSON AUTO` in SSMS

## Known issues to avoid
- Volume sharing must be enabled in Docker Desktop (Settings → Resources → Shared Drives)
- sqlserver.configurator container should run automatically but doesn't (TODO to fix)
- EventName in Events table must match RegionName or code throws exception (line 355 in OdysseyRepository.cs)
- Login fails if connection string points to wrong server/database
- Problem IDs changed from 0-based to 1-based (needs code updates)

## Migration status
Copying OdysseyMvc4 (.NET Framework) to OdysseyMvc2024 (.NET 10.0):
- ✅ BaseRegistrationController - Complete
- ✅ HomeController - Complete
- ✅ JudgesRegistrationController - Complete
- ❓ TournamentRegistrationController - Stopped at line 144

## Output format
- Explain which project(s) are affected and why
- Show file paths relative to repo root
- For .NET Framework changes, note potential compatibility issues
- When touching database, mention both init/ and root copies
- Provide Docker and connection string changes separately
