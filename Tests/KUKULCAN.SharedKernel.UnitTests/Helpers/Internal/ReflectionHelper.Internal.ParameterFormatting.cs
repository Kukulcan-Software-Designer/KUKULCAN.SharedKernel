using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Provides formatting helpers for reflection parameter collections.
/// </summary>
internal static class ReflectionParameterFormattingHelper
{
    #region Parameter lists

    /// <summary>
    /// Builds the canonical representation of a parameter list.
    /// </summary>
    public static string BuildParameterList(
        IReadOnlyCollection<ParameterInfo> parameters)
    {
        ArgumentNullException.ThrowIfNull(parameters);

        if (parameters.Count == 0)
            return string.Empty;

        return string.Join(
            "|",
            parameters.Select(BuildParameterSignature));
    }

    /// <summary>
    /// Builds the canonical representation of a parameter type list.
    /// </summary>
    public static string BuildParameterTypeList(
        IReadOnlyCollection<Type> parameterTypes)
    {
        ArgumentNullException.ThrowIfNull(parameterTypes);

        if (parameterTypes.Count == 0)
            return string.Empty;

        return string.Join(
            "|",
            parameterTypes.Select(
                ReflectionTypeNameHelper.GetCanonicalTypeName));
    }

    #endregion

    #region Individual parameters

    /// <summary>
    /// Builds the canonical representation of a parameter.
    /// </summary>
    public static string BuildParameterSignature(
        ParameterInfo parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        var builder = new StringBuilder();

        if (parameter.IsIn)
            builder.Append("in ");

        if (parameter.IsOut)
            builder.Append("out ");

        if (parameter.ParameterType.IsByRef && !parameter.IsOut)
            builder.Append("ref ");

        builder.Append(
            ReflectionTypeNameHelper.GetCanonicalTypeName(
                GetUnderlyingParameterType(parameter)));

        return builder.ToString();
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Gets the underlying parameter type.
    /// </summary>
    public static Type GetUnderlyingParameterType(
        ParameterInfo parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        if (parameter.ParameterType.IsByRef)
            return parameter.ParameterType.GetElementType()!;

        return parameter.ParameterType;
    }

    /// <summary>
    /// Determines whether the parameter is passed by reference.
    /// </summary>
    public static bool IsByReference(
        ParameterInfo parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        return parameter.ParameterType.IsByRef;
    }

    /// <summary>
    /// Determines whether the parameter is an input parameter.
    /// </summary>
    public static bool IsInput(
        ParameterInfo parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        return parameter.IsIn;
    }

    /// <summary>
    /// Determines whether the parameter is an output parameter.
    /// </summary>
    public static bool IsOutput(
        ParameterInfo parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        return parameter.IsOut;
    }

    /// <summary>
    /// Determines whether the parameter is optional.
    /// </summary>
    public static bool IsOptional(
        ParameterInfo parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        return parameter.IsOptional;
    }

    /// <summary>
    /// Determines whether the parameter defines a default value.
    /// </summary>
    public static bool HasDefaultValue(
        ParameterInfo parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter);

        return parameter.HasDefaultValue;
    }

    #endregion
}
