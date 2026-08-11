using KUKULCAN.SharedKernel.Attributes;

namespace KUKULCAN.SharedKernel.UnitTests.Attributes;

/// <summary>
/// Contains unit tests for <see cref="ValueObjectMemberAttribute"/>.
/// </summary>
[TestFixture]
public sealed class ValueObjectMemberAttributeTests
{
    /// <summary>
    /// Verifies that the attribute can only be applied to properties.
    /// </summary>
    [Test]
    public void AttributeUsage_ShouldRestrictApplicationToProperties()
    {
        AttributeUsageAttribute usage = typeof(ValueObjectMemberAttribute)
            .GetCustomAttributes(typeof(AttributeUsageAttribute), inherit: true)
            .Cast<AttributeUsageAttribute>()
            .Single();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(usage.ValidOn, Is.EqualTo(AttributeTargets.Property));
            Assert.That(usage.AllowMultiple, Is.False);
        }
    }
}
