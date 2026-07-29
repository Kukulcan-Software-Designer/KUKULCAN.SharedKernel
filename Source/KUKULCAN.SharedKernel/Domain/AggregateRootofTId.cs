using System;
using KUKULCAN.SharedKernel.Abstractions;
using KUKULCAN.SharedKernel.Abstractions.Capabilities;
using KUKULCAN.SharedKernel.DomainEvents;
using KUKULCAN.SharedKernel.Identifiers.Interfaces;

namespace KUKULCAN.SharedKernel.Domain;

/// <summary>
/// Represents the base class for aggregate roots.
/// </summary>
/// <typeparam name="TId">
/// Type of the aggregate identifier.
/// </typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot, IHasDomainEvents where TId : IEntityId
{
    private readonly DomainEventCollection _domainEvents = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class.
    /// This constructor is intended only for serializers and ORMs such as Entity Framework Core.
    /// </summary>
    protected AggregateRoot()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class.
    /// </summary>
    /// <param name="id">
    /// Aggregate identifier.
    /// </param>
    protected AggregateRoot(TId id) : base(id)
    {
    }

    /// <inheritdoc/>
    public IReadOnlyCollection<IDomainEvent> DomainEvents =>
        _domainEvents.Items;

    /// <summary>
    /// Registers a domain event.
    /// </summary>
    /// <param name="domainEvent">
    /// Domain event to register.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="domainEvent"/> is <see langword="null"/>.
    /// </exception>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Removes a previously registered domain event.
    /// </summary>
    /// <param name="domainEvent">
    /// Domain event to remove.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="domainEvent"/> is <see langword="null"/>.
    /// </exception>
    protected void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull(domainEvent);

        _domainEvents.Remove(domainEvent);
    }

    /// <inheritdoc/>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    /// <summary>
    /// Returns the pending domain events and clears the internal collection.
    /// </summary>
    /// <remarks>
    /// This method is intended to be used by infrastructure components
    /// responsible for publishing domain events.
    /// </remarks>
    /// <returns>
    /// A snapshot containing the pending domain events.
    /// </returns>
    protected IReadOnlyCollection<IDomainEvent> DequeueDomainEvents()
    {
        return _domainEvents.Dequeue();
    }
}
