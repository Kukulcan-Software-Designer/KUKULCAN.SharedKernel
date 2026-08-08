namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Provides the internal diagnostic engine used by <see cref="ReflectionCache"/>.
/// All diagnostic algorithms are centralized here.
/// </summary>
internal static partial class ReflectionCache
{
    #region Collections

    /// <summary>
    /// Gets the underlying cache pairs.
    /// This is the single source of truth used by every diagnostic algorithm.
    /// </summary>
    private static IEnumerable<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>>
        PairCollection
            => _entries;

    /// <summary>
    /// Gets the cache keys.
    /// </summary>
    private static IEnumerable<ReflectionCacheKey>
        KeyCollection
            => PairCollection.Select(static pair => pair.Key);

    /// <summary>
    /// Gets the cache entries.
    /// </summary>
    private static IEnumerable<ReflectionCacheEntry>
        EntryCollection
            => PairCollection.Select(static pair => pair.Value);

    #endregion

    #region Helpers

    /// <summary>
    /// Converts an enumerable into a read-only collection.
    /// </summary>
    private static IReadOnlyCollection<T> ToReadOnly<T>(
        IEnumerable<T> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return Array.AsReadOnly(source.ToArray());
    }

    #endregion

    #region Exists

    /// <summary>
    /// Determines whether any cache key satisfies the specified predicate.
    /// </summary>
    private static bool ExistsKey(
        Func<ReflectionCacheKey, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return KeyCollection.Any(predicate);
    }

    /// <summary>
    /// Determines whether any cache entry satisfies the specified predicate.
    /// </summary>
    private static bool ExistsEntry(
        Func<ReflectionCacheEntry, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return EntryCollection.Any(predicate);
    }

    /// <summary>
    /// Determines whether any cache pair satisfies the specified predicate.
    /// </summary>
    private static bool ExistsPair(
        Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return PairCollection.Any(predicate);
    }

    #endregion

    #region Count

    /// <summary>
    /// Counts cache keys satisfying the specified predicate.
    /// </summary>
    private static int CountKeys(
        Func<ReflectionCacheKey, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return KeyCollection.Count(predicate);
    }

    /// <summary>
    /// Counts cache entries satisfying the specified predicate.
    /// </summary>
    private static int CountEntries(
        Func<ReflectionCacheEntry, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return EntryCollection.Count(predicate);
    }

    /// <summary>
    /// Counts cache pairs satisfying the specified predicate.
    /// </summary>
    private static int CountPairs(
        Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return PairCollection.Count(predicate);
    }

    #endregion

    #region All

    /// <summary>
    /// Determines whether every cache key satisfies the specified predicate.
    /// </summary>
    private static bool AllKeys(
        Func<ReflectionCacheKey, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return KeyCollection.All(predicate);
    }

    /// <summary>
    /// Determines whether every cache entry satisfies the specified predicate.
    /// </summary>
    private static bool AllEntries(
        Func<ReflectionCacheEntry, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return EntryCollection.All(predicate);
    }

    /// <summary>
    /// Determines whether every cache pair satisfies the specified predicate.
    /// </summary>
    private static bool AllPairs(
        Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return PairCollection.All(predicate);
    }

    #endregion

    #region Enumeration

    /// <summary>
    /// Enumerates every cache key.
    /// </summary>
    private static IReadOnlyCollection<ReflectionCacheKey> EnumerateKeys()
    {
        return ToReadOnly(KeyCollection);
    }

    /// <summary>
    /// Enumerates the cache keys satisfying the specified predicate.
    /// </summary>
    private static IReadOnlyCollection<ReflectionCacheKey> EnumerateKeys(
        Func<ReflectionCacheKey, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return ToReadOnly(
            KeyCollection.Where(predicate));
    }

    /// <summary>
    /// Enumerates every cache entry.
    /// </summary>
    private static IReadOnlyCollection<ReflectionCacheEntry> EnumerateEntries()
    {
        return ToReadOnly(EntryCollection);
    }

    /// <summary>
    /// Enumerates the cache entries satisfying the specified predicate.
    /// </summary>
    private static IReadOnlyCollection<ReflectionCacheEntry> EnumerateEntries(
        Func<ReflectionCacheEntry, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return ToReadOnly(
            EntryCollection.Where(predicate));
    }

