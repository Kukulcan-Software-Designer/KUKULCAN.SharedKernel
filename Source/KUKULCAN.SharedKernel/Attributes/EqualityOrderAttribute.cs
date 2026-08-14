namespace KUKULCAN.SharedKernel.Attributes;

/// <summary>
/// Specifies the comparison order for a ValueObject member.
/// </summary>
/// <param name="order">
/// Zero-based or application-defined comparison order for the member.
/// </param>
[AttributeUsage(AttributeTargets.Property)]
public sealed class EqualityOrderAttribute(int order) : Attribute
{
    /// <summary>
    /// Gets the order used when comparing the associated ValueObject member.
    /// </summary>
    public int Order { get; } = order;
}
