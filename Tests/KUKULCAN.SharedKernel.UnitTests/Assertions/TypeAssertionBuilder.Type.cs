using FluentAssertions;

namespace KUKULCAN.SharedKernel.UnitTests.Assertions;

/// <summary>
/// Provides assertions related to the CLR type kind.
/// </summary>
public sealed partial class TypeAssertionBuilder
{
    #region Class

    /// <summary>
    /// Asserts that the current type is a class.
    /// </summary>
    public TypeAssertionBuilder BeClass()
    {
        Type.IsClass
            .Should()
            .BeTrue($"{Type.FullName} should be a class.");

        return this;
    }

    /// <summary>
    /// Asserts that the current type is not a class.
    /// </summary>
    public TypeAssertionBuilder NotBeClass()
    {
        Type.IsClass
            .Should()
            .BeFalse($"{Type.FullName} should not be a class.");

        return this;
    }

    #endregion

    #region Struct

    /// <summary>
    /// Asserts that the current type is a struct.
    /// </summary>
    public TypeAssertionBuilder BeStruct()
    {
        Type.IsValueType
            .Should()
            .BeTrue($"{Type.FullName} should be a struct.");

        Type.IsEnum
            .Should()
            .BeFalse($"{Type.FullName} is an enum, not a struct.");

        return this;
    }

    /// <summary>
    /// Asserts that the current type is not a struct.
    /// </summary>
    public TypeAssertionBuilder NotBeStruct()
    {
        bool isStruct = Type is { IsValueType: true, IsEnum: false };

        isStruct
            .Should()
            .BeFalse($"{Type.FullName} should not be a struct.");

        return this;
    }

    #endregion

    #region Interface

    /// <summary>
    /// Asserts that the current type is an interface.
    /// </summary>
    public TypeAssertionBuilder BeInterface()
    {
        Type.IsInterface
            .Should()
            .BeTrue($"{Type.FullName} should be an interface.");

        return this;
    }

    /// <summary>
    /// Asserts that the current type is not an interface.
    /// </summary>
    public TypeAssertionBuilder NotBeInterface()
    {
        Type.IsInterface
            .Should()
            .BeFalse($"{Type.FullName} should not be an interface.");

        return this;
    }

    #endregion

    #region Enum

    /// <summary>
    /// Asserts that the current type is an enumeration.
    /// </summary>
    public TypeAssertionBuilder BeEnum()
    {
        Type.IsEnum
            .Should()
            .BeTrue($"{Type.FullName} should be an enum.");

        return this;
    }

    /// <summary>
    /// Asserts that the current type is not an enumeration.
    /// </summary>
    public TypeAssertionBuilder NotBeEnum()
    {
        Type.IsEnum
            .Should()
            .BeFalse($"{Type.FullName} should not be an enum.");

        return this;
    }

    #endregion

    #region Delegate

    /// <summary>
    /// Asserts that the current type is a delegate.
    /// </summary>
    public TypeAssertionBuilder BeDelegate()
    {
        typeof(MulticastDelegate)
            .IsAssignableFrom(Type.BaseType)
            .Should()
            .BeTrue($"{Type.FullName} should be a delegate.");

        return this;
    }

    /// <summary>
    /// Asserts that the current type is not a delegate.
    /// </summary>
    public TypeAssertionBuilder NotBeDelegate()
    {
        var isDelegate =
            Type.BaseType != null &&
            typeof(MulticastDelegate)
                .IsAssignableFrom(Type.BaseType);

        isDelegate
            .Should()
            .BeFalse($"{Type.FullName} should not be a delegate.");

        return this;
    }

    #endregion

    #region Attribute

    /// <summary>
    /// Asserts that the current type derives from <see cref="Attribute"/>.
    /// </summary>
    public TypeAssertionBuilder BeAttribute()
    {
        typeof(Attribute)
            .IsAssignableFrom(Type)
            .Should()
            .BeTrue($"{Type.FullName} should derive from Attribute.");

        return this;
    }

    /// <summary>
    /// Asserts that the current type does not derive from <see cref="Attribute"/>.
    /// </summary>
    public TypeAssertionBuilder NotBeAttribute()
    {
        typeof(Attribute)
            .IsAssignableFrom(Type)
            .Should()
            .BeFalse($"{Type.FullName} should not derive from Attribute.");

        return this;
    }

    #endregion

    #region Exception

    /// <summary>
    /// Asserts that the current type derives from <see cref="Exception"/>.
    /// </summary>
    public TypeAssertionBuilder BeException()
    {
        typeof(Exception)
            .IsAssignableFrom(Type)
            .Should()
            .BeTrue($"{Type.FullName} should derive from Exception.");

        return this;
    }

    /// <summary>
    /// Asserts that the current type does not derive from <see cref="Exception"/>.
    /// </summary>
    public TypeAssertionBuilder NotBeException()
    {
        typeof(Exception)
            .IsAssignableFrom(Type)
            .Should()
            .BeFalse($"{Type.FullName} should not derive from Exception.");

        return this;
    }

    #endregion

    #region Visibility

    /// <summary>
    /// Verifies that the type is public.
    /// </summary>
    public TypeAssertionBuilder BePublic()
    {
        (Type.IsPublic || Type.IsNestedPublic)
            .Should()
            .BeTrue($"{Type.FullName} should be public.");

        return this;
    }

