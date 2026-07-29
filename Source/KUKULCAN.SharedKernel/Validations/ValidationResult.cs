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
    /// Converts this validation result into a <see cref="Result"/>.
    /// </summary>
    /// <remarks>
    /// This method represents the official integration point between the
    /// Validation module and the Results module.
    ///
    /// A successful validation is converted into <see cref="Result.Success()"/>.
    /// A failed validation is converted into a failed <see cref="Result"/>
    /// containing the corresponding validation error.
    ///
    /// Consumers should use this method instead of implementing their own
    /// validation-to-result conversion logic, ensuring a single, consistent
    /// conversion policy throughout the SharedKernel.
    /// </remarks>
    /// <returns>
    /// A successful <see cref="Result"/> when validation succeeds;
    /// otherwise a failed <see cref="Result"/>.
    /// </returns>
    public Result ToResult()
    {
        return IsValid
            ? Result.Success()
            : Result.Failure(ValidationErrors.ValidationFailed());
    }}
