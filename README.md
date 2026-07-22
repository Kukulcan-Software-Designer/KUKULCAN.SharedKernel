# KUKULCAN.SharedKernel

Shared abstractions used by the entire KUKULCAN platform.

## Goals

- Zero business logic.
- Zero infrastructure dependencies.
- Immutable primitives.
- Functional Result pattern.
- Localization-ready errors.
- High testability.
- .NET 10.

## Main namespaces

- Results
- Guards
- Validation
- Time
- Collections
- Maybe
- Identifiers
- Abstractions

This project must not reference Entity Framework, ASP.NET Core, MediatR or FluentValidation.
