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
- Password must match `OdysseyRegistration/sa_password.txt` (for Docker)

### Docker (SQL Server)
- Use `sa_password.txt` for local Docker SQL password (don't commit it)
- Docker Secrets mount this as `/run/secrets/sa_password`

### Production (WinHost)
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
