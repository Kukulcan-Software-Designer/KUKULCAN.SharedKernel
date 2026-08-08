namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Provides canonical formatting services for <see cref="Type"/> instances.
/// </summary>
internal static class ReflectionTypeNameHelper
{
    #region Public API

    /// <summary>
    /// Returns the canonical name of the specified type.
    /// </summary>
    public static string GetCanonicalTypeName(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        if (type.IsArray)
            return FormatArray(type);

        if (type.IsPointer)
            return FormatPointer(type);

        if (type.IsByRef)
            return FormatByRef(type);

        if (type.IsGenericParameter)
            return type.Name;

        if (type.IsGenericType)
            return FormatGeneric(type);

        return type.FullName ?? type.Name;
    }

    #endregion

    #region Arrays

    private static string FormatArray(Type type)
    {
        return $"{GetCanonicalTypeName(type.GetElementType()!)}[]";
    }

    #endregion

    #region ByRef

    private static string FormatByRef(Type type)
    {
        return $"{GetCanonicalTypeName(type.GetElementType()!)}&";
    }

    #endregion

    #region Pointer

    private static string FormatPointer(Type type)
    {
        return $"{GetCanonicalTypeName(type.GetElementType()!)}*";
    }

    #endregion

    #region Generic

    private static string FormatGeneric(Type type)
    {
        var genericDefinition = type.GetGenericTypeDefinition();

        var genericArguments =
            string.Join(
                ",",
                type.GetGenericArguments()
                    .Select(GetCanonicalTypeName));

        return $"{genericDefinition.FullName}[{genericArguments}]";
    }

    #endregion
}
