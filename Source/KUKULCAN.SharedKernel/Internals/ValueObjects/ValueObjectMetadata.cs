namespace KUKULCAN.SharedKernel.Internals.ValueObjects;

/// <summary>
/// Cached metadata for a ValueObject type.
/// </summary>
internal sealed class ValueObjectMetadata
{
    /// <summary>
    /// Gets the ValueObject type.
    /// </summary>
    public required Type Type { get; init; }

    /// <summary>
    /// Gets the members participating in equality.
    /// </summary>
    public required IReadOnlyList<ValueObjectProperty> Members
    {
        get;
        init;
    }
}
