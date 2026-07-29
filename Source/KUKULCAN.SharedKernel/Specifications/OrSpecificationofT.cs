using System;
using KUKULCAN.SharedKernel.Specifications.Internals;

namespace KUKULCAN.SharedKernel.Specifications;

/// <summary>
/// Represents the logical disjunction of two specifications.
/// </summary>
/// <typeparam name="T">
/// Type evaluated by the specification.
/// </typeparam>
public sealed class OrSpecification<T> : Specification<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrSpecification{T}"/> class.
    /// </summary>
    /// <param name="left">
    /// Left specification.
    /// </param>
    /// <param name="right">
    /// Right specification.
    /// </param>
    public OrSpecification(Specification<T> left, Specification<T> right)
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
    public override Expression<Func<T, bool>> Criteria => ExpressionCombiner.Or(Left.Criteria, Right.Criteria);
}
