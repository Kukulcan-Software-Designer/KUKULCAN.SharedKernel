using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides constructor parameter metadata inspection services.
/// </summary>
public partial class ReflectionHelper
{
    #region Parameter names

    /// <summary>
    /// Gets every constructor parameter name.
    /// </summary>
    public IReadOnlyCollection<string> GetParameterNames(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Select(p => p.Name!)
            .ToArray();
    }

    /// <summary>
    /// Determines whether every constructor parameter has a valid name.
    /// </summary>
    public bool AllParametersAreNamed(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .All(p => !string.IsNullOrWhiteSpace(p.Name));
    }

    /// <summary>
    /// Determines whether constructor parameter names are unique.
    /// </summary>
    public bool ParameterNamesAreUnique(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        var names = GetParameterNames(type);

        return names.Count == names.Distinct(StringComparer.Ordinal).Count();
    }

    #endregion

    #region Optional parameters

    /// <summary>
    /// Determines whether any constructor parameter is optional.
    /// </summary>
    public bool ContainsOptionalParameter(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Any(p => p.IsOptional);
    }

    /// <summary>
    /// Determines whether every constructor parameter is required.
    /// </summary>
    public bool AllParametersAreRequired(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .All(p => !p.IsOptional);
    }

    /// <summary>
    /// Gets every optional parameter.
    /// </summary>
    public IReadOnlyCollection<ParameterInfo> GetOptionalParameters(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Where(p => p.IsOptional)
            .ToArray();
    }

    #endregion

    #region Default values

    /// <summary>
    /// Determines whether any parameter has a default value.
    /// </summary>
    public bool ContainsDefaultValue(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Any(p => p.HasDefaultValue);
    }

    /// <summary>
    /// Gets every parameter defining a default value.
    /// </summary>
    public IReadOnlyCollection<ParameterInfo> GetParametersWithDefaultValues(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Where(p => p.HasDefaultValue)
            .ToArray();
    }

    #endregion

    #region Params arrays

    /// <summary>
    /// Determines whether any constructor uses a params array.
    /// </summary>
    public bool ContainsParamsArray(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Any(p => p.IsDefined(typeof(ParamArrayAttribute), false));
    }

    /// <summary>
    /// Gets every params array parameter.
    /// </summary>
    public IReadOnlyCollection<ParameterInfo> GetParamsArrays(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Where(p => p.IsDefined(typeof(ParamArrayAttribute), false))
            .ToArray();
    }

    #endregion

    #region ByRef parameters

    /// <summary>
    /// Determines whether any parameter is passed by reference.
    /// </summary>
    public bool ContainsByRefParameter(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Any(p => p.ParameterType.IsByRef);
    }

    /// <summary>
    /// Determines whether any parameter is declared as ref.
    /// </summary>
    public bool ContainsRefParameter(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Any(p => p.ParameterType.IsByRef && !p.IsOut);
    }

    /// <summary>
    /// Determines whether any parameter is declared as out.
    /// </summary>
    public bool ContainsOutParameter(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Any(p => p.IsOut);
    }

    /// <summary>
    /// Determines whether any parameter is declared as in.
    /// </summary>
    public bool ContainsInParameter(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Any(p => p.IsIn && !p.IsOut);
    }

    #endregion
}
