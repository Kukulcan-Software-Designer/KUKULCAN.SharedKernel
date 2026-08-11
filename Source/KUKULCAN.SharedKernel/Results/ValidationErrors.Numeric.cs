using KUKULCAN.SharedKernel.Results.Internals;

namespace KUKULCAN.SharedKernel.Results;

/// <summary>
/// Provides factory methods for numeric comparison validation errors.
/// </summary>
public static partial class ValidationErrors
{
    /// <summary>
    /// Creates an error indicating that the value must be greater than the specified limit.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the compared value.
    /// </typeparam>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <param name="limit">
    /// Comparison limit.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is null, empty or consists only of white-space characters.
    /// </exception>
    public static Error GreaterThan<T>(string propertyName, T limit) where T : IComparable<T>
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(CommonErrorCodes.GreaterThan, ValidationMessages.GreaterThan(propertyName, limit));
    }

    /// <summary>
    /// Creates an error indicating that the value must be greater than or equal to the specified limit.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the compared value.
    /// </typeparam>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <param name="limit">
    /// Comparison limit.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is null, empty or consists only of white-space characters.
    /// </exception>
    public static Error GreaterThanOrEqual<T>(string propertyName, T limit) where T : IComparable<T>
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(CommonErrorCodes.GreaterThanOrEqual, ValidationMessages.GreaterThanOrEqual(propertyName, limit));
    }

    /// <summary>
    /// Creates an error indicating that the value must be less than the specified limit.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the compared value.
    /// </typeparam>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <param name="limit">
    /// Comparison limit.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is null, empty or consists only of white-space characters.
    /// </exception>
    public static Error LessThan<T>(string propertyName, T limit) where T : IComparable<T>
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(CommonErrorCodes.LessThan, ValidationMessages.LessThan(propertyName, limit));
    }

    /// <summary>
    /// Creates an error indicating that the value must be less than or equal to the specified limit.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the compared value.
    /// </typeparam>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <param name="limit">
    /// Comparison limit.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is null, empty or consists only of white-space characters.
    /// </exception>
    public static Error LessThanOrEqual<T>(string propertyName, T limit) where T : IComparable<T>
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(CommonErrorCodes.LessThanOrEqual, ValidationMessages.LessThanOrEqual(propertyName, limit));
    }

    /// <summary>
    /// Creates an error indicating that the value is outside the allowed range.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the compared value.
    /// </typeparam>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <param name="minimum">
    /// Minimum allowed value.
    /// </param>
    /// <param name="maximum">
    /// Maximum allowed value.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is null, empty or consists only of white-space characters.
    /// </exception>
    public static Error Between<T>(string propertyName, T minimum, T maximum) where T : IComparable<T>
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(CommonErrorCodes.Between, ValidationMessages.Between(propertyName, minimum, maximum));
    }
}