    /// <summary>
    /// Verifies that the type is internal.
    /// </summary>
    public TypeAssertionBuilder BeInternal()
    {
        (Type.IsNotPublic || Type.IsNestedAssembly)
            .Should()
            .BeTrue($"{Type.FullName} should be internal.");

        return this;
    }

    /// <summary>
    /// Verifies that the type is not public.
    /// </summary>
    public TypeAssertionBuilder NotBePublic()
    {
        (Type.IsPublic || Type.IsNestedPublic)
            .Should()
            .BeFalse($"{Type.FullName} should not be public.");

        return this;
    }

    /// <summary>
    /// Verifies that the type is not internal.
    /// </summary>
    public TypeAssertionBuilder NotBeInternal()
    {
        (Type.IsNotPublic || Type.IsNestedAssembly)
            .Should()
            .BeFalse($"{Type.FullName} should not be internal.");

        return this;
    }

    #endregion

    #region Nested

    /// <summary>
    /// Verifies that the type is nested.
    /// </summary>
    public TypeAssertionBuilder BeNested()
    {
        Type.IsNested
            .Should()
            .BeTrue($"{Type.FullName} should be nested.");

        return this;
    }

    /// <summary>
    /// Verifies that the type is not nested.
    /// </summary>
    public TypeAssertionBuilder NotBeNested()
    {
        Type.IsNested
            .Should()
            .BeFalse($"{Type.FullName} should not be nested.");

        return this;
    }

    /// <summary>
    /// Verifies that the type is nested public.
    /// </summary>
    public TypeAssertionBuilder BeNestedPublic()
    {
        Type.IsNestedPublic
            .Should()
            .BeTrue($"{Type.FullName} should be nested public.");

        return this;
    }

    /// <summary>
    /// Verifies that the type is nested private.
    /// </summary>
    public TypeAssertionBuilder BeNestedPrivate()
    {
        Type.IsNestedPrivate
            .Should()
            .BeTrue($"{Type.FullName} should be nested private.");

        return this;
    }

    /// <summary>
    /// Verifies that the type is nested internal.
    /// </summary>
    public TypeAssertionBuilder BeNestedInternal()
    {
        Type.IsNestedAssembly
            .Should()
            .BeTrue($"{Type.FullName} should be nested internal.");

        return this;
    }

    #endregion

    #region Abstract

    /// <summary>
    /// Verifies that the type is abstract.
    /// </summary>
    public TypeAssertionBuilder BeAbstract()
    {
        Type.IsAbstract
            .Should()
            .BeTrue($"{Type.FullName} should be abstract.");

        return this;
    }

    /// <summary>
    /// Verifies that the type is not abstract.
    /// </summary>
    public TypeAssertionBuilder NotBeAbstract()
    {
        Type.IsAbstract
            .Should()
            .BeFalse($"{Type.FullName} should not be abstract.");

        return this;
    }

    #endregion

    #region Sealed

    /// <summary>
    /// Verifies that the type is sealed.
    /// </summary>
    public TypeAssertionBuilder BeSealed()
    {
        Type.IsSealed
            .Should()
            .BeTrue($"{Type.FullName} should be sealed.");

        return this;
    }

    /// <summary>
    /// Verifies that the type is not sealed.
    /// </summary>
    public TypeAssertionBuilder NotBeSealed()
    {
        Type.IsSealed
            .Should()
            .BeFalse($"{Type.FullName} should not be sealed.");

        return this;
    }

    #endregion

    #region Static

    /// <summary>
    /// Verifies that the type is static.
    /// </summary>
    public TypeAssertionBuilder BeStatic()
    {
        (Type is { IsAbstract: true, IsSealed: true })
            .Should()
            .BeTrue($"{Type.FullName} should be static.");

        return this;
    }

    /// <summary>
    /// Verifies that the type is not static.
    /// </summary>
    public TypeAssertionBuilder NotBeStatic()
    {
        (Type is { IsAbstract: true, IsSealed: true })
            .Should()
            .BeFalse($"{Type.FullName} should not be static.");

        return this;
    }

    #endregion

    #region Generic

    /// <summary>
    /// Verifies that the type is generic.
    /// </summary>
    public TypeAssertionBuilder BeGeneric()
    {
        Type.IsGenericType
            .Should()
            .BeTrue($"{Type.FullName} should be generic.");

        return this;
    }

    /// <summary>
    /// Verifies that the type is not generic.
    /// </summary>
    public TypeAssertionBuilder NotBeGeneric()
    {
        Type.IsGenericType
            .Should()
            .BeFalse($"{Type.FullName} should not be generic.");

        return this;
    }

    /// <summary>
    /// Verifies that the type is an open generic type.
    /// </summary>
    public TypeAssertionBuilder BeOpenGeneric()
    {
        Type.ContainsGenericParameters
            .Should()
            .BeTrue($"{Type.FullName} should be an open generic type.");

        return this;
    }

    /// <summary>
    /// Verifies that the type is a closed generic type.
    /// </summary>
    public TypeAssertionBuilder BeClosedGeneric()
    {
        (Type is { IsGenericType: true, ContainsGenericParameters: false })
            .Should()
            .BeTrue($"{Type.FullName} should be a closed generic type.");

        return this;
    }

    /// <summary>
    /// Verifies that the type is a generic type definition.
    /// </summary>
    public TypeAssertionBuilder BeGenericDefinition()
    {
        Type.IsGenericTypeDefinition
            .Should()
            .BeTrue($"{Type.FullName} should be a generic type definition.");

        return this;
    }

    #endregion
}
