using FluentAssertions;

namespace KUKULCAN.SharedKernel.UnitTests.Assertions;

/// <summary>
/// Provides assertions related to inheritance,
/// assignability and interface implementation.
/// </summary>
public sealed partial class TypeAssertionBuilder
{
    #region Base Type

    /// <summary>
    /// Verifies that the current type derives directly from
    /// the specified base type.
    /// </summary>
    public TypeAssertionBuilder HaveBaseType<TBase>()
    {
        Type.BaseType
            .Should()
            .Be(typeof(TBase),
                $"{Type.FullName} should have {typeof(TBase).FullName} as its direct base type.");

        return this;
    }

    /// <summary>
    /// Verifies that the current type does not derive directly
    /// from the specified base type.
    /// </summary>
    public TypeAssertionBuilder NotHaveBaseType<TBase>()
    {
        Type.BaseType
            .Should()
            .NotBe(typeof(TBase),
                $"{Type.FullName} should not have {typeof(TBase).FullName} as its direct base type.");

        return this;
    }

    #endregion

    #region Inheritance

    /// <summary>
    /// Verifies that the current type derives from
    /// the specified base type.
    /// </summary>
    public TypeAssertionBuilder DeriveFrom<TBase>()
    {
        Type.IsSubclassOf(typeof(TBase))
            .Should()
            .BeTrue($"{Type.FullName} should derive from {typeof(TBase).FullName}.");

        return this;
    }

    /// <summary>
    /// Verifies that the current type does not derive
    /// from the specified base type.
    /// </summary>
    public TypeAssertionBuilder NotDeriveFrom<TBase>()
    {
        Type.IsSubclassOf(typeof(TBase))
            .Should()
            .BeFalse($"{Type.FullName} should not derive from {typeof(TBase).FullName}.");

        return this;
    }

    #endregion

    #region Assignability

    /// <summary>
    /// Verifies that the current type can be assigned to
    /// the specified type.
    /// </summary>
    public TypeAssertionBuilder BeAssignableTo<TTarget>()
    {
        typeof(TTarget)
            .IsAssignableFrom(Type)
            .Should()
            .BeTrue($"{Type.FullName} should be assignable to {typeof(TTarget).FullName}.");

        return this;
    }

    /// <summary>
    /// Verifies that the current type cannot be assigned
    /// to the specified type.
    /// </summary>
    public TypeAssertionBuilder NotBeAssignableTo<TTarget>()
    {
        typeof(TTarget)
            .IsAssignableFrom(Type)
            .Should()
            .BeFalse($"{Type.FullName} should not be assignable to {typeof(TTarget).FullName}.");

        return this;
    }

    #endregion

    #region Interfaces

    /// <summary>
    /// Verifies that the current type implements the specified interface.
    /// </summary>
    public TypeAssertionBuilder Implement<TInterface>()
    {
        Reflection
            .Implements(Type, typeof(TInterface))
            .Should()
            .BeTrue($"{Type.FullName} should implement {typeof(TInterface).FullName}.");

        return this;
    }

    /// <summary>
    /// Verifies that the current type does not implement
    /// the specified interface.
    /// </summary>
    public TypeAssertionBuilder NotImplement<TInterface>()
    {
        Reflection
            .Implements(Type, typeof(TInterface))
            .Should()
            .BeFalse($"{Type.FullName} should not implement {typeof(TInterface).FullName}.");

        return this;
    }

    /// <summary>
    /// Verifies that the current type exposes the specified interface.
    /// </summary>
    public TypeAssertionBuilder HaveInterface<TInterface>()
    {
        Reflection
            .Interfaces(Type)
            .Should()
            .Contain(typeof(TInterface),
                $"{Type.FullName} should expose interface {typeof(TInterface).FullName}.");

        return this;
    }

    /// <summary>
    /// Verifies that the current type does not expose
    /// the specified interface.
    /// </summary>
    public TypeAssertionBuilder NotHaveInterface<TInterface>()
    {
        Reflection
            .Interfaces(Type)
            .Should()
            .NotContain(typeof(TInterface),
                $"{Type.FullName} should not expose interface {typeof(TInterface).FullName}.");

        return this;
    }

    #endregion

    #region Generic Constraints

    /// <summary>
    /// Verifies that the current generic type parameter
    /// is constrained to the specified base type.
    /// </summary>
    public TypeAssertionBuilder HaveGenericConstraint<TConstraint>()
    {
        Type.GetGenericArguments()
            .Should()
            .NotBeEmpty($"{Type.FullName} should define generic arguments.");

        var constraints = Type.GetGenericArguments()
            .SelectMany(x => x.GetGenericParameterConstraints());

        constraints
            .Should()
            .Contain(typeof(TConstraint),
                $"{Type.FullName} should contain generic constraint {typeof(TConstraint).FullName}.");

        return this;
    }

    #endregion

    #region Interfaces Count

    /// <summary>
    /// Verifies the number of implemented interfaces.
    /// </summary>
    public TypeAssertionBuilder HaveInterfaceCount(int expectedCount)
    {
        Reflection
            .Interfaces(Type)
            .Count
            .Should()
            .Be(expectedCount);

        return this;
    }

    #endregion
}
