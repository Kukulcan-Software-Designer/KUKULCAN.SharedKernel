using KUKULCAN.SharedKernel.Abstractions;
using KUKULCAN.SharedKernel.Domain;
using KUKULCAN.SharedKernel.Identifiers.Interfaces;

namespace KUKULCAN.SharedKernel.UnitTests.Domain;

/// <summary>
/// Contains unit tests for <see cref="Entity{TId}"/>.
/// </summary>
[TestFixture]
public sealed class EntityTests
{
    #region Construction

    /// <summary>
    /// Verifies that the identifier constructor assigns the supplied identifier.
    /// </summary>
    [Test]
    public void Constructor_WithIdentifier_ShouldAssignIdentifier()
    {
        var id = new TestEntityId(Guid.NewGuid());

        var entity = new TestEntity(id);

        Assert.That(
            entity.Id,
            Is.EqualTo(id));
    }

    /// <summary>
    /// Verifies that the identifier constructor rejects a null identifier.
    /// </summary>
    [Test]
    public void Constructor_WithNullIdentifier_ShouldThrowArgumentNullException()
    {
        Assert.That(
            () => new TestEntity(null!),
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that the parameterless constructor initializes the identifier
    /// to its default value.
    /// </summary>
    [Test]
    public void ParameterlessConstructor_ShouldInitializeIdentifierToDefault()
    {
        var entity = new TestEntity();

        Assert.That(
            entity.Id,
            Is.Null);
    }

    #endregion

    #region Equality

    /// <summary>
    /// Verifies that an entity is equal to itself.
    /// </summary>
    [Test]
    public void Equals_WithSameReference_ShouldReturnTrue()
    {
        var entity = new TestEntity(
            new TestEntityId(Guid.NewGuid()));

        Assert.That(
            entity.Equals(entity),
            Is.True);
    }

    /// <summary>
    /// Verifies that an entity is not equal to null.
    /// </summary>
    [Test]
    public void Equals_WithNull_ShouldReturnFalse()
    {
        var entity = new TestEntity(
            new TestEntityId(Guid.NewGuid()));

        Assert.That(
            entity,
            Is.Not.EqualTo((Entity<TestEntityId>?)null));
    }

    /// <summary>
    /// Verifies that two entities of the same type with the same identifier
    /// are equal.
    /// </summary>
    [Test]
    public void Equals_WithSameTypeAndSameIdentifier_ShouldReturnTrue()
    {
        var id = new TestEntityId(Guid.NewGuid());

        var left = new TestEntity(id);
        var right = new TestEntity(id);

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    /// <summary>
    /// Verifies that two entities of the same type with different identifiers
    /// are not equal.
    /// </summary>
    [Test]
    public void Equals_WithSameTypeAndDifferentIdentifier_ShouldReturnFalse()
    {
        var left = new TestEntity(
            new TestEntityId(Guid.NewGuid()));

        var right = new TestEntity(
            new TestEntityId(Guid.NewGuid()));

        Assert.That(
            left.Equals(right),
            Is.False);
    }

    /// <summary>
    /// Verifies that two entities of different runtime types are not equal,
    /// even when their identifiers are equal.
    /// </summary>
    [Test]
    public void Equals_WithDifferentTypesAndSameIdentifier_ShouldReturnFalse()
    {
        var id = new TestEntityId(Guid.NewGuid());

        var left = new TestEntity(id);
        var right = new OtherTestEntity(id);

        Assert.That(
            left.Equals(right),
            Is.False);
    }

    /// <summary>
    /// Verifies that Equals(object) recognizes equal entities.
    /// </summary>
    [Test]
    public void EqualsObject_WithSameTypeAndSameIdentifier_ShouldReturnTrue()
    {
        var id = new TestEntityId(Guid.NewGuid());

        var left = new TestEntity(id);
        object right = new TestEntity(id);

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    /// <summary>
    /// Verifies that Equals(object) returns false for null.
    /// </summary>
    [Test]
    public void EqualsObject_WithNull_ShouldReturnFalse()
    {
        var entity = new TestEntity(
            new TestEntityId(Guid.NewGuid()));

        Assert.That(
            entity.Equals((object?)null),
            Is.False);
    }

    /// <summary>
    /// Verifies that Equals(object) returns false for an unrelated object.
    /// </summary>
    [Test]
    public void EqualsObject_WithDifferentObjectType_ShouldReturnFalse()
    {
        var entity = new TestEntity(
            new TestEntityId(Guid.NewGuid()));

        Assert.That(
            entity.Equals(new object()),
            Is.False);
    }

    #endregion

    #region IEntity

    /// <summary>
    /// Verifies that the entity implements the generic IEntity contract.
    /// </summary>
    [Test]
    public void Entity_ShouldImplementGenericIEntity()
    {
        var entity = new TestEntity(
            new TestEntityId(Guid.NewGuid()));

        Assert.That(
            entity,
            Is.AssignableTo<IEntity<TestEntityId>>());
    }

    /// <summary>
    /// Verifies that the entity implements the non-generic IEntity contract.
    /// </summary>
    [Test]
    public void Entity_ShouldImplementIEntity()
    {
        var entity = new TestEntity(
            new TestEntityId(Guid.NewGuid()));

        Assert.That(
            entity,
            Is.AssignableTo<IEntity>());
    }

    /// <summary>
    /// Verifies that the non-generic IEntity interface exposes the same
    /// identifier instance as the generic property.
    /// </summary>
    [Test]
    public void IEntity_Id_ShouldReturnSameIdentifier()
    {
        var id = new TestEntityId(Guid.NewGuid());
        var entity = new TestEntity(id);

        IEntity nonGenericEntity = entity;

        Assert.That(
            nonGenericEntity.Id,
            Is.SameAs(id));
    }

    #endregion

    #region Operators

    /// <summary>
    /// Verifies that the equality operator returns true for equal entities.
    /// </summary>
    [Test]
    public void EqualityOperator_WithEqualEntities_ShouldReturnTrue()
    {
        var id = new TestEntityId(Guid.NewGuid());

        var left = new TestEntity(id);
        var right = new TestEntity(id);

        Assert.That(
            left == right,
            Is.True);
    }

    /// <summary>
    /// Verifies that the equality operator returns false for different entities.
    /// </summary>
    [Test]
    public void EqualityOperator_WithDifferentEntities_ShouldReturnFalse()
    {
        var left = new TestEntity(
            new TestEntityId(Guid.NewGuid()));

        var right = new TestEntity(
            new TestEntityId(Guid.NewGuid()));

        Assert.That(
            left == right,
            Is.False);
    }

    /// <summary>
    /// Verifies that the equality operator returns true when both operands
    /// are null.
    /// </summary>
    [Test]
    public void EqualityOperator_WithBothNull_ShouldReturnTrue()
    {
        TestEntity? left = null;
        TestEntity? right = null;

        Assert.That(
            left == right,
            Is.True);
    }

    /// <summary>
    /// Verifies that the equality operator returns false when only the left
    /// operand is null.
    /// </summary>
    [Test]
    public void EqualityOperator_WithNullLeft_ShouldReturnFalse()
    {
        TestEntity? left = null;

        var right = new TestEntity(
            new TestEntityId(Guid.NewGuid()));

        Assert.That(
            left == right,
            Is.False);
    }

    /// <summary>
    /// Verifies that the equality operator returns false when only the right
    /// operand is null.
    /// </summary>
    [Test]
    public void EqualityOperator_WithNullRight_ShouldReturnFalse()
    {
        var left = new TestEntity(
            new TestEntityId(Guid.NewGuid()));

        TestEntity? right = null;

        Assert.That(
            left == right,
            Is.False);
    }

    /// <summary>
    /// Verifies that the inequality operator returns false for equal entities.
    /// </summary>
    [Test]
    public void InequalityOperator_WithEqualEntities_ShouldReturnFalse()
    {
        var id = new TestEntityId(Guid.NewGuid());

        var left = new TestEntity(id);
        var right = new TestEntity(id);

        Assert.That(
            left != right,
            Is.False);
    }

    /// <summary>
    /// Verifies that the inequality operator returns true for different entities.
    /// </summary>
    [Test]
    public void InequalityOperator_WithDifferentEntities_ShouldReturnTrue()
    {
        var left = new TestEntity(
            new TestEntityId(Guid.NewGuid()));

        var right = new TestEntity(
            new TestEntityId(Guid.NewGuid()));

        Assert.That(
            left != right,
            Is.True);
    }

    /// <summary>
    /// Verifies that the inequality operator returns false when both operands
    /// are null.
    /// </summary>
    [Test]
    public void InequalityOperator_WithBothNull_ShouldReturnFalse()
    {
        TestEntity? left = null;
        TestEntity? right = null;

        Assert.That(
            left != right,
            Is.False);
    }

    #endregion

    #region Hash code

    /// <summary>
    /// Verifies that equal entities produce equal hash codes.
    /// </summary>
    [Test]
    public void GetHashCode_WithEqualEntities_ShouldReturnSameHashCode()
    {
        var id = new TestEntityId(Guid.NewGuid());

        var left = new TestEntity(id);
        var right = new TestEntity(id);

        Assert.That(
            left.GetHashCode(),
            Is.EqualTo(right.GetHashCode()));
    }

    #endregion

    #region ToString

    /// <summary>
    /// Verifies the textual representation of an entity.
    /// </summary>
    [Test]
    public void ToString_ShouldIncludeTypeNameAndIdentifier()
    {
        var id = new TestEntityId(Guid.NewGuid());

        var entity = new TestEntity(id);

        Assert.That(
            entity.ToString(),
            Is.EqualTo($"TestEntity [{id}]"));
    }

    #endregion

    #region Test implementations

    private sealed class TestEntity
        : Entity<TestEntityId>
    {
        public TestEntity()
        {
        }

        public TestEntity(TestEntityId id)
            : base(id)
        {
        }
    }

    private sealed class OtherTestEntity : Entity<TestEntityId>
    {
        public OtherTestEntity(TestEntityId id) : base(id)
        {
        }
    }

    private sealed class TestEntityId : IEntityId
    {
        public TestEntityId(Guid value)
        {
            Value = value;
        }

        public object Value { get; }

        public bool Equals(IEntityId? other)
        {
            return other is TestEntityId entityId &&
                   Equals(entityId);
        }

        public override bool Equals(object? obj)
        {
            return obj is TestEntityId other &&
                   Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString()!;
        }

        public static bool operator ==(
            TestEntityId? left,
            TestEntityId? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(
            TestEntityId? left,
            TestEntityId? right)
        {
            return !(left == right);
        }
    }

    #endregion
}
