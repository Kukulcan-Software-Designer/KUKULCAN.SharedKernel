using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Cache invalidation operations.
/// </summary>
internal static partial class ReflectionCache
{
    public static void Invalidate(ReflectionCacheKey key)
    {
        ArgumentNullException.ThrowIfNull(key);

        Remove(key);
    }

    public static bool TryInvalidate(ReflectionCacheKey key)
    {
        ArgumentNullException.ThrowIfNull(key);

        return TryRemove(key);
    }

    public static bool TryInvalidate(
        ReflectionCacheKey key,
        out ReflectionCacheEntry? invalidatedEntry)
    {
        ArgumentNullException.ThrowIfNull(key);

        return TryRemove(
            key,
            out invalidatedEntry);
    }

    public static bool TryInvalidate<T>(
        ReflectionCacheKey key,
        out T? invalidatedValue)
    {
        ArgumentNullException.ThrowIfNull(key);

        return TryRemove(
            key,
            out invalidatedValue);
    }

    public static void InvalidateRange(
        IEnumerable<ReflectionCacheKey> keys)
    {
        ArgumentNullException.ThrowIfNull(keys);

        RemoveRange(keys);
    }

    public static bool TryInvalidateRange(
        IEnumerable<ReflectionCacheKey> keys)
    {
        ArgumentNullException.ThrowIfNull(keys);

        return TryRemoveRange(keys);
    }

    public static IReadOnlyList<ReflectionCacheEntry> InvalidateRangeWithResult(
        IEnumerable<ReflectionCacheKey> keys)
    {
        ArgumentNullException.ThrowIfNull(keys);

        return RemoveRangeWithResult(keys);
    }

    public static bool CanInvalidate(
        ReflectionCacheKey key)
    {
        ArgumentNullException.ThrowIfNull(key);

        return CanRemove(key);
    }

    /// <summary>
    /// Invalidates every cache entry belonging to the specified category.
    /// </summary>
    public static int InvalidateCategory(
        string category)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(category);

        return RemoveByCategory(category);
    }

    public static int InvalidateType(Type ownerType)
    {
        ArgumentNullException.ThrowIfNull(ownerType);

        return RemoveByType(ownerType);
    }

    public static int InvalidateAssembly(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        return RemoveByAssembly(assembly);
    }

    public static int InvalidateModule(Module module)
    {
        ArgumentNullException.ThrowIfNull(module);

        return RemoveByModule(module);
    }

    public static int InvalidateNamespace(string @namespace)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);

        return RemoveByNamespace(@namespace);
    }

    public static int InvalidateGenericType(Type genericTypeDefinition)
    {
        ArgumentNullException.ThrowIfNull(genericTypeDefinition);

        return RemoveByGenericType(genericTypeDefinition);
    }

    public static int InvalidateValueType(Type valueType)
    {
        ArgumentNullException.ThrowIfNull(valueType);

        return RemoveByValueType(valueType);
    }

    public static int InvalidateWithMetadata()
        => RemoveWithMetadata();

    public static int InvalidateWithoutMetadata()
        => RemoveWithoutMetadata();

    public static int InvalidateAssignableTo(Type baseType)
    {
        ArgumentNullException.ThrowIfNull(baseType);

        return RemoveByAssignableType(baseType);
    }

    public static int InvalidateExpired()
        => Purge();

    public static int InvalidateWhere(
        Func<ReflectionCacheEntry, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return RemoveIf(predicate);
    }

    public static int InvalidateWhere(
        Func<ReflectionCacheKey, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return RemoveWhere(predicate);
    }

    public static int InvalidateWhere(
        Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return RemoveWhere(predicate);
    }

    public static int InvalidateAll()
        => RemoveAll();

    public static IReadOnlyList<ReflectionCacheEntry> InvalidateAllWithResult()
        => RemoveAllWithResult();

    public static bool HasExpiredEntries()
        => HasRemovableEntries(static entry => entry.IsExpired);

    public static bool HasInvalidatableEntries(
        Func<ReflectionCacheEntry, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return HasRemovableEntries(predicate);
    }

    public static int CountInvalidatableEntries(
        Func<ReflectionCacheEntry, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return CountRemovable(predicate);
    }
}
