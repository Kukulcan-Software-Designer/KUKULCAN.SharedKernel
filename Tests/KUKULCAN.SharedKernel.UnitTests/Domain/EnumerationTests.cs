using KUKULCAN.SharedKernel.Domain;

namespace KUKULCAN.SharedKernel.UnitTests.Domain;

/// <summary>
/// Contains unit tests for <see cref="Enumeration"/>.
/// </summary>
[TestFixture]
public sealed class EnumerationTests
{
    #region Construction

    /// <summary>
    /// Verifies that the constructor assigns the identifier and name.
    /// </summary>
    [Test]
    public void Constructor_WithValidValues_ShouldAssignProperties()
    {
        var value = new TestEnumeration(10, "Ten");

        Assert.Multiple(() =>
        {
            Assert.That(value.Id, Is.EqualTo(10));
            Assert.That(value.Name, Is.EqualTo("Ten"));
        });
    }

    /// <summary>
    /// Verifies that a negative identifier is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithNegativeId_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.That(
            () => new TestEnumeration(-1, "Negative"),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }

    /// <summary>
    /// Verifies that a null name is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithNullName_ShouldThrowArgumentNullException()
    {
        Assert.That(
            () => new TestEnumeration(1, null!),
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that an empty name is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        Assert.That(
            () => new TestEnumeration(1, string.Empty),
            Throws.ArgumentException);
    }

    /// <summary>
    /// Verifies that a whitespace-only name is rejected.
    /// </summary>
    [Test]
    public void Constructor_WithWhitespaceName_ShouldThrowArgumentException()
    {
        Assert.That(
            () => new TestEnumeration(1, "   "),
            Throws.ArgumentException);
    }

    #endregion

    #region GetAll

    /// <summary>
    /// Verifies that GetAll returns every declared public static enumeration
    /// value ordered by identifier.
    /// </summary>
    [Test]
    public void GetAll_ShouldReturnDeclaredValuesOrderedById()
    {
        List<TestEnumeration> values = [.. Enumeration.GetAll<TestEnumeration>()];

        Assert.That(
            values,
            Has.Count.EqualTo(3));

        Assert.Multiple(() =>
        {
            Assert.That(values[0], Is.SameAs(TestEnumeration.First));
            Assert.That(values[1], Is.SameAs(TestEnumeration.Second));
            Assert.That(values[2], Is.SameAs(TestEnumeration.Third));
        });
    }

    /// <summary>
    /// Verifies that GetAll does not include values inherited from a base
    /// enumeration type.
    /// </summary>
    [Test]
    public void GetAll_ShouldOnlyIncludeValuesDeclaredByRequestedType()
    {
        List<DerivedEnumeration> values = [.. Enumeration.GetAll<DerivedEnumeration>()];

        Assert.That(
            values,
            Has.Count.EqualTo(2));

        Assert.Multiple(() =>
        {
            Assert.That(
                values,
                Has.All.Not.SameAs(BaseEnumeration.Base));

            Assert.That(
                values[0],
                Is.SameAs(DerivedEnumeration.First));

            Assert.That(
                values[1],
                Is.SameAs(DerivedEnumeration.Second));
        });
    }

    /// <summary>
    /// Verifies that repeated GetAll calls return the cached collection.
    /// </summary>
    [Test]
    public void GetAll_CalledMultipleTimes_ShouldReturnSameCollection()
    {
        var first = Enumeration.GetAll<TestEnumeration>();
        var second = Enumeration.GetAll<TestEnumeration>();

        Assert.That(
            first,
            Is.SameAs(second));
    }

    /// <summary>
    /// Verifies that GetAll returns an empty collection when the enumeration
    /// type declares no public static enumeration values.
    /// </summary>
    [Test]
    public void GetAll_WithNoDeclaredValues_ShouldReturnEmptyCollection()
    {
        var values = Enumeration.GetAll<EmptyEnumeration>();

        Assert.That(
            values,
            Is.Empty);
    }

    #endregion

    #region CompareTo

    /// <summary>
    /// Verifies that CompareTo returns zero for equal identifiers.
    /// </summary>
    [Test]
    public void CompareTo_WithSameId_ShouldReturnZero()
    {
        var left = new TestEnumeration(10, "Ten");
        var right = new TestEnumeration(10, "Another Ten");

        Assert.That(
            left.CompareTo(right),
            Is.Zero);
    }

    /// <summary>
    /// Verifies that CompareTo returns a negative value when the current
    /// identifier is smaller.
    /// </summary>
    [Test]
    public void CompareTo_WithSmallerId_ShouldReturnNegativeValue()
    {
        var left = new TestEnumeration(10, "Ten");
        var right = new TestEnumeration(20, "Twenty");

        Assert.That(
            left.CompareTo(right),
            Is.LessThan(0));
    }

    /// <summary>
    /// Verifies that CompareTo returns a positive value when the current
    /// identifier is greater.
    /// </summary>
    [Test]
    public void CompareTo_WithGreaterId_ShouldReturnPositiveValue()
    {
        var left = new TestEnumeration(20, "Twenty");
        var right = new TestEnumeration(10, "Ten");

        Assert.That(
            left.CompareTo(right),
            Is.GreaterThan(0));
    }

    /// <summary>
    /// Verifies that CompareTo returns one when comparing against null.
    /// </summary>
    [Test]
    public void CompareTo_WithNull_ShouldReturnOne()
    {
        var value = new TestEnumeration(10, "Ten");

        Assert.That(
            value.CompareTo((Enumeration?)null),
            Is.EqualTo(1));
    }

    /// <summary>
    /// Verifies that the object overload of CompareTo handles null.
    /// </summary>
    [Test]
    public void CompareToObject_WithNull_ShouldReturnOne()
    {
        var value = new TestEnumeration(10, "Ten");

        Assert.That(
            value.CompareTo((object?)null),
            Is.EqualTo(1));
    }

    /// <summary>
    /// Verifies that the object overload compares another Enumeration
    /// instance using its identifier.
    /// </summary>
    [Test]
    public void CompareToObject_WithEnumeration_ShouldCompareById()
    {
        var left = new TestEnumeration(10, "Ten");
        object right = new TestEnumeration(20, "Twenty");

        Assert.That(
            left.CompareTo(right),
            Is.LessThan(0));
    }

    /// <summary>
    /// Verifies that the object overload treats an unrelated object as null
    /// because of the implementation's cast semantics.
    /// </summary>
    [Test]
    public void CompareToObject_WithUnrelatedObject_ShouldReturnOne()
    {
        var value = new TestEnumeration(10, "Ten");

        Assert.That(
            value.CompareTo(new object()),
            Is.EqualTo(1));
    }

    #endregion

    #region Equality

    /// <summary>
    /// Verifies that an enumeration is equal to another instance of the same
    /// type with the same identifier.
    /// </summary>
    [Test]
    public void Equals_WithSameTypeAndSameId_ShouldReturnTrue()
    {
        var left = new TestEnumeration(10, "Ten");
        var right = new TestEnumeration(10, "Another Ten");

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    /// <summary>
    /// Verifies that different identifiers are not equal.
    /// </summary>
    [Test]
    public void Equals_WithDifferentId_ShouldReturnFalse()
    {
        var left = new TestEnumeration(10, "Ten");
        var right = new TestEnumeration(20, "Twenty");

        Assert.That(
            left.Equals(right),
            Is.False);
    }

    /// <summary>
    /// Verifies that different runtime enumeration types are not equal,
    /// even when their identifiers are equal.
    /// </summary>
    [Test]
    public void Equals_WithDifferentTypesAndSameId_ShouldReturnFalse()
    {
        var left = new TestEnumeration(10, "Ten");
        var right = new OtherEnumeration(10, "Ten");

        Assert.That(
            left.Equals(right),
            Is.False);
    }

    /// <summary>
    /// Verifies that an enumeration is not equal to null.
    /// </summary>
    [Test]
    public void Equals_WithNull_ShouldReturnFalse()
    {
        var value = new TestEnumeration(10, "Ten");

        Assert.That(
            value.Equals(null),
            Is.False);
    }

    /// <summary>
    /// Verifies that Equals returns false for an unrelated object.
    /// </summary>
    [Test]
    public void Equals_WithUnrelatedObject_ShouldReturnFalse()
    {
        var value = new TestEnumeration(10, "Ten");

        Assert.That(
            value.Equals(new object()),
            Is.False);
    }

    #endregion

    #region Operators

    /// <summary>
    /// Verifies that the equality operator returns true for equal values.
    /// </summary>
    [Test]
    public void EqualityOperator_WithEqualValues_ShouldReturnTrue()
    {
        var left = new TestEnumeration(10, "Ten");
        var right = new TestEnumeration(10, "Another Ten");

        Assert.That(
            left == right,
            Is.True);
    }

    /// <summary>
    /// Verifies that the equality operator returns false for different values.
    /// </summary>
    [Test]
    public void EqualityOperator_WithDifferentValues_ShouldReturnFalse()
    {
        var left = new TestEnumeration(10, "Ten");
        var right = new TestEnumeration(20, "Twenty");

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
        Enumeration? left = null;
        Enumeration? right = null;

        Assert.That(
            left == right,
            Is.True);
    }

    /// <summary>
    /// Verifies that the equality operator returns false when only one
    /// operand is null.
    /// </summary>
    [Test]
    public void EqualityOperator_WithOneNull_ShouldReturnFalse()
    {
        Enumeration? left = null;
        var right = new TestEnumeration(10, "Ten");

        Assert.That(
            left == right,
            Is.False);

        Assert.That(
            right == left,
            Is.False);
    }

    /// <summary>
    /// Verifies that the inequality operator returns false for equal values.
    /// </summary>
    [Test]
    public void InequalityOperator_WithEqualValues_ShouldReturnFalse()
    {
        var left = new TestEnumeration(10, "Ten");
        var right = new TestEnumeration(10, "Another Ten");

        Assert.That(
            left != right,
            Is.False);
    }

    /// <summary>
    /// Verifies that the inequality operator returns true for different values.
    /// </summary>
    [Test]
    public void InequalityOperator_WithDifferentValues_ShouldReturnTrue()
    {
        var left = new TestEnumeration(10, "Ten");
        var right = new TestEnumeration(20, "Twenty");

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
        Enumeration? left = null;
        Enumeration? right = null;

        Assert.That(
            left != right,
            Is.False);
    }

    #endregion

    #region Hash code

    /// <summary>
    /// Verifies that equal enumeration values produce equal hash codes.
    /// </summary>
    [Test]
    public void GetHashCode_WithEqualValues_ShouldReturnSameHashCode()
    {
        var left = new TestEnumeration(10, "Ten");
        var right = new TestEnumeration(10, "Another Ten");

        Assert.That(
            left.GetHashCode(),
            Is.EqualTo(right.GetHashCode()));
    }

    #endregion

    #region ToString

    /// <summary>
    /// Verifies that ToString returns the enumeration name.
    /// </summary>
    [Test]
    public void ToString_ShouldReturnName()
    {
        var value = new TestEnumeration(10, "Ten");

        Assert.That(
            value.ToString(),
            Is.EqualTo("Ten"));
    }

    #endregion

    #region Test enumerations

    private sealed class TestEnumeration(
        int id,
        string name) : Enumeration(id, name)
    {
        public static readonly TestEnumeration First =
            new(10, "Ten");

        public static readonly TestEnumeration Second =
            new(20, "Twenty");

        public static readonly TestEnumeration Third =
            new(30, "Thirty");
    }

    private sealed class OtherEnumeration : Enumeration
    {
        public OtherEnumeration(
            int id,
            string name)
            : base(id, name)
        {
        }
    }

    private class BaseEnumeration : Enumeration
    {
        public static readonly BaseEnumeration Base =
            new(1, "Base");

        protected BaseEnumeration(
            int id,
            string name)
            : base(id, name)
        {
        }
    }

    private sealed class DerivedEnumeration : BaseEnumeration
    {
        public static readonly DerivedEnumeration First =
            new(10, "First");

        public static readonly DerivedEnumeration Second =
            new(20, "Second");

        private DerivedEnumeration(
            int id,
            string name)
            : base(id, name)
        {
        }
    }

    private sealed class EmptyEnumeration : Enumeration
    {
        private EmptyEnumeration(
            int id,
            string name)
            : base(id, name)
        {
        }
    }

    #endregion
}
