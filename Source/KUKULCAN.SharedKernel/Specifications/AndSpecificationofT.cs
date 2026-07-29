using System;
using KUKULCAN.SharedKernel.Specifications.Internals;

namespace KUKULCAN.SharedKernel.Specifications;

/// <summary>
/// Represents the logical conjunction of two specifications.
/// </summary>
/// <typeparam name="T">
/// Type evaluated by the specification.
/// </typeparam>
public sealed class AndSpecification<T> : Specification<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AndSpecification{T}"/> class.
    /// </summary>
    /// <param name="left">
    /// Left specification.
    /// </param>
    /// <param name="right">
    /// Right specification.
    /// </param>
    public AndSpecification(Specification<T> left, Specification<T> right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        Left = left;
        Right = right;
    }

    /// <summary>
    /// Gets the left specification.
    /// </summary>
    public Specification<T> Left { get; }

    /// <summary>
    /// Gets the right specification.
    /// </summary>
    public Specification<T> Right { get; }

    /// <inheritdoc />
    public override Expression<Func<T, bool>> Criteria => ExpressionCombiner.And(Left.Criteria, Right.Criteria);
}
