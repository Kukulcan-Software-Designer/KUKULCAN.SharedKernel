using KUKULCAN.SharedKernel.Exceptions;
using KUKULCAN.SharedKernel.Results;
using KUKULCAN.SharedKernel.Validations;

namespace KUKULCAN.SharedKernel.UnitTests.Exceptions;

/// <summary>
/// Contains unit tests for <see cref="ValidationException"/>.
/// </summary>
[TestFixture]
public sealed class ValidationExceptionTests
{
    /// <summary>
    /// Verifies that a failed validation result can create a
    /// <see cref="ValidationException"/>.
    /// </summary>
    [Test]
    public void Constructor_WithInvalidValidationResult_ShouldCreateException()
    {
        var validationResult = CreateInvalidResult();

        var exception = new ValidationException(validationResult);

        Assert.Multiple(() =>
        {
            Assert.That(exception, Is.Not.Null);
            Assert.That(
                exception.ValidationResult,
                Is.SameAs(validationResult));
        });
    }

    /// <summary>
    /// Verifies that the validation failures are exposed from the
    /// associated validation result.
    /// </summary>
    [Test]
    public void Failures_ShouldReturnValidationResultFailures()
    {
        var validationResult = CreateInvalidResult();

        var exception = new ValidationException(validationResult);

        Assert.That(
            exception.Failures,
            Is.SameAs(validationResult.Failures));
    }

    /// <summary>
    /// Verifies that a null validation result throws
    /// <see cref="ArgumentNullException"/>.
    /// </summary>
    [Test]
    public void Constructor_WithNullValidationResult_ShouldThrowArgumentNullException()
    {
        Assert.That(
            () => new ValidationException(null!),
            Throws.TypeOf<ArgumentNullException>());
    }

    /// <summary>
    /// Verifies that a successful validation result is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithSuccessfulValidationResult_ShouldThrowArgumentException()
    {
        Assert.That(
            () => new ValidationException(ValidationResult.Success),
            Throws.TypeOf<ArgumentException>());
    }

    /// <summary>
    /// Verifies that the exception uses the SharedKernel validation
    /// failure error.
    /// </summary>
    [Test]
    public void Constructor_WithInvalidValidationResult_ShouldUseValidationFailedError()
    {
        var exception = new ValidationException(
            CreateInvalidResult());

        Assert.That(
            exception.Error.Code,
            Is.EqualTo(CommonErrorCodes.ValidationFailed));
    }

    /// <summary>
    /// Verifies that the exception message is the description of the
    /// associated validation error.
    /// </summary>
    [Test]
    public void Constructor_WithInvalidValidationResult_ShouldUseValidationErrorDescription()
    {
        var exception = new ValidationException(
            CreateInvalidResult());

        Assert.That(
            exception.Message,
            Is.EqualTo(exception.Error.Description));
    }

    /// <summary>
    /// Verifies that the supplied inner exception is preserved.
    /// </summary>
    [Test]
    public void Constructor_WithInnerException_ShouldPreserveInnerException()
    {
        var validationResult = CreateInvalidResult();
        var innerException = new InvalidOperationException(
            "Validation infrastructure failure.");

        var exception = new ValidationException(
            validationResult,
            innerException);

        Assert.That(
            exception.InnerException,
            Is.SameAs(innerException));
    }

    /// <summary>
    /// Verifies that a null inner exception is accepted.
    /// </summary>
    [Test]
    public void Constructor_WithNullInnerException_ShouldAcceptNull()
    {
        var exception = new ValidationException(
            CreateInvalidResult(),
            null);

        Assert.That(
            exception.InnerException,
            Is.Null);
    }

    /// <summary>
    /// Verifies that the exception derives from
    /// <see cref="SharedKernelException"/>.
    /// </summary>
    [Test]
    public void ValidationException_ShouldDeriveFromSharedKernelException()
    {
        var exception = new ValidationException(
            CreateInvalidResult());

        Assert.That(
            exception,
            Is.InstanceOf<SharedKernelException>());
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
