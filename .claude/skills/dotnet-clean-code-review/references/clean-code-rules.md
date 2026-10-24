# Clean code review rules

Source: Based on https://antondevtips.com/blog/how-to-make-ai-write-high-quality-code-in-dotnet

- Prefer clear names over generic names like `Manager`, `Helper`, `Utility`, and `Service` unless the abstraction is genuinely the domain concept.
- Avoid `async void`; prefer `Task`/`ValueTask` with an `Async` suffix.
- Accept `CancellationToken` in async APIs that can be cancelled.
- Avoid boolean flags in method parameters and constructor arguments.
- Prefer small, cohesive classes and methods over large procedural blocks.
- Prefer composition over inheritance when the inheritance chain is more than one layer deep.
- Avoid `#region` blocks and other structural noise.
- Prefer interface return types that reflect the actual contract, such as `IReadOnlyList<T>`.
- Keep review notes anchored to maintainability and readability, not stylistic preferences alone.
