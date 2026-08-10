using KUKULCAN.SharedKernel.Results;

namespace KUKULCAN.SharedKernel.UnitTests.Results;

/// <summary>
/// Contains unit tests for
/// <see cref="ValidationErrors"/>.
/// </summary>
[TestFixture]
public sealed class ValidationErrorsTests
{
    [Test]
    public void ValidationFailed_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.ValidationFailed();

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.ValidationFailed));

        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void Required_WithValidPropertyName_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.Required("Name");

        Assert.That(error.Code, Is.EqualTo(CommonErrorCodes.Required));
        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void Null_WithValidPropertyName_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.Null("Name");

        Assert.That(error.Code, Is.EqualTo(CommonErrorCodes.Null));
        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void Empty_WithValidPropertyName_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.Empty("Name");

        Assert.That(error.Code, Is.EqualTo(CommonErrorCodes.Empty));
        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void MinLength_WithValidArguments_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.MinLength("Name", 3);

        Assert.That(error.Code, Is.EqualTo(CommonErrorCodes.MinLength));
        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void MaxLength_WithValidArguments_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.MaxLength("Name", 100);

        Assert.That(error.Code, Is.EqualTo(CommonErrorCodes.MaxLength));
        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void ExactLength_WithValidArguments_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.ExactLength("Code", 8);

        Assert.That(error.Code, Is.EqualTo(CommonErrorCodes.ExactLength));
        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void GreaterThan_WithValidArguments_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.GreaterThan("Age", 18);

        Assert.That(error.Code, Is.EqualTo(CommonErrorCodes.GreaterThan));
        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void GreaterThanOrEqual_WithValidArguments_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.GreaterThanOrEqual("Age", 18);

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.GreaterThanOrEqual));

        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void LessThan_WithValidArguments_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.LessThan("Age", 65);

        Assert.That(error.Code, Is.EqualTo(CommonErrorCodes.LessThan));
        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void LessThanOrEqual_WithValidArguments_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.LessThanOrEqual("Age", 65);

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.LessThanOrEqual));

        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void Between_WithValidArguments_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.Between("Age", 18, 65);

        Assert.That(error.Code, Is.EqualTo(CommonErrorCodes.Between));
        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void InvalidFormat_WithValidPropertyName_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.InvalidFormat("Code");

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.InvalidFormat));

        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void InvalidPattern_WithValidPropertyName_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.InvalidPattern("Code");

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.InvalidPattern));

        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void InvalidEmail_WithValidPropertyName_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.InvalidEmail("Email");

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.InvalidEmail));

        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void InvalidPhone_WithValidPropertyName_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.InvalidPhone("Phone");

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.InvalidPhone));

        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void InvalidUrl_WithValidPropertyName_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.InvalidUrl("Website");

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.InvalidUrl));

        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void CollectionMustBeEmpty_WithValidPropertyName_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.CollectionMustBeEmpty("Items");

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.CollectionMustBeEmpty));

        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void CollectionMustNotBeEmpty_WithValidPropertyName_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.CollectionMustNotBeEmpty("Items");

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.CollectionMustNotBeEmpty));

        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void Duplicate_WithValidPropertyName_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.Duplicate("Items");

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.Duplicate));

        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void InvalidCollectionItem_WithValidPropertyName_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.InvalidCollectionItem("Items");

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.InvalidCollectionItem));

        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void PastDate_WithValidPropertyName_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.PastDate("BirthDate");

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.PastDate));

        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void FutureDate_WithValidPropertyName_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.FutureDate("ExpirationDate");

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.FutureDate));

        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void InvalidEnum_WithValidPropertyName_ShouldReturnExpectedError()
    {
        var error = ValidationErrors.InvalidEnum("Status");

        Assert.That(
            error.Code,
            Is.EqualTo(CommonErrorCodes.InvalidEnum));

        Assert.That(error.Description, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public void PropertyName_WithNull_ShouldThrowArgumentException()
    {
        Assert.Multiple(() =>
        {
            Assert.That(
                () => ValidationErrors.Required(null!),
                Throws.TypeOf<ArgumentNullException>());

            Assert.That(
                () => ValidationErrors.Null(null!),
                Throws.TypeOf<ArgumentNullException>());

            Assert.That(
                () => ValidationErrors.Empty(null!),
                Throws.TypeOf<ArgumentNullException>());

            Assert.That(
                () => ValidationErrors.InvalidFormat(null!),
                Throws.TypeOf<ArgumentNullException>());

            Assert.That(
                () => ValidationErrors.InvalidPattern(null!),
                Throws.TypeOf<ArgumentNullException>());

            Assert.That(
                () => ValidationErrors.InvalidEmail(null!),
                Throws.TypeOf<ArgumentNullException>());

            Assert.That(
                () => ValidationErrors.InvalidPhone(null!),
                Throws.TypeOf<ArgumentNullException>());

            Assert.That(
                () => ValidationErrors.InvalidUrl(null!),
                Throws.TypeOf<ArgumentNullException>());

            Assert.That(
                () => ValidationErrors.CollectionMustBeEmpty(null!),
                Throws.TypeOf<ArgumentNullException>());

            Assert.That(
                () => ValidationErrors.CollectionMustNotBeEmpty(null!),
                Throws.TypeOf<ArgumentNullException>());

            Assert.That(
                () => ValidationErrors.Duplicate(null!),
                Throws.TypeOf<ArgumentNullException>());

            Assert.That(
                () => ValidationErrors.InvalidCollectionItem(null!),
                Throws.TypeOf<ArgumentNullException>());

            Assert.That(
                () => ValidationErrors.PastDate(null!),
                Throws.TypeOf<ArgumentNullException>());

            Assert.That(
                () => ValidationErrors.FutureDate(null!),
                Throws.TypeOf<ArgumentNullException>());

            Assert.That(
                () => ValidationErrors.InvalidEnum(null!),
                Throws.TypeOf<ArgumentNullException>());
        });
    }

    [Test]
    public void PropertyName_WithEmpty_ShouldThrowArgumentException()
    {
        Assert.Multiple(() =>
        {
            Assert.That(
                () => ValidationErrors.Required(string.Empty),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.Null(string.Empty),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.Empty(string.Empty),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.InvalidFormat(string.Empty),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.InvalidPattern(string.Empty),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.InvalidEmail(string.Empty),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.InvalidPhone(string.Empty),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.InvalidUrl(string.Empty),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.CollectionMustBeEmpty(string.Empty),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.CollectionMustNotBeEmpty(string.Empty),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.Duplicate(string.Empty),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.InvalidCollectionItem(string.Empty),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.PastDate(string.Empty),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.FutureDate(string.Empty),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.InvalidEnum(string.Empty),
                Throws.ArgumentException);
        });
    }

    [Test]
    public void PropertyName_WithWhitespace_ShouldThrowArgumentException()
    {
        Assert.Multiple(() =>
        {
            Assert.That(
                () => ValidationErrors.Required("   "),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.Null("   "),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.Empty("   "),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.InvalidFormat("   "),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.InvalidPattern("   "),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.InvalidEmail("   "),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.InvalidPhone("   "),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.InvalidUrl("   "),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.CollectionMustBeEmpty("   "),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.CollectionMustNotBeEmpty("   "),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.Duplicate("   "),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.InvalidCollectionItem("   "),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.PastDate("   "),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.FutureDate("   "),
                Throws.ArgumentException);

            Assert.That(
                () => ValidationErrors.InvalidEnum("   "),
                Throws.ArgumentException);
        });
    }

    [Test]
    public void Length_WithZero_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            Assert.That(
                () => ValidationErrors.MinLength("Name", 0),
                Throws.TypeOf<ArgumentOutOfRangeException>());

            Assert.That(
                () => ValidationErrors.MaxLength("Name", 0),
                Throws.TypeOf<ArgumentOutOfRangeException>());

            Assert.That(
                () => ValidationErrors.ExactLength("Name", 0),
                Throws.TypeOf<ArgumentOutOfRangeException>());
        });
    }

    [Test]
    public void Length_WithNegativeValue_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.Multiple(() =>
        {
            Assert.That(
                () => ValidationErrors.MinLength("Name", -1),
                Throws.TypeOf<ArgumentOutOfRangeException>());

            Assert.That(
                () => ValidationErrors.MaxLength("Name", -1),
                Throws.TypeOf<ArgumentOutOfRangeException>());

            Assert.That(
                () => ValidationErrors.ExactLength("Name", -1),
                Throws.TypeOf<ArgumentOutOfRangeException>());
        });
    }

    [Test]
    public void Length_WithOne_ShouldBeAccepted()
    {
        Assert.Multiple(() =>
        {
            Assert.That(
                ValidationErrors.MinLength("Name", 1).Code,
                Is.EqualTo(CommonErrorCodes.MinLength));

            Assert.That(
                ValidationErrors.MaxLength("Name", 1).Code,
                Is.EqualTo(CommonErrorCodes.MaxLength));

            Assert.That(
                ValidationErrors.ExactLength("Name", 1).Code,
                Is.EqualTo(CommonErrorCodes.ExactLength));
        });
    }

    [Test]
    public void NumericComparisons_ShouldUseExpectedCodes()
    {
        Assert.Multiple(() =>
        {
            Assert.That(
                ValidationErrors.GreaterThan("Value", 10).Code,
                Is.EqualTo(CommonErrorCodes.GreaterThan));

            Assert.That(
                ValidationErrors.GreaterThanOrEqual("Value", 10).Code,
                Is.EqualTo(CommonErrorCodes.GreaterThanOrEqual));

            Assert.That(
                ValidationErrors.LessThan("Value", 10).Code,
                Is.EqualTo(CommonErrorCodes.LessThan));

            Assert.That(
                ValidationErrors.LessThanOrEqual("Value", 10).Code,
                Is.EqualTo(CommonErrorCodes.LessThanOrEqual));

            Assert.That(
                ValidationErrors.Between("Value", 1, 10).Code,
                Is.EqualTo(CommonErrorCodes.Between));
        });
    }
}
