namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Direct remove operations for the reflection cache.
/// </summary>
internal static partial class ReflectionCache
{

    /// <summary>
    /// Removes a cache entry.
    /// </summary>
    public static void Remove(
        ReflectionCacheKey key)
    {
        ArgumentNullException.ThrowIfNull(key);

        if (!TryRemove(key))
        {
            throw new KeyNotFoundException(
                $"Reflection cache entry '{key}' was not found.");
        }
    }

    /// <summary>
    /// Attempts to remove a cache entry.
    /// </summary>
    public static bool TryRemove(
        ReflectionCacheKey key)
    {
        ArgumentNullException.ThrowIfNull(key);

        return _entries.TryRemove(
            key,
            out _);
    }

    /// <summary>
    /// Attempts to remove a cache entry.
    /// </summary>
    public static bool TryRemove(ReflectionCacheKey key, out ReflectionCacheEntry? removedEntry)
    {
        ArgumentNullException.ThrowIfNull(key);

        return _entries.TryRemove(key, out removedEntry);
    }

    /// <summary>
    /// Attempts to remove a cache entry and returns the typed value.
    /// </summary>
    public static bool TryRemove<T>(
        ReflectionCacheKey key,
        out T? value)
    {
        ArgumentNullException.ThrowIfNull(key);

        value = default;

        if (!TryRemove(key, out ReflectionCacheEntry? entry) || entry is null)
        {
            return false;
        }

        return entry.TryGetValue(out value);
    }

    /// <summary>
    /// Determines whether the specified key can be removed.
    /// </summary>
    public static bool CanRemove(
        ReflectionCacheKey key)
    {
        ArgumentNullException.ThrowIfNull(key);

        return Contains(key);
    }

    /// <summary>
    /// Removes multiple cache entries.
    /// Every key must exist.
    /// </summary>
    public static void RemoveRange(
        IEnumerable<ReflectionCacheKey> keys)
    {
        ArgumentNullException.ThrowIfNull(keys);

        foreach (ReflectionCacheKey key in keys)
        {
            Remove(key);
        }
    }

    /// <summary>
    /// Attempts to remove multiple cache entries.
    /// </summary>
    public static bool TryRemoveRange(
        IEnumerable<ReflectionCacheKey> keys)
    {
        ArgumentNullException.ThrowIfNull(keys);

        bool success = true;

        foreach (ReflectionCacheKey key in keys)
        {
            success &= TryRemove(key);
        }

        return success;
    }

    /// <summary>
    /// Removes multiple cache entries and returns the removed entries.
    /// </summary>
    public static IReadOnlyList<ReflectionCacheEntry> RemoveRangeWithResult(IEnumerable<ReflectionCacheKey> keys)
    {
        ArgumentNullException.ThrowIfNull(keys);

        List<ReflectionCacheEntry> removedEntries = new();

        foreach (ReflectionCacheKey key in keys)
        {
            if (TryRemove(key, out ReflectionCacheEntry? entry) && entry is not null)
            {
                removedEntries.Add(entry);
            }
        }

        return removedEntries;
    }

    /// <summary>
    /// Removes every cache entry.
    /// </summary>
    public static int RemoveAll()
    {
        int removed = _entries.Count;

        _entries.Clear();

        return removed;
    }

    /// <summary>
    /// Removes every cache entry and returns the removed entries.
    /// </summary>
    public static IReadOnlyList<ReflectionCacheEntry> RemoveAllWithResult()
    {
        List<ReflectionCacheEntry> removed =
            [.. _entries.Values];

        _entries.Clear();

        return removed;
    }
}