    /// <summary>
    /// Enumerates the cache entries satisfying the specified pair predicate.
    /// </summary>
    private static IReadOnlyCollection<ReflectionCacheEntry> EnumerateEntries(
        Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return ToReadOnly(
            PairCollection
                .Where(predicate)
                .Select(static pair => pair.Value));
    }

    /// <summary>
    /// Enumerates every cached value.
    /// </summary>
    private static IReadOnlyCollection<object?> EnumerateValues()
    {
        return ToReadOnly(
            EntryCollection.Select(static entry => entry.Value));
    }

    /// <summary>
    /// Enumerates every cached value satisfying the specified predicate.
    /// </summary>
    private static IReadOnlyCollection<object?> EnumerateValues(
        Func<object?, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return ToReadOnly(
            EntryCollection
                .Select(static entry => entry.Value)
                .Where(predicate));
    }

    #endregion

    #region Find

    /// <summary>
    /// Finds the first cache key satisfying the specified predicate.
    /// </summary>
    private static ReflectionCacheKey? FindFirstKey(
        Func<ReflectionCacheKey, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return KeyCollection.FirstOrDefault(predicate);
    }

    /// <summary>
    /// Finds the first cache entry satisfying the specified predicate.
    /// </summary>
    private static ReflectionCacheEntry? FindFirstEntry(
        Func<ReflectionCacheEntry, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return EntryCollection.FirstOrDefault(predicate);
    }

    /// <summary>
    /// Finds the first cache pair satisfying the specified predicate.
    /// </summary>
    private static KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>? FindFirstPair(
        Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return PairCollection.FirstOrDefault(predicate);
    }

    #endregion

    #region Require

    /// <summary>
    /// Gets the cache entry associated with the specified key.
    /// Throws if the key does not exist.
    /// </summary>
    private static ReflectionCacheEntry RequireEntry(
        ReflectionCacheKey key)
    {
        ArgumentNullException.ThrowIfNull(key);

        if (!_entries.TryGetValue(key, out ReflectionCacheEntry? entry))
        {
            throw new KeyNotFoundException(
                $"Reflection cache entry '{key}' was not found.");
        }

        return entry;
    }

    /// <summary>
    /// Gets the cached value associated with the specified key.
    /// </summary>
    private static object? RequireValue(
        ReflectionCacheKey key)
    {
        return RequireEntry(key).Value;
    }

    /// <summary>
    /// Gets the cached value associated with the specified key.
    /// </summary>
    private static T RequireValue<T>(
        ReflectionCacheKey key)
    {
        object? value = RequireValue(key);

        if (value is not T typedValue)
        {
            throw new InvalidCastException(
                $"The cached value associated with '{key}' cannot be cast to '{typeof(T).FullName}'.");
        }

        return typedValue;
    }

    #endregion

    /// <summary>
    /// Attempts to obtain the cache entry associated with the specified key.
    /// </summary>
    /// <param name="key">
    /// Cache key.
    /// </param>
    /// <param name="entry">
    /// When this method returns, contains the associated cache entry if found;
    /// otherwise <see langword="null"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the cache entry exists;
    /// otherwise <see langword="false"/>.
    /// </returns>
    private static bool TryRequireEntry(ReflectionCacheKey key, out ReflectionCacheEntry? entry)
    {
        ArgumentNullException.ThrowIfNull(key);

        return _entries.TryGetValue(key, out entry);
    }

    /// <summary>
    /// Enumerates cache entries whose key satisfies the specified predicate.
    /// </summary>
    private static IEnumerable<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>> EnumerateByKey(
        Func<ReflectionCacheKey, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return PairCollection.Where(pair => predicate(pair.Key));
    }

    /// <summary>
    /// Finds the first cache entry whose key satisfies the specified predicate.
    /// </summary>
    private static ReflectionCacheEntry? FindFirstByKey(
        Func<ReflectionCacheKey, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>? pair =
            FindFirstPair(p => predicate(p.Key));

        return pair?.Value;
    }

}
