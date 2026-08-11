namespace KUKULCAN.SharedKernel.Attributes;

/// <summary>
/// Marks a property as participating in ValueObject equality.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ValueObjectMemberAttribute : Attribute
{
}
