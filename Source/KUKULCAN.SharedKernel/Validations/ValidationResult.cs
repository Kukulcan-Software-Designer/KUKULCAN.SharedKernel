using System;
using System.Linq;
using KUKULCAN.SharedKernel.Results;
using KUKULCAN.SharedKernel.Validations.Internals;

namespace KUKULCAN.SharedKernel.Validations;

/// <summary>
/// Represents the immutable result of a validation operation.
/// </summary>
public sealed record ValidationResult
{
    /// <summary>
    /// Represents a successful validation result.
    /// </summary>
    public static readonly ValidationResult Success = new(
        Array.Empty<ValidationFailure>());

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationResult"/> class.
    /// </summary>
    /// <param name="failures">
    /// Validation failures.
    /// </param>
    private ValidationResult(IEnumerable<ValidationFailure> failures)
    {
        ValidationFailure[] array = failures.ToArray();

        Failures = Array.AsReadOnly(array);
    }

    /// <summary>
    /// Gets a value indicating whether the validation succeeded.
    /// </summary>
    public bool IsValid => Failures.Count == 0;

    /// <summary>
    /// Gets the validation failures.
    /// </summary>
    public IReadOnlyList<ValidationFailure> Failures { get; }

    /// <summary>
    /// Creates a failed validation result.
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
    /// <exception cref="ArgumentException">
    /// <paramref name="failures"/> is empty.
    /// </exception>
    public static ValidationResult Failure(IEnumerable<ValidationFailure> failures)
    {
        ArgumentNullException.ThrowIfNull(failures);
        ValidationFailure[] array = failures.ToArray();

        if (array.Length == 0)
        {
            throw new ArgumentException(
                ValidationInternalMessages.FailuresCannotBeEmpty(),
                nameof(failures));
        }

        return new ValidationResult(array);
    }

    /// <summary>
    /// Converts the validation result into a non-generic result.
    /// </summary>
    /// <returns>
    /// A <see cref="Result"/> representing the validation outcome.
    /// </returns>
    // If we want to preserve the information in the results in the future,
    //then Result will need to support metadata or an enriched error.
    public Result ToResult()
    {
        return IsValid
            ? Result.Success()
            : Result.Failure(ValidationErrors.ValidationFailed());
    }
}
