using System;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Represents a snapshot of the current reflection cache statistics.
/// </summary>
internal sealed record ReflectionCacheStatistics
{
    /// <summary>
    /// Gets the number of entries currently stored in the cache.
    /// </summary>
    public int EntryCount { get; init; }

    /// <summary>
    /// Gets the total number of successful cache lookups.
    /// </summary>
    public long HitCount { get; init; }

    /// <summary>
    /// Gets the total number of failed cache lookups.
    /// </summary>
    public long MissCount { get; init; }

    /// <summary>
    /// Gets the total number of cache insertions.
    /// </summary>
    public long AddCount { get; init; }

    /// <summary>
    /// Gets the total number of cache removals.
    /// </summary>
    public long RemoveCount { get; init; }

    /// <summary>
    /// Gets the total number of cleanup operations.
    /// </summary>
    public long CleanupCount { get; init; }

    /// <summary>
    /// Gets the total number of invalidation operations.
    /// </summary>
    public long InvalidationCount { get; init; }

    /// <summary>
    /// Gets the UTC instant when this snapshot was created.
    /// </summary>
    public DateTimeOffset CreatedOn { get; init; } = DateTimeOffset.UtcNow;
}
