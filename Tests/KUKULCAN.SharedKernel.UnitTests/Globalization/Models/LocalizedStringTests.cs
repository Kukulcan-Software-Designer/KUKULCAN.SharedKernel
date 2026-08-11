using KUKULCAN.SharedKernel.Globalization.Models;

namespace KUKULCAN.SharedKernel.UnitTests.Globalization.Models;

/// <summary>
/// Contains unit tests for <see cref="LocalizedString"/>.
/// </summary>
[TestFixture]
public sealed class LocalizedStringTests
{
    /// <summary>
    /// Verifies that construction preserves every localized-string component.
    /// </summary>
    /// <param name="resourceNotFound">Whether the resource lookup failed.</param>
    [TestCase(false)]
    [TestCase(true)]
    public void Constructor_ShouldPreserveLocalizedStringData(bool resourceNotFound)
    {
        const string key = "Customer.Name";
        const string value = "Client";

        var localizedString = new LocalizedString(key, value, resourceNotFound);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(localizedString.Key, Is.EqualTo(key));
            Assert.That(localizedString.Value, Is.EqualTo(value));
            Assert.That(localizedString.ResourceNotFound, Is.EqualTo(resourceNotFound));
        }
    }

    /// <summary>
    /// Verifies that localized strings with equal components are equal and have matching hash codes.
    /// </summary>
    [Test]
    public void Equality_WithSameData_ShouldReturnTrue()
    {
        var first = new LocalizedString("Customer.Name", "Customer", false);
        var second = new LocalizedString("Customer.Name", "Customer", false);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(first, Is.EqualTo(second));
            Assert.That(first.GetHashCode(), Is.EqualTo(second.GetHashCode()));
        }
    }

    /// <summary>
    /// Verifies that localized strings with different keys are not equal.
    /// </summary>
    [Test]
    public void Equality_WithDifferentKey_ShouldReturnFalse()
    {
        var first = new LocalizedString("Customer.Name", "Customer", false);
        var second = new LocalizedString("Customer.Description", "Customer", false);

        Assert.That(first, Is.Not.EqualTo(second));
    }

    /// <summary>
    /// Verifies that localized strings with different values are not equal.
    /// </summary>
    [Test]
    public void Equality_WithDifferentValue_ShouldReturnFalse()
    {
        var first = new LocalizedString("Customer.Name", "Customer", false);
        var second = new LocalizedString("Customer.Name", "Client", false);

        Assert.That(first, Is.Not.EqualTo(second));
    }

    /// <summary>
    /// Verifies that localized strings with different resource-not-found states are not equal.
    /// </summary>
    [Test]
    public void Equality_WithDifferentResourceNotFoundValue_ShouldReturnFalse()
    {
        var first = new LocalizedString("Customer.Name", "Customer", false);
        var second = new LocalizedString("Customer.Name", "Customer", true);

        Assert.That(first, Is.Not.EqualTo(second));
    }

    /// <summary>
    /// Verifies that a with-expression creates a changed copy without mutating the original instance.
    /// </summary>
    [Test]
    public void WithExpression_ShouldCreateNewInstanceWithoutChangingOriginal()
    {
        var original = new LocalizedString("Customer.Name", "Customer", false);

        LocalizedString changed = original with { Value = "Client", ResourceNotFound = true };

        using (Assert.EnterMultipleScope())
        {
            Assert.That(changed, Is.Not.SameAs(original));
            Assert.That(original.Value, Is.EqualTo("Customer"));
            Assert.That(original.ResourceNotFound, Is.False);
            Assert.That(changed.Key, Is.EqualTo(original.Key));
            Assert.That(changed.Value, Is.EqualTo("Client"));
            Assert.That(changed.ResourceNotFound, Is.True);
        }
    }
}
