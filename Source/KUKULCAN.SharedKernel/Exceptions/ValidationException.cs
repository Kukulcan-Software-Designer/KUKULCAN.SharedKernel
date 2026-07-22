using KUKULCAN.SharedKernel.Exceptions.Internals;
using KUKULCAN.SharedKernel.Results;
using KUKULCAN.SharedKernel.Validations;
using ValidationResult = KUKULCAN.SharedKernel.Validations.ValidationResult;
using Validations_ValidationResult = KUKULCAN.SharedKernel.Validations.ValidationResult;

namespace KUKULCAN.SharedKernel.Exceptions;

/// <summary>
/// Represents an exception thrown when one or more validation failures occur.
/// </summary>
public sealed class ValidationException : SharedKernelException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="validationResult"/> class.
    /// </summary>
    /// <param name="validationResult">
    /// Validation result.
    /// </param>
    /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">
    /// <paramref name="validationResult"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ComponentModel">
    /// <paramref name="validationResult"/> represents a successful validation.
    /// </exception>
    public ValidationException(ValidationResult validationResult) : this(validationResult, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="validationResult"/> class.
    /// </summary>
    /// <param name="validationResult">
    /// Validation result.
    /// </param>
    /// <param name="innerException">
    /// The exception that caused the current exception.
    /// </param>
    /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">
    /// <paramref name="validationResult"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ComponentModel">
    /// <paramref name="validationResult"/> represents a successful validation.
    /// </exception>
    public ValidationException(Validations_ValidationResult validationResult, Exception? innerException)
        : base(ValidationErrors.ValidationFailed(), innerException)
    {
        ArgumentNullException.ThrowIfNull(validationResult);

        if (validationResult.IsValid)
        {
            throw new ArgumentException(
                ExceptionMessages.ValidationResultMustContainFailures(),
                nameof(validationResult));
        }

        ValidationResult = validationResult;
    }

    /// <summary>
    /// Gets the validation result associated with the exception.
    /// </summary>
    public ValidationResult ValidationResult { get; }

    /// <summary>
    /// Gets the validation failures.
    /// </summary>
    public IReadOnlyList<ValidationFailure> Failures =>
        ValidationResult.Failures;
}
