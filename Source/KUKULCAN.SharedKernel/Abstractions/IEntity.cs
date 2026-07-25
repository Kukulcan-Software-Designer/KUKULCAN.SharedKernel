using KUKULCAN.SharedKernel.Identifiers.Interfaces;

namespace KUKULCAN.SharedKernel.Abstractions;

/// <summary>
/// Represents an entity.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets the entity identifier.
    /// </summary>
    IEntityId Id { get; }
}
