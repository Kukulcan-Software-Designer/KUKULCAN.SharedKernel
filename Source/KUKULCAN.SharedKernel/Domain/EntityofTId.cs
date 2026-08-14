using KUKULCAN.SharedKernel.Abstractions;
using KUKULCAN.SharedKernel.Identifiers.Interfaces;

namespace KUKULCAN.SharedKernel.Domain;

/// <summary>
/// Represents an entity.
/// </summary>
/// <typeparam name="TId">
/// Type of the entity identifier.
/// </typeparam>
public abstract class Entity<TId> : IEntity<TId>, IEquatable<Entity<TId>> where TId : IEntityId
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class.
    /// This constructor is intended only for Entity Framework Core.
    /// </summary>
    protected Entity()
    {
        Id = default!;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class.
    /// </summary>
    /// <param name="id">
    /// Entity identifier.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="id"/> is <see langword="null"/>.
    /// </exception>
    protected Entity(TId id)
    {
        ArgumentNullException.ThrowIfNull(id);

        Id = id;
    }

    /// <inheritdoc/>
    public TId Id { get; protected set; }

    /// <inheritdoc/>
    IEntityId IEntity.Id => Id;

    /// <inheritdoc/>
    public bool Equals(Entity<TId>? other)
    {
        if (other is null)
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return GetType() == other.GetType() && EqualityComparer<TId>.Default.Equals(Id, other.Id);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Equals(entity);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(GetType(), Id);
    }

    /// <summary>
    /// Determines whether two entities are equal.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns>
    /// <see langword="true"/> when both entities are equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines whether two entities are different.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns>
    /// <see langword="true"/> when the entities are different; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !(left == right);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{GetType().Name} [{Id}]";
    }
}
