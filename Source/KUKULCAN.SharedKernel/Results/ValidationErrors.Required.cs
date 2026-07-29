using System;
using KUKULCAN.SharedKernel.Results.Internals;

namespace KUKULCAN.SharedKernel.Results;

/// <summary>
/// Provides factory methods for required-value validation errors.
/// </summary>
public static partial class ValidationErrors
{
    /// <summary>
    /// Creates an error indicating that a required value is missing.
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
    public static Error Required(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(
            CommonErrorCodes.Required,
            ValidationMessages.Required(propertyName));
    }

    /// <summary>
    /// Creates an error indicating that a value must be <see langword="null"/>.
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
    public static Error Null(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(
            CommonErrorCodes.Null,
            ValidationMessages.Null(propertyName));
    }

    /// <summary>
    /// Creates an error indicating that a value cannot be empty.
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
    public static Error Empty(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(
            CommonErrorCodes.Empty,
            ValidationMessages.Empty(propertyName));
    }
}
