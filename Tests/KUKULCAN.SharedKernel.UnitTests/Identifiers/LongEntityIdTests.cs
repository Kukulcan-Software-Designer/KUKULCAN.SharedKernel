using KUKULCAN.SharedKernel.Identifiers;

namespace KUKULCAN.SharedKernel.UnitTests.Identifiers;

/// <summary>
/// Contains unit tests for
/// <see cref="KUKULCAN.SharedKernel.Identifiers.LongEntityId"/>.
/// </summary>
[TestFixture]
public sealed class LongEntityIdTests
{
    /// <summary>
    /// Verifies that a positive long value is accepted.
    /// </summary>
    [Test]
    public void Constructor_WithPositiveValue_ShouldCreateIdentifier()
    {
        var identifier = new TestLongEntityId(42L);

        Assert.That(identifier, Is.Not.Null);
    }

    /// <summary>
    /// Verifies that the supplied value is stored.
    /// </summary>
    [Test]
    public void Constructor_WithPositiveValue_ShouldStoreValue()
    {
        const long value = 42L;

        var identifier = new TestLongEntityId(value);

        Assert.That(
            identifier.Value,
            Is.EqualTo(value));
    }

    /// <summary>
    /// Verifies that the smallest valid long value is accepted.
    /// </summary>
    [Test]
    public void Constructor_WithValueOne_ShouldCreateIdentifier()
    {
        var identifier = new TestLongEntityId(1L);

        Assert.That(
            identifier.Value,
            Is.EqualTo(1L));
    }

    /// <summary>
    /// Verifies that zero is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithZero_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.That(
            () => new TestLongEntityId(0L),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /// <summary>
    /// Verifies that negative values are rejected.
    /// </summary>
    [Test]
    public void Constructor_WithNegativeValue_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.That(
            () => new TestLongEntityId(-1L),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /// <summary>
    /// Verifies that the smallest possible long value is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithMinimumLongValue_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.That(
            () => new TestLongEntityId(long.MinValue),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /// <summary>
    /// Verifies that the exception identifies the value parameter.
    /// </summary>
    [Test]
    public void Constructor_WithInvalidValue_ShouldUseValueAsParameterName()
    {
        long value = 0L;

        var exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => new TestLongEntityId(value));

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
        var identifier = new TestLongEntityId();

        Assert.That(identifier, Is.Not.Null);
    }

    /// <summary>
    /// Verifies that the parameterless constructor leaves the underlying
    /// long value at its default value.
    /// </summary>
    [Test]
    public void ParameterlessConstructor_ShouldInitializeDefaultValue()
    {
        var identifier = new TestLongEntityId();

        Assert.That(
            identifier.Value,
            Is.EqualTo(0L));
    }

    /// <summary>
    /// Verifies that LongEntityId derives from EntityId{long}.
    /// </summary>
    [Test]
    public void LongEntityId_ShouldDeriveFromEntityIdLong()
    {
        var identifier = new TestLongEntityId(42L);

        Assert.That(
            identifier,
            Is.InstanceOf<EntityId<long>>());
    }

    /// <summary>
    /// Verifies that the maximum positive long value is accepted.
    /// </summary>
    [Test]
    public void Constructor_WithMaximumLongValue_ShouldStoreValue()
    {
        var identifier = new TestLongEntityId(long.MaxValue);

        Assert.That(
            identifier.Value,
            Is.EqualTo(long.MaxValue));
    }

    /// <summary>
    /// Verifies that equal long identifiers compare as equal.
    /// </summary>
    [Test]
    public void Equals_WithSameValue_ShouldReturnTrue()
    {
        var left = new TestLongEntityId(42L);
        var right = new TestLongEntityId(42L);

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    /// <summary>
    /// Verifies that different long identifiers compare as unequal.
    /// </summary>
    [Test]
    public void Equals_WithDifferentValue_ShouldReturnFalse()
    {
        var left = new TestLongEntityId(42L);
        var right = new TestLongEntityId(43L);

        Assert.That(
            left.Equals(right),
            Is.False);
    }

    /// <summary>
    /// Verifies that equal long identifiers have the same hash code.
    /// </summary>
    [Test]
    public void GetHashCode_WithEqualIdentifiers_ShouldReturnSameHashCode()
    {
        var left = new TestLongEntityId(42L);
        var right = new TestLongEntityId(42L);

        Assert.That(
            left.GetHashCode(),
            Is.EqualTo(right.GetHashCode()));
    }

    /// <summary>
    /// Verifies that ToString returns the underlying long representation.
    /// </summary>
    [Test]
    public void ToString_ShouldReturnUnderlyingValueRepresentation()
    {
        var identifier = new TestLongEntityId(12345L);

        Assert.That(
            identifier.ToString(),
            Is.EqualTo("12345"));
    }

    /// <summary>
    /// Test implementation of LongEntityId used exclusively by the
    /// unit tests.
    /// </summary>
    private sealed class TestLongEntityId : LongEntityId
    {
        /// <summary>
        /// Initializes a new test identifier for materialization scenarios.
        /// </summary>
        public TestLongEntityId()
        {
        }

        /// <summary>
        /// Initializes a new test identifier with the specified value.
        /// </summary>
        /// <param name="value">Identifier value.</param>
        public TestLongEntityId(long value)
            : base(value)
        {
        }
    }
}
