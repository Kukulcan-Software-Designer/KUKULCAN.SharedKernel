using System.Globalization;
using KUKULCAN.SharedKernel.Versioning;

namespace KUKULCAN.SharedKernel.UnitTests.Versioning;

/// <summary>
/// Contains unit tests for <see cref="SemanticVersion"/>.
/// </summary>
[TestFixture]
public sealed class SemanticVersionTests
{
    /// <summary>
    /// Verifies that construction preserves the mandatory semantic-version components.
    /// </summary>
    [Test]
    public void Constructor_WithCoreComponents_ShouldPreserveValues()
    {
        var version = new SemanticVersion(1, 2, 3);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(version.Major, Is.EqualTo(1));
            Assert.That(version.Minor, Is.EqualTo(2));
            Assert.That(version.Patch, Is.EqualTo(3));
            Assert.That(version.Prerelease, Is.Null);
            Assert.That(version.BuildMetadata, Is.Null);
            Assert.That(version.IsPrerelease, Is.False);
            Assert.That(version.HasBuildMetadata, Is.False);
        }
    }

    /// <summary>
    /// Verifies that construction preserves prerelease and build metadata identifiers.
    /// </summary>
    [Test]
    public void Constructor_WithPrereleaseAndBuildMetadata_ShouldPreserveValues()
    {
        var version = new SemanticVersion(1, 2, 3, "beta.1", "build.42");

        using (Assert.EnterMultipleScope())
        {
            Assert.That(version.Prerelease, Is.EqualTo("beta.1"));
            Assert.That(version.BuildMetadata, Is.EqualTo("build.42"));
            Assert.That(version.IsPrerelease, Is.True);
            Assert.That(version.HasBuildMetadata, Is.True);
        }
    }

    /// <summary>
    /// Verifies that empty optional identifiers are normalized to null.
    /// </summary>
    /// <param name="identifier">The blank identifier to normalize.</param>
    [TestCase("")]
    [TestCase("   ")]
    public void Constructor_WithBlankOptionalIdentifiers_ShouldNormalizeThemToNull(string? identifier)
    {
        var version = new SemanticVersion(1, 2, 3, identifier, identifier);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(version.Prerelease, Is.Null);
            Assert.That(version.BuildMetadata, Is.Null);
            Assert.That(version.IsPrerelease, Is.False);
            Assert.That(version.HasBuildMetadata, Is.False);
        }
    }

    /// <summary>
    /// Verifies that null optional identifiers are normalized to null.
    /// </summary>
    [Test]
    public void Constructor_WithNullOptionalIdentifiers_ShouldNormalizeThemToNull()
    {
        var version = new SemanticVersion(1, 2, 3, null, null);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(version.Prerelease, Is.Null);
            Assert.That(version.BuildMetadata, Is.Null);
        }
    }

    /// <summary>
    /// Verifies that negative mandatory components are rejected.
    /// </summary>
    /// <param name="major">The major component supplied to the constructor.</param>
    /// <param name="minor">The minor component supplied to the constructor.</param>
    /// <param name="patch">The patch component supplied to the constructor.</param>
    /// <param name="parameterName">The expected failing parameter name.</param>
    [TestCase(-1, 0, 0, "major")]
    [TestCase(0, -1, 0, "minor")]
    [TestCase(0, 0, -1, "patch")]
    public void Constructor_WithNegativeComponent_ShouldThrowArgumentOutOfRangeException(
        int major,
        int minor,
        int patch,
        string parameterName)
    {
        ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => new SemanticVersion(major, minor, patch))!;

        Assert.That(exception.ParamName, Is.EqualTo(parameterName));
    }

    /// <summary>
    /// Verifies that identifiers containing spaces are rejected.
    /// </summary>
    /// <param name="identifier">The invalid identifier.</param>
    /// <param name="parameterName">The expected failing parameter name.</param>
    [TestCase("alpha beta", "prerelease")]
    [TestCase("build 42", "buildMetadata")]
    public void Constructor_WithIdentifierContainingSpaces_ShouldThrowArgumentException(
        string identifier,
        string parameterName)
    {
        ArgumentException exception = parameterName == "prerelease"
            ? Assert.Throws<ArgumentException>(() => new SemanticVersion(1, 2, 3, identifier))!
            : Assert.Throws<ArgumentException>(() => new SemanticVersion(1, 2, 3, buildMetadata: identifier))!;

        Assert.That(exception.ParamName, Is.EqualTo(parameterName));
    }

    /// <summary>
    /// Verifies that a complete version string is parsed into its components.
    /// </summary>
    [Test]
    public void Parse_WithCompleteVersion_ShouldCreateEquivalentVersion()
    {
        SemanticVersion version = SemanticVersion.Parse("2.3.4-rc.1+build.7", CultureInfo.InvariantCulture);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(version.Major, Is.EqualTo(2));
            Assert.That(version.Minor, Is.EqualTo(3));
            Assert.That(version.Patch, Is.EqualTo(4));
            Assert.That(version.Prerelease, Is.EqualTo("rc.1"));
            Assert.That(version.BuildMetadata, Is.EqualTo("build.7"));
        }
    }

    /// <summary>
    /// Verifies that a character span is parsed into an equivalent version.
    /// </summary>
    [Test]
    public void Parse_WithCharacterSpan_ShouldCreateEquivalentVersion()
    {
        SemanticVersion version = SemanticVersion.Parse("2.3.4".AsSpan(), CultureInfo.InvariantCulture);

        Assert.That(version, Is.EqualTo(new SemanticVersion(2, 3, 4)));
    }

    /// <summary>
    /// Verifies that malformed version text is rejected during parsing.
    /// </summary>
    /// <param name="text">The invalid version text.</param>
    [TestCase("")]
    [TestCase("   ")]
    [TestCase("1.2")]
    [TestCase("1.2.3.4")]
    [TestCase("major.2.3")]
    public void Parse_WithInvalidText_ShouldThrowFormatException(string? text)
    {
        Assert.That(
            () => SemanticVersion.Parse(text!),
            Throws.TypeOf<FormatException>());
    }

    /// <summary>
    /// Verifies that null version text is rejected during parsing.
    /// </summary>
    [Test]
    public void Parse_WithNullText_ShouldThrowFormatException()
    {
        Assert.That(
            () => SemanticVersion.Parse(null!),
            Throws.TypeOf<FormatException>());
    }

    /// <summary>
    /// Verifies that valid version text produces a parsed version.
    /// </summary>
    [Test]
    public void TryParse_WithValidText_ShouldReturnTrueAndVersion()
    {
        bool parsed = SemanticVersion.TryParse("1.2.3-alpha+build.4", out SemanticVersion? version);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(parsed, Is.True);
            Assert.That(version, Is.EqualTo(new SemanticVersion(1, 2, 3, "alpha", "build.4")));
        }
    }

    /// <summary>
    /// Verifies that a valid character span produces a parsed version.
    /// </summary>
    [Test]
    public void TryParse_WithValidCharacterSpan_ShouldReturnTrueAndVersion()
    {
        bool parsed = SemanticVersion.TryParse("1.2.3".AsSpan(), CultureInfo.InvariantCulture, out SemanticVersion? version);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(parsed, Is.True);
            Assert.That(version, Is.EqualTo(new SemanticVersion(1, 2, 3)));
        }
    }

    /// <summary>
    /// Verifies that malformed version text produces no parsed version.
    /// </summary>
    /// <param name="text">The invalid version text.</param>
    [TestCase("")]
    [TestCase("   ")]
    [TestCase("1.2")]
    [TestCase("1.2.3.4")]
    [TestCase("major.2.3")]
    public void TryParse_WithInvalidText_ShouldReturnFalseAndNull(string? text)
    {
        bool parsed = SemanticVersion.TryParse(text, out SemanticVersion? version);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(parsed, Is.False);
            Assert.That(version, Is.Null);
        }
    }

    /// <summary>
    /// Verifies that null version text produces no parsed version.
    /// </summary>
    [Test]
    public void TryParse_WithNullText_ShouldReturnFalseAndNull()
    {
        bool parsed = SemanticVersion.TryParse(null, out SemanticVersion? version);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(parsed, Is.False);
            Assert.That(version, Is.Null);
        }
    }

    /// <summary>
    /// Verifies that versions with equal components are equal and have equal hash codes.
    /// </summary>
    [Test]
    public void Equality_WithEqualValues_ShouldReturnTrue()
    {
        var first = new SemanticVersion(1, 2, 3, "alpha", "build.4");
        var second = new SemanticVersion(1, 2, 3, "alpha", "build.4");

        using (Assert.EnterMultipleScope())
        {
            Assert.That(first, Is.EqualTo(second));
            Assert.That(first.GetHashCode(), Is.EqualTo(second.GetHashCode()));
            Assert.That(first == second, Is.True);
        }
    }

    /// <summary>
    /// Verifies that build metadata participates in value equality.
    /// </summary>
    [Test]
    public void Equality_WithDifferentBuildMetadata_ShouldReturnFalse()
    {
        var first = new SemanticVersion(1, 2, 3, buildMetadata: "build.1");
        var second = new SemanticVersion(1, 2, 3, buildMetadata: "build.2");

        Assert.That(first, Is.Not.EqualTo(second));
    }

    /// <summary>
    /// Verifies that major, minor, and patch components determine version ordering.
    /// </summary>
    /// <param name="leftMajor">The major component of the left version.</param>
    /// <param name="leftMinor">The minor component of the left version.</param>
    /// <param name="leftPatch">The patch component of the left version.</param>
    /// <param name="rightMajor">The major component of the right version.</param>
    /// <param name="rightMinor">The minor component of the right version.</param>
    /// <param name="rightPatch">The patch component of the right version.</param>
    [TestCase(1, 0, 0, 1, 0, 1)]
    [TestCase(1, 0, 0, 1, 1, 0)]
    [TestCase(1, 0, 0, 2, 0, 0)]
    public void CompareTo_WithDifferentCoreComponents_ShouldUseMajorMinorAndPatch(
        int leftMajor,
        int leftMinor,
        int leftPatch,
        int rightMajor,
        int rightMinor,
        int rightPatch)
    {
        var left = new SemanticVersion(leftMajor, leftMinor, leftPatch);
        var right = new SemanticVersion(rightMajor, rightMinor, rightPatch);

        Assert.That(left.CompareTo(right), Is.LessThan(0));
    }

    /// <summary>
    /// Verifies that a stable version sorts after a prerelease with the same core components.
    /// </summary>
    [Test]
    public void CompareTo_WithPrereleaseAndStableVersion_ShouldTreatStableVersionAsNewer()
    {
        var prerelease = new SemanticVersion(1, 0, 0, "beta");
        var stable = new SemanticVersion(1, 0, 0);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(prerelease.CompareTo(stable), Is.LessThan(0));
            Assert.That(stable.CompareTo(prerelease), Is.GreaterThan(0));
        }
    }

    /// <summary>
    /// Verifies that prerelease identifiers use ordinal ordering.
    /// </summary>
    [Test]
    public void CompareTo_WithDifferentPrereleases_ShouldUseOrdinalOrder()
    {
        var alpha = new SemanticVersion(1, 0, 0, "alpha");
        var beta = new SemanticVersion(1, 0, 0, "beta");

        Assert.That(alpha.CompareTo(beta), Is.LessThan(0));
    }

    /// <summary>
    /// Verifies that build metadata does not affect ordering and a version sorts above null.
    /// </summary>
    [Test]
    public void CompareTo_ShouldIgnoreBuildMetadataAndTreatNullAsLower()
    {
        var first = new SemanticVersion(1, 0, 0, buildMetadata: "build.1");
        var second = new SemanticVersion(1, 0, 0, buildMetadata: "build.2");

        using (Assert.EnterMultipleScope())
        {
            Assert.That(first.CompareTo(second), Is.Zero);
            Assert.That(first.CompareTo(null), Is.EqualTo(1));
        }
    }

    /// <summary>
    /// Verifies that all version components are represented in the formatted value.
    /// </summary>
    /// <param name="major">The major version component.</param>
    /// <param name="minor">The minor version component.</param>
    /// <param name="patch">The patch version component.</param>
    /// <param name="prerelease">The optional prerelease identifier.</param>
    /// <param name="buildMetadata">The optional build metadata identifier.</param>
    /// <param name="expected">The expected formatted version.</param>
    [TestCase(1, 2, 3, null, null, "1.2.3")]
    [TestCase(1, 2, 3, "beta", null, "1.2.3-beta")]
    [TestCase(1, 2, 3, null, "build.4", "1.2.3+build.4")]
    [TestCase(1, 2, 3, "beta", "build.4", "1.2.3-beta+build.4")]
    public void ToString_ShouldFormatAllVersionComponents(
        int major,
        int minor,
        int patch,
        string? prerelease,
        string? buildMetadata,
        string expected)
    {
        var version = new SemanticVersion(major, minor, patch, prerelease, buildMetadata);

        Assert.That(version.ToString(), Is.EqualTo(expected));
    }
}
