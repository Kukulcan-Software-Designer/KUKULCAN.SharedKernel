using System;
using KUKULCAN.SharedKernel.Results.Internals;

namespace KUKULCAN.SharedKernel.Results;

/// <summary>
/// Provides factory methods for enumeration validation errors.
/// </summary>
public static partial class ValidationErrors
{
    /// <summary>
    /// Creates an error indicating that an enumeration value is invalid.
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
    public static Error InvalidEnum(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(
            CommonErrorCodes.InvalidEnum,
            ValidationMessages.InvalidEnum(propertyName));
    }
}
