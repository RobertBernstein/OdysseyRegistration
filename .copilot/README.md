# OdysseyRegistration Project Configuration

This directory contains project-specific instructions for GitHub Copilot CLI.

## Files

- **instructions.md** - Main project instructions including repo structure, tech stack, workflows, and commands
- **odysseymvc4-architecture.md** - **Comprehensive architecture reference for the OdysseyMvc4 production project** (ASP.NET MVC 5 / .NET Framework 4.8): full project structure, controller hierarchy, entity models, database schema, registration flow patterns, ViewData classes, multi-region support, email system, configuration, deployment, business rules, and known issues
- **odysseymvc2024-architecture.md** - **Comprehensive architecture reference for the OdysseyMvc2024 migration target** (ASP.NET Core MVC / .NET 10.0): project structure, DI-based controller hierarchy, EF Core Code First models, repository interface, ViewData classes, migration status, key differences from OdysseyMvc4, known issues, and decompilation artifacts
- **api-conventions.md** - API and controller patterns specific to this ASP.NET MVC project
- **performance-reliability.md** - Database, EF, HTTP, caching, and logging best practices
- **security.md** - Security rules for secrets management, authentication, and data protection

## Purpose

These files help GitHub Copilot CLI understand:
- The complex multi-project structure (Framework + Core migration in progress)
- Production constraints (OdysseyMvc4 must not break)
- Local development setup (Docker Compose + SQL Server)
- Security requirements (secrets, WinHost production environment)
- Known issues and migration status

## Usage

These instructions are automatically loaded when working in this repository. They guide Copilot to:
- Make appropriate suggestions for Framework vs Core projects
- Follow security best practices
- Use correct commands and workflows
- Avoid known issues and pitfalls

## Maintenance

Update these files when:
- Migration milestones are reached
- New projects are added to the solution
- Conventions or patterns change
- New issues or gotchas are discovered
