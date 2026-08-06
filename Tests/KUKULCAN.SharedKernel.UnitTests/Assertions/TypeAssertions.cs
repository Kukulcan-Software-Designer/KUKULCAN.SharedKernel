namespace KUKULCAN.SharedKernel.UnitTests.Assertions;

/// <summary>
/// Entry point for CLR type assertions.
///
/// Provides a fluent API to validate the public contract,
/// architecture and implementation details of a type.
/// </summary>
public static class TypeAssertions
{
    /// <summary>
    /// Starts a fluent assertion chain for the specified type.
    /// </summary>
    public static TypeAssertionBuilder For<T>()
    {
        return new TypeAssertionBuilder(typeof(T));
    }

    /// <summary>
    /// Starts a fluent assertion chain for the specified type.
    /// </summary>
    public static TypeAssertionBuilder For(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return new TypeAssertionBuilder(type);
    }
}
