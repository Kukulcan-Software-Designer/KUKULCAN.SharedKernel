using System;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Represents the configuration options for <see cref="ReflectionCache"/>.
/// </summary>
internal sealed class ReflectionCacheOptions
{
    /// <summary>
    /// Gets or sets the default expiration time.
    /// </summary>
    public TimeSpan DefaultExpiration { get; set; } = TimeSpan.FromMinutes(30);

    /// <summary>
    /// Gets or sets the maximum number of entries.
    /// </summary>
    public int MaximumEntries { get; set; } = 4096;

    /// <summary>
    /// Gets or sets a value indicating whether expired entries
    /// are removed automatically.
    /// </summary>
    public bool RemoveExpiredEntries { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether statistics are enabled.
    /// </summary>
    public bool EnableStatistics { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether events are enabled.
    /// </summary>
    public bool EnableEvents { get; set; } = true;
}
