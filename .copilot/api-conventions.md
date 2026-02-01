# API Conventions

## Controller patterns
- Use traditional ASP.NET MVC controllers (not Minimal APIs)
- Controllers inherit from Controller base class
- Use appropriate HTTP verb attributes ([HttpGet], [HttpPost], etc.)
- Return ActionResult or typed IActionResult results

## Async patterns
- All async methods must accept CancellationToken
- Pass CancellationToken through the entire call chain
- Use ConfigureAwait(false) for library code
- No sync-over-async (.Result, .Wait, .GetAwaiter().GetResult())

## Validation
- Use DataAnnotations on models
- Validate early in controller actions with ModelState.IsValid
- Return appropriate status codes (BadRequest, NotFound, etc.)

## Error handling
- Use try-catch blocks for database operations
- Log exceptions before returning error responses
- Return user-friendly error messages (don't expose implementation details)
- Use ProblemDetails for API responses when appropriate

## Security
- All endpoints must have appropriate authentication/authorization
- Use [Authorize] attribute to protect endpoints
- Never expose sensitive data in responses or logs
- Validate and sanitize all user inputs
