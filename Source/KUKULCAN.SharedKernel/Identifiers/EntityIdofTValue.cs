using System;
using KUKULCAN.SharedKernel.Identifiers.Interfaces;

namespace KUKULCAN.SharedKernel.Identifiers;

/// <summary>
/// Represents a strongly typed entity identifier.
/// </summary>
/// <typeparam name="TValue">
/// Type of the underlying identifier value.
/// </typeparam>
public abstract class EntityId<TValue> : IEntityId<TValue>, IEquatable<EntityId<TValue>> where TValue : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityId{TValue}"/> class.
    /// This constructor is intended only for Entity Framework Core materialization.
    /// </summary>
    protected EntityId()
    {
        Value = default!;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EntityId{TValue}"/> class.
    /// </summary>
    /// <param name="value">
    /// Underlying identifier value.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="value"/> is <see langword="null"/>.
    /// </exception>
    protected EntityId(TValue value)
    {
        ArgumentNullException.ThrowIfNull(value);

        Value = value;
    }

    /// <summary>
    /// Gets the underlying identifier value.
    /// </summary>
    public TValue Value { get; protected set; }

    /// <inheritdoc/>
    object IEntityId.Value => Value;

    /// <inheritdoc/>
    public bool Equals(EntityId<TValue>? other)
    {
        if (other is null)
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return GetType() == other.GetType() && EqualityComparer<TValue>.Default.Equals(Value, other.Value);
    }

    /// <inheritdoc/>
    public bool Equals(IEntityId<TValue>? other)
    {
        return other is EntityId<TValue> entityId &&
               Equals(entityId);
    }

    /// <inheritdoc/>
    public bool Equals(IEntityId? other)
    {
        return other is EntityId<TValue> entityId &&
               Equals(entityId);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj switch
        {
            EntityId<TValue> entityId => Equals(entityId),
            IEntityId<TValue> interfaceId => Equals(interfaceId),
            IEntityId interfaceId => Equals(interfaceId),
            _ => false
        };
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(GetType(), Value);
    }

    /// <summary>
    /// Determines whether two identifiers are equal.
    /// </summary>
    /// <param name="left">
    /// Left identifier.
    /// </param>
    /// <param name="right">
    /// Right identifier.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if both identifiers are equal; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public static bool operator ==(EntityId<TValue>? left, EntityId<TValue>? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines whether two identifiers are different.
    /// </summary>
    /// <param name="left">
    /// Left identifier.
    /// </param>
    /// <param name="right">
    /// Right identifier.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if both identifiers are different; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public static bool operator !=(EntityId<TValue>? left, EntityId<TValue>? right)
    {
        return !(left == right);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return Value.ToString() ?? string.Empty;
    }
}
