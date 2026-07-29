using KUKULCAN.SharedKernel.DomainEvents;

namespace KUKULCAN.SharedKernel.Abstractions.Capabilities;

/// <summary>
/// Represents an object that stores domain events.
/// </summary>
public interface IHasDomainEvents
{
    /// <summary>
    /// Gets the pending domain events.
    /// </summary>
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    /// <summary>
    /// Removes all pending domain events.
    /// </summary>
    void ClearDomainEvents();
}
