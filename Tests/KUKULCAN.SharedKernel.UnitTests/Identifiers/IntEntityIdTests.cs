using KUKULCAN.SharedKernel.Identifiers;

namespace KUKULCAN.SharedKernel.UnitTests.Identifiers;

/// <summary>
/// Contains unit tests for
/// <see cref="KUKULCAN.SharedKernel.Identifiers.IntEntityId"/>.
/// </summary>
[TestFixture]
public sealed class IntEntityIdTests
{
    /// <summary>
    /// Verifies that a positive integer is accepted.
    /// </summary>
    [Test]
    public void Constructor_WithPositiveValue_ShouldCreateIdentifier()
    {
        var identifier = new TestIntEntityId(42);

        Assert.That(identifier, Is.Not.Null);
    }

    /// <summary>
    /// Verifies that the supplied value is stored.
    /// </summary>
    [Test]
    public void Constructor_WithPositiveValue_ShouldStoreValue()
    {
        const int value = 42;

        var identifier = new TestIntEntityId(value);

        Assert.That(
            identifier.Value,
            Is.EqualTo(value));
    }

    /// <summary>
    /// Verifies that the smallest valid integer value is accepted.
    /// </summary>
    [Test]
    public void Constructor_WithValueOne_ShouldCreateIdentifier()
    {
        var identifier = new TestIntEntityId(1);

        Assert.That(
            identifier.Value,
            Is.EqualTo(1));
    }

    /// <summary>
    /// Verifies that zero is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithZero_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.That(
            () => new TestIntEntityId(0),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /// <summary>
    /// Verifies that negative values are rejected.
    /// </summary>
    [Test]
    public void Constructor_WithNegativeValue_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.That(
            () => new TestIntEntityId(-1),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /// <summary>
    /// Verifies that an arbitrary negative value is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithMinimumIntValue_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.That(
            () => new TestIntEntityId(int.MinValue),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /// <summary>
    /// Verifies that the exception identifies the value parameter.
    /// </summary>
    [Test]
    public void Constructor_WithInvalidValue_ShouldUseValueAsParameterName()
    {
        var value = 0;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => new TestIntEntityId(value));

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
        var identifier = new TestIntEntityId();

        Assert.That(identifier, Is.Not.Null);
    }

    /// <summary>
    /// Verifies that the parameterless constructor leaves the underlying
    /// integer at its default value.
    /// </summary>
    [Test]
    public void ParameterlessConstructor_ShouldInitializeDefaultValue()
    {
        var identifier = new TestIntEntityId();

        Assert.That(
            identifier.Value,
            Is.EqualTo(0));
    }

    /// <summary>
    /// Verifies that IntEntityId derives from EntityId{int}.
    /// </summary>
    [Test]
    public void IntEntityId_ShouldDeriveFromEntityIdInt()
    {
        var identifier = new TestIntEntityId(42);

        Assert.That(
            identifier,
            Is.InstanceOf<EntityId<int>>());
    }

    /// <summary>
    /// Verifies that the identifier preserves a large positive value.
    /// </summary>
    [Test]
    public void Constructor_WithMaximumIntValue_ShouldStoreValue()
    {
        var identifier = new TestIntEntityId(int.MaxValue);

        Assert.That(
            identifier.Value,
            Is.EqualTo(int.MaxValue));
    }

    /// <summary>
    /// Verifies that equal integer identifiers compare as equal.
    /// </summary>
    [Test]
    public void Equals_WithSameValue_ShouldReturnTrue()
    {
        var left = new TestIntEntityId(42);
        var right = new TestIntEntityId(42);

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    /// <summary>
    /// Verifies that different integer identifiers compare as unequal.
    /// </summary>
    [Test]
    public void Equals_WithDifferentValue_ShouldReturnFalse()
    {
        var left = new TestIntEntityId(42);
        var right = new TestIntEntityId(43);

        Assert.That(
            left.Equals(right),
            Is.False);
    }

    /// <summary>
    /// Verifies that equal integer identifiers have the same hash code.
    /// </summary>
    [Test]
    public void GetHashCode_WithEqualIdentifiers_ShouldReturnSameHashCode()
    {
        var left = new TestIntEntityId(42);
        var right = new TestIntEntityId(42);

        Assert.That(
            left.GetHashCode(),
            Is.EqualTo(right.GetHashCode()));
    }

    /// <summary>
    /// Verifies that ToString returns the underlying integer representation.
    /// </summary>
    [Test]
    public void ToString_ShouldReturnUnderlyingValueRepresentation()
    {
        var identifier = new TestIntEntityId(12345);

        Assert.That(
            identifier.ToString(),
            Is.EqualTo("12345"));
    }

    /// <summary>
    /// Test implementation of IntEntityId used exclusively by the
    /// unit tests.
    /// </summary>
    private sealed class TestIntEntityId : IntEntityId
    {
        /// <summary>
        /// Initializes a new test identifier for materialization scenarios.
        /// </summary>
        public TestIntEntityId()
        {
        }

        /// <summary>
        /// Initializes a new test identifier with the specified value.
        /// </summary>
        /// <param name="value">Identifier value.</param>
        public TestIntEntityId(int value)
            : base(value)
        {
        }
    }
}
