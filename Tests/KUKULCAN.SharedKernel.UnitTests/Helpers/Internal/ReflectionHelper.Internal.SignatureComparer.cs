using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Provides comparison services for reflection signatures.
/// </summary>
internal static class ReflectionSignatureComparer
{
    #region Constructor comparison

    /// <summary>
    /// Determines whether two constructors expose the same signature.
    /// </summary>
    public static bool AreEquivalent(
        ConstructorInfo first,
        ConstructorInfo second)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);

        return ReflectionSignatureHelper.BuildConstructorSignature(first) ==
               ReflectionSignatureHelper.BuildConstructorSignature(second);
    }

    /// <summary>
    /// Determines whether the constructor matches the specified signature.
    /// </summary>
    public static bool Matches(
        ConstructorInfo constructor,
        IReadOnlyCollection<Type> parameterTypes)
    {
        ArgumentNullException.ThrowIfNull(constructor);
        ArgumentNullException.ThrowIfNull(parameterTypes);

        return ReflectionSignatureHelper.BuildConstructorSignature(constructor) ==
               ReflectionSignatureHelper.BuildConstructorSignature(parameterTypes);
    }

    #endregion

    #region Method comparison

    /// <summary>
    /// Determines whether two methods expose the same signature.
    /// </summary>
    public static bool AreEquivalent(
        MethodInfo first,
        MethodInfo second)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);

        return ReflectionSignatureHelper.BuildMethodSignature(first) ==
               ReflectionSignatureHelper.BuildMethodSignature(second);
    }

    #endregion

    #region Property comparison

    /// <summary>
    /// Determines whether two properties expose the same signature.
    /// </summary>
    public static bool AreEquivalent(
        PropertyInfo first,
        PropertyInfo second)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);

        return ReflectionSignatureHelper.BuildPropertySignature(first) ==
               ReflectionSignatureHelper.BuildPropertySignature(second);
    }

    #endregion

    #region Event comparison

    /// <summary>
    /// Determines whether two events expose the same signature.
    /// </summary>
    public static bool AreEquivalent(
        EventInfo first,
        EventInfo second)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);

        return ReflectionSignatureHelper.BuildEventSignature(first) ==
               ReflectionSignatureHelper.BuildEventSignature(second);
    }

    #endregion

    #region Field comparison

    /// <summary>
    /// Determines whether two fields expose the same signature.
    /// </summary>
    public static bool AreEquivalent(
        FieldInfo first,
        FieldInfo second)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);

        return ReflectionSignatureHelper.BuildFieldSignature(first) ==
               ReflectionSignatureHelper.BuildFieldSignature(second);
    }

    #endregion

    #region Generic comparison

    /// <summary>
    /// Determines whether two collections of constructors are equivalent.
    /// </summary>
    public static bool AreEquivalent(
        IEnumerable<ConstructorInfo> first,
        IEnumerable<ConstructorInfo> second)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);

        var left = first
            .Select(ReflectionSignatureHelper.BuildConstructorSignature)
            .OrderBy(s => s);

        var right = second
            .Select(ReflectionSignatureHelper.BuildConstructorSignature)
            .OrderBy(s => s);

        return left.SequenceEqual(right);
    }

    /// <summary>
    /// Determines whether two collections of methods are equivalent.
    /// </summary>
    public static bool AreEquivalent(
        IEnumerable<MethodInfo> first,
        IEnumerable<MethodInfo> second)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);

        var left = first
            .Select(ReflectionSignatureHelper.BuildMethodSignature)
            .OrderBy(s => s);

        var right = second
            .Select(ReflectionSignatureHelper.BuildMethodSignature)
            .OrderBy(s => s);

        return left.SequenceEqual(right);
    }

    #endregion

    #region Signature equality

    /// <summary>
    /// Determines whether two signatures are equal.
    /// </summary>
    public static bool AreEqual(
        string first,
        string second)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);

        return StringComparer.Ordinal.Equals(first, second);
    }

    /// <summary>
    /// Determines whether two signatures are equal ignoring case.
    /// </summary>
    public static bool AreEqualIgnoreCase(
        string first,
        string second)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);

        return StringComparer.OrdinalIgnoreCase.Equals(first, second);
    }

    #endregion
}
