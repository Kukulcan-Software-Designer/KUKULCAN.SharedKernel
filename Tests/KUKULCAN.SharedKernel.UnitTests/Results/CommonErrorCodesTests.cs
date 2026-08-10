using System.Reflection;
using KUKULCAN.SharedKernel.Results;

namespace KUKULCAN.SharedKernel.UnitTests.Results;

/// <summary>
/// Contains unit tests for the
/// <see cref="CommonErrorCodes"/> constants.
/// </summary>
[TestFixture]
public sealed class CommonErrorCodesTests
{
    /// <summary>
    /// Verifies that all public constants exposed by
    /// <see cref="CommonErrorCodes"/> have a non-null value.
    /// </summary>
    [Test]
    public void PublicConstants_ShouldNotBeNull()
    {
        FieldInfo[] fields = GetPublicConstantFields();

        Assert.That(fields, Is.Not.Empty);

        foreach (FieldInfo field in fields)
        {
            Assert.That(
                field.GetValue(null),
                Is.Not.Null,
                $"Constant '{field.Name}' must not be null.");
        }
    }

    /// <summary>
    /// Verifies that all public constants exposed by
    /// <see cref="CommonErrorCodes"/> contain a non-empty value.
    /// </summary>
    [Test]
    public void PublicConstants_ShouldNotBeEmpty()
    {
        FieldInfo[] fields = GetPublicConstantFields();

        Assert.That(fields, Is.Not.Empty);

        foreach (FieldInfo field in fields)
        {
            var value = field.GetValue(null) as string;

            Assert.That(
                value,
                Is.Not.Null.And.Not.Empty,
                $"Constant '{field.Name}' must contain a value.");
        }
    }

    /// <summary>
    /// Verifies that all public constants exposed by
    /// <see cref="CommonErrorCodes"/> contain non-whitespace values.
    /// </summary>
    [Test]
    public void PublicConstants_ShouldNotContainWhitespaceOnlyValues()
    {
        FieldInfo[] fields = GetPublicConstantFields();

        Assert.That(fields, Is.Not.Empty);

        foreach (FieldInfo field in fields)
        {
            var value = field.GetValue(null) as string;

            Assert.That(
                value,
                Is.Not.Null.And.Not.WhiteSpace,
                $"Constant '{field.Name}' must not contain only whitespace.");
        }
    }

    /// <summary>
    /// Verifies that all public error codes are unique.
    /// </summary>
    [Test]
    public void PublicConstants_ShouldHaveUniqueValues()
    {
        FieldInfo[] fields = GetPublicConstantFields();

        Assert.That(fields, Is.Not.Empty);

        var constants = fields
            .Select(field => new
            {
                field.Name,
                Value = field.GetValue(null) as string
            })
            .ToArray();

        var duplicates = constants
            .Where(item => item.Value is not null)
            .GroupBy(item => item.Value!, StringComparer.Ordinal)
            .Where(group => group.Count() > 1)
            .Select(
                group =>
                    $"{group.Key}: {string.Join(
                        ", ",
                        group.Select(item => item.Name))}")
            .ToArray();

        Assert.That(
            duplicates,
            Is.Empty,
            "CommonErrorCodes contains duplicated values: " +
            string.Join("; ", duplicates));
    }

    /// <summary>
    /// Verifies that every public constant is a string constant.
    /// </summary>
    [Test]
    public void PublicConstants_ShouldBeStrings()
    {
        FieldInfo[] fields = GetPublicConstantFields();

        Assert.That(fields, Is.Not.Empty);

        foreach (FieldInfo field in fields)
        {
            Assert.That(
                field.FieldType,
                Is.EqualTo(typeof(string)),
                $"Constant '{field.Name}' must be a string.");
        }
    }

    /// <summary>
    /// Verifies that all public constants are declared as constants.
    /// </summary>
    [Test]
    public void PublicConstants_ShouldBeConstantFields()
    {
        FieldInfo[] fields = GetPublicConstantFields();

        Assert.That(fields, Is.Not.Empty);

        foreach (FieldInfo field in fields)
        {
            Assert.That(
                field.IsLiteral,
                Is.True,
                $"Field '{field.Name}' must be declared as const.");

            Assert.That(
                field.IsStatic,
                Is.True,
                $"Field '{field.Name}' must be static.");
        }
    }

    /// <summary>
    /// Gets all public string constants declared by
    /// <see cref="CommonErrorCodes"/> and its partial declarations.
    /// </summary>
    private static FieldInfo[] GetPublicConstantFields()
    {
        return typeof(CommonErrorCodes)
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
            .Where(
                field =>
                    field.IsLiteral &&
                    !field.IsInitOnly &&
                    field.FieldType == typeof(string))
            .OrderBy(field => field.Name, StringComparer.Ordinal)
            .ToArray();
    }
}
