using KUKULCAN.SharedKernel.Identifiers.Interfaces;

namespace KUKULCAN.SharedKernel.Abstractions;

/// <summary>
/// Represents an entity with a strongly typed identifier.
/// </summary>
/// <typeparam name="TId">
/// Type of the entity identifier.
/// </typeparam>
public interface IEntity<TId> : IEntity where TId : IEntityId
{
    /// <summary>
    /// Gets the entity identifier.
    /// </summary>
    new TId Id { get; }
}
