---
name: dotnet-clean-code-review
source: Based on https://antondevtips.com/blog/how-to-make-ai-write-high-quality-code-in-dotnet
description: Review C# and .NET code for clean code problems including Manager/Helper/Utils class names, #region usage, boolean flag parameters, weak return types, async naming, CancellationToken usage, single-implementation interfaces, and deep inheritance. Use when the user asks to "review my changes", "do a clean code review", "check this class", or wants naming and readability feedback on C# code. Do NOT use for correctness bugs or security issues - use /code-review for that. Do NOT use when writing new code.
---

# Dotnet clean code review

Review the target code for the following issues:

1. Avoid `async void`; methods should return `Task` or `ValueTask` and end with `Async`.
2. Public methods should accept a `CancellationToken` when they perform I/O, database work, or long-running operations.
3. Do not use `#region` blocks or large utility classes that hide logic.
4. Avoid boolean parameters; prefer a clear method name or a value object/option object.
5. Prefer focused interfaces, and avoid single-implementation interfaces unless they are required for a framework.
6. Prefer composition over deep inheritance chains.
7. Prefer readable, domain-specific names over `Manager`, `Helper`, `Utility`, or `Service` when they do not reflect a real responsibility.
8. Return strong types (`IReadOnlyList<T>`, `IReadOnlyDictionary<TKey, TValue>`, etc.) instead of weak or overly broad abstractions when they match the intent.
9. Keep review comments specific, actionable, and tied to readability, maintainability, or architecture, not personal preference alone.

## Workflow

- Start with the final diff or the exact file under review.
- Check the method and class names before looking at implementation details.
- Review for abstractions that are too broad or duplicated.
- Check async signatures and cancellation support.
- Check for boolean flags and parameter smell.
- Flag code that is readable but structurally wrong.
- Prefer high-signal comments over noisy style nitpicks.

Return a concise review: important issues first, then minor recommendations, then a short "no issues found" summary if nothing stands out.
