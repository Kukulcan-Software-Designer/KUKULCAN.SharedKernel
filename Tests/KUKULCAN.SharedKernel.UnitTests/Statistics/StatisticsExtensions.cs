using System.Collections.ObjectModel;

namespace KUKULCAN.SharedKernel.UnitTests.Statistics;

/// <summary>
/// Provides extension methods for the statistics infrastructure.
/// </summary>
public static class StatisticsExtensions
{
    #region IEnumerable

    /// <summary>
    /// Creates a distribution from the specified sequence.
    /// </summary>
    public static DistributionSnapshot<T> ToDistribution<T>(
        this IEnumerable<T> source)
        where T : notnull
    {
        ArgumentNullException.ThrowIfNull(source);

        return StatisticsBuilder.BuildDistribution(source);
    }

    /// <summary>
    /// Creates a distribution using the specified selector.
    /// </summary>
    public static DistributionSnapshot<TKey> ToDistribution<TSource, TKey>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> selector)
        where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(selector);

        return StatisticsBuilder.BuildDistribution(
            source.Select(selector));
    }

    /// <summary>
    /// Calculates the arithmetic mean.
    /// </summary>
    public static double Mean(
        this IEnumerable<double> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return StatisticsMath.Mean(source);
    }

    /// <summary>
    /// Calculates the arithmetic mean.
    /// </summary>
    public static double Mean(
        this IEnumerable<int> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return StatisticsMath.Mean(source);
    }

    /// <summary>
    /// Calculates the median.
    /// </summary>
    public static double Median(
        this IEnumerable<double> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return StatisticsMath.Median(source);
    }

    /// <summary>
    /// Calculates the variance.
    /// </summary>
    public static double Variance(
        this IEnumerable<double> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return StatisticsMath.Variance(source);
    }

    /// <summary>
    /// Calculates the standard deviation.
    /// </summary>
    public static double StandardDeviation(
        this IEnumerable<double> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return StatisticsMath.StandardDeviation(source);
    }

    /// <summary>
    /// Calculates the coefficient of variation.
    /// </summary>
    public static double CoefficientOfVariation(
        this IEnumerable<double> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return StatisticsMath.CoefficientOfVariation(source);
    }

    #endregion

    #region Dictionary

    /// <summary>
    /// Converts a dictionary into a distribution.
    /// </summary>
    public static DistributionSnapshot<TKey> ToDistribution<TKey>(
        this IReadOnlyDictionary<TKey, int> source)
        where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(source);

        return StatisticsBuilder.BuildDistribution(source);
    }

    /// <summary>
    /// Converts a dictionary into a distribution.
    /// </summary>
    public static DistributionSnapshot<TKey> ToDistribution<TKey>(
        this IDictionary<TKey, int> source)
        where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(source);

        return StatisticsBuilder.BuildDistribution(
            new ReadOnlyDictionary<TKey, int>(source));
    }

    #endregion

    #region StatisticsSnapshot

    /// <summary>
    /// Gets a required metric.
    /// </summary>
    public static T Require<T>(
        this StatisticsSnapshot snapshot,
        string metricName)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        return snapshot.GetRequired<T>(metricName);
    }

    /// <summary>
    /// Determines whether a metric exists.
    /// </summary>
    public static bool HasMetric(
        this StatisticsSnapshot snapshot,
        string metricName)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        return snapshot.Contains(metricName);
    }

    /// <summary>
    /// Enumerates metric names.
    /// </summary>
    public static IEnumerable<string> MetricNames(
        this StatisticsSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        return snapshot.Names;
    }

    /// <summary>
    /// Enumerates metric values.
    /// </summary>
    public static IEnumerable<object?> MetricValues(
        this StatisticsSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        return snapshot.Metrics;
    }

    #endregion

    #region DistributionSnapshot

    /// <summary>
    /// Gets the dominant entry.
    /// </summary>
    public static DistributionEntry<TKey>? Dominant<TKey>(
        this DistributionSnapshot<TKey> snapshot)
        where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        return snapshot.Dominant;
    }

    /// <summary>
    /// Gets the top N entries.
    /// </summary>
    public static IReadOnlyList<DistributionEntry<TKey>> Top<TKey>(
        this DistributionSnapshot<TKey> snapshot,
        int count)
        where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        return snapshot.Top(count);
    }

    /// <summary>
    /// Gets the bottom N entries.
    /// </summary>
    public static IReadOnlyList<DistributionEntry<TKey>> Bottom<TKey>(
        this DistributionSnapshot<TKey> snapshot,
        int count)
        where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        return snapshot.Bottom(count);
    }

    /// <summary>
    /// Determines whether the specified key exists.
    /// </summary>
    public static bool ContainsKey<TKey>(
        this DistributionSnapshot<TKey> snapshot,
        TKey key)
        where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        return snapshot.Contains(key);
    }

    #endregion

    #region Formatting

    /// <summary>
    /// Formats a statistics snapshot.
    /// </summary>
    public static string ToStatisticsString(
        this StatisticsSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        return StatisticsFormatter.FormatSnapshot(snapshot);
    }

    /// <summary>
    /// Formats a distribution.
    /// </summary>
    public static string ToStatisticsString<TKey>(
        this DistributionSnapshot<TKey> snapshot)
        where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        return StatisticsFormatter.FormatDistribution(snapshot.Entries);
    }

    #endregion
}
