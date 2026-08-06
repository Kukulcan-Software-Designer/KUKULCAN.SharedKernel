using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Provides public diagnostic operations for the reflection cache.
/// </summary>
internal static partial class ReflectionCache
{
    #region Contains

    /// <summary>
    /// Determines whether the specified cache key exists.
    /// </summary>
    public static bool Contains(
        ReflectionCacheKey key)
    {
        ArgumentNullException.ThrowIfNull(key);

        return _entries.ContainsKey(key);
    }

    /// <summary>
    /// Determines whether a cache entry exists for the specified owner type.
    /// </summary>
    public static bool ContainsType(
        Type ownerType)
    {
        ArgumentNullException.ThrowIfNull(ownerType);

        return ExistsKey(
            key => key.OwnerType == ownerType);
    }

    /// <summary>
    /// Determines whether a cache entry exists for the specified category.
    /// </summary>
    public static bool ContainsCategory(string category)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(category);

        return ExistsKey(key => StringComparer.Ordinal.Equals(key.Category, category));
    }

    /// <summary>
    /// Determines whether a cache entry exists for the specified assembly.
    /// </summary>
    public static bool ContainsAssembly(
        Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        return ExistsKey(
            key => key.OwnerType.Assembly == assembly);
    }

    /// <summary>
    /// Determines whether a cache entry exists for the specified module.
    /// </summary>
    public static bool ContainsModule(
        Module module)
    {
        ArgumentNullException.ThrowIfNull(module);

        return ExistsKey(
            key => key.OwnerType.Module == module);
    }

    /// <summary>
    /// Determines whether a cache entry exists for the specified namespace.
    /// </summary>
    public static bool ContainsNamespace(
        string @namespace)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);

        return ExistsKey(
            key => string.Equals(
                key.OwnerType.Namespace,
                @namespace,
                StringComparison.Ordinal));
    }

    /// <summary>
    /// Determines whether a cache entry exists assignable to the specified type.
    /// </summary>
    public static bool ContainsAssignableTo(Type baseType)
    {
        ArgumentNullException.ThrowIfNull(baseType);

        return ExistsKey(key => baseType.IsAssignableFrom(key.OwnerType));
    }

    /// <summary>
    /// Determines whether a cached value exists assignable to the specified type.
    /// </summary>
    public static bool ContainsValueType(Type valueType)
    {
        ArgumentNullException.ThrowIfNull(valueType);

        return ExistsEntry(entry => valueType.IsInstanceOfType(entry.Value));
    }

    /// <summary>
    /// Determines whether the cache contains metadata.
    /// </summary>
    public static bool ContainsMetadata()
        => ExistsEntry(
            entry => entry.Metadata.Count != 0);

    /// <summary>
    /// Determines whether the cache contains expired entries.
    /// </summary>
    public static bool ContainsExpiredEntries()
        => ExistsEntry(
            entry => entry.IsExpired);

    /// <summary>
    /// Determines whether the cache contains an entry satisfying the specified predicate.
    /// </summary>
    public static bool Contains(
        Func<ReflectionCacheEntry, bool> predicate)
        => ExistsEntry(predicate);

    #endregion

    #region Get

    /// <summary>
    /// Gets the cache entry associated with the specified key.
    /// </summary>
    public static ReflectionCacheEntry GetEntry(
        ReflectionCacheKey key)
    {
        return RequireEntry(key);
    }

    /// <summary>
    /// Attempts to obtain the cache entry associated with the specified key.
    /// </summary>
    public static bool TryGetEntry(
        ReflectionCacheKey key,
        out ReflectionCacheEntry? entry)
    {
        return TryRequireEntry(key, out entry);
    }

    /// <summary>
    /// Gets the cached value associated with the specified key.
    /// </summary>
    public static object? GetValue(
        ReflectionCacheKey key)
    {
        return RequireValue(key);
    }

    /// <summary>
    /// Gets the cached value associated with the specified key.
    /// </summary>
    public static TValue GetValue<TValue>(
        ReflectionCacheKey key)
    {
        return RequireValue<TValue>(key);
    }

    /// <summary>
    /// Attempts to obtain the cached value associated with the specified key.
    /// </summary>
    public static bool TryGetValue(ReflectionCacheKey key, out object? value)
    {
        value = null;

        if (!TryRequireEntry(key, out ReflectionCacheEntry? entry))
        {
            return false;
        }

        value = entry?.Value;
        return true;
    }

    /// <summary>
    /// Attempts to obtain the cached value associated with the specified key.
    /// </summary>
    public static bool TryGetValue<TValue>(ReflectionCacheKey key, out TValue? value)
    {
        value = default;

        if (!TryRequireEntry(key, out ReflectionCacheEntry? entry))
        {
            return false;
        }

        if (entry?.Value is not TValue typedValue)
        {
            return false;
        }

        value = typedValue;
        return true;
    }

    /// <summary>
    /// Gets the metadata associated with the specified cache key.
    /// </summary>
    public static IDictionary<string, object?> GetMetadata(ReflectionCacheKey key)
    {
        return RequireEntry(key).Metadata;
    }

    #endregion

    #region Enumeration

    public static IReadOnlyCollection<ReflectionCacheEntry> Enumerate() => EnumerateEntries();

    public static IReadOnlyCollection<ReflectionCacheEntry> EnumerateCategory(string category)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(category);

        return EnumerateByKey(key => StringComparer.Ordinal.Equals(key.Category, category));
    }

    public static IReadOnlyCollection<ReflectionCacheEntry> EnumerateType(Type ownerType)
    {
        ArgumentNullException.ThrowIfNull(ownerType);

        return EnumerateByKey(key => key.OwnerType == ownerType);
    }

    public static IReadOnlyCollection<ReflectionCacheEntry> EnumerateAssembly(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        return EnumerateByKey(key => key.OwnerType.Assembly == assembly);
    }

    public static IReadOnlyCollection<ReflectionCacheEntry> EnumerateModule(Module module)
    {
        ArgumentNullException.ThrowIfNull(module);

        return EnumerateByKey(key => key.OwnerType.Module == module);
    }

    public static IReadOnlyCollection<ReflectionCacheEntry> EnumerateNamespace(string @namespace)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);

        return EnumerateByKey(
            key => string.Equals(
                key.OwnerType.Namespace,
                @namespace,
                StringComparison.Ordinal));
    }

    public static IReadOnlyCollection<ReflectionCacheEntry> EnumerateAssignableTo(Type baseType)
    {
        ArgumentNullException.ThrowIfNull(baseType);

        return EnumerateByKey(key => baseType.IsAssignableFrom(key.OwnerType));
    }

    public static IReadOnlyCollection<ReflectionCacheEntry> Find(Func<ReflectionCacheEntry, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return EnumerateEntries(predicate);
    }

    public static ReflectionCacheEntry? FindFirst(Func<ReflectionCacheEntry, bool> predicate)
        => FindFirstEntry(predicate);

    public static ReflectionCacheEntry? FindFirst(Type ownerType)
    {
        ArgumentNullException.ThrowIfNull(ownerType);

        return FindFirstByKey(
            key => key.OwnerType == ownerType);
    }

    public static ReflectionCacheEntry? FindFirst(string category)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(category);

        return FindFirstByKey(key => StringComparer.Ordinal.Equals(key.Category, category));
    }
    #endregion

    #region Statistics

    /// <summary>
    /// Gets the number of cache entries.
    /// </summary>
    public static int Count() => _entries.Count;

    /// <summary>
    /// Counts cache entries satisfying the specified predicate.
    /// </summary>
    public static int Count(Func<ReflectionCacheEntry, bool> predicate)
    {
        return CountEntries(predicate);
    }

    /// <summary>
    /// Counts cache entries belonging to the specified category.
    /// </summary>
    public static int CountCategory(string category)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(category);

        return CountKeys(key => StringComparer.Ordinal.Equals(key.Category, category));
    }

    /// <summary>
    /// Counts cache entries belonging to the specified owner type.
    /// </summary>
    public static int CountType(Type ownerType)
    {
        ArgumentNullException.ThrowIfNull(ownerType);

        return CountKeys(key => key.OwnerType == ownerType);
    }

    /// <summary>
    /// Counts expired cache entries.
    /// </summary>
    public static int CountExpired()
    {
        return CountEntries(entry => entry.IsExpired);
    }

    /// <summary>
    /// Counts cache entries containing metadata.
    /// </summary>
    public static int CountMetadata()
    {
        return CountEntries(entry => entry.Metadata.Count != 0);
    }

    /// <summary>
    /// Determines whether the cache is empty.
    /// </summary>
    public static bool IsEmpty() => _entries.IsEmpty;

    /// <summary>
    /// Determines whether the cache contains entries.
    /// </summary>
    public static bool IsNotEmpty() => !_entries.IsEmpty;

    /// <summary>
    /// Determines whether every cache entry is expired.
    /// </summary>
    public static bool AllExpired()
    {
        return !_entries.IsEmpty && AllEntries(entry => entry.IsExpired);
    }

    /// <summary>
    /// Determines whether every cache entry contains metadata.
    /// </summary>
    public static bool AllContainMetadata()
    {
        return !_entries.IsEmpty && AllEntries(entry => entry.Metadata.Count != 0);
    }

    /// <summary>
    /// Creates a snapshot containing the current cache statistics.
    /// </summary>
    public static ReflectionCacheStatistics Statistics()
    {
        return new ReflectionCacheStatistics();
    }

    #endregion
}
