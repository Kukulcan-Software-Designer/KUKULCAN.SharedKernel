using System;

namespace KUKULCAN.SharedKernel.Results.Internals;

/// <summary>
/// Provides standard validation error messages.
/// </summary>
internal static class ValidationMessages
{
    /// <summary>
    /// Returns the message indicating that one or more validation failures occurred.
    /// </summary>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string ValidationFailed()
    {
        return "One or more validation failures occurred.";
    }

    #region Required

    /// <summary>
    /// Returns the message indicating that a required value is missing.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string Required(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' is required.";
    }

    /// <summary>
    /// Returns the message indicating that the value must be <see langword="null"/>.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string Null(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' must be null.";
    }

    /// <summary>
    /// Returns the message indicating that the value cannot be empty.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string Empty(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' cannot be empty.";
    }

    #endregion

    #region Length

    /// <summary>
    /// Returns the message indicating that the value length is below the minimum allowed.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <param name="minimumLength">
    /// Minimum allowed length.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string MinLength(string propertyName, int minimumLength)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(minimumLength);

        return $"'{propertyName}' must be at least {minimumLength} characters long.";
    }

    /// <summary>
    /// Returns the message indicating that the value length exceeds the maximum allowed.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <param name="maximumLength">
    /// Maximum allowed length.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string MaxLength(string propertyName, int maximumLength)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumLength);

        return $"'{propertyName}' must not exceed {maximumLength} characters.";
    }

    /// <summary>
    /// Returns the message indicating that the value length does not match the expected length.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <param name="expectedLength">
    /// Expected length.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string ExactLength(string propertyName, int expectedLength)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(expectedLength);

        return $"'{propertyName}' must be exactly {expectedLength} characters long.";
    }

    #endregion

    #region Numeric

    /// <summary>
    /// Returns the message indicating that the value must be greater than the specified limit.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the comparison value.
    /// </typeparam>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <param name="limit">
    /// Comparison limit.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string GreaterThan<T>(string propertyName, T limit)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' must be greater than '{limit}'.";
    }

    /// <summary>
    /// Returns the message indicating that the value must be greater than or equal to the specified limit.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the comparison value.
    /// </typeparam>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <param name="limit">
    /// Comparison limit.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string GreaterThanOrEqual<T>(string propertyName, T limit)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' must be greater than or equal to '{limit}'.";
    }

    /// <summary>
    /// Returns the message indicating that the value must be less than the specified limit.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the comparison value.
    /// </typeparam>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <param name="limit">
    /// Comparison limit.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string LessThan<T>(string propertyName, T limit)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' must be less than '{limit}'.";
    }

    /// <summary>
    /// Returns the message indicating that the value must be less than or equal to the specified limit.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the comparison value.
    /// </typeparam>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <param name="limit">
    /// Comparison limit.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string LessThanOrEqual<T>(string propertyName, T limit)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' must be less than or equal to '{limit}'.";
    }

    /// <summary>
    /// Returns the message indicating that the value is outside the allowed range.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the comparison value.
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
    /// The formatted message.
    /// </returns>
    internal static string Between<T>(string propertyName, T minimum, T maximum)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' must be between '{minimum}' and '{maximum}'.";
    }

    #endregion

    #region Pattern

    /// <summary>
    /// Returns the message indicating that the value has an invalid format.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string InvalidFormat(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' has an invalid format.";
    }

    /// <summary>
    /// Returns the message indicating that the value does not match the required pattern.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string InvalidPattern(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' does not match the required pattern.";
    }

    /// <summary>
    /// Returns the message indicating that the e-mail address is invalid.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string InvalidEmail(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' is not a valid e-mail address.";
    }

    /// <summary>
    /// Returns the message indicating that the phone number is invalid.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string InvalidPhone(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' is not a valid phone number.";
    }

    /// <summary>
    /// Returns the message indicating that the URL is invalid.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string InvalidUrl(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' is not a valid URL.";
    }

    #endregion

    #region Collection

    /// <summary>
    /// Returns the message indicating that the collection must be empty.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string CollectionMustBeEmpty(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' must be empty.";
    }

    /// <summary>
    /// Returns the message indicating that the collection cannot be empty.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string CollectionMustNotBeEmpty(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' must not be empty.";
    }

    /// <summary>
    /// Returns the message indicating that the collection contains duplicate values.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string Duplicate(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' contains duplicate values.";
    }

    /// <summary>
    /// Returns the message indicating that the collection contains an invalid item.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string InvalidCollectionItem(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' contains an invalid item.";
    }

    #endregion

    #region Date

    /// <summary>
    /// Returns the message indicating that the date must be in the past.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string PastDate(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' must be a date in the past.";
    }

    /// <summary>
    /// Returns the message indicating that the date must be in the future.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string FutureDate(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' must be a date in the future.";
    }

    #endregion

    #region Enum

    /// <summary>
    /// Returns the message indicating that the enumeration value is invalid.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string InvalidEnum(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return $"'{propertyName}' contains an invalid value.";
    }

    #endregion
}
