# Security Rules

## Secrets management
- NEVER commit secrets, passwords, or connection strings to git
- Ensure .gitignore includes secrets files

### Development (User Secrets)
OdysseyMvc2024 uses ASP.NET Core User Secrets for the connection string:
```bash
cd OdysseyRegistration\OdysseyMvc2024
dotnet user-secrets set "ConnectionStrings:OdysseyConnection" "Server=localhost;Database=DB_12824_registration;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
dotnet user-secrets list  # View stored secrets
```
- Stored at: `%APPDATA%\Microsoft\UserSecrets\{UserSecretsId}\secrets.json`
- Connection string must include `Password=` placeholder (placeholder is acceptable)

### OdysseyMvc2024 SQL password injection (File-based, all environments)

#### How it works
OdysseyMvc2024 requires a password file in all environments (development, staging, production):

1. **Build process (Development only)**: The MSBuild target (`CopyPasswordFile`) runs before every build:
   - Creates the `src/OdysseyMvc2024/protected/` directory if it doesn't exist
   - Copies `src/sa_password.txt` → `src/OdysseyMvc2024/protected/sql-pw.txt`
   - Skips copying if file is unchanged (efficient builds)

2. **Startup process (All environments)**: When app starts (`Program.cs` line 29):
   - `ConnectionStringPasswordInjector.InjectSqlPasswordFromProtectedFile()` is called
   - Reads password from `protected/sql-pw.txt` (required to exist)
   - Injects it into connection string after `Password=` placeholder
   - **Throws exception if file doesn't exist** — prevents misconfigured deployments
   - Connection string is ready for database access

3. **File locations**:
   - Source (shared, development only): `src/sa_password.txt` (one password for all local development)
   - Target (per-project): `src/OdysseyMvc2024/protected/sql-pw.txt` (must exist in all environments)
   - Format: Plain text, single line, password only (whitespace trimmed)

#### Publish behavior
The `protected/` directory is **excluded from publish** in `.csproj`:
`xml
<ItemGroup>
  <!-- Exclude protected directory from publish (must be uploaded separately per environment) -->
  <Exclude Include="protected\**" />
</ItemGroup>
`
This ensures:
- Development: Auto-created by build, not published to other machines
- Staging/Production: Not auto-deployed, must be uploaded separately before app starts

#### Setup for new machines (Development)
When setting up OdysseyMvc2024 on a new development machine:

1. Create `src/sa_password.txt` with SQL Server password (one line, no extra whitespace)
2. Rebuild OdysseyMvc2024 → automatically creates `protected/sql-pw.txt` from `sa_password.txt`
3. Start the app → password is injected into connection string at runtime

#### Deployment to Staging and Production
The `protected/` directory is not published. Password file must be uploaded before app starts:

**Required steps (for each deployment):**
1. Publish application normally (`protected/` is excluded)
2. Create `protected/` directory on server via FTP
3. Upload `protected/sql-pw.txt` via FTPS with environment-specific password
4. Start/restart application → password is injected at startup

**Note**: Environment variables are NOT used for passwords. The file-based approach ensures:
- Consistent behavior across all environments
- No accidental password exposure in config files
- Explicit deployment step prevents misconfiguration

#### File safety
- `src/sa_password.txt`: Development only, in `.gitignore`, never committed
- `src/OdysseyMvc2024/protected/`: Excluded from publish, in `.gitignore`, never committed
- `protected/sql-pw.txt`: Auto-created in dev (build), manually uploaded in staging/prod
- Password is never exposed in logs or error messages

### Docker (SQL Server)
- Use `sa_password.txt` for local Docker SQL password (don't commit it)
- Docker Secrets mount this as `/run/secrets/sa_password`

### Staging and Production (WinHost)
Choose one of these approaches:

**Option 1: Environment Variables (Recommended)**
- WinHost Control Panel → Site Manager → IIS Settings → Application Settings
- Name: `ConnectionStrings:OdysseyConnection`
- Value: Full connection string with production password

**Option 2: appsettings.Production.json**
- Create directly on server via FTP
- Keep out of source control

**Option 3: Web.config (Legacy .NET Framework)**
- Use Web.config transforms or edit on server after deployment

**Option 4: Azure Key Vault (Future)**
- For Azure App Service migration

## Authentication & Authorization
- All protected endpoints must have [Authorize] attribute
- Use role-based authorization where appropriate
- Verify user identity before sensitive operations
- Don't rely on client-side validation alone
- Implement proper session management

## Input validation
- Validate all user inputs server-side
- Use parameterized queries (prevent SQL injection)
- Sanitize inputs before display (prevent XSS)
- Validate file uploads (size, type, content)
- Reject malformed or suspicious requests

## Data protection
- Don't log sensitive data (passwords, tokens, PII)
- Don't expose sensitive IDs/tokens in URLs
- Use HTTPS for all production traffic
- Encrypt sensitive data at rest where required
- Follow principle of least privilege for database access

## CORS & Headers
- Configure CORS restrictively (no AllowAnyOrigin in production)
- Set appropriate security headers
- Disable detailed error messages in production
- Remove server identification headers

## Known vulnerabilities
- Keep NuGet packages updated (especially jQuery, jQuery.UI)
- Review security warnings in build output
- Address vulnerability scan findings promptly
- Test security changes in dev environment first

## Red zones (require human review before merging)
Changes touching any of the following areas must include tests, a written risk assessment, and a rollback note:
- Authentication / authorization
- Billing / payments
- Database migrations
- Security configuration
