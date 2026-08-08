using System.Collections.Concurrent;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Core infrastructure of the reflection cache.
/// </summary>
internal static partial class ReflectionCache
{
    /// <summary>
    /// Internal cache storage.
    /// </summary>
    private static readonly ConcurrentDictionary<
        ReflectionCacheKey,
        ReflectionCacheEntry> _entries = new();

    /// <summary>
    /// Synchronizes compound cache operations.
    /// </summary>
    private static readonly Lock _lock = new();

    /// <summary>
    /// Current cache configuration.
    /// </summary>
    private static ReflectionCacheOptions _options = new();

    /// <summary>
    /// Gets the configured cache options.
    /// </summary>
    public static ReflectionCacheOptions Options => _options;

    /// <summary>
    /// Configures the cache.
    /// </summary>
    public static void Configure(ReflectionCacheOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        using (_lock.EnterScope())
        {
            _options = options;
        }
    }

    /// <summary>
    /// Clears the cache.
    /// </summary>
    public static void Clear()
    {
        using (_lock.EnterScope())
        {
            _entries.Clear();
        }
    }

}
