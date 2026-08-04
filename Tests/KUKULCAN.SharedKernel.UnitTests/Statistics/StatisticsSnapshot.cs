using System.Collections.ObjectModel;

namespace KUKULCAN.SharedKernel.UnitTests.Statistics;

/// <summary>
/// Represents an immutable snapshot of a set of statistics.
/// </summary>
public sealed class StatisticsSnapshot
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StatisticsSnapshot"/> class.
    /// </summary>
    public StatisticsSnapshot(
        IEnumerable<KeyValuePair<string, object?>> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        Values = new ReadOnlyDictionary<string, object?>(
            values.ToDictionary(
                kv => kv.Key,
                kv => kv.Value,
                StringComparer.Ordinal));
    }

    /// <summary>
    /// Gets the stored statistics.
    /// </summary>
    public IReadOnlyDictionary<string, object?> Values { get; }

    /// <summary>
    /// Gets the number of stored metrics.
    /// </summary>
    public int Count
        => Values.Count;

    /// <summary>
    /// Determines whether the snapshot contains the specified metric.
    /// </summary>
    public bool Contains(
        string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return Values.ContainsKey(name);
    }

    /// <summary>
    /// Gets a metric.
    /// </summary>
    public object? Get(
        string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return Values.GetValueOrDefault(name);
    }

    /// <summary>
    /// Gets a metric strongly typed.
    /// </summary>
    public T? Get<T>(
        string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (!Values.TryGetValue(name, out var value))
            return default;

        if (value is T typed)
            return typed;

        return default;
    }

    /// <summary>
    /// Gets a required metric.
    /// </summary>
    public T GetRequired<T>(
        string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (!Values.TryGetValue(name, out var value))
            throw new KeyNotFoundException(
                $"Statistic '{name}' was not found.");

        if (value is not T typed)
        {
            throw new InvalidCastException(
                $"Statistic '{name}' is not of type {typeof(T).FullName}.");
        }

        return typed;
    }

    /// <summary>
    /// Gets every metric name.
    /// </summary>
    public IReadOnlyCollection<string> Names
        => Values.Keys.ToArray();

    /// <summary>
    /// Gets every metric value.
    /// </summary>
    public IReadOnlyCollection<object?> Metrics
        => Values.Values.ToArray();

    /// <summary>
    /// Returns all numeric metrics.
    /// </summary>
    public IReadOnlyDictionary<string, double> NumericMetrics
        => Values
            .Where(static x =>
                x.Value is byte
                || x.Value is short
                || x.Value is int
                || x.Value is long
                || x.Value is float
                || x.Value is double
                || x.Value is decimal)
            .ToDictionary(
                x => x.Key,
                x => Convert.ToDouble(x.Value));

    /// <summary>
    /// Returns every metric that satisfies the specified predicate.
    /// </summary>
    public IReadOnlyDictionary<string, object?> Where(
        Func<KeyValuePair<string, object?>, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return Values
            .Where(predicate)
            .ToDictionary(
                x => x.Key,
                x => x.Value);
    }

    /// <summary>
    /// Creates a copy of this snapshot.
    /// </summary>
    public StatisticsSnapshot Clone()
    {
        return new StatisticsSnapshot(Values);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"Statistics: {Count} metrics";
    }
}
