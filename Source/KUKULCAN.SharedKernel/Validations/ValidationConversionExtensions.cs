using System;
using KUKULCAN.SharedKernel.Results;

namespace KUKULCAN.SharedKernel.Validations;

/// <summary>
/// Provides conversion methods for validation types.
/// </summary>
public static class ValidationConversionExtensions
{
    /// <summary>
    /// Converts a validation failure into a validation result.
    /// </summary>
    /// <param name="failure">
    /// Validation failure.
    /// </param>
    /// <returns>
    /// A failed <see cref="ValidationResult"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="failure"/> is <see langword="null"/>.
    /// </exception>
    public static ValidationResult ToValidationResult(this ValidationFailure failure)
    {
        ArgumentNullException.ThrowIfNull(failure);

        return ValidationResult.Failure(
            [failure]);
    }

    /// <summary>
    /// Converts a sequence of validation failures into a validation result.
    /// </summary>
    /// <param name="failures">
    /// Validation failures.
    /// </param>
    /// <returns>
    /// A failed <see cref="ValidationResult"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="failures"/> is <see langword="null"/>.
    /// </exception>
    public static ValidationResult ToValidationResult(this IEnumerable<ValidationFailure> failures)
    {
        ArgumentNullException.ThrowIfNull(failures);

        return ValidationResult.Failure(failures);
    }
}
