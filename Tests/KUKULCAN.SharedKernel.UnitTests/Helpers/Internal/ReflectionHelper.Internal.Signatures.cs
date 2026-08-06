using System;
using System.Collections.Generic;
using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Provides canonical reflection signature generation.
/// </summary>
internal static class ReflectionSignatureHelper
{
    #region Constructors

    /// <summary>
    /// Builds the canonical signature of a constructor.
    /// </summary>
    public static string BuildConstructorSignature(
        ConstructorInfo constructor)
    {
        ArgumentNullException.ThrowIfNull(constructor);

        return ReflectionParameterFormattingHelper.BuildParameterList(
            constructor.GetParameters());
    }

    /// <summary>
    /// Builds the canonical signature from parameter types.
    /// </summary>
    public static string BuildConstructorSignature(
        IReadOnlyCollection<Type> parameterTypes)
    {
        ArgumentNullException.ThrowIfNull(parameterTypes);

        return ReflectionParameterFormattingHelper.BuildParameterTypeList(
            parameterTypes);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Builds the canonical signature of a method.
    /// </summary>
    public static string BuildMethodSignature(
        MethodInfo method)
    {
        ArgumentNullException.ThrowIfNull(method);

        return string.Concat(
            ReflectionTypeNameHelper.GetCanonicalTypeName(method.ReturnType),
            "|",
            method.Name,
            "|",
            ReflectionParameterFormattingHelper.BuildParameterList(
                method.GetParameters()));
    }

    #endregion

    #region Properties

    /// <summary>
    /// Builds the canonical signature of a property.
    /// </summary>
    public static string BuildPropertySignature(
        PropertyInfo property)
    {
        ArgumentNullException.ThrowIfNull(property);

        return string.Concat(
            ReflectionTypeNameHelper.GetCanonicalTypeName(property.PropertyType),
            "|",
            property.Name);
    }

    #endregion

    #region Events

    /// <summary>
    /// Builds the canonical signature of an event.
    /// </summary>
    public static string BuildEventSignature(
        EventInfo eventInfo)
    {
        ArgumentNullException.ThrowIfNull(eventInfo);

        return string.Concat(
            ReflectionTypeNameHelper.GetCanonicalTypeName(eventInfo.EventHandlerType!),
            "|",
            eventInfo.Name);
    }

    #endregion

    #region Fields

    /// <summary>
    /// Builds the canonical signature of a field.
    /// </summary>
    public static string BuildFieldSignature(
        FieldInfo field)
    {
        ArgumentNullException.ThrowIfNull(field);

        return string.Concat(
            ReflectionTypeNameHelper.GetCanonicalTypeName(field.FieldType),
            "|",
            field.Name);
    }

    #endregion

    #region Delegates

    /// <summary>
    /// Builds the canonical signature of a delegate.
    /// </summary>
    public static string BuildDelegateSignature(
        Type delegateType)
    {
        ArgumentNullException.ThrowIfNull(delegateType);

        if (!typeof(Delegate).IsAssignableFrom(delegateType))
            throw new ArgumentException(
                "The supplied type is not a delegate.",
                nameof(delegateType));

        var invoke = delegateType.GetMethod(nameof(Action.Invoke));

        if (invoke is null)
            throw new InvalidOperationException(
                $"Delegate '{delegateType.FullName}' does not expose Invoke().");

        return BuildMethodSignature(invoke);
    }

    #endregion

    #region Types

    /// <summary>
    /// Builds the canonical signature of a type.
    /// </summary>
    public static string BuildTypeSignature(
        Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return ReflectionTypeNameHelper.GetCanonicalTypeName(type);
    }

    #endregion
}
