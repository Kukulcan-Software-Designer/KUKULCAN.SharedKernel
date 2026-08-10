using KUKULCAN.SharedKernel.Exceptions;
using KUKULCAN.SharedKernel.Results;

namespace KUKULCAN.SharedKernel.UnitTests.Exceptions;

/// <summary>
/// Contains unit tests for the behavior provided by
/// <see cref="SharedKernelException"/>.
/// </summary>
[TestFixture]
public sealed class SharedKernelExceptionTests
{
    /// <summary>
    /// Verifies that the associated error is exposed through the
    /// <see cref="SharedKernelException.Error"/> property.
    /// </summary>
    [Test]
    public void Constructor_WithError_ShouldExposeError()
    {
        var error = CreateError();

        var exception = new DomainException(error);

        Assert.That(exception.Error, Is.SameAs(error));
    }

    /// <summary>
    /// Verifies that the exception message is taken from the error
    /// description.
    /// </summary>
    [Test]
    public void Constructor_WithError_ShouldUseErrorDescriptionAsMessage()
    {
        var error = CreateError();

        var exception = new DomainException(error);

        Assert.That(
            exception.Message,
            Is.EqualTo(error.Description));
    }

    /// <summary>
    /// Verifies that a null error throws
    /// <see cref="ArgumentNullException"/>.
    /// </summary>
    [Test]
    public void Constructor_WithNullError_ShouldThrowArgumentNullException()
    {
        Assert.That(
            () => new DomainException(null!),
            Throws.TypeOf<ArgumentNullException>());
    }

    /// <summary>
    /// Verifies that the inner exception is preserved by the base
    /// exception implementation.
    /// </summary>
    [Test]
    public void Constructor_WithInnerException_ShouldPreserveInnerException()
    {
        var error = CreateError();
        var innerException = new InvalidOperationException("Inner error.");

        var exception = new DomainException(
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
        var error = CreateError();

        var exception = new DomainException(
            error,
            null);

        Assert.That(
            exception.InnerException,
            Is.Null);
    }

    /// <summary>
    /// Verifies that the exception derives from <see cref="Exception"/>.
    /// </summary>
    [Test]
    public void Exception_ShouldDeriveFromException()
    {
        var exception = new DomainException(CreateError());

        Assert.That(
            exception,
            Is.InstanceOf<Exception>());
    }

    /// <summary>
    /// Verifies that the domain exception derives from
    /// <see cref="SharedKernelException"/>.
    /// </summary>
    [Test]
    public void DomainException_ShouldDeriveFromSharedKernelException()
    {
        var exception = new DomainException(CreateError());

        Assert.That(
            exception,
            Is.InstanceOf<SharedKernelException>());
    }

    /// <summary>
    /// Creates a representative error for the tests.
    /// </summary>
    private static Error CreateError()
    {
        return new Error(
            "test.error",
            "Test error.");
    }
}
