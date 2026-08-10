using KUKULCAN.SharedKernel.Results;
using KUKULCAN.SharedKernel.Validations;

namespace KUKULCAN.SharedKernel.UnitTests.Validations;

/// <summary>
/// Contains unit tests for <see cref="ValidationResult"/>.
/// </summary>
[TestFixture]
public sealed class ValidationResultTests
{
    /// <summary>
    /// Verifies that the predefined successful result is valid.
    /// </summary>
    [Test]
    public void Success_ShouldBeValid()
    {
        Assert.That(ValidationResult.Success.IsValid, Is.True);
    }

    /// <summary>
    /// Verifies that the predefined successful result contains no failures.
    /// </summary>
    [Test]
    public void Success_ShouldContainNoFailures()
    {
        Assert.That(
            ValidationResult.Success.Failures,
            Is.Empty);
    }

    /// <summary>
    /// Verifies that the predefined successful result is reused.
    /// </summary>
    [Test]
    public void Success_ShouldReturnSameInstance()
    {
        Assert.That(
            ValidationResult.Success,
            Is.SameAs(ValidationResult.Success));
    }

    /// <summary>
    /// Verifies that a failed result is invalid.
    /// </summary>
    [Test]
    public void Failure_WithOneFailure_ShouldBeInvalid()
    {
        var failure = CreateFailure();

        var result = ValidationResult.Failure(
            new[] { failure });

        Assert.That(result.IsValid, Is.False);
    }

    /// <summary>
    /// Verifies that a failed result contains the supplied failure.
    /// </summary>
    [Test]
    public void Failure_WithOneFailure_ShouldContainFailure()
    {
        var failure = CreateFailure();

        var result = ValidationResult.Failure(
            new[] { failure });

        Assert.Multiple(() =>
        {
            Assert.That(result.Failures, Has.Count.EqualTo(1));
            Assert.That(result.Failures[0], Is.EqualTo(failure));
        });
    }

    /// <summary>
    /// Verifies that multiple validation failures are preserved.
    /// </summary>
    [Test]
    public void Failure_WithMultipleFailures_ShouldPreserveFailures()
    {
        var first = new ValidationFailure(
            "Name",
            "required",
            "Name is required.");

        var second = new ValidationFailure(
            "Email",
            "invalid",
            "Email is invalid.");

        var result = ValidationResult.Failure(
            new[] { first, second });

        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Failures, Has.Count.EqualTo(2));
            Assert.That(result.Failures[0], Is.EqualTo(first));
            Assert.That(result.Failures[1], Is.EqualTo(second));
        });
    }

    /// <summary>
    /// Verifies that the order of validation failures is preserved.
    /// </summary>
    [Test]
    public void Failure_WithMultipleFailures_ShouldPreserveOrder()
    {
        var first = new ValidationFailure(
            "First",
            "first",
            "First failure.");

        var second = new ValidationFailure(
            "Second",
            "second",
            "Second failure.");

        var third = new ValidationFailure(
            "Third",
            "third",
            "Third failure.");

        var result = ValidationResult.Failure(
            new[] { first, second, third });

        Assert.Multiple(() =>
        {
            Assert.That(result.Failures[0], Is.EqualTo(first));
            Assert.That(result.Failures[1], Is.EqualTo(second));
            Assert.That(result.Failures[2], Is.EqualTo(third));
        });
    }

    /// <summary>
    /// Verifies that a null failure collection throws
    /// <see cref="ArgumentNullException"/>.
    /// </summary>
    [Test]
    public void Failure_WithNullFailures_ShouldThrowArgumentNullException()
    {
        Assert.That(
            () => ValidationResult.Failure(null!),
            Throws.TypeOf<ArgumentNullException>());
    }

    /// <summary>
    /// Verifies that an empty failure collection throws
    /// <see cref="ArgumentException"/>.
    /// </summary>
    [Test]
    public void Failure_WithEmptyFailures_ShouldThrowArgumentException()
    {
        Assert.That(
            () => ValidationResult.Failure(
                Array.Empty<ValidationFailure>()),
            Throws.TypeOf<ArgumentException>());
    }

    /// <summary>
    /// Verifies that the exception generated for an empty failure
    /// collection identifies the failures parameter.
    /// </summary>
    [Test]
    public void Failure_WithEmptyFailures_ShouldIdentifyFailuresParameter()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => ValidationResult.Failure(
                Array.Empty<ValidationFailure>()));

        Assert.That(
            exception!.ParamName,
            Is.EqualTo("failures"));
    }

    /// <summary>
    /// Verifies that the resulting failures collection is read-only.
    /// </summary>
    [Test]
    public void Failure_ShouldExposeReadOnlyFailures()
    {
        var failure = CreateFailure();

        var result = ValidationResult.Failure(
            new[] { failure });

        Assert.That(
            result.Failures,
            Is.TypeOf<System.Collections.ObjectModel.ReadOnlyCollection<ValidationFailure>>());
    }

    /// <summary>
    /// Verifies that the result copies the supplied failure collection.
    /// </summary>
    [Test]
    public void Failure_ShouldCopySourceCollection()
    {
        var failure = CreateFailure();
        var source = new List<ValidationFailure> { failure };

        var result = ValidationResult.Failure(source);

        source.Clear();

        Assert.Multiple(() =>
        {
            Assert.That(result.Failures, Has.Count.EqualTo(1));
            Assert.That(result.Failures[0], Is.EqualTo(failure));
        });
    }

    /// <summary>
    /// Verifies that a successful validation result converts to a successful
    /// <see cref="KUKULCAN.SharedKernel.Results.Result"/>.
    /// </summary>
    [Test]
    public void ToResult_WhenValid_ShouldReturnSuccess()
    {
        var result = ValidationResult.Success;

        var converted = result.ToResult();

        Assert.That(converted.IsSuccess, Is.True);
    }

    /// <summary>
    /// Verifies that a failed validation result converts to a failed
    /// <see cref="KUKULCAN.SharedKernel.Results.Result"/>.
    /// </summary>
    [Test]
    public void ToResult_WhenInvalid_ShouldReturnFailure()
    {
        var validationResult = ValidationResult.Failure(
            new[] { CreateFailure() });

        var result = validationResult.ToResult();

        Assert.That(result.IsSuccess, Is.False);
    }

    /// <summary>
    /// Verifies that an invalid validation result produces the expected
    /// validation error code when converted to a <see cref="KUKULCAN.SharedKernel.Results.Result"/>.
    /// </summary>
    [Test]
    public void ToResult_WhenInvalid_ShouldReturnValidationFailedError()
    {
        var validationResult = ValidationResult.Failure(
            new[] { CreateFailure() });

        var result = validationResult.ToResult();

        Assert.Multiple(() =>
        {
            Assert.That(result.IsFailure, Is.True);
            Assert.That(
                result.Error.Code,
                Is.EqualTo(CommonErrorCodes.ValidationFailed));
        });
    }

    /// <summary>
    /// Verifies that the predefined successful validation result converts
    /// to a successful result.
    /// </summary>
    [Test]
    public void ToResult_FromSuccess_ShouldReturnSuccessfulResult()
    {
        var result = ValidationResult.Success.ToResult();

        Assert.Multiple(() =>
        {
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.IsFailure, Is.False);
        });
    }

    /// <summary>
    /// Creates a representative validation failure for the tests.
    /// </summary>
    private static ValidationFailure CreateFailure()
    {
        return new ValidationFailure(
            "Name",
            "required",
            "Name is required.");
    }
}
