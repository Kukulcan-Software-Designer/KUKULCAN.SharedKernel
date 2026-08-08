using KUKULCAN.SharedKernel.Results;

namespace KUKULCAN.SharedKernel.UnitTests.Results;

/// <summary>
/// Contains unit tests for <see cref="Error"/>.
/// </summary>
[TestFixture]
public sealed class ErrorTests
{
    /// <summary>
    /// Verifies that a valid code and description are stored correctly.
    /// </summary>
    [Test]
    public void Constructor_WithValidValues_ShouldCreateError()
    {
        const string code = "Test.Error";
        const string description = "Test error description.";

        var error = new Error(code, description);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(error.Code, Is.EqualTo(code));
            Assert.That(error.Description, Is.EqualTo(description));
        }
    }

    /// <summary>
    /// Verifies that a null code is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithNullCode_ShouldThrowArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => new Error(null!, "Description"));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("code"));
    }

    /// <summary>
    /// Verifies that an empty code is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithEmptyCode_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => new Error(string.Empty, "Description"));

        Assert.That(exception, Is.Not.Null);
    }

    /// <summary>
    /// Verifies that a whitespace-only code is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithWhitespaceCode_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => new Error("   ", "Description"));

        Assert.That(exception, Is.Not.Null);
    }

    /// <summary>
    /// Verifies that a null description is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithNullDescription_ShouldThrowArgumentNullException()
    {
        var exception = Assert.Throws<ArgumentNullException>(
            () => new Error("Test.Error", null!));

        Assert.That(exception, Is.Not.Null);
        Assert.That(exception!.ParamName, Is.EqualTo("description"));
    }

    /// <summary>
    /// Verifies that an empty description is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithEmptyDescription_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => new Error("Test.Error", string.Empty));

        Assert.That(exception, Is.Not.Null);
    }

    /// <summary>
    /// Verifies that a whitespace-only description is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithWhitespaceDescription_ShouldThrowArgumentException()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => new Error("Test.Error", "   "));

        Assert.That(exception, Is.Not.Null);
    }

    /// <summary>
    /// Verifies that <see cref="Error.None"/> uses the None error code.
    /// </summary>
    [Test]
    public void None_ShouldHaveNoneCode()
    {
        Assert.That(Error.None.Code, Is.EqualTo(CommonErrorCodes.None));
    }

    /// <summary>
    /// Verifies that <see cref="Error.None"/> has a non-empty description.
    /// </summary>
    [Test]
    public void None_ShouldHaveDescription()
    {
        Assert.That(Error.None.Description, Is.Not.Null.And.Not.Empty);
    }

    /// <summary>
    /// Verifies that <see cref="Error.None"/> returns the same instance
    /// on repeated access.
    /// </summary>
    [Test]
    public void None_ShouldReturnSameInstance()
    {
        Error first = Error.None;
        Error second = Error.None;

        Assert.That(ReferenceEquals(first, second), Is.True);
    }

    /// <summary>
    /// Verifies that two errors with the same values are equal.
    /// </summary>
    [Test]
    public void Equality_WithSameCodeAndDescription_ShouldBeEqual()
    {
        var first = new Error("Test.Error", "Test error description.");
        var second = new Error("Test.Error", "Test error description.");

        Assert.That(first, Is.EqualTo(second));
    }

    /// <summary>
    /// Verifies that errors with different codes are not equal.
    /// </summary>
    [Test]
    public void Equality_WithDifferentCode_ShouldNotBeEqual()
    {
        var first = new Error("Test.Error.One", "Test error description.");
        var second = new Error("Test.Error.Two", "Test error description.");

        Assert.That(first, Is.Not.EqualTo(second));
    }

    /// <summary>
    /// Verifies that errors with different descriptions are not equal.
    /// </summary>
    [Test]
    public void Equality_WithDifferentDescription_ShouldNotBeEqual()
    {
        var first = new Error("Test.Error", "First description.");
        var second = new Error("Test.Error", "Second description.");

        Assert.That(first, Is.Not.EqualTo(second));
    }

    /// <summary>
    /// Verifies that equal errors have equal hash codes.
    /// </summary>
    [Test]
    public void GetHashCode_WithEqualErrors_ShouldReturnSameValue()
    {
        var first = new Error("Test.Error", "Test error description.");
        var second = new Error("Test.Error", "Test error description.");

        Assert.That(first.GetHashCode(), Is.EqualTo(second.GetHashCode()));
    }

    /// <summary>
    /// Verifies that the string representation contains the code and
    /// description in the expected format.
    /// </summary>
    [Test]
    public void ToString_ShouldReturnCodeAndDescription()
    {
        const string code = "Test.Error";
        const string description = "Test error description.";

        var error = new Error(code, description);
        string result = error.ToString();

        Assert.That(result, Is.EqualTo($"{code}: {description}"));
    }
}
