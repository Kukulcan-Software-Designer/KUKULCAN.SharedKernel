# KUKULCAN.SharedKernel.UnitTests

## 1. Purpose

`KUKULCAN.SharedKernel.UnitTests` is the unit test project for the `KUKULCAN.SharedKernel` library.

Its purpose is not merely to verify that the code "works", but to provide an **architectural safety net** for the Shared Kernel:

- verify the observable behavior of public classes;
- protect established contracts and business rules;
- detect regressions;
- document expected behavior through executable tests;
- verify edge cases and error contracts;
- preserve architectural decisions as the project evolves.

The project targets **.NET 10** and uses **NUnit 4** as its primary testing framework.

---

## 2. Testing Philosophy

Throughout the development of `KUKULCAN.SharedKernel`, one fundamental rule has been established:

> **Tests must follow the real implementation and its real behavior, not an imagined architecture.**

Before creating a test suite for a module:

1. Audit the existing implementation.
2. Review the tests that already exist.
3. Identify observable behavior that is genuinely not covered.
4. Add only the tests that are actually necessary.
5. Execute the tests.
6. Resolve discrepancies between implementation and contract.
7. Once the module is completely green, declare it **frozen**.

Once a module is frozen, its tests become an important part of its contract. They must not be modified merely to accommodate an implementation change.

A frozen module should only be modified when there is a **real effect**, defect, incompatibility, or justified architectural decision.

---

## 3. Why This Strategy Was Chosen

### 3.1. Avoiding Artificial Tests

Not every type or interface necessarily requires its own test suite.

For example, an `internal` class may be completely covered through the public behavior that consumes it. Creating a dedicated test suite solely to increase the number of tests provides little value.

Therefore, no artificial suites are created for internal implementation details such as:

- `ParameterReplacer`;
- `ExpressionCombiner`;
- internal structural comparison components;
- internal caches;
- internal helpers that do not represent an independent public contract.

Their behavior is verified indirectly through the public API that uses them.

### 3.2. Protecting Behavior, Not Implementation

Tests should not be unnecessarily coupled to implementation details.

For example, in `Specifications`, there is no need to test directly how `ParameterReplacer` substitutes expression parameters.

What matters is that:

```csharp
left.And(right)
```

produces a correct and evaluable expression even when both expressions were created using different parameters.

The internal mechanism is therefore covered as a consequence of testing the public behavior.

### 3.3. Avoiding False Contracts

A test must not require behavior that the contract does not guarantee.

An important example appeared in the `ValueObject` tests:

> Two different objects may legitimately produce the same hash code.

Therefore, the tests do not require different values to have different hash codes.

The correct contract is:

```text
Equal objects
    =>
Equal hash codes
```

but not:

```text
Different objects
    =>
Different hash codes
```

This principle applies throughout the project:

> **Tests verify what must be true, not what normally happens.**

---

## 4. Test Project Architecture

The test project is organized around shared infrastructure, helpers, assertions, and module-specific behavioral suites.

```text
KUKULCAN.SharedKernel.UnitTests/
│
├── Infrastructure/
│   ├── TestBase.cs
│   ├── EntityTestBase.cs
│   ├── ValueObjectTestBase.cs
│   ├── ContractTestBase.cs
│   ├── ParserTestBase.cs
│   ├── ComparerTestBase.cs
│   ├── ThreadSafetyTestBase.cs
│   └── SerializationTestBase.cs
│
├── Helpers/
│   ├── ReflectionHelper.*
│   ├── AssertionHelper.cs
│   ├── EqualityHelper.cs
│   ├── HashCodeHelper.cs
│   ├── SerializationHelper.cs
│   ├── RandomDataHelper.cs
│   └── ThreadHelper.cs
│
├── Assertions/
│   └── TypeAssertions.*
│
├── Statistics/
│   └── Testing statistics infrastructure
│
├── Results/
├── Validations/
├── Exceptions/
├── Guards/
├── Identifiers/
├── Maybe/
├── Time/
├── Domain/
├── DomainEvents/
├── Collections/
├── Specifications/
├── Globalization/
├── Attributes/
└── Versioning/
```

