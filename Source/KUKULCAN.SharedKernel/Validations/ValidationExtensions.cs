using System;

namespace KUKULCAN.SharedKernel.Validations;

/// <summary>
/// Provides extension methods for creating validation results.
/// </summary>
public static class ValidationExtensions
{
    /// <summary>
    /// Creates a failed validation result from a single validation failure.
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

        return ValidationResult.Failure([failure]);
    }

    /// <summary>
    /// Creates a failed validation result from a collection of validation failures.
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
