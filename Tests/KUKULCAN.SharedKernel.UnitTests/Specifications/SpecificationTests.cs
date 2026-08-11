using System.Linq.Expressions;
using KUKULCAN.SharedKernel.Specifications;

namespace KUKULCAN.SharedKernel.UnitTests.Specifications;

/// <summary>
/// Contains unit tests for the specification subsystem.
/// </summary>
[TestFixture]
public sealed class SpecificationTests
{
    #region Criteria

    /// <summary>
    /// Verifies that the specification exposes its criteria.
    /// </summary>
    [Test]
    public void Criteria_ShouldReturnDefinedExpression()
    {
        var specification = new PositiveSpecification();

        Assert.That(
            specification.Criteria,
            Is.Not.Null);
    }

    /// <summary>
    /// Verifies that the criteria expression evaluates correctly.
    /// </summary>
    [Test]
    public void Criteria_ShouldEvaluateCorrectly()
    {
        var specification = new PositiveSpecification();

        var predicate = specification.Criteria.Compile();

        Assert.Multiple(() =>
        {
            Assert.That(predicate(10), Is.True);
            Assert.That(predicate(0), Is.False);
            Assert.That(predicate(-10), Is.False);
        });
    }

    #endregion

    #region AndSpecification

    /// <summary>
    /// Verifies that And creates an AND specification.
    /// </summary>
    [Test]
    public void AndSpecification_ShouldCreateAndSpecification()
    {
        var positive = new PositiveSpecification();
        var even = new EvenSpecification();
        Specification<int> result = positive.And(even);

        Assert.That(
            result,
            Is.TypeOf<AndSpecification<int>>());
    }

    /// <summary>
    /// Verifies that And evaluates both criteria.
    /// </summary>
    [Test]
    public void AndSpecification_ShouldRequireBothCriteria()
    {
        var positive = new PositiveSpecification();
        var even = new EvenSpecification();
        Specification<int> result = positive.And(even);
        Func<int, bool> predicate = result.Criteria.Compile();

        Assert.Multiple(() =>
        {
            Assert.That(predicate(2), Is.True);
            Assert.That(predicate(4), Is.True);
            Assert.That(predicate(1), Is.False);
            Assert.That(predicate(-2), Is.False);
        });
    }

