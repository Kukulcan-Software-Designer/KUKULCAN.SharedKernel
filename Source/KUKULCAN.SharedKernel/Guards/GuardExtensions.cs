using System;

namespace KUKULCAN.SharedKernel.Guards;

/// <summary>
/// Extension methods for Guard.
/// </summary>
public static class GuardExtensions
{
    /// <summary>
    /// Ensures the value is not null.
    /// </summary>
    public static T ThrowIfNull<T>(this T? value)
        where T : class
        => Guard.NotNull(value);

    /// <summary>
    /// Ensures the string is not null or whitespace.
    /// </summary>
    public static string ThrowIfNullOrWhiteSpace(
        this string? value)
        => Guard.NotNullOrWhiteSpace(value);

    /// <summary>
    /// Ensures the Guid is not empty.
    /// </summary>
    public static Guid ThrowIfEmpty(
        this Guid value)
        => Guard.NotEmpty(value);
}
