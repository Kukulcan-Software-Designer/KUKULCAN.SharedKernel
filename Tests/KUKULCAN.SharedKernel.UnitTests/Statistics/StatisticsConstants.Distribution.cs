namespace KUKULCAN.SharedKernel.UnitTests.Statistics;

/// <summary>
/// Distribution constants.
/// </summary>
public static partial class StatisticsConstants
{
    /// <summary>
    /// Minimum percentage to consider an entry dominant.
    /// </summary>
    public const double DominantThreshold = 0.50;

    /// <summary>
    /// Maximum percentage to consider an entry rare.
    /// </summary>
    public const double RareThreshold = 0.01;

    /// <summary>
    /// Default histogram bucket count.
    /// </summary>
    public const int DefaultBucketCount = 10;

    /// <summary>
    /// Default maximum distribution entries.
    /// </summary>
    public const int DefaultMaximumEntries = 1000;
}
