using System;

namespace KUKULCAN.SharedKernel.Attributes;

/// <summary>
/// Excludes a property from ValueObject equality.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class IgnoreEqualityAttribute : Attribute
{
}
