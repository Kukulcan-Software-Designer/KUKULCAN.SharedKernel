using KUKULCAN.SharedKernel.Domain;

namespace KUKULCAN.SharedKernel.UnitTests.Domain;

/// <summary>
/// Contains unit tests for <see cref="ValueObject"/>.
/// </summary>
[TestFixture]
public sealed class ValueObjectTests
{
    #region Equals

    /// <summary>
    /// Verifies that a value object is equal to itself.
    /// </summary>
    [Test]
    public void Equals_WithSameReference_ShouldReturnTrue()
    {
        var value = new PersonName("Juan", "Pardo");

        Assert.That(
            value.Equals(value),
            Is.True);
    }

    /// <summary>
    /// Verifies that a value object is not equal to null.
    /// </summary>
    [Test]
    public void Equals_WithNull_ShouldReturnFalse()
    {
        var value = new PersonName("Juan", "Pardo");

        Assert.That(
            value.Equals((ValueObject?)null),
            Is.False);
    }

    /// <summary>
    /// Verifies that value objects of the same type with equal components
    /// are equal.
    /// </summary>
    [Test]
    public void Equals_WithSameTypeAndSameComponents_ShouldReturnTrue()
    {
        var left = new PersonName("Juan", "Pardo");
        var right = new PersonName("Juan", "Pardo");

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    /// <summary>
    /// Verifies that value objects of the same type with different components
    /// are not equal.
    /// </summary>
    [Test]
    public void Equals_WithSameTypeAndDifferentComponents_ShouldReturnFalse()
    {
        var left = new PersonName("Juan", "Pardo");
        var right = new PersonName("Juan", "Garcia");

        Assert.That(
            left.Equals(right),
            Is.False);
    }

    /// <summary>
    /// Verifies that value objects of different runtime types are not equal,
    /// even when their components have the same values.
    /// </summary>
    [Test]
    public void Equals_WithDifferentTypesAndSameComponents_ShouldReturnFalse()
    {
        var left = new PersonName("Juan", "Pardo");
        var right = new CompanyName("Juan", "Pardo");

        Assert.That(
            left.Equals(right),
            Is.False);
    }

    /// <summary>
    /// Verifies that equality is independent of object reference identity
    /// when the value object components are equal.
    /// </summary>
    [Test]
    public void Equals_WithDifferentReferencesAndSameComponents_ShouldReturnTrue()
    {
        var left = new PersonName("Juan", "Pardo");
        var right = new PersonName("Juan", "Pardo");

        Assert.That(
            ReferenceEquals(left, right),
            Is.False);

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    #endregion

    #region Equals(object)

    /// <summary>
    /// Verifies that Equals(object) recognizes an equal value object.
    /// </summary>
    [Test]
    public void EqualsObject_WithEqualValueObject_ShouldReturnTrue()
    {
        var left = new PersonName("Juan", "Pardo");
        object right = new PersonName("Juan", "Pardo");

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
        var value = new PersonName("Juan", "Pardo");

        Assert.That(
            value.Equals((object?)null),
            Is.False);
    }

    /// <summary>
    /// Verifies that Equals(object) returns false for a non-value-object.
    /// </summary>
    [Test]
    public void EqualsObject_WithDifferentObjectType_ShouldReturnFalse()
    {
        var value = new PersonName("Juan", "Pardo");

        Assert.That(
            value.Equals(new object()),
            Is.False);
    }

    /// <summary>
    /// Verifies that Equals(object) respects runtime type equality.
    /// </summary>
    [Test]
    public void EqualsObject_WithDifferentValueObjectType_ShouldReturnFalse()
    {
        var value = new PersonName("Juan", "Pardo");

        Assert.That(
            value.Equals(new CompanyName("Juan", "Pardo")),
            Is.False);
    }

    #endregion

    #region Operators

    /// <summary>
    /// Verifies that the equality operator returns true for equal
    /// value objects.
    /// </summary>
    [Test]
    public void EqualityOperator_WithEqualValueObjects_ShouldReturnTrue()
    {
        var left = new PersonName("Juan", "Pardo");
        var right = new PersonName("Juan", "Pardo");

        Assert.That(
            left == right,
            Is.True);
    }

    /// <summary>
    /// Verifies that the equality operator returns false for different
    /// value objects.
    /// </summary>
    [Test]
    public void EqualityOperator_WithDifferentValueObjects_ShouldReturnFalse()
    {
        var left = new PersonName("Juan", "Pardo");
        var right = new PersonName("Juan", "Garcia");

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
        PersonName? left = null;
        PersonName? right = null;

        Assert.That(
            left == right,
            Is.True);
    }

    /// <summary>
    /// Verifies that the equality operator returns false when only the
    /// left operand is null.
    /// </summary>
    [Test]
    public void EqualityOperator_WithNullLeft_ShouldReturnFalse()
    {
        PersonName? left = null;
        var right = new PersonName("Juan", "Pardo");

        Assert.That(
            left == right,
            Is.False);
    }

    /// <summary>
    /// Verifies that the equality operator returns false when only the
    /// right operand is null.
    /// </summary>
    [Test]
    public void EqualityOperator_WithNullRight_ShouldReturnFalse()
    {
        var left = new PersonName("Juan", "Pardo");
        PersonName? right = null;

        Assert.That(
            left == right,
            Is.False);
    }

    /// <summary>
    /// Verifies that the inequality operator returns false for equal
    /// value objects.
    /// </summary>
    [Test]
    public void InequalityOperator_WithEqualValueObjects_ShouldReturnFalse()
    {
        var left = new PersonName("Juan", "Pardo");
        var right = new PersonName("Juan", "Pardo");

        Assert.That(
            left != right,
            Is.False);
    }

    /// <summary>
    /// Verifies that the inequality operator returns true for different
    /// value objects.
    /// </summary>
    [Test]
    public void InequalityOperator_WithDifferentValueObjects_ShouldReturnTrue()
    {
        var left = new PersonName("Juan", "Pardo");
        var right = new PersonName("Juan", "Garcia");

        Assert.That(
            left != right,
            Is.True);
    }

    /// <summary>
    /// Verifies that the inequality operator returns false when both
    /// operands are null.
    /// </summary>
    [Test]
    public void InequalityOperator_WithBothNull_ShouldReturnFalse()
    {
        PersonName? left = null;
        PersonName? right = null;

        Assert.That(
            left != right,
            Is.False);
    }

    #endregion

    #region Null components

    /// <summary>
    /// Verifies that equal null components are considered equal.
    /// </summary>
    [Test]
    public void Equals_WithNullComponents_ShouldReturnTrueWhenComponentsMatch()
    {
        var left = new NullableName(null, "Pardo");
        var right = new NullableName(null, "Pardo");

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    /// <summary>
    /// Verifies that a null component differs from a non-null component.
    /// </summary>
    [Test]
    public void Equals_WithOneNullComponent_ShouldReturnFalse()
    {
        var left = new NullableName(null, "Pardo");
        var right = new NullableName("Juan", "Pardo");

        Assert.That(
            left.Equals(right),
            Is.False);
    }

    /// <summary>
    /// Verifies that two value objects containing only null components
    /// are equal.
    /// </summary>
    [Test]
    public void Equals_WithAllComponentsNull_ShouldReturnTrue()
    {
        var left = new NullableName(null, null);
        var right = new NullableName(null, null);

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    #endregion

    #region Enumerable components

    /// <summary>
    /// Verifies that equal enumerable components are compared structurally.
    /// </summary>
    [Test]
    public void Equals_WithEqualEnumerableComponents_ShouldReturnTrue()
    {
        var left = new TaggedValue(
            "Value",
            new[] { 1, 2, 3 });

        var right = new TaggedValue(
            "Value",
            new[] { 1, 2, 3 });

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    /// <summary>
    /// Verifies that different enumerable component values are not equal.
    /// </summary>
    [Test]
    public void Equals_WithDifferentEnumerableComponents_ShouldReturnFalse()
    {
        var left = new TaggedValue(
            "Value",
            new[] { 1, 2, 3 });

        var right = new TaggedValue(
            "Value",
            new[] { 1, 2, 4 });

        Assert.That(
            left.Equals(right),
            Is.False);
    }

    /// <summary>
    /// Verifies that enumerable component order participates in equality.
    /// </summary>
    [Test]
    public void Equals_WithEnumerableComponentsInDifferentOrder_ShouldReturnFalse()
    {
        var left = new TaggedValue(
            "Value",
            new[] { 1, 2, 3 });

        var right = new TaggedValue(
            "Value",
            new[] { 3, 2, 1 });

        Assert.That(
            left.Equals(right),
            Is.False);
    }

    /// <summary>
    /// Verifies that enumerable components with different lengths are not equal.
    /// </summary>
    [Test]
    public void Equals_WithEnumerableComponentsOfDifferentLengths_ShouldReturnFalse()
    {
        var left = new TaggedValue(
            "Value",
            new[] { 1, 2, 3 });

        var right = new TaggedValue(
            "Value",
            new[] { 1, 2 });

        Assert.That(
            left.Equals(right),
            Is.False);
    }

    /// <summary>
    /// Verifies that nested enumerable components are compared recursively.
    /// </summary>
    [Test]
    public void Equals_WithNestedEnumerableComponents_ShouldCompareRecursively()
    {
        var left = new NestedTags(
            new[]
            {
                new[] { 1, 2 },
                new[] { 3, 4 }
            });

        var right = new NestedTags(
            new[]
            {
                new[] { 1, 2 },
                new[] { 3, 4 }
            });

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    /// <summary>
    /// Verifies that strings inside enumerable components are compared
    /// as strings rather than as character sequences.
    /// </summary>
    [Test]
    public void Equals_WithStringEnumerableComponents_ShouldCompareStrings()
    {
        var left = new StringTags(
            new[] { "one", "two" });

        var right = new StringTags(
            new[] { "one", "two" });

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    /// <summary>
    /// Verifies that null elements inside enumerable components are supported.
    /// </summary>
    [Test]
    public void Equals_WithEnumerableContainingNull_ShouldCompareNullElements()
    {
        var left = new StringTags(
            new string?[] { "one", null, "three" });

        var right = new StringTags(
            new string?[] { "one", null, "three" });

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    #endregion

    #region Hash code

    /// <summary>
    /// Verifies that equal value objects produce equal hash codes.
    /// </summary>
    [Test]
    public void GetHashCode_WithEqualValueObjects_ShouldReturnSameHashCode()
    {
        var left = new PersonName("Juan", "Pardo");
        var right = new PersonName("Juan", "Pardo");

        Assert.That(
            left.GetHashCode(),
            Is.EqualTo(right.GetHashCode()));
    }

    /// <summary>
    /// Verifies that enumerable equality components participate in the hash code.
    /// </summary>
    [Test]
    public void GetHashCode_WithEqualEnumerableComponents_ShouldReturnSameHashCode()
    {
        var left = new TaggedValue(
            "Value",
            new[] { 1, 2, 3 });

        var right = new TaggedValue(
            "Value",
            new[] { 1, 2, 3 });

        Assert.That(
            left.GetHashCode(),
            Is.EqualTo(right.GetHashCode()));
    }

    #endregion
    
    #region ToString

    /// <summary>
    /// Verifies the textual representation of a value object.
    /// </summary>
    [Test]
    public void ToString_ShouldIncludeTypeNameAndComponents()
    {
        var value = new PersonName("Juan", "Pardo");

        Assert.That(
            value.ToString(),
            Is.EqualTo("PersonName { Juan, Pardo }"));
    }

    /// <summary>
    /// Verifies that null components are represented by &lt;null&gt;.
    /// </summary>
    [Test]
    public void ToString_WithNullComponent_ShouldUseNullMarker()
    {
        var value = new NullableName(null, "Pardo");

        Assert.That(
            value.ToString(),
            Is.EqualTo("NullableName { <null>, Pardo }"));
    }

    /// <summary>
    /// Verifies that enumerable components are formatted using brackets.
    /// </summary>
    [Test]
    public void ToString_WithEnumerableComponent_ShouldUseCollectionFormat()
    {
        var value = new TaggedValue(
            "Value",
            new[] { 1, 2, 3 });

        Assert.That(
            value.ToString(),
            Is.EqualTo("TaggedValue { Value, [1, 2, 3] }"));
    }

    /// <summary>
    /// Verifies that nested enumerable components are formatted recursively.
    /// </summary>
    [Test]
    public void ToString_WithNestedEnumerableComponent_ShouldFormatRecursively()
    {
        var value = new NestedTags(
            new[]
            {
                new[] { 1, 2 },
                new[] { 3, 4 }
            });

        Assert.That(
            value.ToString(),
            Is.EqualTo("NestedTags { [[1, 2], [3, 4]] }"));
    }

    /// <summary>
    /// Verifies that null enumerable elements use the null marker.
    /// </summary>
    [Test]
    public void ToString_WithEnumerableContainingNull_ShouldUseNullMarker()
    {
        var value = new StringTags(
            new string?[] { "one", null, "three" });

        Assert.That(
            value.ToString(),
            Is.EqualTo("StringTags { [one, <null>, three] }"));
    }

    #endregion

    #region Test value objects

    private sealed class PersonName : ValueObject
    {
        public PersonName(
            string firstName,
            string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }

        public string LastName { get; }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }

    private sealed class CompanyName : ValueObject
    {
        public CompanyName(
            string firstName,
            string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }

        public string LastName { get; }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }

    private sealed class NullableName : ValueObject
    {
        public NullableName(
            string? firstName,
            string? lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string? FirstName { get; }

        public string? LastName { get; }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }

    private sealed class TaggedValue : ValueObject
    {
        public TaggedValue(
            string value,
            IEnumerable<int> tags)
        {
            Value = value;
            Tags = tags;
        }

        public string Value { get; }

        public IEnumerable<int> Tags { get; }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
            yield return Tags;
        }
    }

    private sealed class NestedTags : ValueObject
    {
        public NestedTags(
            IEnumerable<IEnumerable<int>> tags)
        {
            Tags = tags;
        }

        public IEnumerable<IEnumerable<int>> Tags { get; }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Tags;
        }
    }

    private sealed class StringTags : ValueObject
    {
        public StringTags(
            IEnumerable<string?> tags)
        {
            Tags = tags;
        }

        public IEnumerable<string?> Tags { get; }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Tags;
        }
    }

    #endregion
}
