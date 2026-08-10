using KUKULCAN.SharedKernel.Maybe;

namespace KUKULCAN.SharedKernel.UnitTests.Maybe;

/// <summary>
/// Contains unit tests for <see cref="Maybe{T}"/>.
/// </summary>
[TestFixture]
public sealed class MaybeTests
{
    #region None

    /// <summary>
    /// Verifies that None does not contain a value.
    /// </summary>
    [Test]
    public void None_ShouldNotHaveValue()
    {
        var maybe = Maybe<int>.None;

        Assert.That(maybe.HasValue, Is.False);
    }

    /// <summary>
    /// Verifies that None reports HasNoValue.
    /// </summary>
    [Test]
    public void None_ShouldHaveNoValue()
    {
        var maybe = Maybe<int>.None;

        Assert.That(maybe.HasNoValue, Is.True);
    }

    /// <summary>
    /// Verifies that None returns false from TryGetValue.
    /// </summary>
    [Test]
    public void None_TryGetValue_ShouldReturnFalse()
    {
        var maybe = Maybe<int>.None;

        var result = maybe.TryGetValue(out var value);

        Assert.That(result, Is.False);
        Assert.That(value, Is.EqualTo(default(int)));
    }

    /// <summary>
    /// Verifies that None returns the supplied default value from GetValueOr.
    /// </summary>
    [Test]
    public void None_GetValueOrValue_ShouldReturnDefaultValue()
    {
        var maybe = Maybe<int>.None;

        var result = maybe.GetValueOr(42);

        Assert.That(result, Is.EqualTo(42));
    }

    /// <summary>
    /// Verifies that None evaluates the factory passed to GetValueOr.
    /// </summary>
    [Test]
    public void None_GetValueOrFactory_ShouldInvokeFactory()
    {
        var maybe = Maybe<int>.None;
        var invoked = false;

        var result = maybe.GetValueOr(() =>
        {
            invoked = true;
            return 42;
        });

        Assert.That(result, Is.EqualTo(42));
        Assert.That(invoked, Is.True);
    }

    /// <summary>
    /// Verifies that accessing Value on None throws InvalidOperationException.
    /// </summary>
    [Test]
    public void None_Value_ShouldThrowInvalidOperationException()
    {
        var maybe = Maybe<int>.None;

        Assert.That(
            () => maybe.Value,
            Throws.TypeOf<InvalidOperationException>());
    }

    /// <summary>
    /// Verifies that None is represented by the string "None".
    /// </summary>
    [Test]
    public void None_ToString_ShouldReturnNone()
    {
        var maybe = Maybe<int>.None;

        Assert.That(
            maybe.ToString(),
            Is.EqualTo("None"));
    }

    /// <summary>
    /// Verifies that None has a deterministic hash code.
    /// </summary>
    [Test]
    public void None_GetHashCode_ShouldReturnZero()
    {
        var maybe = Maybe<int>.None;

        Assert.That(
            maybe.GetHashCode(),
            Is.EqualTo(0));
    }

    #endregion

    #region Value

    /// <summary>
    /// Verifies that an implicit conversion from a value creates
    /// a Maybe containing that value.
    /// </summary>
    [Test]
    public void ImplicitConversion_WithValue_ShouldCreateValue()
    {
        Maybe<int> maybe = 42;

        Assert.That(maybe.HasValue, Is.True);
        Assert.That(maybe.HasNoValue, Is.False);
        Assert.That(maybe.Value, Is.EqualTo(42));
    }

    /// <summary>
    /// Verifies that TryGetValue returns true for a Maybe containing
    /// a value.
    /// </summary>
    [Test]
    public void TryGetValue_WithValue_ShouldReturnTrue()
    {
        Maybe<int> maybe = 42;

        var result = maybe.TryGetValue(out var value);

        Assert.That(result, Is.True);
        Assert.That(value, Is.EqualTo(42));
    }

    /// <summary>
    /// Verifies that GetValueOr returns the contained value when present.
    /// </summary>
    [Test]
    public void GetValueOrValue_WithValue_ShouldReturnContainedValue()
    {
        Maybe<int> maybe = 42;

        var result = maybe.GetValueOr(99);

        Assert.That(result, Is.EqualTo(42));
    }

