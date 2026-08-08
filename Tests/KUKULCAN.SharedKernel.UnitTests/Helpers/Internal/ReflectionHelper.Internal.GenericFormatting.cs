using System.Text;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Provides helper methods for formatting and analyzing generic types.
/// </summary>
internal static class ReflectionGenericFormattingHelper
{
    #region Detection

    /// <summary>
    /// Determines whether the supplied type is generic.
    /// </summary>
    public static bool IsGeneric(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type.IsGenericType;
    }

    /// <summary>
    /// Determines whether the supplied type is an open generic.
    /// </summary>
    public static bool IsOpenGeneric(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type.IsGenericTypeDefinition;
    }

    /// <summary>
    /// Determines whether the supplied type is a closed generic.
    /// </summary>
    public static bool IsClosedGeneric(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type is { IsGenericType: true, ContainsGenericParameters: false };
    }

    /// <summary>
    /// Determines whether the supplied type contains generic parameters.
    /// </summary>
    public static bool ContainsGenericParameters(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type.ContainsGenericParameters;
    }

    #endregion

    #region Generic definition

    /// <summary>
    /// Gets the generic type definition.
    /// </summary>
    public static Type GetGenericDefinition(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        if (!type.IsGenericType)
            throw new InvalidOperationException(
                $"'{type.FullName}' is not a generic type.");

        return type.GetGenericTypeDefinition();
    }

    /// <summary>
    /// Gets every generic argument.
    /// </summary>
    public static IReadOnlyCollection<Type> GetGenericArguments(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type.GetGenericArguments();
    }

    /// <summary>
    /// Gets the number of generic arguments.
    /// </summary>
    public static int GetGenericArity(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type.GetGenericArguments().Length;
    }

    #endregion

    #region Formatting

    /// <summary>
    /// Formats a generic type using canonical notation.
    /// </summary>
    public static string FormatGenericType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        if (!type.IsGenericType)
            return ReflectionTypeNameHelper.GetCanonicalTypeName(type);

        var definition = type.GetGenericTypeDefinition();

        var builder = new StringBuilder();

        builder.Append(definition.FullName);

        builder.Append('<');

        builder.Append(
            string.Join(
                ", ",
                type.GetGenericArguments()
                    .Select(ReflectionTypeNameHelper.GetCanonicalTypeName)));

        builder.Append('>');

        return builder.ToString();
    }

    #endregion

    #region Constraints

    /// <summary>
    /// Gets the constraints of a generic parameter.
    /// </summary>
    public static IReadOnlyCollection<Type> GetConstraints(Type genericParameter)
    {
        ArgumentNullException.ThrowIfNull(genericParameter);

        if (!genericParameter.IsGenericParameter)
            return Array.Empty<Type>();

        return genericParameter.GetGenericParameterConstraints();
    }

    /// <summary>
    /// Determines whether a generic parameter has constraints.
    /// </summary>
    public static bool HasConstraints(Type genericParameter)
    {
        ArgumentNullException.ThrowIfNull(genericParameter);

        if (!genericParameter.IsGenericParameter)
            return false;

        return genericParameter
            .GetGenericParameterConstraints()
            .Length > 0;
    }

    #endregion

    #region Assignability

    /// <summary>
    /// Determines whether a type implements the specified open generic interface.
    /// </summary>
    public static bool ImplementsOpenGeneric(
        Type type,
        Type openGeneric)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(openGeneric);

        return type.GetInterfaces()
            .Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == openGeneric);
    }

    /// <summary>
    /// Determines whether a type inherits from the specified open generic type.
    /// </summary>
    public static bool InheritsOpenGeneric(
        Type type,
        Type openGeneric)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(openGeneric);

        while (type != null && type != typeof(object))
        {
            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == openGeneric)
            {
                return true;
            }

            type = type.BaseType!;
        }

        return false;
    }

    #endregion
}
