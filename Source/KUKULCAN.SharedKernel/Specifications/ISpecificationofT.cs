namespace KUKULCAN.SharedKernel.Specifications;

/// <summary>
/// Represents a business rule that can be translated into a LINQ expression.
/// </summary>
/// <typeparam name="T">
/// Type evaluated by the specification.
/// </typeparam>
public interface ISpecification<T>
{
    /// <summary>
    /// Gets the expression that defines the specification.
    /// </summary>
    Expression<Func<T, bool>> Criteria { get; }
}
