using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides constructor search services.
/// </summary>
public partial class ReflectionHelper
{
    #region Constructor search

    /// <summary>
    /// Finds the constructor matching the specified signature.
    /// </summary>
    public ConstructorInfo? FindConstructor(
        Type type,
        params Type[] parameterTypes)
    {
        ArgumentNullException.ThrowIfNull(type);

        parameterTypes ??= Array.Empty<Type>();

        return GetConstructors(type)
            .SingleOrDefault(c => MatchConstructorSignature(c, parameterTypes));
    }

    /// <summary>
    /// Attempts to find a constructor matching the supplied signature.
    /// </summary>
    public bool TryFindConstructor(
        Type type,
        out ConstructorInfo? constructor,
        params Type[] parameterTypes)
    {
        ArgumentNullException.ThrowIfNull(type);

        constructor = FindConstructor(type, parameterTypes);

        return constructor is not null;
    }

    /// <summary>
    /// Determines whether the specified constructor exists.
    /// </summary>
    public bool ContainsConstructor(
        Type type,
        params Type[] parameterTypes)
    {
        ArgumentNullException.ThrowIfNull(type);

        return FindConstructor(type, parameterTypes) is not null;
    }

    /// <summary>
    /// Finds every constructor matching the supplied predicate.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> FindConstructors(
        Type type,
        Func<ConstructorInfo, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(predicate);

        return GetConstructors(type)
            .Where(predicate)
            .ToArray();
    }

    /// <summary>
    /// Finds every public constructor matching the supplied predicate.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> FindPublicConstructors(
        Type type,
        Func<ConstructorInfo, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(predicate);

        return GetPublicConstructors(type)
            .Where(predicate)
            .ToArray();
    }

    /// <summary>
    /// Finds constructors with the specified parameter count.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> FindConstructorsByParameterCount(
        Type type,
        int parameterCount)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .Where(c => c.GetParameters().Length == parameterCount)
            .ToArray();
    }

    /// <summary>
    /// Finds constructors having at least the specified number of parameters.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> FindConstructorsWithMinimumParameters(
        Type type,
        int minimumParameterCount)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .Where(c => c.GetParameters().Length >= minimumParameterCount)
            .ToArray();
    }

    /// <summary>
    /// Finds constructors having at most the specified number of parameters.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> FindConstructorsWithMaximumParameters(
        Type type,
        int maximumParameterCount)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .Where(c => c.GetParameters().Length <= maximumParameterCount)
            .ToArray();
    }

    /// <summary>
    /// Finds constructors receiving the specified parameter type.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> FindConstructorsReceiving(
        Type type,
        Type parameterType)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(parameterType);

        return GetConstructors(type)
            .Where(c =>
                c.GetParameters()
                 .Any(p => p.ParameterType == parameterType))
            .ToArray();
    }

    /// <summary>
    /// Finds constructors receiving a parameter assignable to the specified type.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> FindConstructorsReceivingAssignableTo(
        Type type,
        Type parameterType)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(parameterType);

        return GetConstructors(type)
            .Where(c =>
                c.GetParameters()
                 .Any(p => parameterType.IsAssignableFrom(p.ParameterType)))
            .ToArray();
    }

    /// <summary>
    /// Finds constructors matching the supplied parameter predicate.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> FindConstructorsReceiving(
        Type type,
        Func<ParameterInfo, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(predicate);

        return GetConstructors(type)
            .Where(c =>
                c.GetParameters()
                 .Any(predicate))
            .ToArray();
    }

    #endregion

    #region Internal helpers

    /// <summary>
    /// Determines whether a constructor matches the supplied signature.
    /// </summary>
    internal bool MatchConstructorSignature(
        ConstructorInfo constructor,
        IReadOnlyList<Type> signature)
    {
        ParameterInfo[] parameters = constructor.GetParameters();

        if (parameters.Length != signature.Count)
            return false;

        return !parameters.Where((t, i) => t.ParameterType != signature[i]).Any();
    }

    #endregion
}
