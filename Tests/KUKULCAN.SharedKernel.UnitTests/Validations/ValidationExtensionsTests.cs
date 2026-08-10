using KUKULCAN.SharedKernel.Validations;

namespace KUKULCAN.SharedKernel.UnitTests.Validations;

/// <summary>
/// Contains unit tests for <see cref="ValidationExtensions"/>.
/// </summary>
[TestFixture]
public sealed class ValidationExtensionsTests
{
    /// <summary>
    /// Verifies that a single validation failure is converted into
    /// an invalid validation result.
    /// </summary>
    [Test]
    public void ToValidationResult_WithFailure_ShouldReturnInvalidResult()
    {
        var failure = CreateFailure();

        var result = failure.ToValidationResult();

        Assert.That(result.IsValid, Is.False);
    }

    /// <summary>
    /// Verifies that a single validation failure is preserved in the
    /// resulting validation result.
    /// </summary>
    [Test]
    public void ToValidationResult_WithFailure_ShouldPreserveFailure()
    {
        var failure = CreateFailure();

        var result = failure.ToValidationResult();

        Assert.Multiple(() =>
        {
            Assert.That(result.Failures, Has.Count.EqualTo(1));
            Assert.That(result.Failures[0], Is.EqualTo(failure));
        });
    }

    /// <summary>
    /// Verifies that a null validation failure throws
    /// <see cref="ArgumentNullException"/>.
    /// </summary>
    [Test]
    public void ToValidationResult_WithNullFailure_ShouldThrowArgumentNullException()
    {
        ValidationFailure? failure = null;

        Assert.That(
            () => failure!.ToValidationResult(),
            Throws.TypeOf<ArgumentNullException>());
    }

    /// <summary>
    /// Verifies that multiple validation failures are converted into
    /// one invalid validation result.
    /// </summary>
    [Test]
    public void ToValidationResult_WithMultipleFailures_ShouldReturnInvalidResult()
    {
        var failures = new[]
        {
            CreateFailure(),
            new ValidationFailure(
                "Email",
                "invalid",
                "Email is invalid.")
        };

        var result = failures.ToValidationResult();

        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.Failures, Has.Count.EqualTo(2));
        });
    }

    /// <summary>
    /// Verifies that multiple validation failures preserve their order.
    /// </summary>
    [Test]
    public void ToValidationResult_WithMultipleFailures_ShouldPreserveOrder()
    {
        var first = CreateFailure();

        var second = new ValidationFailure(
            "Email",
            "invalid",
            "Email is invalid.");

        var third = new ValidationFailure(
            "Age",
            "greater_than",
            "Age must be greater than zero.");

        var failures = new[]
        {
            first,
            second,
            third
        };

        var result = failures.ToValidationResult();

        Assert.Multiple(() =>
        {
            Assert.That(result.Failures[0], Is.EqualTo(first));
            Assert.That(result.Failures[1], Is.EqualTo(second));
            Assert.That(result.Failures[2], Is.EqualTo(third));
        });
    }

    /// <summary>
    /// Verifies that an empty failure collection is rejected by the
    /// underlying validation result factory.
    /// </summary>
    [Test]
    public void ToValidationResult_WithEmptyFailures_ShouldThrowArgumentException()
    {
        var failures = Array.Empty<ValidationFailure>();

        Assert.That(
            () => failures.ToValidationResult(),
            Throws.TypeOf<ArgumentException>());
    }

    /// <summary>
    /// Verifies that a null failure collection throws
    /// <see cref="ArgumentNullException"/>.
    /// </summary>
    [Test]
    public void ToValidationResult_WithNullFailures_ShouldThrowArgumentNullException()
    {
        IEnumerable<ValidationFailure>? failures = null;

        Assert.That(
            () => failures!.ToValidationResult(),
            Throws.TypeOf<ArgumentNullException>());
    }

    /// <summary>
    /// Verifies that a mutable source collection is copied by the
    /// resulting validation result.
    /// </summary>
    [Test]
    public void ToValidationResult_WithMutableCollection_ShouldCopyFailures()
    {
        var failure = CreateFailure();

        var failures = new List<ValidationFailure>
        {
            failure
        };

        var result = failures.ToValidationResult();

        failures.Clear();

        Assert.Multiple(() =>
        {
            Assert.That(result.Failures, Has.Count.EqualTo(1));
            Assert.That(result.Failures[0], Is.EqualTo(failure));
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
