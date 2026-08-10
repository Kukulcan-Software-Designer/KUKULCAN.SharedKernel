using KUKULCAN.SharedKernel.Identifiers;
using KUKULCAN.SharedKernel.Identifiers.Interfaces;

namespace KUKULCAN.SharedKernel.UnitTests.Identifiers;

/// <summary>
/// Contains unit tests for
/// <see cref="KUKULCAN.SharedKernel.Identifiers.EntityId{TValue}"/>.
/// </summary>
[TestFixture]
public sealed class EntityIdTests
{
    /// <summary>
    /// Verifies that the constructor stores the supplied value.
    /// </summary>
    [Test]
    public void Constructor_WithValue_ShouldStoreValue()
    {
        var identifier = new TestEntityId(42);

        Assert.That(identifier.Value, Is.EqualTo(42));
    }

    /// <summary>
    /// Verifies that the parameterless constructor initializes the value
    /// to its default representation.
    /// </summary>
    [Test]
    public void ParameterlessConstructor_ShouldInitializeValueToDefault()
    {
        var identifier = new TestEntityId();

        Assert.That(identifier.Value, Is.EqualTo(0));
    }

    /// <summary>
    /// Verifies that two identifiers with the same concrete type and value
    /// are equal.
    /// </summary>
    [Test]
    public void Equals_WithSameTypeAndValue_ShouldReturnTrue()
    {
        var left = new TestEntityId(42);
        var right = new TestEntityId(42);

        Assert.That(left.Equals(right), Is.True);
    }

    /// <summary>
    /// Verifies that two identifiers with different values are not equal.
    /// </summary>
    [Test]
    public void Equals_WithDifferentValue_ShouldReturnFalse()
    {
        var left = new TestEntityId(42);
        var right = new TestEntityId(43);

        Assert.That(left.Equals(right), Is.False);
    }

    /// <summary>
    /// Verifies that identifiers with different concrete types are not equal
    /// even when their underlying values are equal.
    /// </summary>
    [Test]
    public void Equals_WithDifferentConcreteType_ShouldReturnFalse()
    {
        var first = new TestEntityId(42);
        var second = new OtherTestEntityId(42);

        Assert.That(first.Equals(second), Is.False);
    }

    /// <summary>
    /// Verifies that comparing an identifier with null returns false.
    /// </summary>
    [Test]
    public void Equals_WithNull_ShouldReturnFalse()
    {
        var identifier = new TestEntityId(42);

        Assert.That(identifier.Equals((TestEntityId?)null), Is.False);
    }

    /// <summary>
    /// Verifies that comparing an identifier with itself returns true.
    /// </summary>
    [Test]
    public void Equals_WithSameInstance_ShouldReturnTrue()
    {
        var identifier = new TestEntityId(42);

        Assert.That(identifier.Equals(identifier), Is.True);
    }

    /// <summary>
    /// Verifies equality through IEntityId{TValue}.
    /// </summary>
    [Test]
    public void Equals_WithTypedEntityIdInterface_ShouldReturnTrueForEqualIdentifier()
    {
        var identifier = new TestEntityId(42);
        IEntityId<int> other = new TestEntityId(42);

        Assert.That(identifier.Equals(other), Is.True);
    }

    /// <summary>
    /// Verifies that equality through IEntityId{TValue} rejects
    /// a different concrete identifier type.
    /// </summary>
    [Test]
    public void Equals_WithTypedEntityIdInterfaceAndDifferentConcreteType_ShouldReturnFalse()
    {
        var identifier = new TestEntityId(42);
        IEntityId<int> other = new OtherTestEntityId(42);

        Assert.That(identifier.Equals(other), Is.False);
    }

    /// <summary>
    /// Verifies equality through the non-generic IEntityId interface.
    /// </summary>
    [Test]
    public void Equals_WithEntityIdInterface_ShouldReturnTrueForEqualIdentifier()
    {
        var identifier = new TestEntityId(42);
        IEntityId other = new TestEntityId(42);

        Assert.That(identifier.Equals(other), Is.True);
    }

    /// <summary>
    /// Verifies that equality through IEntityId rejects an identifier
    /// with a different concrete type.
    /// </summary>
    [Test]
    public void Equals_WithEntityIdInterfaceAndDifferentConcreteType_ShouldReturnFalse()
    {
        var identifier = new TestEntityId(42);
        IEntityId other = new OtherTestEntityId(42);

        Assert.That(identifier.Equals(other), Is.False);
    }

    /// <summary>
    /// Verifies equality through object.Equals.
    /// </summary>
    [Test]
    public void EqualsObject_WithEqualIdentifier_ShouldReturnTrue()
    {
        var identifier = new TestEntityId(42);
        object other = new TestEntityId(42);

        Assert.That(identifier.Equals(other), Is.True);
    }