    /// <summary>
    /// Verifies that And rejects a null specification.
    /// </summary>
    [Test]
    public void AndSpecification_WithNull_ShouldThrowArgumentNullException()
    {
        var specification = new PositiveSpecification();

        Assert.That(
            () => specification.And(null!),
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that the AndSpecification constructor preserves the
    /// left specification.
    /// </summary>
    [Test]
    public void AndSpecification_Constructor_ShouldAssignLeftSpecification()
    {
        var left = new PositiveSpecification();
        var right = new EvenSpecification();
        var specification = new AndSpecification<int>(left, right);

        Assert.That(
            specification.Left,
            Is.SameAs(left));
    }

    /// <summary>
    /// Verifies that the AndSpecification constructor preserves the
    /// right specification.
    /// </summary>
    [Test]
    public void AndSpecification_Constructor_ShouldAssignRightSpecification()
    {
        var left = new PositiveSpecification();
        var right = new EvenSpecification();
        var specification = new AndSpecification<int>(left, right);

        Assert.That(
            specification.Right,
            Is.SameAs(right));
    }

    /// <summary>
    /// Verifies that the AndSpecification constructor rejects a null
    /// left specification.
    /// </summary>
    [Test]
    public void AndSpecification_Constructor_WithNullLeft_ShouldThrowArgumentNullException()
    {
        var right = new EvenSpecification();

        Assert.That(
            () => new AndSpecification<int>(null!, right),
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that the AndSpecification constructor rejects a null
    /// right specification.
    /// </summary>
    [Test]
    public void AndSpecification_Constructor_WithNullRight_ShouldThrowArgumentNullException()
    {
        var left = new PositiveSpecification();

        Assert.That(
            () => new AndSpecification<int>(left, null!),
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that the AndSpecification criteria combines the left and
    /// right criteria into a single expression.
    /// </summary>
    [Test]
    public void AndSpecification_Criteria_ShouldCombineBothSpecifications()
    {
        var left = new MinimumSpecification(10);
        var right = new MaximumSpecification(20);
        var specification = new AndSpecification<int>(left, right);
        Func<int, bool> predicate = specification.Criteria.Compile();

        Assert.Multiple(() =>
        {
            Assert.That(predicate(9), Is.False);
            Assert.That(predicate(10), Is.True);
            Assert.That(predicate(15), Is.True);
            Assert.That(predicate(20), Is.True);
            Assert.That(predicate(21), Is.False);
        });
    }

    #endregion

    #region OrSpecification

    /// <summary>
    /// Verifies that Or creates an OR specification.
    /// </summary>
    [Test]
    public void Or_ShouldCreateOrSpecification()
    {
        var positive = new PositiveSpecification();
        var even = new EvenSpecification();
        Specification<int> result = positive.Or(even);

        Assert.That(
            result,
            Is.TypeOf<OrSpecification<int>>());
    }

    /// <summary>
    /// Verifies that Or succeeds when either criterion is satisfied.
    /// </summary>
    [Test]
    public void Or_ShouldRequireAtLeastOneCriterion()
    {
        var positive = new PositiveSpecification();
        var even = new EvenSpecification();
        Specification<int> result = positive.Or(even);
        Func<int, bool> predicate = result.Criteria.Compile();

        Assert.Multiple(() =>
        {
            Assert.That(predicate(2), Is.True);
            Assert.That(predicate(3), Is.True);
            Assert.That(predicate(-2), Is.True);
            Assert.That(predicate(-3), Is.False);
        });
    }

    /// <summary>
    /// Verifies that Or rejects a null specification.
    /// </summary>
    [Test]
    public void Or_WithNull_ShouldThrowArgumentNullException()
    {
        var specification = new PositiveSpecification();

        Assert.That(
            () => specification.Or(null!),
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that the OrSpecification constructor preserves the
    /// left specification.
    /// </summary>
    [Test]
    public void OrSpecification_Constructor_ShouldAssignLeftSpecification()
    {
        var left = new PositiveSpecification();
        var right = new EvenSpecification();
        var specification = new OrSpecification<int>(left, right);

        Assert.That(
            specification.Left,
            Is.SameAs(left));
    }

    /// <summary>
    /// Verifies that the OrSpecification constructor preserves the
    /// right specification.
    /// </summary>
    [Test]
    public void OrSpecification_Constructor_ShouldAssignRightSpecification()
    {
        var left = new PositiveSpecification();
        var right = new EvenSpecification();
        var specification = new OrSpecification<int>(left, right);

        Assert.That(
            specification.Right,
            Is.SameAs(right));
    }

    /// <summary>
    /// Verifies that the OrSpecification constructor rejects a null
    /// left specification.
    /// </summary>
    [Test]
    public void OrSpecification_Constructor_WithNullLeft_ShouldThrowArgumentNullException()
    {
        var right = new EvenSpecification();

        Assert.That(
            () => new OrSpecification<int>(null!, right),
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that the OrSpecification constructor rejects a null
    /// right specification.
    /// </summary>
    [Test]
    public void OrSpecification_Constructor_WithNullRight_ShouldThrowArgumentNullException()
    {
        var left = new PositiveSpecification();

        Assert.That(
            () => new OrSpecification<int>(left, null!),
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that the OrSpecification criteria combines the left and
    /// right criteria into a single expression.
    /// </summary>
    [Test]
    public void OrSpecification_Criteria_ShouldCombineBothSpecifications()
    {
        var left = new MinimumSpecification(10);
        var right = new MaximumSpecification(0);
        var specification = new OrSpecification<int>(left, right);
        Func<int, bool> predicate = specification.Criteria.Compile();

        Assert.Multiple(() =>
        {
            Assert.That(predicate(-1), Is.True);
            Assert.That(predicate(0), Is.True);
            Assert.That(predicate(5), Is.False);
            Assert.That(predicate(10), Is.True);
            Assert.That(predicate(20), Is.True);
        });
    }

    #endregion

    #region NotSpecification

    /// <summary>
    /// Verifies that Not creates a NotSpecification.
    /// </summary>
    [Test]
    public void Not_ShouldCreateNotSpecification()
    {
        var positive = new PositiveSpecification();
        Specification<int> result = positive.Not();

        Assert.That(
            result,
            Is.TypeOf<NotSpecification<int>>());
    }

    /// <summary>
    /// Verifies that Not reverses the result of the original criterion.
    /// </summary>
    [Test]
    public void Not_ShouldNegateCriteria()
    {
        var positive = new PositiveSpecification();
        Specification<int> result = positive.Not();
        Func<int, bool> predicate = result.Criteria.Compile();

        Assert.Multiple(() =>
        {
            Assert.That(predicate(10), Is.False);
            Assert.That(predicate(0), Is.True);
            Assert.That(predicate(-10), Is.True);
        });
    }

    /// <summary>
    /// Verifies that the NotSpecification constructor preserves the
    /// specification being negated.
    /// </summary>
    [Test]
    public void NotSpecification_Constructor_ShouldAssignSpecification()
    {
        var original = new PositiveSpecification();
        var specification = new NotSpecification<int>(original);

        Assert.That(
            specification.Specification,
            Is.SameAs(original));
    }

    /// <summary>
    /// Verifies that the NotSpecification constructor rejects a null
    /// specification.
    /// </summary>
    [Test]
    public void NotSpecification_Constructor_WithNull_ShouldThrowArgumentNullException()
    {
        Assert.That(
            () => new NotSpecification<int>(null!),
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that the NotSpecification criteria negates the original
    /// criteria.
    /// </summary>
    [Test]
    public void NotSpecification_Criteria_ShouldNegateOriginalSpecification()
    {
        var original = new MinimumSpecification(10);
        var specification = new NotSpecification<int>(original);
        Func<int, bool> predicate = specification.Criteria.Compile();

        Assert.Multiple(() =>
        {
            Assert.That(predicate(9), Is.True);
            Assert.That(predicate(10), Is.False);
            Assert.That(predicate(20), Is.False);
        });
    }

    #endregion

    #region Operators

    /// <summary>
    /// Verifies that the AND operator creates an AND specification.
    /// </summary>
    [Test]
    public void AndOperator_ShouldCreateAndSpecification()
    {
        Specification<int> left = new PositiveSpecification();
        Specification<int> right = new EvenSpecification();
        Specification<int> result = left & right;

        Assert.That(
            result,
            Is.TypeOf<AndSpecification<int>>());
    }

    /// <summary>
    /// Verifies that the AND operator rejects a null left operand.
    /// </summary>
    [Test]
    public void AndOperator_WithNullLeft_ShouldThrowArgumentNullException()
    {
        Specification<int>? left = null;
        Specification<int> right = new PositiveSpecification();

        Assert.That(
            () => left! & right,
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that the AND operator rejects a null right operand.
    /// </summary>
    [Test]
    public void AndOperator_WithNullRight_ShouldThrowArgumentNullException()
    {
        Specification<int> left = new PositiveSpecification();
        Specification<int>? right = null;

        Assert.That(
            () => left & right!,
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that the OR operator creates an OR specification.
    /// </summary>
    [Test]
    public void OrOperator_ShouldCreateOrSpecification()
    {
        Specification<int> left = new PositiveSpecification();
        Specification<int> right = new EvenSpecification();
        Specification<int> result = left | right;

        Assert.That(
            result,
            Is.TypeOf<OrSpecification<int>>());
    }

    /// <summary>
    /// Verifies that the OR operator rejects a null left operand.
    /// </summary>
    [Test]
    public void OrOperator_WithNullLeft_ShouldThrowArgumentNullException()
    {
        Specification<int>? left = null;
        Specification<int> right = new PositiveSpecification();

        Assert.That(
            () => left! | right,
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that the OR operator rejects a null right operand.
    /// </summary>
    [Test]
    public void OrOperator_WithNullRight_ShouldThrowArgumentNullException()
    {
        Specification<int> left = new PositiveSpecification();
        Specification<int>? right = null;

        Assert.That(
            () => left | right!,
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that the NOT operator creates a NotSpecification.
    /// </summary>
    [Test]
    public void NotOperator_ShouldCreateNotSpecification()
    {
        Specification<int> specification =
            new PositiveSpecification();
        Specification<int> result = !specification;

        Assert.That(
            result,
            Is.TypeOf<NotSpecification<int>>());
    }

    /// <summary>
    /// Verifies that the NOT operator rejects a null specification.
    /// </summary>
    [Test]
    public void NotOperator_WithNull_ShouldThrowArgumentNullException()
    {
        Specification<int>? specification = null;

        Assert.That(
            () => !specification!,
            Throws.ArgumentNullException);
    }

    #endregion

    #region Nested composition

    /// <summary>
    /// Verifies that specifications can be composed into nested expressions.
    /// </summary>
    [Test]
    public void NestedComposition_ShouldEvaluateCorrectly()
    {
        Specification<int> positive = new PositiveSpecification();
        Specification<int> even = new EvenSpecification();
        Specification<int> greaterThanTen =
            new GreaterThanTenSpecification();
        Specification<int> specification =
            (positive & even) | greaterThanTen;
        Func<int, bool> predicate = specification.Criteria.Compile();

        Assert.Multiple(() =>
        {
            Assert.That(predicate(2), Is.True);
            Assert.That(predicate(8), Is.True);
            Assert.That(predicate(11), Is.True);
            Assert.That(predicate(3), Is.False);
            Assert.That(predicate(-2), Is.False);
        });
    }

    /// <summary>
    /// Verifies that expressions with independently declared parameters
    /// can be combined and evaluated correctly.
    /// </summary>
    [Test]
    public void Composition_WithDifferentParameters_ShouldEvaluateCorrectly()
    {
        var left = new MinimumSpecification(10);
        var right = new MaximumSpecification(20);
        Specification<int> specification = left.And(right);
        Func<int, bool> predicate = specification.Criteria.Compile();

        Assert.Multiple(() =>
        {
            Assert.That(predicate(10), Is.True);
            Assert.That(predicate(15), Is.True);
            Assert.That(predicate(20), Is.True);
            Assert.That(predicate(9), Is.False);
            Assert.That(predicate(21), Is.False);
        });
    }

    #endregion

    #region Test specifications

    private sealed class PositiveSpecification : Specification<int>
    {
        public override Expression<Func<int, bool>> Criteria =>
            value => value > 0;
    }

    private sealed class EvenSpecification : Specification<int>
    {
        public override Expression<Func<int, bool>> Criteria =>
            value => value % 2 == 0;
    }

    private sealed class GreaterThanTenSpecification : Specification<int>
    {
        public override Expression<Func<int, bool>> Criteria =>
            value => value > 10;
    }

    private sealed class MinimumSpecification(int minimum) : Specification<int>
    {
        public override Expression<Func<int, bool>> Criteria =>
            value => value >= minimum;
    }

    private sealed class MaximumSpecification(int maximum) : Specification<int>
    {
        public override Expression<Func<int, bool>> Criteria =>
            value => value <= maximum;
    }

    #endregion
}
