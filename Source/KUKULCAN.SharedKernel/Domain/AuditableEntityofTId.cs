using System;
using KUKULCAN.SharedKernel.Abstractions.Capabilities;
using KUKULCAN.SharedKernel.Identifiers.Interfaces;

namespace KUKULCAN.SharedKernel.Domain;

/// <summary>
/// Represents an auditable aggregate root.
/// </summary>
/// <typeparam name="TId">
/// Type of the aggregate identifier.
/// </typeparam>
public abstract class AuditableEntity<TId> : AggregateRoot<TId>, IAuditable where TId : IEntityId
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuditableEntity{TId}"/> class.
    /// This constructor is intended only for Entity Framework Core.
    /// </summary>
    protected AuditableEntity()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuditableEntity{TId}"/> class.
    /// </summary>
    /// <param name="id">
    /// Aggregate identifier.
    /// </param>
    protected AuditableEntity(TId id) : base(id)
    {
    }

    /// <inheritdoc/>
    public DateTimeOffset CreatedOn { get; internal set; }

    /// <inheritdoc/>
    public DateTimeOffset? ModifiedOn { get; internal set; }
}
