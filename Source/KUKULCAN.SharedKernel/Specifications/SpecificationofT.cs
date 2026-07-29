using System;

namespace KUKULCAN.SharedKernel.Specifications;

/// <summary>
/// Represents the base class for all business specifications.
/// </summary>
/// <typeparam name="T">
/// Type evaluated by the specification.
/// </typeparam>
public abstract class Specification<T> : ISpecification<T>
{
    /// <summary>
    /// Gets the expression that defines the specification.
    /// </summary>
    public abstract Expression<Func<T, bool>> Criteria
    {
        get;
    }

    /// <summary>
    /// Combines this specification with another using a logical AND.
    /// </summary>
    /// <param name="other">
    /// Specification to combine.
    /// </param>
    /// <returns>
    /// A combined specification.
    /// </returns>
    public Specification<T> And(Specification<T> other)
    {
        ArgumentNullException.ThrowIfNull(other);

        return new AndSpecification<T>(this, other);
    }

    /// <summary>
    /// Combines this specification with another using a logical OR.
    /// </summary>
    /// <param name="other">
    /// Specification to combine.
    /// </param>
    /// <returns>
    /// A combined specification.
    /// </returns>
    public Specification<T> Or(Specification<T> other)
    {
        ArgumentNullException.ThrowIfNull(other);

        return new OrSpecification<T>(this, other);
    }

    /// <summary>
    /// Creates the logical negation of this specification.
    /// </summary>
    /// <returns>
    /// A negated specification.
    /// </returns>
    public Specification<T> Not()
    {
        return new NotSpecification<T>(this);
    }

    /// <summary>
    /// Combines two specifications using a logical AND.
    /// </summary>
    public static Specification<T> operator &(Specification<T> left, Specification<T> right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        return left.And(right);
    }

    /// <summary>
    /// Combines two specifications using a logical OR.
    /// </summary>
    public static Specification<T> operator |(Specification<T> left, Specification<T> right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        return left.Or(right);
    }

    /// <summary>
    /// Negates a specification.
    /// </summary>
    public static Specification<T> operator !(Specification<T> specification)
    {
        ArgumentNullException.ThrowIfNull(specification);

        return specification.Not();
    }
}
