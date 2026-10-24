# Detection commands

Source: Based on https://antondevtips.com/blog/how-to-make-ai-write-high-quality-code-in-dotnet

Use these commands to support review work:

- `git diff --name-only`
- `git diff -- src`
- `dotnet build`
- `dotnet test` with an explicit project path when you need a focused regression check

The review should not replace correctness checks, but it should highlight readability, maintainability, and architecture issues before a human reviewer spends time on them.
