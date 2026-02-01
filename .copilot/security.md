# Security Rules

## Secrets management
- NEVER commit secrets, passwords, or connection strings to git
- Use User Secrets (secrets.json) for local development
- Store production secrets in hosting control panel
- Use sa_password.txt for local Docker SQL password (but don't commit it)
- Ensure .gitignore includes secrets files

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
