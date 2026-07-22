namespace KUKULCAN.SharedKernel.Identifiers.Interfaces;

/// <summary>
/// Generates entity identifiers.
/// </summary>
/// <typeparam name="TId">
/// Type of the generated identifier.
/// </typeparam>
public interface IIdGenerator<TId> where TId : IEntityId
{
    /// <summary>
    /// Creates a new identifier.
    /// </summary>
    /// <returns>
    /// A new identifier.
    /// </returns>
    TId New();
}