    /// <summary>
    /// Verifies that object.Equals rejects an object of another type.
    /// </summary>
    [Test]
    public void EqualsObject_WithDifferentObjectType_ShouldReturnFalse()
    {
        var identifier = new TestEntityId(42);

        Assert.That(identifier.Equals("42"), Is.False);
    }

    /// <summary>
    /// Verifies that object.Equals rejects null.
    /// </summary>
    [Test]
    public void EqualsObject_WithNull_ShouldReturnFalse()
    {
        var identifier = new TestEntityId(42);

        Assert.That(identifier.Equals(null), Is.False);
    }

    /// <summary>
    /// Verifies that equal identifiers have the same hash code.
    /// </summary>
    [Test]
    public void GetHashCode_WithEqualIdentifiers_ShouldReturnSameHashCode()
    {
        var left = new TestEntityId(42);
        var right = new TestEntityId(42);

        Assert.That(
            left.GetHashCode(),
            Is.EqualTo(right.GetHashCode()));
    }

    /// <summary>
    /// Verifies that the hash code incorporates the concrete identifier type.
    /// </summary>
    [Test]
    public void GetHashCode_WithDifferentConcreteType_ShouldNormallyDiffer()
    {
        var first = new TestEntityId(42);
        var second = new OtherTestEntityId(42);

        Assert.That(
            first.GetHashCode(),
            Is.Not.EqualTo(second.GetHashCode()));
    }

    /// <summary>
    /// Verifies the equality operator for equal identifiers.
    /// </summary>
    [Test]
    public void EqualityOperator_WithEqualIdentifiers_ShouldReturnTrue()
    {
        var left = new TestEntityId(42);
        var right = new TestEntityId(42);

        Assert.That(left == right, Is.True);
    }

    /// <summary>
    /// Verifies the equality operator for different identifiers.
    /// </summary>
    [Test]
    public void EqualityOperator_WithDifferentIdentifiers_ShouldReturnFalse()
    {
        var left = new TestEntityId(42);
        var right = new TestEntityId(43);

        Assert.That(left == right, Is.False);
    }

    /// <summary>
    /// Verifies that two null identifiers are considered equal.
    /// </summary>
    [Test]
    public void EqualityOperator_WithBothNull_ShouldReturnTrue()
    {
        TestEntityId? left = null;
        TestEntityId? right = null;

        Assert.That(left == right, Is.True);
    }

    /// <summary>
    /// Verifies that a null identifier and a non-null identifier are not equal.
    /// </summary>
    [Test]
    public void EqualityOperator_WithOneNull_ShouldReturnFalse()
    {
        TestEntityId? left = null;
        var right = new TestEntityId(42);

        Assert.That(left == right, Is.False);
    }

    /// <summary>
    /// Verifies the inequality operator for different identifiers.
    /// </summary>
    [Test]
    public void InequalityOperator_WithDifferentIdentifiers_ShouldReturnTrue()
    {
        var left = new TestEntityId(42);
        var right = new TestEntityId(43);

        Assert.That(left != right, Is.True);
    }

    /// <summary>
    /// Verifies the inequality operator for equal identifiers.
    /// </summary>
    [Test]
    public void InequalityOperator_WithEqualIdentifiers_ShouldReturnFalse()
    {
        var left = new TestEntityId(42);
        var right = new TestEntityId(42);

        Assert.That(left != right, Is.False);
    }

    /// <summary>
    /// Verifies that ToString returns the string representation of the
    /// underlying value.
    /// </summary>
    [Test]
    public void ToString_ShouldReturnUnderlyingValueRepresentation()
    {
        var identifier = new TestEntityId(12345);

        Assert.That(
            identifier.ToString(),
            Is.EqualTo("12345"));
    }

    /// <summary>
    /// Verifies that the underlying value remains strongly typed.
    /// </summary>
    [Test]
    public void Value_ShouldRemainStronglyTyped()
    {
        var identifier = new TestEntityId(42);

        int value = identifier.Value;

        Assert.That(value, Is.EqualTo(42));
    }

    /// <summary>
    /// Test implementation of EntityId{TValue} used exclusively by the
    /// unit tests.
    /// </summary>
    private sealed class TestEntityId : EntityId<int>
    {
        /// <summary>
        /// Initializes a new test identifier.
        /// </summary>
        public TestEntityId()
        {
        }

        /// <summary>
        /// Initializes a new test identifier with a value.
        /// </summary>
        /// <param name="value">Identifier value.</param>
        public TestEntityId(int value)
            : base(value)
        {
        }
    }

    /// <summary>
    /// Second test implementation used to verify that equality includes
    /// the concrete identifier type.
    /// </summary>
    private sealed class OtherTestEntityId : EntityId<int>
    {
        /// <summary>
        /// Initializes a new test identifier.
        /// </summary>
        /// <param name="value">Identifier value.</param>
        public OtherTestEntityId(int value)
            : base(value)
        {
        }
    }
}
