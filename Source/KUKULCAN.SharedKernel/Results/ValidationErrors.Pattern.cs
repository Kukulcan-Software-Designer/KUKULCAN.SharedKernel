using KUKULCAN.SharedKernel.Results.Internals;

namespace KUKULCAN.SharedKernel.Results;

/// <summary>
/// Provides factory methods for pattern validation errors.
/// </summary>
public static partial class ValidationErrors
{
    /// <summary>
    /// Creates an error indicating that the value has an invalid format.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is null, empty or consists only of white-space characters.
    /// </exception>
    public static Error InvalidFormat(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(
            CommonErrorCodes.InvalidFormat,
            ValidationMessages.InvalidFormat(propertyName));
    }

    /// <summary>
    /// Creates an error indicating that the value does not match the required pattern.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is null, empty or consists only of white-space characters.
    /// </exception>
    public static Error InvalidPattern(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(
            CommonErrorCodes.InvalidPattern,
            ValidationMessages.InvalidPattern(propertyName));
    }

    /// <summary>
    /// Creates an error indicating that an e-mail address is invalid.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is null, empty or consists only of white-space characters.
    /// </exception>
    public static Error InvalidEmail(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(
            CommonErrorCodes.InvalidEmail,
            ValidationMessages.InvalidEmail(propertyName));
    }

    /// <summary>
    /// Creates an error indicating that a phone number is invalid.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is null, empty or consists only of white-space characters.
    /// </exception>
    public static Error InvalidPhone(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(
            CommonErrorCodes.InvalidPhone,
            ValidationMessages.InvalidPhone(propertyName));
    }

    /// <summary>
    /// Creates an error indicating that a URL is invalid.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is null, empty or consists only of white-space characters.
    /// </exception>
    public static Error InvalidUrl(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(
            CommonErrorCodes.InvalidUrl,
            ValidationMessages.InvalidUrl(propertyName));
    }
}