    /// <summary>
    /// Verifies that GetValueOr with a factory does not invoke the factory
    /// when a value is already present.
    /// </summary>
    [Test]
    public void GetValueOrFactory_WithValue_ShouldNotInvokeFactory()
    {
        Maybe<int> maybe = 42;
        var invoked = false;

        var result = maybe.GetValueOr(() =>
        {
            invoked = true;
            return 99;
        });

        Assert.That(result, Is.EqualTo(42));
        Assert.That(invoked, Is.False);
    }

    /// <summary>
    /// Verifies that ToString returns the contained value representation.
    /// </summary>
    [Test]
    public void ToString_WithValue_ShouldReturnValueRepresentation()
    {
        Maybe<int> maybe = 42;

        Assert.That(
            maybe.ToString(),
            Is.EqualTo("42"));
    }

    /// <summary>
    /// Verifies that equal values produce equal Maybe instances.
    /// </summary>
    [Test]
    public void Equals_WithSameValue_ShouldReturnTrue()
    {
        Maybe<int> left = 42;
        Maybe<int> right = 42;

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    /// <summary>
    /// Verifies that different values produce unequal Maybe instances.
    /// </summary>
    [Test]
    public void Equals_WithDifferentValue_ShouldReturnFalse()
    {
        Maybe<int> left = 42;
        Maybe<int> right = 43;

        Assert.That(
            left.Equals(right),
            Is.False);
    }

    /// <summary>
    /// Verifies that equal Maybe instances have equal hash codes.
    /// </summary>
    [Test]
    public void GetHashCode_WithEqualValues_ShouldReturnSameHashCode()
    {
        Maybe<int> left = 42;
        Maybe<int> right = 42;

        Assert.That(
            left.GetHashCode(),
            Is.EqualTo(right.GetHashCode()));
    }

    #endregion

    #region Null values

    /// <summary>
    /// Verifies that an implicit conversion from null creates None
    /// for a reference type.
    /// </summary>
    [Test]
    public void ImplicitConversion_WithNullReference_ShouldCreateNone()
    {
        string? value = null;

        Maybe<string> maybe = value;

        Assert.That(maybe.HasValue, Is.False);
        Assert.That(maybe.HasNoValue, Is.True);
    }

    /// <summary>
    /// Verifies that TryGetValue returns false when a null reference
    /// is converted to Maybe.
    /// </summary>
    [Test]
    public void ImplicitConversion_WithNullReference_TryGetValue_ShouldReturnFalse()
    {
        string? value = null;

        Maybe<string> maybe = value;

        var result = maybe.TryGetValue(out var actual);

        Assert.That(result, Is.False);
        Assert.That(actual, Is.Null);
    }

    /// <summary>
    /// Verifies that GetValueOr returns the supplied fallback for None.
    /// </summary>
    [Test]
    public void ImplicitConversion_WithNullReference_GetValueOr_ShouldReturnFallback()
    {
        string? value = null;

        Maybe<string> maybe = value;

        var result = maybe.GetValueOr("fallback");

        Assert.That(result, Is.EqualTo("fallback"));
    }

    #endregion

    #region Factory validation

    /// <summary>
    /// Verifies that GetValueOr rejects a null factory.
    /// </summary>
    [Test]
    public void GetValueOrFactory_WithNullFactory_ShouldThrowArgumentNullException()
    {
        var maybe = Maybe<int>.None;

        Assert.That(
            () => maybe.GetValueOr(null!),
            Throws.TypeOf<ArgumentNullException>());
    }

    /// <summary>
    /// Verifies that a null factory is rejected even when a value exists.
    /// </summary>
    [Test]
    public void GetValueOrFactory_WithValueAndNullFactory_ShouldThrowArgumentNullException()
    {
        Maybe<int> maybe = 42;

        Assert.That(
            () => maybe.GetValueOr(null!),
            Throws.TypeOf<ArgumentNullException>());
    }

    #endregion

    #region Equality

    /// <summary>
    /// Verifies that two None instances are equal.
    /// </summary>
    [Test]
    public void Equality_WithTwoNoneInstances_ShouldReturnTrue()
    {
        var left = Maybe<int>.None;
        var right = Maybe<int>.None;

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    /// <summary>
    /// Verifies that None and a value are not equal.
    /// </summary>
    [Test]
    public void Equality_WithNoneAndValue_ShouldReturnFalse()
    {
        var none = Maybe<int>.None;
        Maybe<int> value = 42;

        Assert.That(
            none.Equals(value),
            Is.False);
    }

    /// <summary>
    /// Verifies that a value and None are not equal.
    /// </summary>
    [Test]
    public void Equality_WithValueAndNone_ShouldReturnFalse()
    {
        Maybe<int> value = 42;
        var none = Maybe<int>.None;

        Assert.That(
            value.Equals(none),
            Is.False);
    }

    /// <summary>
    /// Verifies that Equals(object) recognizes an equal Maybe instance.
    /// </summary>
    [Test]
    public void EqualsObject_WithEqualMaybe_ShouldReturnTrue()
    {
        Maybe<int> maybe = 42;
        object other = (Maybe<int>)42;

        Assert.That(
            maybe.Equals(other),
            Is.True);
    }

    /// <summary>
    /// Verifies that Equals(object) rejects an object of another type.
    /// </summary>
    [Test]
    public void EqualsObject_WithDifferentType_ShouldReturnFalse()
    {
        Maybe<int> maybe = 42;

        Assert.That(
            maybe.Equals("42"),
            Is.False);
    }

    /// <summary>
    /// Verifies that two None instances compare equal with ==.
    /// </summary>
    [Test]
    public void EqualityOperator_WithTwoNoneInstances_ShouldReturnTrue()
    {
        var left = Maybe<int>.None;
        var right = Maybe<int>.None;

        Assert.That(
            left == right,
            Is.True);
    }

    /// <summary>
    /// Verifies that equal values compare equal with ==.
    /// </summary>
    [Test]
    public void EqualityOperator_WithEqualValues_ShouldReturnTrue()
    {
        Maybe<int> left = 42;
        Maybe<int> right = 42;

        Assert.That(
            left == right,
            Is.True);
    }

    /// <summary>
    /// Verifies that different values compare unequal with ==.
    /// </summary>
    [Test]
    public void EqualityOperator_WithDifferentValues_ShouldReturnFalse()
    {
        Maybe<int> left = 42;
        Maybe<int> right = 43;

        Assert.That(
            left == right,
            Is.False);
    }

    /// <summary>
    /// Verifies that None and a value compare unequal with !=.
    /// </summary>
    [Test]
    public void InequalityOperator_WithNoneAndValue_ShouldReturnTrue()
    {
        var none = Maybe<int>.None;
        Maybe<int> value = 42;

        Assert.That(
            none != value,
            Is.True);
    }

    /// <summary>
    /// Verifies that equal values compare equal with != returning false.
    /// </summary>
    [Test]
    public void InequalityOperator_WithEqualValues_ShouldReturnFalse()
    {
        Maybe<int> left = 42;
        Maybe<int> right = 42;

        Assert.That(
            left != right,
            Is.False);
    }

    #endregion

    #region Generic reference types

    /// <summary>
    /// Verifies that Maybe works with reference types.
    /// </summary>
    [Test]
    public void ReferenceType_WithValue_ShouldStoreReference()
    {
        var value = new TestValue("value");

        Maybe<TestValue> maybe = value;

        Assert.That(
            maybe.Value,
            Is.SameAs(value));
    }

    /// <summary>
    /// Verifies that equality for reference types uses the default
    /// equality comparer.
    /// </summary>
    [Test]
    public void ReferenceType_WithEqualValues_ShouldCompareAccordingToEqualityComparer()
    {
        var leftValue = new TestValue("same");
        var rightValue = new TestValue("same");

        Maybe<TestValue> left = leftValue;
        Maybe<TestValue> right = rightValue;

        Assert.That(
            left.Equals(right),
            Is.True);
    }

    /// <summary>
    /// Simple reference type used to verify generic Maybe behavior.
    /// </summary>
    private sealed record TestValue(string Value);

    #endregion
}
