using System.Reflection;

namespace KUKULCAN.SharedKernel.Internals.ValueObjects;

/// <summary>
/// Represents a cached ValueObject member.
/// </summary>
internal sealed class ValueObjectProperty
{
    /// <summary>
    /// Gets the property name.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets the reflected property.
    /// </summary>
    public required PropertyInfo Property { get; init; }

    /// <summary>
    /// Gets the compiled getter.
    /// </summary>
    public required Func<object, object?> Getter { get; init; }
}
