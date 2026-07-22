using KUKULCAN.SharedKernel.Results.Internals;

namespace KUKULCAN.SharedKernel.Results;

/// <summary>
/// Provides factory methods for date validation errors.
/// </summary>
public static partial class ValidationErrors
{
    /// <summary>
    /// Creates an error indicating that a date must be in the past.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is <see langword="null"/>, empty or consists only of white-space characters.
    /// </exception>
    public static Error PastDate(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(
            CommonErrorCodes.PastDate,
            ValidationMessages.PastDate(propertyName));
    }

    /// <summary>
    /// Creates an error indicating that a date must be in the future.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is <see langword="null"/>, empty or consists only of white-space characters.
    /// </exception>
    public static Error FutureDate(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(
            CommonErrorCodes.FutureDate,
            ValidationMessages.FutureDate(propertyName));
    }
}