The test project mirrors the conceptual organization of the Shared Kernel without unnecessarily reproducing its internal implementation structure.

---

## 5. Test Infrastructure

### `Infrastructure`

`TestBase` provides common services used by multiple test suites.

These include:

- `AutoFixture`;
- `AutoMoq`;
- object creation;
- collection creation;
- dependency freezing;
- mock creation;
- `ReflectionHelper`;
- `SerializationHelper`;
- `RandomDataHelper`;
- `EqualityHelper`;
- `HashCodeHelper`;
- `ThreadHelper`;
- `AssertionHelper`.

The responsibility of `TestBase` is intentionally kept small.

Domain-specific rules are not placed in this class. When a family of tests requires specialized behavior, a more specific base class is used.

This prevents `TestBase` from becoming a monolithic testing utility.

---

## 6. Helpers

### `ReflectionHelper`

Centralizes reflection operations used by multiple test suites.

It is used when tests need to inspect:

- types;
- constructors;
- members;
- parameters;
- inheritance hierarchies;
- metadata;
- public signatures.

Keeping reflection logic in one place prevents unnecessary duplication.

### `AssertionHelper`

Groups complex or repetitive assertions that are shared by multiple suites.

### `EqualityHelper`

Provides common support for verifying equality contracts without duplicating comparison logic.

### `HashCodeHelper`

Centralizes hash-code-related checks.

Its purpose is especially important for preserving the correct equality/hash-code contract:

```text
Equals(a, b) == true
        =>
a.GetHashCode() == b.GetHashCode()
```

It does not incorrectly require different objects to produce different hash codes.

### `SerializationHelper`

Provides shared support for tests that require serialization.

### `RandomDataHelper`

Provides variable test data where appropriate, preventing certain tests from depending exclusively on hard-coded values.

### `ThreadHelper`

Provides shared support for concurrency and thread-safety scenarios.

---

## 7. Framework and Tooling

The project uses:

| Technology             | Purpose                        |
|------------------------|--------------------------------|
| .NET 10                | Target framework               |
| NUnit 4                | Test framework                 |
| NUnit3TestAdapter      | Test runner integration        |
| NUnit.Analyzers        | NUnit-specific static analysis |
| Microsoft.NET.Test.Sdk | Test execution infrastructure  |
| AutoFixture            | Test data generation           |
| AutoFixture.AutoMoq    | AutoFixture + Moq integration  |
| Moq                    | Mocking                        |
| FluentAssertions       | Complementary assertions       |
| coverlet.collector     | Code coverage                  |

The project has nullable reference types enabled:

```xml
<Nullable>enable</Nullable>
```

and implicit usings enabled:

```xml
<ImplicitUsings>enable</ImplicitUsings>
```

The project also uses:

```xml
<LangVersion>latest</LangVersion>
```

to take advantage of current C# compiler capabilities compatible with .NET 10.

---

## 8. Tested Modules

### `Results`

Tests the Shared Kernel result system.

Coverage includes:

- `Error`;
- `Result`;
- `Result<T>`;
- `CommonErrors`;
- `CommonErrorCodes`;
- `ValidationErrors`.

The tests verify:

- successful and failed results;
- value presence and absence;
- errors;
- error codes;
- metadata;
- equality;
- result combinations;
- conversions;
- extension methods;
- error contracts.

#### Architectural Decision

Error text is not duplicated arbitrarily throughout the tests.

Tests respect the error codes and contracts defined by the Shared Kernel.

---

### `Validations`

Tests the validation subsystem.

Coverage includes:

- `ValidationResult`;
- `ValidationFailure`;
- `ValidatorSeverity`;
- validation extensions;
- validation throwing extensions.

The tests verify:

