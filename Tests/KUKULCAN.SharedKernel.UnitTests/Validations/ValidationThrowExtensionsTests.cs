using KUKULCAN.SharedKernel.Exceptions;
using KUKULCAN.SharedKernel.Validations;

namespace KUKULCAN.SharedKernel.UnitTests.Validations;

/// <summary>
/// Contains unit tests for <see cref="ValidationThrowExtensions"/>.
/// </summary>
[TestFixture]
public sealed class ValidationThrowExtensionsTests
{
    /// <summary>
    /// Verifies that a valid validation result does not throw an exception.
    /// </summary>
    [Test]
    public void ThrowIfInvalid_WithValidResult_ShouldNotThrow()
    {
        Assert.DoesNotThrow(
            () => ValidationResult.Success.ThrowIfInvalid());
    }

    /// <summary>
    /// Verifies that an invalid validation result throws
    /// <see cref="ValidationException"/>.
    /// </summary>
    [Test]
    public void ThrowIfInvalid_WithInvalidResult_ShouldThrowValidationException()
    {
        var validationResult = CreateInvalidResult();

        Assert.That(
            () => validationResult.ThrowIfInvalid(),
            Throws.TypeOf<ValidationException>());
    }

    /// <summary>
    /// Verifies that a null validation result throws
    /// <see cref="ArgumentNullException"/>.
    /// </summary>
    [Test]
    public void ThrowIfInvalid_WithNullResult_ShouldThrowArgumentNullException()
    {
        ValidationResult? validationResult = null;

        Assert.That(
            () => validationResult!.ThrowIfInvalid(),
            Throws.TypeOf<ArgumentNullException>());
    }

    /// <summary>
    /// Verifies that the thrown validation exception contains the
    /// validation result that caused the failure.
    /// </summary>
    [Test]
    public void ThrowIfInvalid_WithInvalidResult_ShouldPreserveValidationResult()
    {
        var validationResult = CreateInvalidResult();

        var exception = Assert.Throws<ValidationException>(
            () => validationResult.ThrowIfInvalid());

        Assert.That(
            exception!.ValidationResult,
            Is.SameAs(validationResult));
    }

    /// <summary>
    /// Verifies that a validation result containing multiple failures
    /// still causes a validation exception.
    /// </summary>
    [Test]
    public void ThrowIfInvalid_WithMultipleFailures_ShouldThrowValidationException()
    {
        var validationResult = ValidationResult.Failure(
            new[]
            {
                new ValidationFailure(
                    "Name",
                    "required",
                    "Name is required."),

                new ValidationFailure(
                    "Email",
                    "invalid",
                    "Email is invalid.")
            });

        Assert.That(
            () => validationResult.ThrowIfInvalid(),
            Throws.TypeOf<ValidationException>());
    }

    /// <summary>
    /// Creates a representative invalid validation result.
    /// </summary>
    private static ValidationResult CreateInvalidResult()
    {
        return ValidationResult.Failure(
            new[]
            {
                new ValidationFailure(
                    "Name",
                    "required",
                    "Name is required.")
            });
    }
}
