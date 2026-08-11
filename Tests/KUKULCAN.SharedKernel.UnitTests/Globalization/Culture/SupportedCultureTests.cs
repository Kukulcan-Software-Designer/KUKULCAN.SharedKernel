using System.Globalization;
using KUKULCAN.SharedKernel.Globalization.Culture;

namespace KUKULCAN.SharedKernel.UnitTests.Globalization.Culture;

/// <summary>
/// Contains unit tests for <see cref="SupportedCulture"/>.
/// </summary>
[TestFixture]
public sealed class SupportedCultureTests
{
    /// <summary>
    /// Verifies that every predefined culture exposes its canonical name.
    /// </summary>
    [Test]
    public void PredefinedCultures_ShouldExposeExpectedNames()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(SupportedCulture.Invariant.Name, Is.Empty);
            Assert.That(SupportedCulture.SpanishSpain.Name, Is.EqualTo("es-ES"));
            Assert.That(SupportedCulture.SpanishMexico.Name, Is.EqualTo("es-MX"));
            Assert.That(SupportedCulture.EnglishUnitedStates.Name, Is.EqualTo("en-US"));
        }
    }

    /// <summary>
    /// Verifies that all predefined cultures are returned in name order.
    /// </summary>
    [Test]
    public void All_ShouldContainEveryPredefinedCultureInNameOrder()
    {
        SupportedCulture[] cultures = SupportedCulture.All.ToArray();

        Assert.That(
            cultures,
            Is.EqualTo(
            [
                SupportedCulture.Invariant,
                SupportedCulture.EnglishUnitedStates,
                SupportedCulture.SpanishSpain,
                SupportedCulture.SpanishMexico,
            ]));
    }

    /// <summary>
    /// Verifies that the registered culture collection exposes each predefined instance.
    /// </summary>
    [Test]
    public void All_ShouldExposeTheRegisteredInstances()
    {
        IReadOnlyCollection<SupportedCulture> cultures = SupportedCulture.All;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(cultures, Does.Contain(SupportedCulture.Invariant));
            Assert.That(cultures, Does.Contain(SupportedCulture.SpanishSpain));
            Assert.That(cultures, Does.Contain(SupportedCulture.SpanishMexico));
            Assert.That(cultures, Does.Contain(SupportedCulture.EnglishUnitedStates));
        }
    }

    /// <summary>
    /// Verifies that a Spanish (Spain) name resolves to its registered culture,
    /// regardless of casing.
    /// </summary>
    /// <param name="name">The name used to retrieve the culture.</param>
    [TestCase("es-ES")]
    [TestCase("ES-es")]
    public void FromName_WithSpanishSpainName_ShouldReturnRegisteredCulture(string name)
    {
        SupportedCulture culture = SupportedCulture.FromName(name);

        Assert.That(culture, Is.SameAs(SupportedCulture.SpanishSpain));
    }

    /// <summary>
    /// Verifies that a Spanish (Mexico) name resolves to its registered culture,
    /// regardless of casing.
    /// </summary>
    /// <param name="name">The name used to retrieve the culture.</param>
    [TestCase("es-MX")]
    [TestCase("ES-mx")]
    public void FromName_WithSpanishMexicoName_ShouldReturnRegisteredCulture(string name)
    {
        SupportedCulture culture = SupportedCulture.FromName(name);

        Assert.That(culture, Is.SameAs(SupportedCulture.SpanishMexico));
    }

    /// <summary>
    /// Verifies that an English (United States) name resolves to its registered culture,
    /// regardless of casing.
    /// </summary>
    /// <param name="name">The name used to retrieve the culture.</param>
    [TestCase("en-US")]
    [TestCase("EN-us")]
    public void FromName_WithEnglishUnitedStatesName_ShouldReturnRegisteredCulture(string name)
    {
        SupportedCulture culture = SupportedCulture.FromName(name);

        Assert.That(culture, Is.SameAs(SupportedCulture.EnglishUnitedStates));
    }

    /// <summary>
    /// Verifies that a null culture name is rejected.
    /// </summary>
    [Test]
    public void FromName_WithNullName_ShouldThrowArgumentNullException()
    {
        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => SupportedCulture.FromName(null!))!;

        Assert.That(exception.ParamName, Is.EqualTo("name"));
    }

    /// <summary>
    /// Verifies that empty and whitespace-only culture names are rejected.
    /// </summary>
    /// <param name="name">The invalid culture name.</param>
    [TestCase("")]
    [TestCase("   ")]
    public void FromName_WithEmptyOrWhitespaceName_ShouldThrowArgumentException(string name)
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => SupportedCulture.FromName(name))!;

        Assert.That(exception.ParamName, Is.EqualTo("name"));
    }

    /// <summary>
    /// Verifies that an unregistered culture name is rejected and identified in the error.
    /// </summary>
    [Test]
    public void FromName_WithUnsupportedName_ShouldThrowArgumentException()
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => SupportedCulture.FromName("fr-FR"))!;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(exception.ParamName, Is.EqualTo("name"));
            Assert.That(exception.Message, Does.Contain("fr-FR"));
        }
    }

    /// <summary>
    /// Verifies whether a culture name is registered, independently of casing.
    /// </summary>
    /// <param name="name">The culture name to evaluate.</param>
    /// <param name="expected">The expected registration result.</param>
    [TestCase("es-ES", true)]
    [TestCase("ES-es", true)]
    [TestCase("es-MX", true)]
    [TestCase("en-US", true)]
    [TestCase("fr-FR", false)]
    public void IsSupported_ShouldDetermineWhetherNameIsRegistered(string name, bool expected)
    {
        Assert.That(SupportedCulture.IsSupported(name), Is.EqualTo(expected));
    }

    /// <summary>
    /// Verifies that a null culture name is rejected when checking registration.
    /// </summary>
    [Test]
    public void IsSupported_WithNullName_ShouldThrowArgumentNullException()
    {
        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => SupportedCulture.IsSupported(null!))!;

        Assert.That(exception.ParamName, Is.EqualTo("name"));
    }

    /// <summary>
    /// Verifies that empty and whitespace-only names are rejected when checking registration.
    /// </summary>
    /// <param name="name">The invalid culture name.</param>
    [TestCase("")]
    [TestCase("   ")]
    public void IsSupported_WithEmptyOrWhitespaceName_ShouldThrowArgumentException(string name)
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => SupportedCulture.IsSupported(name))!;

        Assert.That(exception.ParamName, Is.EqualTo("name"));
    }

    /// <summary>
    /// Verifies that the exposed metadata matches the associated .NET culture.
    /// </summary>
    /// <param name="culture">The predefined culture whose metadata is verified.</param>
    [TestCaseSource(nameof(PredefinedCultures))]
    public void CultureMetadata_ShouldMatchUnderlyingCultureInfo(SupportedCulture culture)
    {
        CultureInfo expected = string.IsNullOrEmpty(culture.Name)
            ? CultureInfo.InvariantCulture
            : CultureInfo.GetCultureInfo(culture.Name);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(culture.CultureInfo.Name, Is.EqualTo(expected.Name));
            Assert.That(culture.DisplayName, Is.EqualTo(expected.DisplayName));
            Assert.That(culture.NativeName, Is.EqualTo(expected.NativeName));
            Assert.That(culture.IsNeutralCulture, Is.EqualTo(expected.IsNeutralCulture));
            Assert.That(culture.Parent.Name, Is.EqualTo(expected.Parent.Name));
        }
    }

    /// <summary>
    /// Verifies that a culture is equal to an equivalent culture and has a consistent hash code.
    /// </summary>
    [Test]
    public void Equality_WithSameCulture_ShouldReturnTrue()
    {
        SupportedCulture culture = SupportedCulture.SpanishSpain;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(culture.Equals(SupportedCulture.SpanishSpain), Is.True);
            Assert.That(culture.Equals((object?)SupportedCulture.SpanishSpain), Is.True);
            Assert.That(culture.GetHashCode(), Is.EqualTo(SupportedCulture.SpanishSpain.GetHashCode()));
        }
    }

    /// <summary>
    /// Verifies that a culture is not equal to a different culture, null, or another object type.
    /// </summary>
    [Test]
    public void Equality_WithDifferentOrNullCulture_ShouldReturnFalse()
    {
        SupportedCulture culture = SupportedCulture.SpanishSpain;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(culture.Equals(SupportedCulture.SpanishMexico), Is.False);
            Assert.That(culture.Equals(null), Is.False);
            Assert.That(culture, Is.Not.EqualTo(new object()));
        }
    }

    /// <summary>
    /// Verifies that equality and inequality operators follow culture equality semantics.
    /// </summary>
    [Test]
    public void EqualityOperators_ShouldReflectCultureEquality()
    {
        SupportedCulture spanishSpain = SupportedCulture.SpanishSpain;
        SupportedCulture sameSpanishSpain = SupportedCulture.FromName("ES-es");
        SupportedCulture? nullCulture = null;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(spanishSpain == sameSpanishSpain, Is.True);
            Assert.That(spanishSpain != SupportedCulture.SpanishMexico, Is.True);
            Assert.That(spanishSpain == nullCulture, Is.False);
            Assert.That(nullCulture == null, Is.True);
        }
    }

    /// <summary>
    /// Verifies that cultures are compared by name and sort above a null culture.
    /// </summary>
    [Test]
    public void CompareTo_ShouldSortByNameAndTreatNullAsLower()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(SupportedCulture.Invariant.CompareTo(SupportedCulture.EnglishUnitedStates), Is.LessThan(0));
            Assert.That(SupportedCulture.EnglishUnitedStates.CompareTo(SupportedCulture.SpanishSpain), Is.LessThan(0));
            Assert.That(SupportedCulture.SpanishMexico.CompareTo(SupportedCulture.SpanishSpain), Is.GreaterThan(0));
            Assert.That(SupportedCulture.SpanishSpain.CompareTo(null), Is.EqualTo(1));
            Assert.That(SupportedCulture.SpanishSpain.CompareTo(SupportedCulture.SpanishSpain), Is.EqualTo(0));
        }
    }

    /// <summary>
    /// Verifies that the string representation is the culture name.
    /// </summary>
    [Test]
    public void ToString_ShouldReturnCultureName()
    {
        Assert.That(SupportedCulture.SpanishMexico.ToString(), Is.EqualTo("es-MX"));
    }

    private static IEnumerable<SupportedCulture> PredefinedCultures =>
    [
        SupportedCulture.Invariant,
        SupportedCulture.SpanishSpain,
        SupportedCulture.SpanishMexico,
        SupportedCulture.EnglishUnitedStates,
    ];
}
