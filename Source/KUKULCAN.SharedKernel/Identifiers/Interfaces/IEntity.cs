namespace KUKULCAN.SharedKernel.Identifiers.Interfaces;

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
