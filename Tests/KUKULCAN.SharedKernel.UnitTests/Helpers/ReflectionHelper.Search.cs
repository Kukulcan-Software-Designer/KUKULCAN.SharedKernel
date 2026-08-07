using System;
using System.Linq;
using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides search operations over reflected members.
/// </summary>
public partial class ReflectionHelper
{
    #region Methods

    public bool TryFindMethod<T>(
        string methodName,
        out MethodInfo? method)
    {
        return TryFindMethod(typeof(T), methodName, out method);
    }

    public bool TryFindMethod(
        Type type,
        string methodName,
        out MethodInfo? method)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentException.ThrowIfNullOrWhiteSpace(methodName);

        method = PublicMethods(type)
            .FirstOrDefault(x => x.Name == methodName);

        return method is not null;
    }

    public MethodInfo GetRequiredMethod<T>(string methodName)
    {
        if (TryFindMethod<T>(methodName, out var method))
            return method!;

        throw new MissingMethodException(
            $"{typeof(T).FullName} does not contain a public method named '{methodName}'.");
    }

    #endregion

    #region Properties

    public bool TryFindProperty<T>(
        string propertyName,
        out PropertyInfo? property)
    {
        return TryFindProperty(typeof(T), propertyName, out property);
    }

    public bool TryFindProperty(
        Type type,
        string propertyName,
        out PropertyInfo? property)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        property = PublicProperties(type)
            .FirstOrDefault(x => x.Name == propertyName);

        return property is not null;
    }

    public PropertyInfo GetRequiredProperty<T>(string propertyName)
    {
        if (TryFindProperty<T>(propertyName, out var property))
            return property!;

        throw new MissingMemberException(
            $"{typeof(T).FullName} does not contain a public property named '{propertyName}'.");
    }

    #endregion

    #region Fields

    public bool TryFindField<T>(
        string fieldName,
        out FieldInfo? field)
    {
        return TryFindField(typeof(T), fieldName, out field);
    }

    public bool TryFindField(
        Type type,
        string fieldName,
        out FieldInfo? field)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentException.ThrowIfNullOrWhiteSpace(fieldName);

        field = PublicFields(type)
            .FirstOrDefault(x => x.Name == fieldName);

        return field is not null;
    }

    public FieldInfo GetRequiredField<T>(string fieldName)
    {
        if (TryFindField<T>(fieldName, out var field))
            return field!;

        throw new MissingFieldException(
            $"{typeof(T).FullName} does not contain a public field named '{fieldName}'.");
    }

    #endregion

    #region Constructors

    public bool TryFindDefaultConstructor<T>(
        out ConstructorInfo? constructor)
    {
        constructor = PublicConstructors<T>()
            .FirstOrDefault(x => x.GetParameters().Length == 0);

        return constructor is not null;
    }

    public ConstructorInfo GetRequiredDefaultConstructor<T>()
    {
        if (TryFindDefaultConstructor<T>(out var constructor))
            return constructor!;

        throw new MissingMethodException(
            $"{typeof(T).FullName} does not expose a public parameterless constructor.");
    }

    #endregion

    #region Interfaces

    public bool Implements<TImplementation, TContract>()
    {
        return typeof(TContract)
            .IsAssignableFrom(typeof(TImplementation));
    }

    public bool Implements(Type implementation, Type contract)
    {
        ArgumentNullException.ThrowIfNull(implementation);
        ArgumentNullException.ThrowIfNull(contract);

        return contract.IsAssignableFrom(implementation);
    }

    #endregion

    #region Attributes

    public bool HasAttribute<TAttribute, TObject>()
        where TAttribute : Attribute
    {
        return typeof(TObject)
            .GetCustomAttribute<TAttribute>() is not null;
    }

    public bool HasAttribute<TAttribute>(
        MemberInfo member)
        where TAttribute : Attribute
    {
        ArgumentNullException.ThrowIfNull(member);

        return member.GetCustomAttribute<TAttribute>() is not null;
    }

    #endregion
}
