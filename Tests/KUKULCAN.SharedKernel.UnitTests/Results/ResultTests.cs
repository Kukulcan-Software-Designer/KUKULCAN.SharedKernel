using KUKULCAN.SharedKernel.Results;

namespace KUKULCAN.SharedKernel.UnitTests.Results;

/// <summary>
/// Contains unit tests for <see cref="KUKULCAN.SharedKernel.Results.Result"/>./// </summary>
[TestFixture]
public sealed class ResultTests
{
    /// <summary>
    /// Exposes the protected <see cref="KUKULCAN.SharedKernel.Results.Result"/>
    /// constructor for testing.
    /// </summary>
    [Test]
    public void Success_ShouldCreateSuccessfulResult()
    {
        Result result = Result.Success();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.IsFailure, Is.False);
            Assert.That(result.Error, Is.EqualTo(Error.None));
        }
    }

    /// <summary>
    /// Verifies that <see cref="KUKULCAN.SharedKernel.Results.Result.Success()"/>
    /// creates a successful result.
    /// </summary>
    [Test]
    public void Failure_ShouldCreateFailedResult()
    {
        var error = new Error(
            "Test.Error",
            "Test error description.");

        Result result = Result.Failure(error);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(error));
        }
    }

    /// <summary>
    /// Verifies that a null error cannot be used to create a failure result.
    /// </summary>
    [Test]
    public void Failure_WithNullError_ShouldThrowArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => Result.Failure(null!));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("error"));
    }

    /// <summary>
    /// Verifies that a successful result is represented correctly as text.
    /// </summary>
    [Test]
    public void Success_ToString_ShouldReturnSuccess()
    {
        Result result = Result.Success();

        Assert.That(result.ToString(), Is.EqualTo("Success"));
    }

    /// <summary>
    /// Verifies that a failed result is represented correctly as text.
    /// </summary>
    [Test]
    public void Failure_ToString_ShouldReturnFailureWithError()
    {
        var error = new Error(
            "Test.Error",
            "Test error description.");

        Result result = Result.Failure(error);

        Assert.That(
            result.ToString(),
            Is.EqualTo($"Failure: {error}"));
    }

    /// <summary>
    /// Verifies that a null error is rejected by the protected constructor.
    /// </summary>
    [Test]
    public void Constructor_WithNullError_ShouldThrowArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => TestResult.Create(false, null!));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("error"));
    }

    /// <summary>
    /// Verifies that a successful result cannot contain an error other than
    /// <see cref="Error.None"/>.
    /// </summary>
    [Test]
    public void Constructor_WithSuccessAndNonNoneError_ShouldThrowArgumentException()
    {
        var error = new Error(
            "Test.Error",
            "Test error description.");

        var exception = Assert.Throws<ArgumentException>(
            () => TestResult.Create(true, error));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("error"));
    }

    /// <summary>
    /// Verifies that a failed result must contain an error other than
    /// <see cref="Error.None"/>.
    /// </summary>
    [Test]
    public void Constructor_WithFailureAndNoneError_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => TestResult.Create(false, Error.None));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("error"));
    }

    /// <summary>
    /// Verifies that the protected constructor creates a valid successful
    /// result when the success state and error are consistent.
    /// </summary>
    [Test]
    public void Constructor_WithSuccessAndNoneError_ShouldCreateSuccessfulResult()
    {
        var result = TestResult.Create(true, Error.None);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.IsFailure, Is.False);
            Assert.That(result.Error, Is.EqualTo(Error.None));
        }
    }

    /// <summary>
    /// Verifies that the protected constructor creates a valid failed result
    /// when the failure state and error are consistent.
    /// </summary>
    [Test]
    public void Constructor_WithFailureAndError_ShouldCreateFailedResult()
    {
        var error = new Error(
            "Test.Error",
            "Test error description.");

        var result = TestResult.Create(false, error);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(error));
        }
    }

    /// <summary>
    /// Verifies that <see cref="KUKULCAN.SharedKernel.Results.Result.Failure(KUKULCAN.SharedKernel.Results.Error)"/>
    /// creates a failed result.
    /// </summary>
    private sealed class TestResult : Result
    {
        private TestResult(
            bool isSuccess,
            Error error)
            : base(isSuccess, error)
        {
        }

        /// <summary>
        /// Creates a test result using the protected constructor.
        /// </summary>
        /// <param name="isSuccess">
        /// Indicates whether the operation succeeded.
        /// </param>
        /// <param name="error">
        /// Error associated with the result.
        /// </param>
        /// <returns>
        /// A test result instance.
        /// </returns>
        public static TestResult Create(
            bool isSuccess,
            Error error)
        {
            return new TestResult(isSuccess, error);
        }
    }
}
