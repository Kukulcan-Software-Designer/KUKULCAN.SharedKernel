using KUKULCAN.SharedKernel.Identifiers;

namespace KUKULCAN.SharedKernel.UnitTests.Identifiers;

/// <summary>
/// Contains unit tests for
/// <see cref="KUKULCAN.SharedKernel.Identifiers.StringEntityId"/>.
/// </summary>
[TestFixture]
public sealed class StringEntityIdTests
{
    /// <summary>
    /// Verifies that a non-empty string is accepted.
    /// </summary>
    [Test]
    public void Constructor_WithValidValue_ShouldCreateIdentifier()
    {
        var identifier = new TestStringEntityId("ABC-123");

        Assert.That(identifier, Is.Not.Null);
    }

    /// <summary>
    /// Verifies that the supplied string is stored unchanged.
    /// </summary>
    [Test]
    public void Constructor_WithValidValue_ShouldStoreValue()
    {
        const string value = "ABC-123";

        var identifier = new TestStringEntityId(value);

        Assert.That(
            identifier.Value,
            Is.EqualTo(value));
    }

    /// <summary>
    /// Verifies that a string containing surrounding whitespace is accepted
    /// and preserved exactly.
    /// </summary>
    [Test]
    public void Constructor_WithValidValueContainingWhitespace_ShouldPreserveValue()
    {
        const string value = "  ABC-123  ";

        var identifier = new TestStringEntityId(value);

        Assert.That(
            identifier.Value,
            Is.EqualTo(value));
    }

    /// <summary>
    /// Verifies that a null value is rejected with ArgumentNullException.
    /// </summary>
    [Test]
    public void Constructor_WithNullValue_ShouldThrowArgumentNullException()
    {
        Assert.That(
            () => new TestStringEntityId(null!),
            Throws.TypeOf<ArgumentNullException>());
    }

    /// <summary>
    /// Verifies that an empty string is rejected with ArgumentException.
    /// </summary>
    [Test]
    public void Constructor_WithEmptyValue_ShouldThrowArgumentException()
    {
        Assert.That(
            () => new TestStringEntityId(string.Empty),
            Throws.TypeOf<ArgumentException>());
    }

    /// <summary>
    /// Verifies that a whitespace-only string is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithWhitespaceValue_ShouldThrowArgumentException()
    {
        Assert.That(
            () => new TestStringEntityId("   "),
            Throws.TypeOf<ArgumentException>());
    }

    /// <summary>
    /// Verifies that a tab-only string is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithTabOnlyValue_ShouldThrowArgumentException()
    {
        Assert.That(
            () => new TestStringEntityId("\t\t"),
            Throws.TypeOf<ArgumentException>());
    }

    /// <summary>
    /// Verifies that a newline-only string is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithNewLineOnlyValue_ShouldThrowArgumentException()
    {
        Assert.That(
            () => new TestStringEntityId("\r\n"),
            Throws.TypeOf<ArgumentException>());
    }

    /// <summary>
    /// Verifies that the null-value exception identifies the value parameter.
    /// </summary>
    [Test]
    public void Constructor_WithNullValue_ShouldUseValueAsParameterName()
    {
        string? value = null;

        var exception = Assert.Throws<ArgumentNullException>(
            () => new TestStringEntityId(value!));

        Assert.That(
            exception!.ParamName,
            Is.EqualTo(nameof(value)));
    }

    /// <summary>
    /// Verifies that the empty-value exception identifies the value parameter.
    /// </summary>
    [Test]
    public void Constructor_WithEmptyValue_ShouldUseValueAsParameterName()
    {
        var value = string.Empty;

        var exception = Assert.Throws<ArgumentException>(
            () => new TestStringEntityId(value));

        Assert.That(
            exception!.ParamName,
            Is.EqualTo(nameof(value)));
    }

    /// <summary>
    /// Verifies that the whitespace-value exception identifies the value
    /// parameter.
    /// </summary>
    [Test]
    public void Constructor_WithWhitespaceValue_ShouldUseValueAsParameterName()
    {
        var value = "   ";

        var exception = Assert.Throws<ArgumentException>(
            () => new TestStringEntityId(value));

        Assert.That(
            exception!.ParamName,
            Is.EqualTo(nameof(value)));
    }

    /// <summary>
    /// Verifies that the parameterless constructor can create an identifier
    /// for materialization scenarios.
    /// </summary>
    [Test]
    public void ParameterlessConstructor_ShouldCreateIdentifier()
    {
        var identifier = new TestStringEntityId();

        Assert.That(identifier, Is.Not.Null);
    }

    /// <summary>
    /// Verifies that StringEntityId derives from EntityId{string}.
    /// </summary>
    [Test]
    public void StringEntityId_ShouldDeriveFromEntityIdString()
    {
        var identifier = new TestStringEntityId("ABC-123");

        Assert.That(
            identifier,
            Is.InstanceOf<EntityId<string>>());
    }

    /// <summary>
    /// Verifies that equal string identifiers compare as equal.
    /// </summary>
    [Test]
    public void Equals_WithSameValue_ShouldReturnTrue()
    {
        var left = new TestStringEntityId("ABC-123");
        var right = new TestStringEntityId("ABC-123");

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    /// <summary>
    /// Verifies that different string identifiers compare as unequal.
    /// </summary>
    [Test]
    public void Equals_WithDifferentValue_ShouldReturnFalse()
    {
        var left = new TestStringEntityId("ABC-123");
        var right = new TestStringEntityId("ABC-456");

        Assert.That(
            left.Equals(right),
            Is.False);
    }

    /// <summary>
    /// Verifies that equal string identifiers have the same hash code.
    /// </summary>
    [Test]
    public void GetHashCode_WithEqualIdentifiers_ShouldReturnSameHashCode()
    {
        var left = new TestStringEntityId("ABC-123");
        var right = new TestStringEntityId("ABC-123");

        Assert.That(
            left.GetHashCode(),
            Is.EqualTo(right.GetHashCode()));
    }

    /// <summary>
    /// Verifies that ToString returns the underlying string representation.
    /// </summary>
    [Test]
    public void ToString_ShouldReturnUnderlyingValueRepresentation()
    {
        const string value = "ABC-123";

        var identifier = new TestStringEntityId(value);

        Assert.That(
            identifier.ToString(),
            Is.EqualTo(value));
    }

    /// <summary>
    /// Test implementation of StringEntityId used exclusively by the
    /// unit tests.
    /// </summary>
    private sealed class TestStringEntityId : StringEntityId
    {
        /// <summary>
        /// Initializes a new test identifier for materialization scenarios.
        /// </summary>
        public TestStringEntityId()
        {
        }

        /// <summary>
        /// Initializes a new test identifier with the specified value.
        /// </summary>
        /// <param name="value">Identifier value.</param>
        public TestStringEntityId(string value)
            : base(value)
        {
        }
    }
}
