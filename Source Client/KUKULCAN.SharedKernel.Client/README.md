# KUKULCAN.SharedKernel.Client

`KUKULCAN.SharedKernel.Client` is an interactive .NET 10 console application
designed to exercise the public API of `KUKULCAN.SharedKernel`.

## Purpose

The client is intentionally different from the NUnit test project.

- NUnit tests verify expected behaviour automatically.
- This console application provides a manual, observable way to exercise the
  library.
- Each menu option maps to a functional module of the SharedKernel.
- The **Public API inventory** option enumerates exported public types and
  their declared public members so the client can also be used as an API
  inspection aid.

The client does not access `Internals` types directly and does not modify the
SharedKernel implementation.

## Requirements

- .NET 10 SDK
- The `KUKULCAN.SharedKernel` project included in this repository

## Run

From the solution directory:

```bash
dotnet restore
dotnet build
dotnet run --project KUKULCAN.SharedKernel.Client/KUKULCAN.SharedKernel.Client.csproj
```

## Menu

The application exposes demonstrations for:

1. Guards
2. Semantic Versioning
3. Attributes
4. Collections
5. Identifiers
6. Maybe
7. Results and Errors
8. Validations
8E. Exceptions
9. Domain
10. Specifications
11. Domain Events
12. Time
13. Globalization
14. Abstractions and Capabilities
15. Public API inventory

`A` runs all demonstrations sequentially.

## Design criteria

The client follows the same architectural discipline used for the SharedKernel
tests:

1. Exercise real public behaviour.
2. Avoid fake behaviour that does not exist in the library.
3. Use small local implementations only where the SharedKernel exposes an
   abstraction or abstract base class and a concrete application type is
   required to exercise that contract.
4. Do not expose or depend on implementation details from `Internals`.
5. Keep the client deterministic wherever practical.

The local implementations of identifiers, specifications, domain events,
localizers, and capabilities exist solely as consumers of the public API.
They represent the way an application would normally consume these contracts.

## Important note

This is a **manual client**, not a replacement for the NUnit test suite.
The test project remains the authoritative automated regression suite.
