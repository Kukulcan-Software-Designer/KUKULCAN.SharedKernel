using System;
using System.Collections.Generic;
using System.Linq;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Internal remove algorithms for the reflection cache.
/// </summary>
internal static partial class ReflectionCache
{
    /// <summary>
    /// Removes every cache entry matching the specified predicate.
    /// </summary>
    private static int RemoveInternal(
        Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        int removed = 0;

        foreach (KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry> pair
                 in _entries.ToArray())
        {
            if (!predicate(pair))
            {
                continue;
            }

            if (_entries.TryRemove(pair.Key, out _))
            {
                removed++;
            }
        }

        return removed;
    }

    /// <summary>
    /// Removes every cache entry matching the specified predicate and returns
    /// the removed entries.
    /// </summary>
    private static IReadOnlyList<ReflectionCacheEntry> RemoveInternalWithResult(
        Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        List<ReflectionCacheEntry> removedEntries = new();

        foreach (KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry> pair
                 in _entries.ToArray())
        {
            if (!predicate(pair))
            {
                continue;
            }

            if (_entries.TryRemove(
                    pair.Key,
                    out ReflectionCacheEntry? removedEntry) &&
                removedEntry is not null)
            {
                removedEntries.Add(removedEntry);
            }
        }

        return removedEntries;
    }

    /// <summary>
    /// Removes the first cache entry matching the specified predicate.
    /// </summary>
    private static bool RemoveFirstInternal(
        Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        foreach (KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry> pair
                 in _entries.ToArray())
        {
            if (!predicate(pair))
            {
                continue;
            }

            return _entries.TryRemove(
                pair.Key,
                out _);
        }

        return false;
    }

    /// <summary>
    /// Removes the first cache entry matching the specified predicate and returns it.
    /// </summary>
    private static bool RemoveFirstInternal(
        Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool> predicate,
        out ReflectionCacheEntry? removedEntry)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        removedEntry = null;

        foreach (KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry> pair
                 in _entries.ToArray())
        {
            if (!predicate(pair))
            {
                continue;
            }

            return _entries.TryRemove(
                pair.Key,
                out removedEntry);
        }

        return false;
    }

    /// <summary>
    /// Removes every cache entry and returns the number of removed entries.
    /// </summary>
    private static int RemoveAllInternal()
    {
        int removed = _entries.Count;

        _entries.Clear();

        return removed;
    }

    /// <summary>
    /// Removes every cache entry and returns the removed entries.
    /// </summary>
    private static IReadOnlyList<ReflectionCacheEntry> RemoveAllInternalWithResult()
    {
        List<ReflectionCacheEntry> removedEntries =
            _entries.Values.ToList();

        _entries.Clear();

        return removedEntries;
    }
}
