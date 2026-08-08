using KUKULCAN.SharedKernel.Results;

namespace KUKULCAN.SharedKernel.UnitTests.Results;

/// <summary>
/// Contains unit tests for
/// <see cref="KUKULCAN.SharedKernel.Results.Result{T}"/>.
/// </summary>
[TestFixture]
public sealed class ResultOfTTests
{
    /// <summary>
    /// Verifies that a successful result stores the supplied value.
    /// </summary>
    [Test]
    public void Success_WithValue_ShouldCreateSuccessfulResult()
    {
        const string value = "test-value";

        Result<string> result = Result<string>.Success(value);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.IsFailure, Is.False);
        Assert.That(result.Error, Is.EqualTo(Error.None));
        Assert.That(result.Value, Is.EqualTo(value));
    }

    /// <summary>
    /// Verifies that a successful result rejects a null reference value.
    /// </summary>
    [Test]
    public void Success_WithNullReferenceValue_ShouldThrowArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => Result<string>.Success(null!));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("value"));
    }

    /// <summary>
    /// Verifies that a successful result can contain a value type.
    /// </summary>
    [Test]
    public void Success_WithValueType_ShouldCreateSuccessfulResult()
    {
        const int value = 42;

        Result<int> result = Result<int>.Success(value);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.IsFailure, Is.False);
        Assert.That(result.Error, Is.EqualTo(Error.None));
        Assert.That(result.Value, Is.EqualTo(value));
    }

    /// <summary>
    /// Verifies that a successful result can contain a nullable value type
    /// when the supplied value has a value.
    /// </summary>
    [Test]
    public void Success_WithNullableValueTypeContainingValue_ShouldCreateSuccessfulResult()
    {
        int? value = 42;

        Result<int?> result = Result<int?>.Success(value);

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.IsFailure, Is.False);
        Assert.That(result.Error, Is.EqualTo(Error.None));
        Assert.That(result.Value, Is.EqualTo(value));
    }

    /// <summary>
    /// Verifies that a failed result contains the supplied error.
    /// </summary>
    [Test]
    public void Failure_WithError_ShouldCreateFailedResult()
    {
        var error = new Error(
            "Test.Error",
            "Test error description.");

        Result<string> result = Result<string>.Failure(error);

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.IsFailure, Is.True);
        Assert.That(result.Error, Is.EqualTo(error));
    }

    /// <summary>
    /// Verifies that a null error cannot be used to create a failed result.
    /// </summary>
    [Test]
    public void Failure_WithNullError_ShouldThrowArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => Result<string>.Failure(null!));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("error"));
    }

    /// <summary>
    /// Verifies that accessing the value of a successful result returns
    /// the original value.
    /// </summary>
    [Test]
    public void Value_OnSuccessfulResult_ShouldReturnValue()
    {
        const string value = "test-value";

        Result<string> result = Result<string>.Success(value);

        Assert.That(result.Value, Is.EqualTo(value));
    }

    /// <summary>
    /// Verifies that accessing the value of a failed result throws an
    /// <see cref="InvalidOperationException"/>.
    /// </summary>
    [Test]
    public void Value_OnFailedResult_ShouldThrowInvalidOperationException()
    {
        var error = new Error(
            "Test.Error",
            "Test error description.");

        Result<string> result = Result<string>.Failure(error);

        var exception = Assert.Throws<InvalidOperationException>(
            () => _ = result.Value);

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.Message, Is.Not.Null.And.Not.Empty);
    }

    /// <summary>
    /// Verifies that the value of a failed result is not exposed even when
    /// the generic type is nullable.
    /// </summary>
    [Test]
    public void Value_OnFailedNullableResult_ShouldThrowInvalidOperationException()
    {
        var error = new Error(
            "Test.Error",
            "Test error description.");

        Result<string?> result = Result<string?>.Failure(error);

        var exception = Assert.Throws<InvalidOperationException>(
            () => _ = result.Value);

        Assert.That(exception, Is.Not.Null);
    }

    /// <summary>
    /// Verifies that the successful result string representation contains
    /// the returned value.
    /// </summary>
    [Test]
    public void ToString_OnSuccessfulResult_ShouldContainValue()
    {
        const string value = "test-value";

        Result<string> result = Result<string>.Success(value);

        Assert.That(
            result.ToString(),
            Is.EqualTo($"Success ({value})"));
    }

    /// <summary>
    /// Verifies that the failed result string representation contains
    /// the associated error.
    /// </summary>
    [Test]
    public void ToString_OnFailedResult_ShouldContainError()
    {
        var error = new Error(
            "Test.Error",
            "Test error description.");

        Result<string> result = Result<string>.Failure(error);

        Assert.That(
            result.ToString(),
            Is.EqualTo($"Failure: {error}"));
    }

    /// <summary>
    /// Verifies that the generic result preserves the supplied value
    /// independently for different generic type arguments.
    /// </summary>
    [Test]
    public void GenericResults_WithDifferentTypes_ShouldPreserveTheirValues()
    {
        const string text = "test";
        const int number = 42;

        Result<string> stringResult = Result<string>.Success(text);
        Result<int> intResult = Result<int>.Success(number);

        Assert.That(stringResult.Value, Is.EqualTo(text));
        Assert.That(intResult.Value, Is.EqualTo(number));
    }

    /// <summary>
    /// Verifies that a failed generic result does not expose a value.
    /// </summary>
    [Test]
    public void Failure_ShouldNotExposeValue()
    {
        var error = new Error(
            "Test.Error",
            "Test error description.");

        Result<int> result = Result<int>.Failure(error);

        Assert.That(result.IsFailure, Is.True);

        Assert.Throws<InvalidOperationException>(
            () => _ = result.Value);
    }
}
