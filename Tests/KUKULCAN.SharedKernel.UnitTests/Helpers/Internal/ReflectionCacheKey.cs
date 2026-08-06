using System;
using System.Diagnostics;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Represents a unique key used by <see cref="ReflectionCache"/> to identify
/// cached reflection metadata.
/// </summary>
[DebuggerDisplay("{ToString(),nq}")]
internal sealed record ReflectionCacheKey
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReflectionCacheKey"/> class.
    /// </summary>
    /// <param name="ownerType">
    /// Type that owns the reflected member.
    /// </param>
    /// <param name="category">
    /// Reflection category.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="ownerType"/> or
    /// <paramref name="category"/> is <see langword="null"/>.
    /// </exception>
    public ReflectionCacheKey(
        Type ownerType,
        string category)
    {
        ArgumentNullException.ThrowIfNull(ownerType);
        ArgumentNullException.ThrowIfNull(category);

        OwnerType = ownerType;
        Category = category;
    }

    /// <summary>
    /// Gets the owner type.
    /// </summary>
    public Type OwnerType { get; }

    /// <summary>
    /// Gets the cache category.
    /// </summary>
    public string Category { get; }

    /// <summary>
    /// Returns a readable representation of the key.
    /// </summary>
    /// <returns>
    /// A string representing the cache key.
    /// </returns>
    public override string ToString()
    {
        return $"{OwnerType.FullName}:{Category}";
    }
}
