# Contributing to TraceTK

Thanks for your interest in contributing to TraceTK.

TraceTK is tracing and diagnostics tooling for OpenTK applications. Contributions that improve its diagnostics, reliability, documentation, tests, and developer experience are welcome.

## Getting Started

Fork the repository and clone your fork locally.

Create a branch for your change rather than working directly on `main`.

Use a descriptive branch name such as:

```text
fix/102-frame-statistics-average
feat/frame-time-range
docs/frame-tracing-guide
```

Prefferably follow this conventions:
`type/issue-description`

## Building

TraceTK requires the .NET SDK used by the repository.

Restore and build the solution with:

```bash
dotnet restore
dotnet build
```

## Testing

Run the complete test suite before submitting a pull request:

```bash
dotnet test
```

New functionality should include tests where appropriate.

Bug fixes should preferably include a test that reproduces the problem and verifies the fix.

## Documentation

Public APIs must include XML documentation.

When changing public APIs, verify that the Docfx documentation can still be generated:

```bash
docfx docfx.json
```

User-facing documentation should be updated when a change affects how TraceTK is used.

## Pull Requests

Keep pull requests focused on a single purpose.

Before submitting a pull request:

* Ensure the project builds successfully.
* Ensure all tests pass.
* Add tests for new or changed behavior where appropriate.
* Document public APIs.
* Update user-facing documentation where necessary.
* Avoid unrelated formatting, refactoring, or cleanup changes.

Describe what the pull request changes, why the change is needed, and how it was tested.

If the pull request resolves an issue, include:

```text
Fixes #123
```

## Issues

Use the appropriate issue form when reporting bugs, requesting features, or suggesting documentation improvements.

Search existing issues before creating a new one.

For bugs, provide enough information to reproduce the problem whenever possible.

## Scope

Large features or architectural changes should generally be discussed in an issue before implementation begins.

This helps avoid spending significant effort on a change that may not fit the direction or scope of TraceTK.
