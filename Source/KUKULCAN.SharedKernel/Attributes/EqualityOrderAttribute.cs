namespace KUKULCAN.SharedKernel.Attributes;

/// <summary>
/// Specifies the comparison order for a ValueObject member.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class EqualityOrderAttribute(int order) : Attribute
{
    public int Order { get; } = order;
}
