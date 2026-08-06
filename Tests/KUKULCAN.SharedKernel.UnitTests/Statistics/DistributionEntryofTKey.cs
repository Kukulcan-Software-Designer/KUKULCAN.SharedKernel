namespace KUKULCAN.SharedKernel.UnitTests.Statistics;

/// <summary>
/// Represents a single entry within a statistical distribution.
/// </summary>
/// <typeparam name="TKey">Type of the distribution key.</typeparam>
public sealed record DistributionEntry<TKey>
    where TKey : notnull
{
    /// <summary>
    /// Gets the distribution key.
    /// </summary>
    public required TKey Key { get; init; }

    /// <summary>
    /// Gets the number of occurrences.
    /// </summary>
    public required int Count { get; init; }

    /// <summary>
    /// Gets the total number of occurrences.
    /// </summary>
    public required int Total { get; init; }

    /// <summary>
    /// Gets the zero-based ranking position.
    /// </summary>
    public int Rank { get; init; }

    /// <summary>
    /// Gets the percentage represented by this entry.
    /// </summary>
    public double Percentage
    {
        get
        {
            if (Total == StatisticsConstants.EmptyCount)
                return StatisticsConstants.ZeroPercent;

            return (double)Count / Total;
        }
    }

    /// <summary>
    /// Gets the percentage in the range 0..100.
    /// </summary>
    public double PercentageValue
        => Percentage * StatisticsConstants.PercentMultiplier;

    /// <summary>
    /// Gets whether this entry is the dominant one.
    /// </summary>
    public bool IsDominant
        => Percentage >= StatisticsConstants.DominantThreshold;

    /// <summary>
    /// Gets whether this entry is considered rare.
    /// </summary>
    public bool IsRare
        => Percentage <= StatisticsConstants.RareThreshold;

    /// <summary>
    /// Gets whether this entry represents a single occurrence.
    /// </summary>
    public bool IsSingleOccurrence
        => Count == StatisticsConstants.SingleOccurrence;

    /// <summary>
    /// Gets whether this entry is empty.
    /// </summary>
    public bool IsEmpty
        => Count == StatisticsConstants.EmptyCount;

    /// <summary>
    /// Gets whether this entry represents the whole population.
    /// </summary>
    public bool IsComplete
        => Count == Total;

    /// <summary>
    /// Gets the inverse percentage.
    /// </summary>
    public double RemainingPercentage
        => StatisticsConstants.FullPercent - Percentage;

    /// <summary>
    /// Gets the inverse percentage expressed in the range 0..100.
    /// </summary>
    public double RemainingPercentageValue
        => RemainingPercentage * StatisticsConstants.PercentMultiplier;

    /// <summary>
    /// Determines whether this entry contains more occurrences than another.
    /// </summary>
    public bool IsGreaterThan(
        DistributionEntry<TKey> other)
    {
        ArgumentNullException.ThrowIfNull(other);

        return Count > other.Count;
    }

    /// <summary>
    /// Determines whether this entry contains fewer occurrences than another.
    /// </summary>
    public bool IsLessThan(
        DistributionEntry<TKey> other)
    {
        ArgumentNullException.ThrowIfNull(other);

        return Count < other.Count;
    }

    /// <summary>
    /// Determines whether this entry has the same number of occurrences as another.
    /// </summary>
    public bool HasSameCountAs(
        DistributionEntry<TKey> other)
    {
        ArgumentNullException.ThrowIfNull(other);

        return Count == other.Count;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Rank + 1}. {Key} : {Count} ({PercentageValue:F2}%)";
    }
}
