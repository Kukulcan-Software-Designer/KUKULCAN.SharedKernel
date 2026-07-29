using System;

namespace KUKULCAN.SharedKernel.DomainEvents.Abstractions;

/// <summary>
/// Represents a domain event.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Gets the instant when the domain event occurred.
    /// </summary>
    DateTimeOffset OccurredOn { get; }
}
