using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace KUKULCAN.SharedKernel.UnitTests.Statistics;

/// <summary>
/// Provides formatting helpers for statistical values.
/// </summary>
public static class StatisticsFormatter
{
    /// <summary>
    /// Formats a percentage.
    /// </summary>
    public static string FormatPercentage(double value, int decimalDigits = StatisticsConstants.DefaultDecimalDigits)
    {
        return value.ToString($"P{decimalDigits}", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats a ratio.
    /// </summary>
    public static string FormatRatio(double value, int decimalDigits = StatisticsConstants.DefaultDecimalDigits)
    {
        return value.ToString($"F{decimalDigits}", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats a numeric value.
    /// </summary>
    public static string FormatNumber(double value, int decimalDigits = StatisticsConstants.DefaultDecimalDigits)
    {
        return value.ToString($"N{decimalDigits}", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats an integer.
    /// </summary>
    public static string FormatInteger(long value)
    {
        return value.ToString("N0", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats a size in bytes.
    /// </summary>
    public static string FormatBytes(long bytes)
    {
        const double kb = 1024d;
        const double mb = kb * 1024d;
        const double gb = mb * 1024d;
        const double tb = gb * 1024d;

        if (bytes < kb)
            return $"{bytes} B";
        if (bytes < mb)
            return $"{bytes / kb:N2} KB";
        if (bytes < gb)
            return $"{bytes / mb:N2} MB";

        return bytes < tb ? $"{bytes / gb:N2} GB" : $"{bytes / tb:N2} TB";
    }

    /// <summary>
    /// Formats a duration.
    /// </summary>
    public static string FormatDuration(TimeSpan duration)
    {
        if (duration.TotalMilliseconds < 1)
            return $"{duration.TotalMilliseconds:N3} ms";
        if (duration.TotalSeconds < 1)
            return $"{duration.TotalMilliseconds:N2} ms";
        if (duration.TotalMinutes < 1)
            return $"{duration.TotalSeconds:N2} s";
        if (duration.TotalHours < 1)
            return $"{duration.TotalMinutes:N2} min";

        return duration.TotalDays < 1 ? $"{duration.TotalHours:N2} h" : $"{duration.TotalDays:N2} d";
    }

    /// <summary>
    /// Formats a distribution entry.
    /// </summary>
    public static string FormatDistribution<TKey>(DistributionEntry<TKey> entry) where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(entry);

        return $"{entry.Key} : " + $"{FormatInteger(entry.Count)} " + $"({FormatPercentage(entry.Percentage)})";
    }

    /// <summary>
    /// Formats a collection of distribution entries using the platform newline.
    /// </summary>
    public static string FormatDistribution<TKey>(IEnumerable<DistributionEntry<TKey>> entries) where TKey : notnull
    {
        return FormatDistribution(entries, Environment.NewLine);
    }

    /// <summary>
    /// Formats a collection of distribution entries.
    /// </summary>
    public static string FormatDistribution<TKey>(IEnumerable<DistributionEntry<TKey>> entries, string separator)
        where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(entries);
        ArgumentNullException.ThrowIfNull(separator);

        return string.Join(separator, entries.Select(FormatDistribution));
    }

    /// <summary>
    /// Formats a statistics snapshot.
    /// </summary>
    public static string FormatSnapshot(StatisticsSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        return string.Join(Environment.NewLine, snapshot.Values.Select(static x => $"{x.Key} = {x.Value}"));
    }

    /// <summary>
    /// Formats a nullable value.
    /// </summary>
    public static string FormatNullable<T>(T? value, string nullText = "<null>") where T : class
    {
        return value?.ToString() ?? nullText;
    }

    /// <summary>
    /// Formats a nullable numeric value.
    /// </summary>
    public static string FormatNullable(double? value, int decimalDigits = StatisticsConstants.DefaultDecimalDigits)
    {
        return value.HasValue ? FormatNumber(value.Value, decimalDigits) : "<null>";
    }

    /// <summary>
    /// Formats a nullable duration.
    /// </summary>
    public static string FormatNullable(TimeSpan? value)
    {
        return value.HasValue ? FormatDuration(value.Value) : "<null>";
    }
}
