using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Represents an immutable snapshot of the cache.
/// </summary>
internal sealed class ReflectionCacheSnapshot
{
    public ReflectionCacheSnapshot(
        IEnumerable<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>> entries)
    {
        Entries = new ReadOnlyDictionary<
            ReflectionCacheKey,
            ReflectionCacheEntry>(
            new Dictionary<
                ReflectionCacheKey,
                ReflectionCacheEntry>(entries));
    }

    /// <summary>
    /// Gets the snapshot entries.
    /// </summary>
    public IReadOnlyDictionary<
        ReflectionCacheKey,
        ReflectionCacheEntry> Entries { get; }

    /// <summary>
    /// Gets the number of entries.
    /// </summary>
    public int Count => Entries.Count;
}
