using System;
using KUKULCAN.SharedKernel.Specifications.Internals;

namespace KUKULCAN.SharedKernel.Specifications;

/// <summary>
/// Represents the logical negation of a specification.
/// </summary>
/// <typeparam name="T">
/// Type evaluated by the specification.
/// </typeparam>
public sealed class NotSpecification<T> : Specification<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotSpecification{T}"/> class.
    /// </summary>
    /// <param name="specification">
    /// Specification to negate.
    /// </param>
    public NotSpecification(Specification<T> specification)
    {
        ArgumentNullException.ThrowIfNull(specification);

        Specification = specification;
    }

    /// <summary>
    /// Gets the specification being negated.
    /// </summary>
    public Specification<T> Specification { get; }

    /// <inheritdoc />
    public override Expression<Func<T, bool>> Criteria => ExpressionCombiner.Not(Specification.Criteria);
}
