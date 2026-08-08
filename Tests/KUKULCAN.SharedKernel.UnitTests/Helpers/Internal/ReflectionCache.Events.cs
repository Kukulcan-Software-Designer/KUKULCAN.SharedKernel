using System;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Provides event integration for <see cref="ReflectionCache"/>.
/// </summary>
internal static partial class ReflectionCache
{
    private static ReflectionCacheEvents Events { get; } = new();

    /// <summary>
    /// Occurs when an entry is added to the reflection cache.
    /// </summary>
    internal static event EventHandler<ReflectionCacheEntry>? EntryAdded
    {
        add => Events.EntryAdded += value;
        remove => Events.EntryAdded -= value;
    }

    /// <summary>
    /// Occurs when an entry is removed from the reflection cache.
    /// </summary>
    internal static event EventHandler<ReflectionCacheEntry>? EntryRemoved
    {
        add => Events.EntryRemoved += value;
        remove => Events.EntryRemoved -= value;
    }

    /// <summary>
    /// Occurs when an entry is invalidated.
    /// </summary>
    internal static event EventHandler<ReflectionCacheEntry>? EntryInvalidated
    {
        add => Events.EntryInvalidated += value;
        remove => Events.EntryInvalidated -= value;
    }

    /// <summary>
    /// Occurs when the reflection cache is cleared.
    /// </summary>
    internal static event EventHandler? CacheCleared
    {
        add => Events.CacheCleared += value;
        remove => Events.CacheCleared -= value;
    }

    private static void RaiseEntryAdded(ReflectionCacheEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        Events.RaiseAdded(entry);
    }

    private static void RaiseEntryRemoved(ReflectionCacheEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        Events.RaiseRemoved(entry);
    }

    private static void RaiseEntryInvalidated(ReflectionCacheEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        Events.RaiseInvalidated(entry);
    }

    private static void RaiseCacheCleared()
    {
        Events.RaiseCleared();
    }
}
