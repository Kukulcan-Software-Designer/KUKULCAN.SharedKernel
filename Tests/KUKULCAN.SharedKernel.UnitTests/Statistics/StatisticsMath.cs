using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace KUKULCAN.SharedKernel.UnitTests.Statistics;

/// <summary>
/// Provides common statistical calculations.
/// </summary>
public static class StatisticsMath
{
    /// <summary>
    /// Calculates a ratio.
    /// </summary>
    public static double Ratio(double numerator, double denominator)
    {
        if (Math.Abs(denominator) <= StatisticsConstants.DefaultTolerance)
            return StatisticsConstants.ZeroPercent;

        return numerator / denominator;
    }

    /// <summary>
    /// Calculates a percentage.
    /// </summary>
    public static double Percentage(double numerator, double denominator)
    {
        return Ratio(numerator, denominator) * StatisticsConstants.PercentMultiplier;
    }

    /// <summary>
    /// Calculates the arithmetic mean.
    /// </summary>
    public static double Mean(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        double[] array = values.ToArray();

        if (array.Length == 0)
            return 0;

        return array.Average();
    }

    /// <summary>
    /// Calculates the arithmetic mean.
    /// </summary>
    public static double Mean(IEnumerable<int> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        return Mean(values.Select(static x => (double)x));
    }

    /// <summary>
    /// Calculates the arithmetic mean.
    /// </summary>
    public static double Mean(IEnumerable<long> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        return Mean(values.Select(static x => (double)x));
    }

    /// <summary>
    /// Calculates the median.
    /// </summary>
    public static double Median(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        double[] array = values.OrderBy(static x => x).ToArray();

        if (array.Length == 0)
            return 0;

        int middle = array.Length / 2;

        if ((array.Length & 1) == 0)
            return (array[middle - 1] + array[middle]) / 2.0;

        return array[middle];
    }

    /// <summary>
    /// Calculates the minimum value.
    /// </summary>
    public static double Minimum(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        double[] array = values.ToArray();

        if (array.Length == 0)
            return 0;

        return array.Min();
    }

    /// <summary>
    /// Calculates the maximum value.
    /// </summary>
    public static double Maximum(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        double[] array = values.ToArray();

        if (array.Length == 0)
            return 0;

        return array.Max();
    }

    /// <summary>
    /// Calculates the range.
    /// </summary>
    public static double Range(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        double[] array = values.ToArray();

        if (array.Length == 0)
            return 0;

        return array.Max() - array.Min();
    }

    /// <summary>
    /// Calculates the population variance.
    /// </summary>
    public static double Variance(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        double[] array = [.. values];
        if (array.Length == 0)
            return 0;

        double mean = array.Average();

        return array
            .Select(x => Math.Pow(x - mean, 2))
            .Average();
    }

    /// <summary>
    /// Calculates the standard deviation.
    /// </summary>
    public static double StandardDeviation(IEnumerable<double> values)
    {
        return Math.Sqrt(Variance(values));
    }

    /// <summary>
    /// Calculates the coefficient of variation.
    /// </summary>
    public static double CoefficientOfVariation(IEnumerable<double> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        double mean = Mean(values);
        if (Math.Abs(mean) <= StatisticsConstants.DefaultTolerance)
            return 0;

        return StandardDeviation(values) / mean;
    }

    /// <summary>
    /// Determines whether two values are approximately equal.
    /// </summary>
    public static bool NearlyEquals(double left, double right, double tolerance = StatisticsConstants.DefaultTolerance)
    {
        return Math.Abs(left - right) <= tolerance;
    }

    /// <summary>
    /// Clamps a value.
    /// </summary>
    public static double Clamp(double value, double minimum, double maximum)
    {
        return Math.Min(Math.Max(value, minimum), maximum);
    }

    /// <summary>
    /// Normalizes a value to the range 0..1.
    /// </summary>
    public static double Normalize(double value, double minimum, double maximum)
    {
        if (NearlyEquals(maximum, minimum))
            return 0;

        return Clamp((value - minimum) / (maximum - minimum), 0, 1);
    }

    /// <summary>
    /// Calculates a weighted mean.
    /// </summary>
    public static double WeightedMean(IEnumerable<double> values, IEnumerable<double> weights)
    {
        ArgumentNullException.ThrowIfNull(values);
        ArgumentNullException.ThrowIfNull(weights);

        double[] valueArray = [.. values];
        double[] weightArray = [.. weights];

        if (valueArray.Length != weightArray.Length)
            throw new ArgumentException("Values and weights must have the same length.");

        if (valueArray.Length == 0)
            return 0;

        double totalWeight = weightArray.Sum();

        if (NearlyEquals(totalWeight, 0))
            return 0;

        double total = 0;

        for (int i = 0; i < valueArray.Length; i++)
        {
            total += valueArray[i] * weightArray[i];
        }

        return total / totalWeight;
    }

    /// <summary>
    /// Attempts to calculate the mean.
    /// </summary>
    public static bool TryMean(IEnumerable<double> values, [NotNullWhen(true)] out double? result)
    {
        ArgumentNullException.ThrowIfNull(values);

        double[] array = values.ToArray();

        if (array.Length == 0)
        {
            result = null;
            return false;
        }

        result = array.Average();
        return true;
    }
}
