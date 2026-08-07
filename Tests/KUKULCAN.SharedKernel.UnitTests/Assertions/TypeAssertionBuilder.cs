using System;
using KUKULCAN.SharedKernel.UnitTests.Helpers;

namespace KUKULCAN.SharedKernel.UnitTests.Assertions;

/// <summary>
/// Fluent builder used to validate a CLR type.
/// </summary>
public partial class TypeAssertionBuilder
{
    private readonly ReflectionHelper _reflection;

    /// <summary>
    /// Gets the reflected type.
    /// </summary>
    public Type Type { get; }

    /// <summary>
    /// Initializes a new assertion builder.
    /// </summary>
    internal TypeAssertionBuilder(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        Type = type;
        _reflection = new ReflectionHelper();
    }

    /// <summary>
    /// Gets the reflection service.
    /// </summary>
    protected ReflectionHelper Reflection => _reflection;
}
