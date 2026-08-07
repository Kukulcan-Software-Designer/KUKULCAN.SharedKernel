using System;
using System.Linq;
using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides validation helpers over CLR types.
///
/// This partial class never throws assertion exceptions.
/// It only evaluates metadata and returns boolean results.
/// </summary>
public partial class ReflectionHelper
{
    #region Type

    public bool IsClass<T>() => typeof(T).IsClass;

    public bool IsAbstract<T>() => typeof(T).IsAbstract;

    public bool IsSealed<T>() => typeof(T).IsSealed;

    public bool IsStatic<T>()
    {
        Type type = typeof(T);

        return type.IsAbstract &&
               type.IsSealed;
    }

    public bool IsRecord<T>()
    {
        return typeof(T)
            .GetMethod(
                "<Clone>$",
                BindingFlags.Instance |
                BindingFlags.NonPublic)
            is not null;
    }

    #endregion

    #region Constructors

    public bool HasDefaultConstructor<T>()
    {
        return PublicConstructors<T>()
            .Any(x => x.GetParameters().Length == 0);
    }

    public bool HasPublicConstructor<T>()
    {
        return PublicConstructors<T>().Any();
    }

    public bool HasSinglePublicConstructor<T>()
    {
        return PublicConstructors<T>().Count == 1;
    }

    #endregion

    #region Properties

    public bool HasPublicProperties<T>()
    {
        return PublicProperties<T>().Any();
    }

    public bool HasPublicProperty<T>(string propertyName)
    {
        return TryFindProperty<T>(propertyName, out _);
    }

    #endregion

    #region Methods

    public bool HasPublicMethods<T>()
    {
        return PublicMethods<T>().Any();
    }

    public bool HasPublicMethod<T>(string methodName)
    {
        return TryFindMethod<T>(methodName, out _);
    }

    #endregion

    #region Fields

    public bool HasPublicFields<T>()
    {
        return PublicFields<T>().Any();
    }

    public bool HasPublicField<T>(string fieldName)
    {
        return TryFindField<T>(fieldName, out _);
    }

    public bool HasNoPublicFields<T>()
    {
        return !HasPublicFields<T>();
    }

    #endregion

    #region Immutability

    /// <summary>
    /// Determines whether every public property
    /// exposes no public setter.
    /// </summary>
    public bool IsImmutable<T>()
    {
        return PublicProperties<T>()
            .All(x =>
            {
                var setter = x.SetMethod;

                return setter is null || !setter.IsPublic;
            });
    }

    #endregion

    #region Equality

    public bool OverridesEquals<T>()
    {
        return typeof(T).GetMethod(nameof(object.Equals), [typeof(object)])?.DeclaringType != typeof(object);
    }

    public bool OverridesHashCode<T>()
    {
        return typeof(T).GetMethod(nameof(GetHashCode), Type.EmptyTypes)?.DeclaringType != typeof(object);
    }

    #endregion
}
