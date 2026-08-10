using KUKULCAN.SharedKernel.Identifiers;

namespace KUKULCAN.SharedKernel.UnitTests.Identifiers;

/// <summary>
/// Contains unit tests for
/// <see cref="KUKULCAN.SharedKernel.Identifiers.GuidEntityId"/>.
/// </summary>
[TestFixture]
public sealed class GuidEntityIdTests
{
    /// <summary>
    /// Verifies that a non-empty Guid is accepted.
    /// </summary>
    [Test]
    public void Constructor_WithValidGuid_ShouldCreateIdentifier()
    {
        var value = Guid.NewGuid();

        var identifier = new TestGuidEntityId(value);

        Assert.That(identifier, Is.Not.Null);
    }

    /// <summary>
    /// Verifies that the supplied Guid is stored as the identifier value.
    /// </summary>
    [Test]
    public void Constructor_WithValidGuid_ShouldStoreValue()
    {
        var value = Guid.NewGuid();

        var identifier = new TestGuidEntityId(value);

        Assert.That(
            identifier.Value,
            Is.EqualTo(value));
    }

    /// <summary>
    /// Verifies that Guid.Empty is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithEmptyGuid_ShouldThrowArgumentException()
    {
        Assert.That(
            () => new TestGuidEntityId(Guid.Empty),
            Throws.TypeOf<ArgumentException>());
    }

    /// <summary>
    /// Verifies that the exception identifies the value parameter.
    /// </summary>
    [Test]
    public void Constructor_WithEmptyGuid_ShouldUseValueAsParameterName()
    {
        var value = Guid.Empty;

        var exception = Assert.Throws<ArgumentException>(
            () => new TestGuidEntityId(value));

        Assert.That(
            exception!.ParamName,
            Is.EqualTo(nameof(value)));
    }

    /// <summary>
    /// Verifies that a non-empty Guid is preserved exactly.
    /// </summary>
    [Test]
    public void Constructor_WithValidGuid_ShouldPreserveExactValue()
    {
        var value = Guid.Parse(
            "7f7e3a5d-4f6b-4c0a-8b2d-1d3e5f6a7b8c");

        var identifier = new TestGuidEntityId(value);

        Assert.That(
            identifier.Value,
            Is.EqualTo(value));
    }

    /// <summary>
    /// Verifies that the parameterless constructor can create an
    /// identifier instance for materialization scenarios.
    /// </summary>
    [Test]
    public void ParameterlessConstructor_ShouldCreateIdentifier()
    {
        var identifier = new TestGuidEntityId();

        Assert.That(identifier, Is.Not.Null);
    }

    /// <summary>
    /// Verifies that the parameterless constructor leaves the underlying
    /// Guid at its default value.
    /// </summary>
    [Test]
    public void ParameterlessConstructor_ShouldInitializeDefaultGuid()
    {
        var identifier = new TestGuidEntityId();

        Assert.That(
            identifier.Value,
            Is.EqualTo(Guid.Empty));
    }

    /// <summary>
    /// Verifies that GuidEntityId derives from EntityId{Guid}.
    /// </summary>
    [Test]
    public void GuidEntityId_ShouldDeriveFromEntityIdGuid()
    {
        var identifier = new TestGuidEntityId(Guid.NewGuid());

        Assert.That(
            identifier,
            Is.InstanceOf<EntityId<Guid>>());
    }

    /// <summary>
    /// Verifies that the identifier string representation is based on
    /// the underlying Guid value.
    /// </summary>
    [Test]
    public void ToString_ShouldReturnGuidRepresentation()
    {
        var value = Guid.NewGuid();
        var identifier = new TestGuidEntityId(value);

        Assert.That(
            identifier.ToString(),
            Is.EqualTo(value.ToString()));
    }

    /// <summary>
    /// Verifies that two identifiers with the same concrete type and
    /// value are equal.
    /// </summary>
    [Test]
    public void Equals_WithSameTypeAndValue_ShouldReturnTrue()
    {
        var value = Guid.NewGuid();

        var left = new TestGuidEntityId(value);
        var right = new TestGuidEntityId(value);

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    /// <summary>
    /// Verifies that identifiers with different Guid values are not equal.
    /// </summary>
    [Test]
    public void Equals_WithDifferentValue_ShouldReturnFalse()
    {
        var left = new TestGuidEntityId(Guid.NewGuid());
        var right = new TestGuidEntityId(Guid.NewGuid());

        Assert.That(
            left.Equals(right),
            Is.False);
    }

    /// <summary>
    /// Verifies that the hash code is consistent for equal identifiers.
    /// </summary>
    [Test]
    public void GetHashCode_WithEqualIdentifiers_ShouldReturnSameHashCode()
    {
        var value = Guid.NewGuid();

        var left = new TestGuidEntityId(value);
        var right = new TestGuidEntityId(value);

        Assert.That(
            left.GetHashCode(),
            Is.EqualTo(right.GetHashCode()));
    }

    /// <summary>
    /// Test implementation of GuidEntityId used exclusively by the
    /// unit tests.
    /// </summary>
    private sealed class TestGuidEntityId : GuidEntityId
    {
        /// <summary>
        /// Initializes a new test identifier for materialization scenarios.
        /// </summary>
        public TestGuidEntityId()
        {
        }

        /// <summary>
        /// Initializes a new test identifier with the specified Guid.
        /// </summary>
        /// <param name="value">Identifier value.</param>
        public TestGuidEntityId(Guid value)
            : base(value)
        {
        }
    }
}