- validation failures;
- severity;
- error collections;
- conversions;
- extensions;
- exception throwing;
- valid and invalid input behavior.

---

### `Exceptions`

Tests the Shared Kernel exception hierarchy.

Coverage includes:

- `SharedKernelException`;
- `ValidationException`;
- `DomainException`;
- `NotFoundException`;
- `ConcurrencyException`;
- `UnauthorizedException`;
- `ForbiddenException`;
- `ConflictException`;
- `UnexpectedException`.

The tests protect:

- hierarchy;
- messages;
- error codes;
- inner exceptions;
- arguments;
- specialized behavior.

---

### `Guards`

Tests the precondition checks provided by `Guard`.

The purpose is to verify that invalid arguments are correctly rejected while valid values are accepted.

The tests cover scenarios such as:

- `null`;
- empty strings;
- whitespace;
- invalid numeric values;
- ranges;
- boolean conditions.

---

### `Identifiers`

Tests the Shared Kernel identifier system.

Coverage includes:

- `EntityId<TValue>`;
- `GuidEntityId`;
- `IntEntityId`;
- `LongEntityId`;
- `StringEntityId`.

The tests verify:

- construction;
- validation;
- equality;
- hash codes;
- string representation;
- valid and invalid values;
- `IEntityId` contracts.

#### Architectural Decision

Identifiers are treated as domain concepts rather than simple primitive values.

Therefore, their invariants and equality semantics are explicitly protected by tests.

---

### `Maybe`

Tests `Maybe<T>` and its optional-value semantics.

Coverage includes:

- value presence;
- value absence;
- creation;
- transformations;
- access;
- equality;
- conversions;
- supported functional operations.

The purpose is to ensure that `Maybe<T>` behaves as a proper optional-value abstraction rather than merely acting as a nullable container.

---

### `Time`

Tests the Shared Kernel time subsystem.

Coverage includes:

- `SystemClock`;
- `FakeClock`;
- `ClockExtensions`;
- `DateTimeExtensions`.

The tests verify:

- UTC handling;
- dates;
- times;
- instant comparison;
- offsets;
- current-day behavior;
- weekend detection;
- clock advancement;
- clock rewind;
- deterministic scenarios through `FakeClock`.

#### Architectural Decision

Time is abstracted through a clock to prevent tests from depending directly on the system clock.

`FakeClock` allows temporal scenarios to be reproduced deterministically.

This module also explicitly distinguishes:

> local date/time ≠ UTC instant.

---

### `Domain`

Tests the core domain primitives.

Coverage includes:

- `ValueObject`;
- `Entity<TId>`;
- `AuditableEntity<TId>`;
- `AggregateRoot<TId>`;
- `Enumeration`.

#### `ValueObject`

Tests cover:

- structural equality;
- equality members;
- member ordering;
- ignored members;
- hash codes;
- different values;
- representation.

A critical architectural decision is that different values are **not required to produce different hash codes**.

#### `Entity`

Tests cover:

- identity;
- equality;
- identifier behavior;
- hash codes;
- different entity types.

#### `AuditableEntity`

Tests cover:

- `CreatedOn`;
- `ModifiedOn`;
- audit rules;
- integration with entity identity.

#### `AggregateRoot`

Tests cover:

- identity;
- domain events;
- event registration;
- event removal;
- event extraction.

#### `Enumeration`

Tests cover:

- identifier;
- name;
- validation;
- `GetAll<T>()`;
- ordering;
- caching;
- equality;
- comparison;
- operators;
- `ToString`.

---

## 9. `DomainEvents`

The `DomainEvents` module is **closed and frozen**.

Coverage includes the public domain-event infrastructure and its contracts.

Tests cover:

- creation;
- occurrence timestamps;
- event storage;
- insertion order;
- registration;
- removal;
- extraction;
- clearing;
- dispatch;
- handlers;
- multiple events;
- error conditions;
- abstraction contracts.

### Architectural Decision

