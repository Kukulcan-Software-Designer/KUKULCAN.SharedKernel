using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Filter-based remove operations for the reflection cache.
/// </summary>
internal static partial class ReflectionCache
{
    /// <summary>
    /// Removes every cache entry satisfying the specified predicate.
    /// </summary>
    public static int RemoveWhere(
        Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return RemoveInternal(predicate);
    }

    /// <summary>
    /// Removes every cache entry whose key satisfies the specified predicate.
    /// </summary>
    public static int RemoveWhere(
        Func<ReflectionCacheKey, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return RemoveInternal(
            FromKey(predicate));
    }

    /// <summary>
    /// Removes every cache entry whose value satisfies the specified predicate.
    /// </summary>
    public static int RemoveWhere(
        Func<ReflectionCacheEntry, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return RemoveInternal(
            FromEntry(predicate));
    }

    /// <summary>
    /// Removes every cache entry belonging to the specified category.
    /// </summary>
    public static int RemoveByCategory(string category)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(category);

        return RemoveInternal(ByCategory(category));
    }

    /// <summary>
    /// Removes every cache entry associated with the specified owner type.
    /// </summary>
    public static int RemoveByType(
        Type ownerType)
    {
        ArgumentNullException.ThrowIfNull(ownerType);

        return RemoveInternal(
            ByType(ownerType));
    }

    /// <summary>
    /// Removes every cache entry associated with the specified assembly.
    /// </summary>
    public static int RemoveByAssembly(
        Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        return RemoveInternal(
            ByAssembly(assembly));
    }

    /// <summary>
    /// Removes every cache entry associated with the specified module.
    /// </summary>
    public static int RemoveByModule(
        Module module)
    {
        ArgumentNullException.ThrowIfNull(module);

        return RemoveInternal(
            ByModule(module));
    }

    /// <summary>
    /// Removes every cache entry whose owner type belongs to the specified namespace.
    /// </summary>
    public static int RemoveByNamespace(
        string @namespace)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);

        return RemoveInternal(
            ByNamespace(@namespace));
    }

    /// <summary>
    /// Removes every cache entry whose owner type is a constructed generic instance
    /// of the specified generic type definition.
    /// </summary>
    public static int RemoveByGenericType(
        Type genericTypeDefinition)
    {
        ArgumentNullException.ThrowIfNull(genericTypeDefinition);

        return RemoveInternal(
            ByGenericType(genericTypeDefinition));
    }

    /// <summary>
    /// Removes every cache entry whose value type is assignable to the specified type.
    /// </summary>
    public static int RemoveByValueType(
        Type valueType)
    {
        ArgumentNullException.ThrowIfNull(valueType);

        return RemoveInternal(
            ByValueType(valueType));
    }

    /// <summary>
    /// Removes every cache entry containing metadata.
    /// </summary>
    public static int RemoveWithMetadata()
    {
        return RemoveInternal(
            HasMetadata());
    }

    /// <summary>
    /// Removes every cache entry without metadata.
    /// </summary>
    public static int RemoveWithoutMetadata()
    {
        return RemoveInternal(
            WithoutMetadata());
    }

    /// <summary>
    /// Removes every cache entry created before the specified instant.
    /// </summary>
    public static int RemoveCreatedBefore(
        DateTimeOffset instant)
    {
        return RemoveInternal(
            CreatedBefore(instant));
    }

    /// <summary>
    /// Removes every cache entry created after the specified instant.
    /// </summary>
    public static int RemoveCreatedAfter(
        DateTimeOffset instant)
    {
        return RemoveInternal(
            CreatedAfter(instant));
    }

    /// <summary>
    /// Removes every cache entry last accessed before the specified instant.
    /// </summary>
    public static int RemoveAccessedBefore(
        DateTimeOffset instant)
    {
        return RemoveInternal(
            AccessedBefore(instant));
    }

    /// <summary>
    /// Removes every cache entry last accessed after the specified instant.
    /// </summary>
    public static int RemoveAccessedAfter(
        DateTimeOffset instant)
    {
        return RemoveInternal(
            AccessedAfter(instant));
    }

    /// <summary>
    /// Removes every cache entry satisfying the specified predicate.
    /// </summary>
    public static int RemoveIf(
        Func<ReflectionCacheEntry, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return RemoveInternal(
            FromEntry(predicate));
    }

    /// <summary>
    /// Removes every expired cache entry.
    /// </summary>
    public static int Purge()
    {
        return RemoveInternal(
            Expired());
    }

    /// <summary>
    /// Removes expired cache entries and compacts the cache.
    /// </summary>
    public static int Compact()
    {
        int removed = Purge();

        GC.Collect();
        GC.WaitForPendingFinalizers();

        return removed;
    }

    /// <summary>
    /// Counts the removable entries matching the specified predicate.
    /// </summary>
    public static int CountRemovable(
        Func<ReflectionCacheEntry, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return _entries
            .ToArray()
            .Count(pair => predicate(pair.Value));
    }

    /// <summary>
    /// Determines whether removable entries matching the specified predicate exist.
    /// </summary>
    public static bool HasRemovableEntries(
        Func<ReflectionCacheEntry, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return _entries
            .ToArray()
            .Any(pair => predicate(pair.Value));
    }

    public static int RemoveByAssignableType(Type baseType)
    {
        ArgumentNullException.ThrowIfNull(baseType);

        return RemoveInternal(
            ByAssignableType(baseType));
    }
}
