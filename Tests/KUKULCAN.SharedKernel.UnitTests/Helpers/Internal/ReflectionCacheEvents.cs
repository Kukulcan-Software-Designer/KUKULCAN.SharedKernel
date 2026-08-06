using System;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Provides cache events.
/// </summary>
internal sealed class ReflectionCacheEvents
{
    public event EventHandler<ReflectionCacheEntry>? EntryAdded;

    public event EventHandler<ReflectionCacheEntry>? EntryRemoved;

    public event EventHandler<ReflectionCacheEntry>? EntryInvalidated;

    public event EventHandler? CacheCleared;

    internal void RaiseAdded(
        ReflectionCacheEntry entry)
    {
        EntryAdded?.Invoke(
            this,
            entry);
    }

    internal void RaiseRemoved(
        ReflectionCacheEntry entry)
    {
        EntryRemoved?.Invoke(
            this,
            entry);
    }

    internal void RaiseInvalidated(
        ReflectionCacheEntry entry)
    {
        EntryInvalidated?.Invoke(
            this,
            entry);
    }

    internal void RaiseCleared()
    {
        CacheCleared?.Invoke(
            this,
            EventArgs.Empty);
    }
}
