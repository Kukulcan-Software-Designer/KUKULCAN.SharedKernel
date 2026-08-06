using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides constructor parameter inspection services.
/// </summary>
public partial class ReflectionHelper
{
    #region Parameter retrieval

    /// <summary>
    /// Gets every constructor parameter.
    /// </summary>
    public IReadOnlyCollection<ParameterInfo> GetParameters(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .SelectMany(c => c.GetParameters())
            .ToArray();
    }

    /// <summary>
    /// Gets every public constructor parameter.
    /// </summary>
    public IReadOnlyCollection<ParameterInfo> GetPublicParameters(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetPublicConstructors(type)
            .SelectMany(c => c.GetParameters())
            .ToArray();
    }

    /// <summary>
    /// Gets the parameters of the specified constructor.
    /// </summary>
    public IReadOnlyList<ParameterInfo> GetParameters(
        ConstructorInfo constructor)
    {
        ArgumentNullException.ThrowIfNull(constructor);

        return constructor.GetParameters();
    }

    #endregion

    #region Parameter count

    /// <summary>
    /// Gets the number of parameters of the supplied constructor.
    /// </summary>
    public int GetParameterCount(
        ConstructorInfo constructor)
    {
        ArgumentNullException.ThrowIfNull(constructor);

        return constructor.GetParameters().Length;
    }

    /// <summary>
    /// Gets the largest constructor arity.
    /// </summary>
    public int GetMaximumParameterCount(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .Select(c => c.GetParameters().Length)
            .DefaultIfEmpty()
            .Max();
    }

    /// <summary>
    /// Gets the smallest constructor arity.
    /// </summary>
    public int GetMinimumParameterCount(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .Select(c => c.GetParameters().Length)
            .DefaultIfEmpty()
            .Min();
    }

    #endregion

    #region Parameter existence

    /// <summary>
    /// Determines whether any constructor contains parameters.
    /// </summary>
    public bool HasParameters(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .Any(c => c.GetParameters().Length > 0);
    }

    /// <summary>
    /// Determines whether every constructor is parameterless.
    /// </summary>
    public bool AreAllConstructorsParameterless(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .All(c => c.GetParameters().Length == 0);
    }

    /// <summary>
    /// Determines whether every constructor contains parameters.
    /// </summary>
    public bool AreAllConstructorsParameterized(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .All(c => c.GetParameters().Length > 0);
    }

    #endregion

    #region Parameter type lookup

    /// <summary>
    /// Determines whether any constructor receives the specified parameter type.
    /// </summary>
    public bool ContainsParameter(
        Type type,
        Type parameterType)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(parameterType);

        return GetParameters(type)
            .Any(p => p.ParameterType == parameterType);
    }

    /// <summary>
    /// Determines whether any constructor receives the specified parameter type.
    /// </summary>
    public bool ContainsParameter<TParameter>(Type type)
    {
        return ContainsParameter(type, typeof(TParameter));
    }

    /// <summary>
    /// Determines whether any constructor receives a parameter assignable to the specified type.
    /// </summary>
    public bool ContainsAssignableParameter(
        Type type,
        Type parameterType)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(parameterType);

        return GetParameters(type)
            .Any(p => parameterType.IsAssignableFrom(p.ParameterType));
    }

    /// <summary>
    /// Determines whether any constructor receives a value type.
    /// </summary>
    public bool ContainsValueTypeParameter(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Any(p => p.ParameterType.IsValueType);
    }

    /// <summary>
    /// Determines whether any constructor receives a reference type.
    /// </summary>
    public bool ContainsReferenceTypeParameter(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Any(p => !p.ParameterType.IsValueType);
    }

    /// <summary>
    /// Determines whether any constructor receives an interface.
    /// </summary>
    public bool ContainsInterfaceParameter(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Any(p => p.ParameterType.IsInterface);
    }

    /// <summary>
    /// Determines whether any constructor receives an abstract class.
    /// </summary>
    public bool ContainsAbstractParameter(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Any(p =>
                p.ParameterType.IsAbstract &&
                !p.ParameterType.IsInterface);
    }

    /// <summary>
    /// Determines whether any constructor receives an enum.
    /// </summary>
    public bool ContainsEnumParameter(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Any(p => p.ParameterType.IsEnum);
    }

    /// <summary>
    /// Determines whether any constructor receives an array.
    /// </summary>
    public bool ContainsArrayParameter(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Any(p => p.ParameterType.IsArray);
    }

    /// <summary>
    /// Determines whether any constructor receives a generic parameter.
    /// </summary>
    public bool ContainsGenericParameter(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Any(p => p.ParameterType.IsGenericType);
    }

    /// <summary>
    /// Determines whether any constructor receives a nullable value type.
    /// </summary>
    public bool ContainsNullableParameter(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetParameters(type)
            .Any(p => Nullable.GetUnderlyingType(p.ParameterType) != null);
    }

    #endregion
}
