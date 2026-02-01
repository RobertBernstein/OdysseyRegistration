# Performance & Reliability Rules

## Database operations
- Use parameterized queries (never string concatenation)
- Keep transactions short and focused
- Use appropriate timeout values (not infinite)
- Consider connection pooling settings
- Use async methods for all database operations

## Entity Framework
- Avoid N+1 queries (use Include/ThenInclude appropriately)
- Use AsNoTracking() for read-only queries
- Project to DTOs instead of returning entities directly
- Dispose DbContext properly (use using statements)
- Don't materialize large result sets (use pagination)

## HTTP clients
- Use IHttpClientFactory (don't new up HttpClient)
- Set explicit timeouts per request
- Wire CancellationToken through to HTTP calls
- Handle transient failures appropriately
- Don't retry indefinitely (set max retry attempts)

## Caching
- Set appropriate TTL values (don't cache forever)
- Version cache keys when schema changes
- Handle cache misses gracefully
- Consider stampede protection for hot keys
- Clear cache entries when underlying data changes

## Logging
- Avoid logging in tight loops
- Use structured logging with semantic properties
- Avoid high-cardinality fields (GUIDs, timestamps)
- Log at appropriate levels (don't log everything as Error)
- Consider performance impact of logging in hot paths

## Resource management
- Dispose IDisposable objects properly (use using statements)
- Avoid keeping connections/resources open longer than needed
- Set reasonable limits on collection sizes
- Monitor memory usage in long-running operations
