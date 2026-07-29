using System;
using KUKULCAN.SharedKernel.Exceptions;

namespace KUKULCAN.SharedKernel.Validations;

/// <summary>
/// Provides fail-fast extensions for validation.
/// </summary>
public static class ValidationThrowExtensions
{
    /// <summary>
    /// Throws a <see cref="ValidationException"/> when validation failed.
    /// </summary>
    /// <param name="validationResult">
    /// Validation result.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="validationResult"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ValidationException">
    /// Validation failed.
    /// </exception>
    public static void ThrowIfInvalid(this ValidationResult validationResult)
    {
        ArgumentNullException.ThrowIfNull(validationResult);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult);
        }
    }
}
