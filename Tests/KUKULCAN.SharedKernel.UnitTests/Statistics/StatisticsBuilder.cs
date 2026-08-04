using System.Collections.ObjectModel;

namespace KUKULCAN.SharedKernel.UnitTests.Statistics;

/// <summary>
/// Provides a fluent builder for creating statistics snapshots and distributions.
/// </summary>
public sealed class StatisticsBuilder
{
    private readonly Dictionary<string, object?> _metrics =
        new(StringComparer.Ordinal);

    /// <summary>
    /// Adds or replaces a metric.
    /// </summary>
    public StatisticsBuilder Add(
        string name,
        object? value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        _metrics[name] = value;

        return this;
    }

    /// <summary>
    /// Adds a metric only if it does not already exist.
    /// </summary>
    public StatisticsBuilder TryAdd(
        string name,
        object? value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        _metrics.TryAdd(name, value);

        return this;
    }

    /// <summary>
    /// Removes a metric.
    /// </summary>
    public StatisticsBuilder Remove(
        string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        _metrics.Remove(name);

        return this;
    }

    /// <summary>
    /// Clears every metric.
    /// </summary>
    public StatisticsBuilder Clear()
    {
        _metrics.Clear();

        return this;
    }

    /// <summary>
    /// Determines whether the specified metric exists.
    /// </summary>
    public bool Contains(
        string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return _metrics.ContainsKey(name);
    }

    /// <summary>
    /// Gets the current metric count.
    /// </summary>
    public int Count
        => _metrics.Count;

    /// <summary>
    /// Builds an immutable statistics snapshot.
    /// </summary>
    public StatisticsSnapshot Build()
    {
        return new StatisticsSnapshot(
            new ReadOnlyDictionary<string, object?>(
                new Dictionary<string, object?>(
                    _metrics,
                    StringComparer.Ordinal)));
    }

    /// <summary>
    /// Creates a distribution snapshot from a sequence of keys.
    /// </summary>
    public static DistributionSnapshot<TKey> BuildDistribution<TKey>(
        IEnumerable<TKey> values)
        where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(values);

        var grouped = values
            .GroupBy(static x => x)
            .OrderByDescending(static g => g.Count())
            .ThenBy(static g => g.Key);

        var total = grouped.Sum(static g => g.Count());

        var rank = 0;

        var entries = grouped.Select(g =>
            new DistributionEntry<TKey>
            {
                Key = g.Key,
                Count = g.Count(),
                Total = total,
                Rank = rank++
            });

        return new DistributionSnapshot<TKey>(entries);
    }

    /// <summary>
    /// Creates a distribution snapshot from an existing dictionary.
    /// </summary>
    public static DistributionSnapshot<TKey> BuildDistribution<TKey>(
        IReadOnlyDictionary<TKey, int> values)
        where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(values);

        var total = values.Values.Sum();

        var rank = 0;

        var entries = values
            .OrderByDescending(static x => x.Value)
            .ThenBy(static x => x.Key)
            .Select(x =>
                new DistributionEntry<TKey>
                {
                    Key = x.Key,
                    Count = x.Value,
                    Total = total,
                    Rank = rank++
                });

        return new DistributionSnapshot<TKey>(entries);
    }

    /// <summary>
    /// Creates a distribution snapshot from weighted values.
    /// </summary>
    public static DistributionSnapshot<TKey> BuildDistribution<TKey>(
        IEnumerable<KeyValuePair<TKey, int>> values)
        where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(values);

        return BuildDistribution(
            values.ToDictionary(
                static x => x.Key,
                static x => x.Value));
    }

    /// <summary>
    /// Creates an empty distribution.
    /// </summary>
    public static DistributionSnapshot<TKey> EmptyDistribution<TKey>()
        where TKey : notnull
    {
        return new DistributionSnapshot<TKey>(
            Enumerable.Empty<DistributionEntry<TKey>>());
    }

    /// <summary>
    /// Creates an empty statistics snapshot.
    /// </summary>
    public static StatisticsSnapshot Empty()
    {
        return new StatisticsSnapshot(
            Enumerable.Empty<KeyValuePair<string, object?>>());
    }
}
