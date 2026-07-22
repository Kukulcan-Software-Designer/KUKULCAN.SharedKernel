using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace KUKULCAN.SharedKernel.Guards;

/// <summary>
/// Provides helper methods to validate arguments.
/// </summary>
public static class Guard
{
    /// <summary>
    /// Ensures the value is not null.
    /// </summary>
    public static T NotNull<T>(
        [NotNull] T? value,
        [CallerArgumentExpression(nameof(value))]
        string? parameterName = null)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(value, parameterName);

        return value;
    }

    /// <summary>
    /// Ensures the string is not null or whitespace.
    /// </summary>
    public static string NotNullOrWhiteSpace(
        string? value,
        [CallerArgumentExpression(nameof(value))]
        string? parameterName = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, parameterName);

        return value;
    }

    /// <summary>
    /// Ensures the Guid is not empty.
    /// </summary>
    public static Guid NotEmpty(
        Guid value,
        [CallerArgumentExpression(nameof(value))]
        string? parameterName = null)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException(
                "The Guid cannot be empty.",
                parameterName);
        }

        return value;
    }

    /// <summary>
    /// Ensures the value is not the default value.
    /// </summary>
    public static T NotDefault<T>(
        T value,
        [CallerArgumentExpression(nameof(value))]
        string? parameterName = null)
        where T : struct
    {
        if (EqualityComparer<T>.Default.Equals(value, default))
        {
            throw new ArgumentException(
                "The value cannot be the default value.",
                parameterName);
        }

        return value;
    }

    /// <summary>
    /// Ensures the collection is not null or empty.
    /// </summary>
    public static IReadOnlyCollection<T> NotNullOrEmpty<T>(
        IReadOnlyCollection<T>? value,
        [CallerArgumentExpression(nameof(value))]
        string? parameterName = null)
    {
        ArgumentNullException.ThrowIfNull(value, parameterName);

        if (value.Count == 0)
        {
            throw new ArgumentException(
                "The collection cannot be empty.",
                parameterName);
        }

        return value;
    }

    /// <summary>
    /// Ensures the value is greater than zero.
    /// </summary>
    public static int Positive(
        int value,
        [CallerArgumentExpression(nameof(value))]
        string? parameterName = null)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(parameterName);
        }

        return value;
    }

    /// <summary>
    /// Ensures the value is greater than zero.
    /// </summary>
    public static decimal Positive(
        decimal value,
        [CallerArgumentExpression(nameof(value))]
        string? parameterName = null)
    {
        if (value <= 0m)
        {
            throw new ArgumentOutOfRangeException(parameterName);
        }

        return value;
    }

    /// <summary>
    /// Ensures the value is inside the specified range.
    /// </summary>
    public static T InRange<T>(
        T value,
        T minimum,
        T maximum,
        [CallerArgumentExpression(nameof(value))]
        string? parameterName = null)
        where T : IComparable<T>
    {
        if (value.CompareTo(minimum) < 0 ||
            value.CompareTo(maximum) > 0)
        {
            throw new ArgumentOutOfRangeException(parameterName);
        }

        return value;
    }

    /// <summary>
    /// Throws an exception when the specified condition is true.
    /// </summary>
    public static void Against(
        bool condition,
        string message,
        [CallerArgumentExpression(nameof(condition))]
        string? parameterName = null)
    {
        if (condition)
        {
            throw new ArgumentException(
                message,
                parameterName);
        }
    }
}
