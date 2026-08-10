using KUKULCAN.SharedKernel.Guards;

namespace KUKULCAN.SharedKernel.UnitTests.Guards;

/// <summary>
/// Contains unit tests for
/// <see cref="KUKULCAN.SharedKernel.Guards.Guard"/>.
/// </summary>
[TestFixture]
public sealed class GuardTests
{
    #region NotDefault

    /// <summary>
    /// Verifies that NotDefault returns a non-default value unchanged.
    /// </summary>
    [Test]
    public void NotDefault_WithNonDefaultValue_ShouldReturnValue()
    {
        const int value = 42;

        var result = Guard.NotDefault(value);

        Assert.That(result, Is.EqualTo(value));
    }

    /// <summary>
    /// Verifies that NotDefault rejects the default value of a value type.
    /// </summary>
    [Test]
    public void NotDefault_WithDefaultValue_ShouldThrowArgumentException()
    {
        Assert.That(
            () => Guard.NotDefault(0),
            Throws.TypeOf<ArgumentException>());
    }

    /// <summary>
    /// Verifies that NotDefault identifies the validated argument.
    /// </summary>
    [Test]
    public void NotDefault_WithDefaultValue_ShouldUseArgumentExpressionAsParameterName()
    {
        var value = 0;

        var exception = Assert.Throws<ArgumentException>(
            () => Guard.NotDefault(value));

        Assert.That(
            exception!.ParamName,
            Is.EqualTo(nameof(value)));
    }

    /// <summary>
    /// Verifies that NotDefault uses the expected validation message.
    /// </summary>
    [Test]
    public void NotDefault_WithDefaultValue_ShouldUseExpectedMessage()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => Guard.NotDefault(0));

        Assert.That(
            exception!.Message,
            Does.StartWith("The value cannot be the default value."));
    }

    /// <summary>
    /// Verifies that NotDefault works with a non-default DateTime.
    /// </summary>
    [Test]
    public void NotDefault_WithNonDefaultDateTime_ShouldReturnValue()
    {
        var value = new DateTime(2026, 1, 15);

        var result = Guard.NotDefault(value);

        Assert.That(result, Is.EqualTo(value));
    }

    /// <summary>
    /// Verifies that NotDefault rejects the default DateTime value.
    /// </summary>
    [Test]
    public void NotDefault_WithDefaultDateTime_ShouldThrowArgumentException()
    {
        Assert.That(
            () => Guard.NotDefault(default(DateTime)),
            Throws.TypeOf<ArgumentException>());
    }

    #endregion

    #region NotEmpty(Guid)

    /// <summary>
    /// Verifies that NotEmpty returns a non-empty Guid unchanged.
    /// </summary>
    [Test]
    public void NotEmptyGuid_WithNonEmptyGuid_ShouldReturnValue()
    {
        var value = Guid.NewGuid();

        var result = Guard.NotEmpty(value);

        Assert.That(result, Is.EqualTo(value));
    }

    /// <summary>
    /// Verifies that NotEmpty rejects Guid.Empty.
    /// </summary>
    [Test]
    public void NotEmptyGuid_WithEmptyGuid_ShouldThrowArgumentException()
    {
        Assert.That(
            () => Guard.NotEmpty(Guid.Empty),
            Throws.TypeOf<ArgumentException>());
    }

    /// <summary>
    /// Verifies that NotEmpty identifies the validated argument.
    /// </summary>
    [Test]
    public void NotEmptyGuid_WithEmptyGuid_ShouldUseArgumentExpressionAsParameterName()
    {
        var value = Guid.Empty;

        var exception = Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(value));

        Assert.That(
            exception!.ParamName,
            Is.EqualTo(nameof(value)));
    }

    /// <summary>
    /// Verifies that NotEmpty uses the expected validation message.
    /// </summary>
    [Test]
    public void NotEmptyGuid_WithEmptyGuid_ShouldUseExpectedMessage()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(Guid.Empty));

        Assert.That(
            exception!.Message,
            Does.StartWith("The Guid cannot be empty."));
    }

    #endregion

    #region NotEmpty(IReadOnlyCollection{T})

    /// <summary>
    /// Verifies that NotEmpty returns a non-empty collection unchanged.
    /// </summary>
    [Test]
    public void NotEmptyCollection_WithNonEmptyCollection_ShouldReturnSameCollection()
    {
        IReadOnlyCollection<int> collection = new[] { 1, 2, 3 };

        var result = Guard.NotEmpty(collection);

        Assert.That(result, Is.SameAs(collection));
    }

    /// <summary>
    /// Verifies that NotEmpty accepts a collection containing
    /// a single element.
    /// </summary>
    [Test]
    public void NotEmptyCollection_WithSingleElement_ShouldReturnCollection()
    {
        IReadOnlyCollection<string> collection = new[] { "value" };

        var result = Guard.NotEmpty(collection);

        Assert.That(result, Is.SameAs(collection));
    }

    /// <summary>
    /// Verifies that NotEmpty rejects an empty collection.
    /// </summary>
    [Test]
    public void NotEmptyCollection_WithEmptyCollection_ShouldThrowArgumentException()
    {
        IReadOnlyCollection<int> collection = Array.Empty<int>();

        Assert.That(
            () => Guard.NotEmpty(collection),
            Throws.TypeOf<ArgumentException>());
    }

    /// <summary>
    /// Verifies that NotEmpty identifies the validated collection.
    /// </summary>
    [Test]
    public void NotEmptyCollection_WithEmptyCollection_ShouldUseArgumentExpressionAsParameterName()
    {
        IReadOnlyCollection<int> collection = Array.Empty<int>();

        var exception = Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(collection));

        Assert.That(
            exception!.ParamName,
            Is.EqualTo(nameof(collection)));
    }

    /// <summary>
    /// Verifies that NotEmpty uses the expected empty-collection
    /// validation message.
    /// </summary>
    [Test]
    public void NotEmptyCollection_WithEmptyCollection_ShouldUseExpectedMessage()
    {
        IReadOnlyCollection<int> collection = Array.Empty<int>();

        var exception = Assert.Throws<ArgumentException>(
            () => Guard.NotEmpty(collection));

        Assert.That(
            exception!.Message,
            Does.StartWith("The collection cannot be empty."));
    }

    /// <summary>
    /// Verifies that NotEmpty rejects a null collection.
    /// </summary>
    [Test]
    public void NotEmptyCollection_WithNullCollection_ShouldThrowArgumentNullException()
    {
        IReadOnlyCollection<int>? collection = null;

        Assert.That(
            () => Guard.NotEmpty(collection!),
            Throws.TypeOf<ArgumentNullException>());
    }

    /// <summary>
    /// Verifies that NotEmpty identifies a null collection correctly.
    /// </summary>
    [Test]
    public void NotEmptyCollection_WithNullCollection_ShouldUseExpectedParameterName()
    {
        IReadOnlyCollection<int>? collection = null;

        var exception = Assert.Throws<ArgumentNullException>(
            () => Guard.NotEmpty(collection!));

        Assert.That(
            exception!.ParamName,
            Is.EqualTo(nameof(collection)));
    }

    /// <summary>
    /// Verifies that NotEmpty accepts a List implementing
    /// IReadOnlyCollection{T}.
    /// </summary>
    [Test]
    public void NotEmptyCollection_WithList_ShouldReturnSameCollection()
    {
        IReadOnlyCollection<int> collection = new List<int>
        {
            10,
            20
        };

        var result = Guard.NotEmpty(collection);

        Assert.That(result, Is.SameAs(collection));
    }

    #endregion
}
