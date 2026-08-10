using KUKULCAN.SharedKernel.Exceptions;
using KUKULCAN.SharedKernel.Results;

namespace KUKULCAN.SharedKernel.UnitTests.Exceptions;

/// <summary>
/// Contains unit tests for <see cref="ConflictException"/>.
/// </summary>
[TestFixture]
public sealed class ConflictExceptionTests
{
    /// <summary>
    /// Verifies that an exception can be created from an error.
    /// </summary>
    [Test]
    public void Constructor_WithError_ShouldCreateException()
    {
        var error = CreateError();

        var exception = new ConflictException(error);

        Assert.That(exception, Is.Not.Null);
    }

    /// <summary>
    /// Verifies that the supplied error is exposed by the exception.
    /// </summary>
    [Test]
    public void Constructor_WithError_ShouldExposeError()
    {
        var error = CreateError();

        var exception = new ConflictException(error);

        Assert.That(
            exception.Error,
            Is.SameAs(error));
    }

    /// <summary>
    /// Verifies that the exception message is taken from the error
    /// description.
    /// </summary>
    [Test]
    public void Constructor_WithError_ShouldUseErrorDescriptionAsMessage()
    {
        var error = CreateError();

        var exception = new ConflictException(error);

        Assert.That(
            exception.Message,
            Is.EqualTo(error.Description));
    }

    /// <summary>
    /// Verifies that a null error is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithNullError_ShouldThrowArgumentNullException()
    {
        Assert.That(
            () => new ConflictException(null!),
            Throws.TypeOf<ArgumentNullException>());
    }

    /// <summary>
    /// Verifies that an inner exception is preserved.
    /// </summary>
    [Test]
    public void Constructor_WithInnerException_ShouldPreserveInnerException()
    {
        var error = CreateError();
        var innerException = new InvalidOperationException(
            "Inner exception.");

        var exception = new ConflictException(
            error,
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
        var exception = new ConflictException(
            CreateError(),
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
    public void ConflictException_ShouldDeriveFromSharedKernelException()
    {
        var exception = new ConflictException(CreateError());

        Assert.That(
            exception,
            Is.InstanceOf<SharedKernelException>());
    }

    /// <summary>
    /// Verifies that the exception derives from <see cref="Exception"/>.
    /// </summary>
    [Test]
    public void ConflictException_ShouldDeriveFromException()
    {
        var exception = new ConflictException(CreateError());

        Assert.That(
            exception,
            Is.InstanceOf<Exception>());
    }

    /// <summary>
    /// Verifies that the exception has the expected runtime type.
    /// </summary>
    [Test]
    public void ConflictException_ShouldHaveExpectedRuntimeType()
    {
        var exception = new ConflictException(CreateError());

        Assert.That(
            exception.GetType(),
            Is.EqualTo(typeof(ConflictException)));
    }

    /// <summary>
    /// Creates a representative conflict error for the tests.
    /// </summary>
    private static Error CreateError()
    {
        return new Error(
            "conflict.test",
            "A conflict occurred.");
    }
}
