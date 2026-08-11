using System.Globalization;
using KUKULCAN.SharedKernel.Globalization.Culture;

namespace KUKULCAN.SharedKernel.UnitTests.Globalization.Culture;

/// <summary>
/// Contains unit tests for <see cref="SupportedCulture"/>.
/// </summary>
[TestFixture]
public sealed class SupportedCultureTests
{
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

    [TestCase("es-ES")]
    [TestCase("ES-es")]
    public void FromName_WithSpanishSpainName_ShouldReturnRegisteredCulture(string name)
    {
        SupportedCulture culture = SupportedCulture.FromName(name);

        Assert.That(culture, Is.SameAs(SupportedCulture.SpanishSpain));
    }

    [TestCase("es-MX")]
    [TestCase("ES-mx")]
    public void FromName_WithSpanishMexicoName_ShouldReturnRegisteredCulture(string name)
    {
        SupportedCulture culture = SupportedCulture.FromName(name);

        Assert.That(culture, Is.SameAs(SupportedCulture.SpanishMexico));
    }

    [TestCase("en-US")]
    [TestCase("EN-us")]
    public void FromName_WithEnglishUnitedStatesName_ShouldReturnRegisteredCulture(string name)
    {
        SupportedCulture culture = SupportedCulture.FromName(name);

        Assert.That(culture, Is.SameAs(SupportedCulture.EnglishUnitedStates));
    }

    [Test]
    public void FromName_WithNullName_ShouldThrowArgumentNullException()
    {
        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => SupportedCulture.FromName(null!))!;

        Assert.That(exception.ParamName, Is.EqualTo("name"));
    }

    [TestCase("")]
    [TestCase("   ")]
    public void FromName_WithEmptyOrWhitespaceName_ShouldThrowArgumentException(string name)
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => SupportedCulture.FromName(name))!;

        Assert.That(exception.ParamName, Is.EqualTo("name"));
    }

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

    [TestCase("es-ES", true)]
    [TestCase("ES-es", true)]
    [TestCase("es-MX", true)]
    [TestCase("en-US", true)]
    [TestCase("fr-FR", false)]
    public void IsSupported_ShouldDetermineWhetherNameIsRegistered(string name, bool expected)
    {
        Assert.That(SupportedCulture.IsSupported(name), Is.EqualTo(expected));
    }

    [Test]
    public void IsSupported_WithNullName_ShouldThrowArgumentNullException()
    {
        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => SupportedCulture.IsSupported(null!))!;

        Assert.That(exception.ParamName, Is.EqualTo("name"));
    }

    [TestCase("")]
    [TestCase("   ")]
    public void IsSupported_WithEmptyOrWhitespaceName_ShouldThrowArgumentException(string name)
    {
        ArgumentException exception = Assert.Throws<ArgumentException>(
            () => SupportedCulture.IsSupported(name))!;

        Assert.That(exception.ParamName, Is.EqualTo("name"));
    }

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