No artificial suites are created for interfaces that contain no independent behavior.

Domain event abstractions are tested through their real implementations and consumers.

This keeps the tests focused on behavior rather than type existence.

---

## 10. `Collections`

The `Collections` module is **closed and frozen**.

Coverage includes:

- `PageRequest`;
- `Page<T>`;
- `PagedList<T>`.

Tests verify:

- page number;
- page size;
- boundaries;
- total item count;
- total page count;
- first/last page;
- previous/next page;
- empty collections;
- items;
- validation.

### Architectural Decision

Pagination is treated as a reusable cross-cutting concern independent of a specific domain model.

---

## 11. `Specifications`

The `Specifications` module is **closed and frozen**.

Coverage includes:

- `Specification<T>`;
- `AndSpecification<T>`;
- `OrSpecification<T>`;
- `NotSpecification<T>`.

The following internal components are covered indirectly:

- `ParameterReplacer`;
- `ExpressionCombiner`.

Tests verify:

- specification criteria;
- evaluation;
- AND composition;
- OR composition;
- NOT composition;
- `&` operator;
- `|` operator;
- `!` operator;
- nested composition;
- expressions created with different parameters;
- null validation;
- preservation of left/right specifications;
- preservation of negated specifications.

### Architectural Decision

No independent `ParameterReplacerTests` or `ExpressionCombinerTests` suites are created.

These are internal implementation components whose relevant behavior is observable through the public specification API.

This keeps the tests contract-oriented and reduces coupling to implementation details.

---

## 12. `Globalization`

The production `Globalization` module contains abstractions and models related to internationalization.

Depending on the final implementation, this includes concepts such as:

- `ITextLocalizer`;
- `ILocalizationProvider`;
- `ICurrentCultureProvider`;
- `LocalizedString`;
- `SupportedCulture`.

Its test suite is introduced only after auditing the actual implementation.

The intended coverage focuses on real behavior such as:

- culture;
- localization;
- localized text representation;
- current culture;
- supported cultures;
- model behavior;
- contracts implemented by actual production classes.

No artificial tests are created for interfaces that have no behavior of their own.

---

## 13. `Attributes`

The Shared Kernel contains attributes such as:

- `IgnoreEqualityAttribute`;
- `EqualityOrderAttribute`;
- `ValueObjectMemberAttribute`.

These attributes configure `ValueObject` behavior.

Coverage should therefore focus on their **observable effect** rather than simply verifying that the attributes can be instantiated.

For example, it is more valuable to verify that a member marked with `IgnoreEqualityAttribute` is excluded from equality than merely verifying that the attribute exists.

---

## 14. `Versioning`

The versioning module contains:

```text
SemanticVersion
```

Its test suite should cover the actual versioning behavior:

- construction;
- parsing;
- comparison;
- equality;
- precedence;
- string representation;
- invalid versions;
- version components.

Only behavior guaranteed by the real implementation should be tested.

---

## 15. Internal Components

Internal Shared Kernel components do not automatically receive independent test suites.

Examples include:

```text
Internals/
├── Equality/
├── ValueObjects/
└── Domain/Internals
```

The architectural rule is:

> **If an internal component exists exclusively to support a public API, its behavior should normally be tested through that public API.**

A direct test suite is justified only if an internal component eventually acquires an independent contract that makes isolated testing valuable.

This allows internal refactoring without unnecessarily rewriting tests.

---

## 16. Contract Tests

Infrastructure classes such as:

- `EntityTestBase`;
- `ValueObjectTestBase`;
- `ContractTestBase`;
- `ComparerTestBase`;
- `ParserTestBase`;
- `SerializationTestBase`;
- `ThreadSafetyTestBase`;

allow common testing patterns to be reused.

Their purpose is not to become independent test suites.

Instead, they provide reusable **behavioral templates** for families of types that share the same contract.

---

## 17. Reflection and Architectural Tests

