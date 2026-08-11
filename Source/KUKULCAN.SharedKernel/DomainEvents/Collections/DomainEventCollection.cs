using KUKULCAN.SharedKernel.DomainEvents.Abstractions;

namespace KUKULCAN.SharedKernel.DomainEvents.Collections;

/// <summary>
/// Represents a specialized collection of domain events.
/// </summary>
public sealed class DomainEventCollection : IEnumerable<IDomainEvent>
{
    private readonly List<IDomainEvent> _events = [];

    /// <summary>
    /// Gets an immutable view of the domain events.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> Items => new ReadOnlyCollection<IDomainEvent>(_events);

    /// <summary>
    /// Gets a value indicating whether the collection is empty.
    /// </summary>
    public bool IsEmpty => _events.Count == 0;

    /// <summary>
    /// Gets the number of domain events in the collection.
    /// </summary>
    public int Count => _events.Count;

    /// <summary>
    /// Adds a domain event to the collection.
    /// </summary>
    /// <param name="domainEvent">
    /// Domain event.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="domainEvent"/> is <see langword="null"/>.
    /// </exception>
    public void Add(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        _events.Add(domainEvent);
    }

    /// <summary>
    /// Removes a domain event from the collection.
    /// </summary>
    /// <param name="domainEvent">
    /// Domain event.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the event was removed; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="domainEvent"/> is <see langword="null"/>.
    /// </exception>
    public bool Remove(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        return _events.Remove(domainEvent);
    }

    /// <summary>
    /// Removes all domain events from the collection.
    /// </summary>
    public void Clear()
    {
        _events.Clear();
    }

    /// <summary>
    /// Returns a snapshot of the current domain events and clears the collection.
    /// </summary>
    /// <returns>
    /// An immutable snapshot containing the pending domain events.
    /// </returns>
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

    /// <inheritdoc/>
    public IEnumerator<IDomainEvent> GetEnumerator()
    {
        return _events.GetEnumerator();
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
