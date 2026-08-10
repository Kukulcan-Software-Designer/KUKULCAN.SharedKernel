using KUKULCAN.SharedKernel.Validations;

namespace KUKULCAN.SharedKernel.UnitTests.Validations;

/// <summary>
/// Contains unit tests for <see cref="ValidationSeverity"/>.
/// </summary>
[TestFixture]
public sealed class ValidationSeverityTests
{
    /// <summary>
    /// Verifies that the information severity has the expected value.
    /// </summary>
    [Test]
    public void Information_ShouldHaveExpectedValue()
    {
        Assert.That(
            (int)ValidationSeverity.Information,
            Is.EqualTo(0));
    }

    /// <summary>
    /// Verifies that the warning severity has the expected value.
    /// </summary>
    [Test]
    public void Warning_ShouldHaveExpectedValue()
    {
        Assert.That(
            (int)ValidationSeverity.Warning,
            Is.EqualTo(1));
    }

    /// <summary>
    /// Verifies that the error severity has the expected value.
    /// </summary>
    [Test]
    public void Error_ShouldHaveExpectedValue()
    {
        Assert.That(
            (int)ValidationSeverity.Error,
            Is.EqualTo(2));
    }

    /// <summary>
    /// Verifies that all validation severities are distinct.
    /// </summary>
    [Test]
    public void Values_ShouldBeDistinct()
    {
        var values = new[]
        {
            ValidationSeverity.Information,
            ValidationSeverity.Warning,
            ValidationSeverity.Error
        };

        Assert.That(
            values.Distinct().Count(),
            Is.EqualTo(3));
    }

    /// <summary>
    /// Verifies that information has lower severity than warning.
    /// </summary>
    [Test]
    public void Information_ShouldBeLessSevereThanWarning()
    {
        Assert.That(
            ValidationSeverity.Information,
            Is.LessThan(ValidationSeverity.Warning));
    }

    /// <summary>
    /// Verifies that warning has lower severity than error.
    /// </summary>
    [Test]
    public void Warning_ShouldBeLessSevereThanError()
    {
        Assert.That(
            ValidationSeverity.Warning,
            Is.LessThan(ValidationSeverity.Error));
    }

    /// <summary>
    /// Verifies that information has lower severity than error.
    /// </summary>
    [Test]
    public void Information_ShouldBeLessSevereThanError()
    {
        Assert.That(
            ValidationSeverity.Information,
            Is.LessThan(ValidationSeverity.Error));
    }

    /// <summary>
    /// Verifies that the default value of the enum is information.
    /// </summary>
    [Test]
    public void DefaultValue_ShouldBeInformation()
    {
        var severity = default(ValidationSeverity);

        Assert.That(
            severity,
            Is.EqualTo(ValidationSeverity.Information));
    }

    /// <summary>
    /// Verifies that every declared enum value is represented by the
    /// expected set of validation severities.
    /// </summary>
    [Test]
    public void EnumValues_ShouldContainExpectedMembers()
    {
        var values = Enum.GetValues<ValidationSeverity>();

        Assert.That(
            values,
            Is.EquivalentTo(
                new[]
                {
                    ValidationSeverity.Information,
                    ValidationSeverity.Warning,
                    ValidationSeverity.Error
                }));
    }
}
