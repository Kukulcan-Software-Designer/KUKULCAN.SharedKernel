using KUKULCAN.SharedKernel.Abstractions.Capabilities;
using KUKULCAN.SharedKernel.Domain;
using KUKULCAN.SharedKernel.Identifiers.Interfaces;

namespace KUKULCAN.SharedKernel.UnitTests.Domain;

/// <summary>
/// Contains unit tests for <see cref="AuditableEntity{TId}"/>.
/// </summary>
[TestFixture]
public sealed class AuditableEntityTests
{
    #region Construction

    /// <summary>
    /// Verifies that the identifier constructor assigns the supplied identifier.
    /// </summary>
    [Test]
    public void Constructor_WithIdentifier_ShouldAssignIdentifier()
    {
        var id = new TestEntityId(Guid.NewGuid());

        var entity = new TestAuditableEntity(id);

        Assert.That(
            entity.Id,
            Is.EqualTo(id));
    }

    /// <summary>
    /// Verifies that the parameterless constructor initializes the identifier
    /// to its default value.
    /// </summary>
    [Test]
    public void ParameterlessConstructor_ShouldInitializeIdentifierToDefault()
    {
        var entity = new TestAuditableEntity();

        Assert.That(
            entity.Id,
            Is.Null);
    }

    /// <summary>
    /// Verifies that the identifier constructor rejects a null identifier.
    /// </summary>
    [Test]
    public void Constructor_WithNullIdentifier_ShouldThrowArgumentNullException()
    {
        Assert.That(
            () => new TestAuditableEntity(null!),
            Throws.ArgumentNullException);
    }

    #endregion

    #region CreatedOn

    /// <summary>
    /// Verifies that CreatedOn is initialized to DateTimeOffset.MinValue.
    /// </summary>
    [Test]
    public void CreatedOn_ByDefault_ShouldBeMinValue()
    {
        var entity = new TestAuditableEntity();

        Assert.That(
            entity.CreatedOn,
            Is.EqualTo(DateTimeOffset.MinValue));
    }

    #endregion

    #region ModifiedOn

    /// <summary>
    /// Verifies that ModifiedOn is null by default.
    /// </summary>
    [Test]
    public void ModifiedOn_ByDefault_ShouldBeNull()
    {
        var entity = new TestAuditableEntity();

        Assert.That(
            entity.ModifiedOn,
            Is.Null);
    }

    #endregion

    #region IAuditable

    /// <summary>
    /// Verifies that the entity implements IAuditable.
    /// </summary>
    [Test]
    public void Entity_ShouldImplementIAuditable()
    {
        var entity = new TestAuditableEntity();

        Assert.That(
            entity,
            Is.AssignableTo<IAuditable>());
    }

    /// <summary>
    /// Verifies that the IAuditable interface exposes the same CreatedOn value
    /// as the entity.
    /// </summary>
    [Test]
    public void IAuditable_CreatedOn_ShouldReturnEntityCreatedOn()
    {
        var entity = new TestAuditableEntity();

        IAuditable auditable = entity;

        Assert.That(
            auditable.CreatedOn,
            Is.EqualTo(entity.CreatedOn));
    }

    /// <summary>
    /// Verifies that the IAuditable interface exposes the same ModifiedOn value
    /// as the entity.
    /// </summary>
    [Test]
    public void IAuditable_ModifiedOn_ShouldReturnEntityModifiedOn()
    {
        var entity = new TestAuditableEntity();

        IAuditable auditable = entity;

        Assert.That(
            auditable.ModifiedOn,
            Is.EqualTo(entity.ModifiedOn));
    }

    #endregion

    #region Inheritance

    /// <summary>
    /// Verifies that the entity inherits from AggregateRoot with the expected
    /// identifier type.
    /// </summary>
    [Test]
    public void Entity_ShouldInheritFromAggregateRoot()
    {
        var entity = new TestAuditableEntity();

        Assert.That(
            entity,
            Is.InstanceOf<AggregateRoot<TestEntityId>>());
    }

    #endregion

    #region Test implementation

    private sealed class TestAuditableEntity
        : AuditableEntity<TestEntityId>
    {
        public TestAuditableEntity()
        {
        }

        public TestAuditableEntity(TestEntityId id)
            : base(id)
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

        public bool Equals(TestEntityId? other)
        {
            return other is not null &&
                   Value.Equals(other.Value);
        }

        public override bool Equals(object? obj)
        {
            return obj is TestEntityId entityId &&
                   Equals(entityId);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString() ?? string.Empty;
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
