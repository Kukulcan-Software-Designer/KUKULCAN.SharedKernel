using KUKULCAN.SharedKernel.Results;

namespace KUKULCAN.SharedKernel.UnitTests.Results;

/// <summary>
/// Contains unit tests for
/// <see cref="KUKULCAN.SharedKernel.Results.CommonErrors"/>.
/// </summary>
[TestFixture]
public sealed class CommonErrorsTests
{
    /// <summary>
    /// Verifies that <see cref="CommonErrors.NotFound"/> creates the
    /// expected error.
    /// </summary>
    [Test]
    public void NotFound_WithResource_ShouldCreateExpectedError()
    {
        const string resource = "Customer";

        var error = CommonErrors.NotFound(resource);

        Assert.That(error.Code, Is.EqualTo(CommonErrorCodes.NotFound));
        Assert.That(
            error.Description,
            Is.EqualTo("The resource 'Customer' was not found."));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.NotFound"/> rejects a null
    /// resource.
    /// </summary>
    [Test]
    public void NotFound_WithNullResource_ShouldThrowArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => CommonErrors.NotFound(null!));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("resource"));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.NotFound"/> rejects an empty
    /// resource.
    /// </summary>
    [Test]
    public void NotFound_WithEmptyResource_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => CommonErrors.NotFound(string.Empty));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("resource"));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.NotFound"/> rejects a
    /// whitespace-only resource.
    /// </summary>
    [Test]
    public void NotFound_WithWhitespaceResource_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => CommonErrors.NotFound("   "));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("resource"));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.Conflict"/> creates the
    /// expected error.
    /// </summary>
    [Test]
    public void Conflict_WithResource_ShouldCreateExpectedError()
    {
        const string resource = "Customer";

        var error = CommonErrors.Conflict(resource);

        Assert.That(error.Code, Is.EqualTo(CommonErrorCodes.Conflict));
        Assert.That(
            error.Description,
            Is.EqualTo("The resource 'Customer' already exists."));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.Conflict"/> rejects a null
    /// resource.
    /// </summary>
    [Test]
    public void Conflict_WithNullResource_ShouldThrowArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => CommonErrors.Conflict(null!));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("resource"));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.Conflict"/> rejects an empty
    /// resource.
    /// </summary>
    [Test]
    public void Conflict_WithEmptyResource_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => CommonErrors.Conflict(string.Empty));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("resource"));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.Conflict"/> rejects a
    /// whitespace-only resource.
    /// </summary>
    [Test]
    public void Conflict_WithWhitespaceResource_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => CommonErrors.Conflict("   "));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("resource"));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.InvalidOperation"/> creates
    /// the expected error.
    /// </summary>
    [Test]
    public void InvalidOperation_WithOperation_ShouldCreateExpectedError()
    {
        const string operation = "DeleteCustomer";

        var error = CommonErrors.InvalidOperation(operation);

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.InvalidOperation));

        Assert.That(
            error.Description,
            Is.EqualTo("The operation 'DeleteCustomer' is not valid."));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.InvalidOperation"/> rejects
    /// a null operation.
    /// </summary>
    [Test]
    public void InvalidOperation_WithNullOperation_ShouldThrowArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => CommonErrors.InvalidOperation(null!));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("operation"));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.InvalidOperation"/> rejects
    /// an empty operation.
    /// </summary>
    [Test]
    public void InvalidOperation_WithEmptyOperation_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => CommonErrors.InvalidOperation(string.Empty));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("operation"));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.InvalidOperation"/> rejects
    /// a whitespace-only operation.
    /// </summary>
    [Test]
    public void InvalidOperation_WithWhitespaceOperation_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => CommonErrors.InvalidOperation("   "));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("operation"));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.NotSupported"/> creates the
    /// expected error.
    /// </summary>
    [Test]
    public void NotSupported_WithOperation_ShouldCreateExpectedError()
    {
        const string operation = "ExportCustomer";

        var error = CommonErrors.NotSupported(operation);

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.NotSupported));

        Assert.That(
            error.Description,
            Is.EqualTo("Operation 'ExportCustomer' is not supported."));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.NotSupported"/> rejects a
    /// null operation.
    /// </summary>
    [Test]
    public void NotSupported_WithNullOperation_ShouldThrowArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => CommonErrors.NotSupported(null!));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("operation"));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.NotSupported"/> rejects an
    /// empty operation.
    /// </summary>
    [Test]
    public void NotSupported_WithEmptyOperation_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => CommonErrors.NotSupported(string.Empty));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("operation"));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.NotSupported"/> rejects a
    /// whitespace-only operation.
    /// </summary>
    [Test]
    public void NotSupported_WithWhitespaceOperation_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => CommonErrors.NotSupported("   "));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("operation"));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.Unauthorized"/> creates the
    /// expected error.
    /// </summary>
    [Test]
    public void Unauthorized_ShouldCreateExpectedError()
    {
        var error = CommonErrors.Unauthorized();

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.Unauthorized));

        Assert.That(
            error.Description,
            Is.EqualTo("Authentication is required."));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.Forbidden"/> creates the
    /// expected error.
    /// </summary>
    [Test]
    public void Forbidden_ShouldCreateExpectedError()
    {
        var error = CommonErrors.Forbidden();

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.Forbidden));

        Assert.That(
            error.Description,
            Is.EqualTo("Access to the requested resource is forbidden."));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.Timeout"/> creates the
    /// expected error.
    /// </summary>
    [Test]
    public void Timeout_ShouldCreateExpectedError()
    {
        var error = CommonErrors.Timeout();

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.Timeout));

        Assert.That(
            error.Description,
            Is.EqualTo("The operation timed out."));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.Cancelled"/> creates the
    /// expected error.
    /// </summary>
    [Test]
    public void Cancelled_ShouldCreateExpectedError()
    {
        var error = CommonErrors.Cancelled();

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.Cancelled));

        Assert.That(
            error.Description,
            Is.EqualTo("The operation was cancelled."));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.Unexpected"/> creates the
    /// expected error.
    /// </summary>
    [Test]
    public void Unexpected_ShouldCreateExpectedError()
    {
        var error = CommonErrors.Unexpected();

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.Unexpected));

        Assert.That(
            error.Description,
            Is.EqualTo("An unexpected error has occurred."));
    }

    /// <summary>
    /// Verifies that <see cref="CommonErrors.Unknown"/> creates the
    /// expected error.
    /// </summary>
    [Test]
    public void Unknown_ShouldCreateExpectedError()
    {
        var error = CommonErrors.Unknown();

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.Unknown));

        Assert.That(
            error.Description,
            Is.EqualTo("An unknown error occurred."));
    }
}
