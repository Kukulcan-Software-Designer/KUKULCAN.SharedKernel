using KUKULCAN.SharedKernel.Attributes;

namespace KUKULCAN.SharedKernel.UnitTests.Attributes;

/// <summary>
/// Contains unit tests for <see cref="EqualityOrderAttribute"/>.
/// </summary>
[TestFixture]
public sealed class EqualityOrderAttributeTests
{
    /// <summary>
    /// Verifies that the supplied equality order is preserved.
    /// </summary>
    /// <param name="order">The equality order supplied to the attribute.</param>
    [TestCase(-1)]
    [TestCase(0)]
    [TestCase(10)]
    public void Constructor_ShouldPreserveSpecifiedOrder(int order)
    {
        var attribute = new EqualityOrderAttribute(order);

        Assert.That(attribute.Order, Is.EqualTo(order));
    }

    /// <summary>
    /// Verifies that the attribute can only be applied to properties.
    /// </summary>
    [Test]
    public void AttributeUsage_ShouldRestrictApplicationToProperties()
    {
        AttributeUsageAttribute usage = GetAttributeUsage<EqualityOrderAttribute>();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(usage.ValidOn, Is.EqualTo(AttributeTargets.Property));
            Assert.That(usage.AllowMultiple, Is.False);
        }
    }

    private static AttributeUsageAttribute GetAttributeUsage<TAttribute>()
        where TAttribute : Attribute
        => typeof(TAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), inherit: true)
            .Cast<AttributeUsageAttribute>()
            .Single();
}
