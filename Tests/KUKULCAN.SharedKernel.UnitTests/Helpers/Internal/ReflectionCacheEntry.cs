using System.Diagnostics;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Represents a cached reflection entry.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
internal sealed class ReflectionCacheEntry
{
    private ReflectionCacheEntry(
        ReflectionCacheKey key,
        object? value)
    {
        Key = key;
        Value = value;

        CreatedOn = DateTimeOffset.UtcNow;
        LastAccess = CreatedOn;
    }

    /// <summary>
    /// Gets the cache key.
    /// </summary>
    public ReflectionCacheKey Key { get; }

    /// <summary>
    /// Gets the cached value.
    /// </summary>
    public object? Value { get; }

    /// <summary>
    /// Gets the metadata associated with the cached entry.
    /// </summary>
    public IDictionary<string, object?> Metadata { get; }
        = new Dictionary<string, object?>();

    /// <summary>
    /// Gets the creation date.
    /// </summary>
    public DateTimeOffset CreatedOn { get; }

    /// <summary>
    /// Gets the last access date.
    /// </summary>
    public DateTimeOffset LastAccess { get; private set; }

    /// <summary>
    /// Gets the access counter.
    /// </summary>
    public long AccessCount { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the entry has expired.
    /// </summary>
    public bool IsExpired => false;

    /// <summary>
    /// Creates a new cache entry.
    /// </summary>
    /// <typeparam name="T">
    /// Cached value type.
    /// </typeparam>
    /// <param name="key">
    /// Cache key.
    /// </param>
    /// <param name="value">
    /// Cached value.
    /// </param>
    /// <returns>
    /// A new <see cref="ReflectionCacheEntry"/>.
    /// </returns>
    public static ReflectionCacheEntry Create<T>(ReflectionCacheKey key,
        T value)
    {
        ArgumentNullException.ThrowIfNull(key);

        return new ReflectionCacheEntry(key, value);
    }

    /// <summary>
    /// Registers an access to the cached entry.
    /// </summary>
    public void Touch()
    {
        AccessCount++;
        LastAccess = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Creates a deep copy of the current entry.
    /// </summary>
    /// <returns>
    /// A cloned <see cref="ReflectionCacheEntry"/>.
    /// </returns>
    public ReflectionCacheEntry Clone()
    {
        ReflectionCacheEntry clone = new(Key, Value);

        clone.AccessCount = AccessCount;
        clone.LastAccess = LastAccess;

        foreach (KeyValuePair<string, object?> pair in Metadata)
        {
            clone.Metadata[pair.Key] = pair.Value;
        }

        return clone;
    }

    private string DebuggerDisplay => $"{Key} | {Value?.GetType().Name ?? "null"} | Accesses={AccessCount}";

    /// <summary>
    /// Attempts to obtain the cached value as the requested type.
    /// </summary>
    /// <typeparam name="T">
    /// Requested value type.
    /// </typeparam>
    /// <param name="value">
    /// Retrieved value.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the value could be cast to
    /// <typeparamref name="T"/>; otherwise <see langword="false"/>.
    /// </returns>
    public bool TryGetValue<T>(out T? value)
    {
        if (Value is T typedValue)
        {
            value = typedValue;
            return true;
        }

        value = default;
        return false;
    }
}
