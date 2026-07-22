using KUKULCAN.SharedKernel.Results.Internals;

namespace KUKULCAN.SharedKernel.Results;

/// <summary>
/// Provides factory methods for length validation errors.
/// </summary>
public static partial class ValidationErrors
{
    /// <summary>
    /// Creates an error indicating that the value length is below the minimum allowed.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <param name="minimumLength">
    /// Minimum allowed length.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is null, empty or consists only of white-space characters.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="minimumLength"/> is less than or equal to zero.
    /// </exception>
    public static Error MinLength(
        string propertyName,
        int minimumLength)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(minimumLength);

        return new Error(
            CommonErrorCodes.MinLength,
            ValidationMessages.MinLength(propertyName, minimumLength));
    }

    /// <summary>
    /// Creates an error indicating that the value length exceeds the maximum allowed.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <param name="maximumLength">
    /// Maximum allowed length.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is null, empty or consists only of white-space characters.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="maximumLength"/> is less than or equal to zero.
    /// </exception>
    public static Error MaxLength(
        string propertyName,
        int maximumLength)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maximumLength);

        return new Error(
            CommonErrorCodes.MaxLength,
            ValidationMessages.MaxLength(propertyName, maximumLength));
    }

    /// <summary>
    /// Creates an error indicating that the value length does not match the expected length.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <param name="expectedLength">
    /// Expected length.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is null, empty or consists only of white-space characters.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="expectedLength"/> is less than or equal to zero.
    /// </exception>
    public static Error ExactLength(
        string propertyName,
        int expectedLength)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(expectedLength);

        return new Error(
            CommonErrorCodes.ExactLength,
            ValidationMessages.ExactLength(propertyName, expectedLength));
    }
}
