using System;
using KUKULCAN.SharedKernel.Abstractions;
using KUKULCAN.SharedKernel.Abstractions.Capabilities;
using KUKULCAN.SharedKernel.Identifiers.Interfaces;

namespace KUKULCAN.SharedKernel.Domain;

/// <summary>
/// Represents an aggregate root.
/// </summary>
/// <typeparam name="TId">
/// Type of the aggregate identifier.
/// </typeparam>
public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot, IHasDomainEvents where TId : IEntityId
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class.
    /// This constructor is intended only for Entity Framework Core.
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
        _domainEvents.AsReadOnly();

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

    /// <summary>
    /// Removes all registered domain events.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
