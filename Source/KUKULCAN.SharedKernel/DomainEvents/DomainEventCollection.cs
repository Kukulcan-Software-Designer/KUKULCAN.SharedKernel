using System;

namespace KUKULCAN.SharedKernel.DomainEvents;

/// <summary>
/// Represents a specialized collection of domain events.
/// </summary>
public sealed class DomainEventCollection
{
    private readonly List<IDomainEvent> _events = [];

    /// <summary>
    /// Gets an immutable view of the domain events.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> Items =>
        new ReadOnlyCollection<IDomainEvent>(_events);

    /// <summary>
    /// Gets a value indicating whether the collection is empty.
    /// </summary>
    public bool IsEmpty => _events.Count == 0;

    /// <summary>
    /// Gets the number of events.
    /// </summary>
    public int Count => _events.Count;

    /// <summary>
    /// Adds a domain event.
    /// </summary>
    public void Add(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        _events.Add(domainEvent);
    }

    /// <summary>
    /// Removes a domain event.
    /// </summary>
    public bool Remove(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        return _events.Remove(domainEvent);
    }

    /// <summary>
    /// Removes all events.
    /// </summary>
    public void Clear()
    {
        _events.Clear();
    }

    /// <summary>
    /// Returns the pending events and clears the collection.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> Dequeue()
    {
        if (_events.Count == 0)
        {
            return Array.Empty<IDomainEvent>();
        }

        IDomainEvent[] snapshot = _events.ToArray();

        _events.Clear();

        return snapshot;
    }
}