`ReflectionHelper` and type assertions allow the project to verify aspects that would otherwise be repetitive or difficult to test directly.

These include:

- constructors;
- visibility;
- inheritance;
- interfaces;
- public members;
- attributes;
- signatures;
- metadata.

This is particularly important for a Shared Kernel because a seemingly small change to a public signature may affect every consumer.

---

## 18. Nullability

The test project uses nullable reference types:

```xml
<Nullable>enable</Nullable>
```

Therefore, tests also participate in protecting correct nullability contracts.

Warnings are not simply suppressed to achieve a clean build.

When a warning such as:

```text
Possible null reference return
```

appears, the first step is to determine whether:

1. the production contract is incorrect;
2. the test is creating an impossible scenario;
3. the nullable contract has not been expressed correctly.

Only after that analysis should the appropriate correction be made.

---

## 19. NUnit Analyzers

The project uses `NUnit.Analyzers`.

These analyzers have been particularly valuable for detecting tests that compile but express impossible or meaningless expectations.

For example:

```text
NUnit2020
```

can detect a `SameAs` assertion whose actual and expected types make the assertion impossible to satisfy.

Such diagnostics are considered part of test quality, not merely cosmetic warnings.

---

## 20. Test Execution

Run all tests with:

```bash
dotnet test
```

Run a specific suite with:

```bash
dotnet test --filter "FullyQualifiedName~SpecificationTests"
```

Examples:

```bash
dotnet test --filter "FullyQualifiedName~EnumerationTests"
dotnet test --filter "FullyQualifiedName~AggregateRootTests"
dotnet test --filter "FullyQualifiedName~DomainEventCollectionTests"
dotnet test --filter "FullyQualifiedName~Collections"
```

A successful test run should complete without test failures and without analyzer diagnostics that indicate an actual defect in the tests.

---

## 21. Module Freezing Criteria

A module is considered **frozen** when:

- the implementation has been audited;
- existing tests have been reviewed;
- all necessary coverage has been added;
- no known implementation/test discrepancies remain;
- the complete test suite is green;
- analyzers report no conceptual problems in the tests;
- no relevant public behavior remains uncovered without justification.

The process is:

```text
IMPLEMENTATION
      ↓
    AUDIT
      ↓
    TESTS
      ↓
 ALL GREEN
      ↓
   FROZEN
```

Once frozen, subsequent modifications require a real justification.

---

## 22. What This Project Is NOT Intended to Do

This project is **not intended** to:

- artificially increase code coverage percentages;
- test every private line of code;
- test empty interfaces directly;
- duplicate tests for internal implementation details;
- verify implementation details that have no contractual value;
- accept any behavior simply because it is the current implementation;
- turn the tests into a second implementation of the Shared Kernel.

Its purpose is to protect:

> **contracts, invariants, and observable behavior.**

---

## 23. Current Module Status

The freezing process has progressed through the following modules:

| Module           | Status           |
|------------------|------------------|
| `Time`           | 🟢 Frozen        |
| `ValueObjects`   | 🟢 Frozen        |
| `Domain`         | 🟢 Frozen        |
| `Enumeration`    | 🟢 Frozen        |
| `AggregateRoot`  | 🟢 Frozen        |
| `DomainEvents`   | 🟢 Frozen        |
| `Collections`    | 🟢 Frozen        |
| `Specifications` | 🟢 Frozen        |
| `Globalization`  | 🟡 Pending audit |
| `Attributes`     | 🟡 Pending       |
| `Versioning`     | 🟡 Pending       |

This table should only be updated when the corresponding module has completed the full audit, coverage, and validation process.

---

## 24. Core Project Rule

The entire testing strategy can be summarized by one rule:

> **We do not write tests to increase the number of tests. We write tests to protect behavior that actually matters.**

This approach allows the `KUKULCAN.SharedKernel.UnitTests` suite to grow together with the Shared Kernel without becoming a maintenance burden or an obstacle to architectural evolution.
