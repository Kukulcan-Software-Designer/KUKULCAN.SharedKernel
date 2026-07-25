using System;
using KUKULCAN.SharedKernel.Domain.Internals;

namespace KUKULCAN.SharedKernel.Domain;

/// <summary>
/// Represents a value object.
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValueObject"/> class.
    /// This constructor is intended only for Entity Framework Core.
    /// </summary>
    protected ValueObject()
    {
    }

    /// <summary>
    /// Returns the components that participate in equality.
    /// </summary>
    /// <returns>
    /// A sequence containing all components that define the value object.
    /// </returns>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <inheritdoc/>
    public bool Equals(ValueObject? other)
    {
        if (other is null)
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        if (GetType() != other.GetType())
        {
            return false;
        }

        return StructuralEqualityComparer.Equals(GetEqualityComponents(), other.GetEqualityComponents());
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is ValueObject other &&
               Equals(other);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return StructuralEqualityComparer.GetHashCode(GetType(), GetEqualityComponents());
    }

    /// <summary>
    /// Determines whether two value objects are equal.
    /// </summary>
    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines whether two value objects are different.
    /// </summary>
    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return StructuralEqualityComparer.Format(GetType(), GetEqualityComponents());
    }
}
