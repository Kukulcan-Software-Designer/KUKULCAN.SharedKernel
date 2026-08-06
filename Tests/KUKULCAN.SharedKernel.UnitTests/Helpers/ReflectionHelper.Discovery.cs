using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides discovery operations over CLR types.
///
/// This partial class exposes cached reflection APIs used by the
/// testing infrastructure.
/// </summary>
public partial class ReflectionHelper
{
    #region Constructors

    /// <summary>
    /// Returns every public constructor.
    /// </summary>
    public IReadOnlyList<ConstructorInfo> PublicConstructors<T>()
    {
        return PublicConstructors(typeof(T));
    }

    /// <summary>
    /// Returns every public constructor.
    /// </summary>
    public IReadOnlyList<ConstructorInfo> PublicConstructors(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetOrAdd(
            _constructors,
            type,
            t => t.GetConstructors(PublicInstance));
    }

    #endregion

    #region Methods

    /// <summary>
    /// Returns every public instance method.
    /// </summary>
    public IReadOnlyList<MethodInfo> PublicMethods<T>()
    {
        return PublicMethods(typeof(T));
    }

    /// <summary>
    /// Returns every public instance method.
    /// </summary>
    public IReadOnlyList<MethodInfo> PublicMethods(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetOrAdd(
            _methods,
            type,
            t =>
                t.GetMethods(PublicInstance)
                 .Where(x => !x.IsSpecialName)
                 .OrderBy(x => x.Name)
                 .ToArray());
    }

    #endregion

    #region Properties

    /// <summary>
    /// Returns every public property.
    /// </summary>
    public IReadOnlyList<PropertyInfo> PublicProperties<T>()
    {
        return PublicProperties(typeof(T));
    }

    /// <summary>
    /// Returns every public property.
    /// </summary>
    public IReadOnlyList<PropertyInfo> PublicProperties(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetOrAdd(
            _properties,
            type,
            t =>
                t.GetProperties(PublicInstance)
                 .OrderBy(x => x.Name)
                 .ToArray());
    }

    #endregion

    #region Fields

    /// <summary>
    /// Returns every public field.
    /// </summary>
    public IReadOnlyList<FieldInfo> PublicFields<T>()
    {
        return PublicFields(typeof(T));
    }

    /// <summary>
    /// Returns every public field.
    /// </summary>
    public IReadOnlyList<FieldInfo> PublicFields(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetOrAdd(
            _fields,
            type,
            t =>
                t.GetFields(Public)
                 .OrderBy(x => x.Name)
                 .ToArray());
    }

    #endregion

    #region Interfaces

    /// <summary>
    /// Returns every implemented interface.
    /// </summary>
    public IReadOnlyList<Type> Interfaces<T>()
    {
        return Interfaces(typeof(T));
    }

    /// <summary>
    /// Returns every implemented interface.
    /// </summary>
    public IReadOnlyList<Type> Interfaces(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetOrAdd(
            _interfaces,
            type,
            t =>
                t.GetInterfaces()
                 .OrderBy(x => x.FullName)
                 .ToArray());
    }

    #endregion

    #region Attributes

    /// <summary>
    /// Returns every attribute declared on the type.
    /// </summary>
    public IReadOnlyList<Attribute> Attributes<T>()
    {
        return Attributes(typeof(T));
    }

    /// <summary>
    /// Returns every attribute declared on the type.
    /// </summary>
    public IReadOnlyList<Attribute> Attributes(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetOrAdd(
            _attributes,
            type,
            t =>
                t.GetCustomAttributes()
                 .Cast<Attribute>()
                 .OrderBy(x => x.GetType().FullName)
                 .ToArray());
    }

    #endregion

    #region Nested Types

    /// <summary>
    /// Returns every nested type.
    /// </summary>
    public IReadOnlyList<Type> NestedTypes<T>()
    {
        return NestedTypes(typeof(T));
    }

    /// <summary>
    /// Returns every nested type.
    /// </summary>
    public IReadOnlyList<Type> NestedTypes(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetOrAdd(
            _nestedTypes,
            type,
            t =>
                t.GetNestedTypes(All)
                 .OrderBy(x => x.FullName)
                 .ToArray());
    }

    #endregion
}
