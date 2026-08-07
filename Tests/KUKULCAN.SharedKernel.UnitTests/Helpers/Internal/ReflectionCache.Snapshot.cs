using System;
using System.Collections.Generic;
using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Provides immutable snapshots of the current reflection cache.
/// </summary>
internal static partial class ReflectionCache
{
    #region Factory

    /// <summary>
    /// Creates an immutable snapshot from the specified cache entries.
    /// </summary>
    /// <param name="entries">
    /// Cache entries to include in the snapshot.
    /// </param>
    /// <returns>
    /// Immutable reflection cache snapshot.
    /// </returns>
    private static ReflectionCacheSnapshot CreateSnapshot(
        IEnumerable<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);

        return new ReflectionCacheSnapshot(entries);
    }

    #endregion

    #region Snapshot

    /// <summary>
    /// Creates an immutable snapshot of the entire reflection cache.
    /// </summary>
    /// <returns>
    /// Immutable reflection cache snapshot.
    /// </returns>
    public static ReflectionCacheSnapshot Snapshot()
    {
        return CreateSnapshot(Enumerate());
    }

    #endregion

    #region Filtered snapshots

    /// <summary>
    /// Creates an immutable snapshot containing the cache entries
    /// belonging to the specified owner type.
    /// </summary>
    /// <param name="ownerType">
    /// Owner type.
    /// </param>
    /// <returns>
    /// Immutable reflection cache snapshot.
    /// </returns>
    public static ReflectionCacheSnapshot SnapshotByType(Type ownerType)
    {
        ArgumentNullException.ThrowIfNull(ownerType);

        return CreateSnapshot(EnumerateType(ownerType));
    }

    /// <summary>
    /// Creates an immutable snapshot containing the cache entries
    /// belonging to the specified assembly.
    /// </summary>
    /// <param name="assembly">
    /// Assembly.
    /// </param>
    /// <returns>
    /// Immutable reflection cache snapshot.
    /// </returns>
    public static ReflectionCacheSnapshot SnapshotByAssembly(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        return CreateSnapshot(EnumerateAssembly(assembly));
    }

    /// <summary>
    /// Creates an immutable snapshot containing the cache entries
    /// belonging to the specified module.
    /// </summary>
    /// <param name="module">
    /// Module.
    /// </param>
    /// <returns>
    /// Immutable reflection cache snapshot.
    /// </returns>
    public static ReflectionCacheSnapshot SnapshotByModule(Module module)
    {
        ArgumentNullException.ThrowIfNull(module);

        return CreateSnapshot(EnumerateModule(module));
    }

    /// <summary>
    /// Creates an immutable snapshot containing the cache entries
    /// belonging to the specified namespace.
    /// </summary>
    /// <param name="namespace">
    /// Namespace.
    /// </param>
    /// <returns>
    /// Immutable reflection cache snapshot.
    /// </returns>
    public static ReflectionCacheSnapshot SnapshotByNamespace(string @namespace)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);

        return CreateSnapshot(EnumerateNamespace(@namespace));
    }

    /// <summary>
    /// Creates an immutable snapshot containing every cache entry
    /// assignable to the specified base type.
    /// </summary>
    /// <param name="baseType">
    /// Base type.
    /// </param>
    /// <returns>
    /// Immutable reflection cache snapshot.
    /// </returns>
    public static ReflectionCacheSnapshot SnapshotByAssignableTo(Type baseType)
    {
        ArgumentNullException.ThrowIfNull(baseType);

        return CreateSnapshot(EnumerateAssignableTo(baseType));
    }

    #endregion

    #region Specialized snapshots

    /// <summary>
    /// Creates an immutable snapshot containing only entries that expose metadata.
    /// </summary>
    /// <returns>
    /// Immutable reflection cache snapshot.
    /// </returns>
    public static ReflectionCacheSnapshot SnapshotWithMetadata()
    {
        return CreateSnapshot(Find(entry => entry.Metadata.Count != 0));
    }

    /// <summary>
    /// Creates an immutable snapshot containing only expired entries.
    /// </summary>
    /// <returns>
    /// Immutable reflection cache snapshot.
    /// </returns>
    public static ReflectionCacheSnapshot SnapshotExpired()
    {
        return CreateSnapshot(Find(entry => entry.IsExpired));
    }

    /// <summary>
    /// Creates an immutable snapshot containing the entries satisfying
    /// the specified predicate.
    /// </summary>
    /// <param name="predicate">
    /// Predicate used to select cache entries.
    /// </param>
    /// <returns>
    /// Immutable reflection cache snapshot.
    /// </returns>
    public static ReflectionCacheSnapshot Snapshot(Func<ReflectionCacheEntry, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return CreateSnapshot(Find(predicate));
    }

    #endregion
}
