using System.Reflection;

using FluentAssertions;

namespace KUKULCAN.SharedKernel.UnitTests.Assertions;

/// <summary>
/// Provides fluent assertions for constructors.
///
/// This file contains all assertions related to constructor
/// discovery, accessibility, signatures, metadata and
/// dependency injection conventions.
///
/// The implementation is intentionally isolated from the rest
/// of the builder in order to keep each partial class focused
/// on a single responsibility.
/// </summary>
public sealed partial class TypeAssertionBuilder
{
    #region Helpers

    /// <summary>
    /// Gets every instance constructor declared by the current type.
    /// </summary>
    private IReadOnlyCollection<ConstructorInfo> Constructors =>
        Type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

    /// <summary>
    /// Gets every public instance constructor.
    /// </summary>
    private IReadOnlyCollection<ConstructorInfo> PublicConstructors => [.. Constructors
            .Where(x => x.IsPublic)];

    /// <summary>
    /// Gets every private instance constructor.
    /// </summary>
    private IReadOnlyCollection<ConstructorInfo> PrivateConstructors => [.. Constructors
            .Where(x => x.IsPrivate)];

    /// <summary>
    /// Gets every protected instance constructor.
    /// </summary>
    private IReadOnlyCollection<ConstructorInfo> ProtectedConstructors => [.. Constructors
            .Where(x => x.IsFamily)];

    /// <summary>
    /// Gets every internal instance constructor.
    /// </summary>
    private IReadOnlyCollection<ConstructorInfo> InternalConstructors => [.. Constructors
            .Where(x => x.IsAssembly)];

    #endregion

    #region Constructor existence

    /// <summary>
    /// Verifies that the current type exposes at least one constructor.
    /// </summary>
    public TypeAssertionBuilder HaveConstructor()
    {
        Constructors
            .Should()
            .NotBeEmpty($"{Type.FullName} should expose at least one constructor.");

        return this;
    }

    /// <summary>
    /// Verifies that the current type exposes no constructors.
    /// </summary>
    public TypeAssertionBuilder NotHaveConstructor()
    {
        Constructors
            .Should()
            .BeEmpty($"{Type.FullName} should not expose constructors.");

        return this;
    }

    /// <summary>
    /// Verifies that the current type exposes public constructors.
    /// </summary>
    public TypeAssertionBuilder HavePublicConstructor()
    {
        PublicConstructors
            .Should()
            .NotBeEmpty($"{Type.FullName} should expose at least one public constructor.");

        return this;
    }

    /// <summary>
    /// Verifies that the current type exposes no public constructors.
    /// </summary>
    public TypeAssertionBuilder NotHavePublicConstructor()
    {
        PublicConstructors
            .Should()
            .BeEmpty($"{Type.FullName} should not expose public constructors.");

        return this;
    }

    /// <summary>
    /// Verifies that the current type exposes private constructors.
    /// </summary>
    public TypeAssertionBuilder HavePrivateConstructor()
    {
        PrivateConstructors
            .Should()
            .NotBeEmpty($"{Type.FullName} should expose private constructors.");

        return this;
    }

    /// <summary>
    /// Verifies that the current type exposes protected constructors.
    /// </summary>
    public TypeAssertionBuilder HaveProtectedConstructor()
    {
        ProtectedConstructors
            .Should()
            .NotBeEmpty($"{Type.FullName} should expose protected constructors.");

        return this;
    }

    /// <summary>
    /// Verifies that the current type exposes internal constructors.
    /// </summary>
    public TypeAssertionBuilder HaveInternalConstructor()
    {
        InternalConstructors
            .Should()
            .NotBeEmpty($"{Type.FullName} should expose internal constructors.");

        return this;
    }

    #endregion

    #region Default constructor

    /// <summary>
    /// Verifies that the current type exposes a public parameterless constructor.
    /// </summary>
    public TypeAssertionBuilder HaveDefaultConstructor()
    {
        PublicConstructors
            .Any(c => c.GetParameters().Length == 0)
            .Should()
            .BeTrue($"{Type.FullName} should expose a public parameterless constructor.");

        return this;
    }

    /// <summary>
    /// Verifies that the current type does not expose a public parameterless constructor.
    /// </summary>
    public TypeAssertionBuilder NotHaveDefaultConstructor()
    {
        PublicConstructors
            .Any(c => c.GetParameters().Length == 0)
            .Should()
            .BeFalse($"{Type.FullName} should not expose a public parameterless constructor.");

        return this;
    }

    /// <summary>
    /// Verifies that the current type exposes a non-public parameterless constructor.
    /// </summary>
    public TypeAssertionBuilder HaveNonPublicDefaultConstructor()
    {
        Constructors
            .Where(c => !c.IsPublic)
            .Any(c => c.GetParameters().Length == 0)
            .Should()
            .BeTrue($"{Type.FullName} should expose a non-public parameterless constructor.");

        return this;
    }

    #endregion

    #region Constructor count

    /// <summary>
    /// Verifies the exact number of instance constructors.
    /// </summary>
    public TypeAssertionBuilder HaveConstructorCount(int expected)
    {
        Constructors
            .Count
            .Should()
            .Be(expected);

        return this;
    }

    /// <summary>
    /// Verifies the exact number of public constructors.
    /// </summary>
    public TypeAssertionBuilder HavePublicConstructorCount(int expected)
    {
        PublicConstructors
            .Count
            .Should()
            .Be(expected);

        return this;
    }

    /// <summary>
    /// Verifies the exact number of private constructors.
    /// </summary>
    public TypeAssertionBuilder HavePrivateConstructorCount(int expected)
    {
        PrivateConstructors
            .Count
            .Should()
            .Be(expected);

        return this;
    }

    /// <summary>
    /// Verifies that exactly one public constructor exists.
    /// </summary>
    public TypeAssertionBuilder HaveSinglePublicConstructor()
    {
        return HavePublicConstructorCount(1);
    }

    /// <summary>
    /// Verifies that exactly one constructor exists.
    /// </summary>
    public TypeAssertionBuilder HaveSingleConstructor()
    {
        return HaveConstructorCount(1);
    }

    #endregion

    #region Constructor visibility

    /// <summary>
    /// Verifies that every constructor is private.
    /// </summary>
    public TypeAssertionBuilder HaveOnlyPrivateConstructors()
    {
        Constructors
            .Should()
            .OnlyContain(c => c.IsPrivate);

        return this;
    }

    /// <summary>
    /// Verifies that every constructor is protected.
    /// </summary>
    public TypeAssertionBuilder HaveOnlyProtectedConstructors()
    {
        Constructors
            .Should()
            .OnlyContain(c => c.IsFamily);

        return this;
    }

    /// <summary>
    /// Verifies that every constructor is internal.
    /// </summary>
    public TypeAssertionBuilder HaveOnlyInternalConstructors()
    {
        Constructors
            .Should()
            .OnlyContain(c => c.IsAssembly);

        return this;
    }

    /// <summary>
    /// Verifies that every constructor is public.
    /// </summary>
    public TypeAssertionBuilder HaveOnlyPublicConstructors()
    {
        Constructors
            .Should()
            .OnlyContain(c => c.IsPublic);

        return this;
    }

    /// <summary>
    /// Verifies that no private constructors exist.
    /// </summary>
    public TypeAssertionBuilder NotHavePrivateConstructors()
    {
        PrivateConstructors
            .Should()
            .BeEmpty();

        return this;
    }

    /// <summary>
    /// Verifies that no protected constructors exist.
    /// </summary>
    public TypeAssertionBuilder NotHaveProtectedConstructors()
    {
        ProtectedConstructors
            .Should()
            .BeEmpty();

        return this;
    }

    /// <summary>
    /// Verifies that no internal constructors exist.
    /// </summary>
    public TypeAssertionBuilder NotHaveInternalConstructors()
    {
        InternalConstructors
            .Should()
            .BeEmpty();

        return this;
    }

    #endregion

    #region Constructor signatures

    /// <summary>
    /// Verifies that a public constructor exists with the specified parameter types.
    /// </summary>
    public TypeAssertionBuilder HaveConstructor(params Type[] parameterTypes)
    {
        parameterTypes ??= Array.Empty<Type>();

        PublicConstructors
            .Any(c => MatchSignature(c, parameterTypes))
            .Should()
            .BeTrue($"{Type.FullName} should expose the specified constructor.");

        return this;
    }

    /// <summary>
    /// Verifies that no public constructor exists with the specified parameter types.
    /// </summary>
    public TypeAssertionBuilder NotHaveConstructor(params Type[] parameterTypes)
    {
        parameterTypes ??= Array.Empty<Type>();

        PublicConstructors
            .Any(c => MatchSignature(c, parameterTypes))
            .Should()
            .BeFalse($"{Type.FullName} should not expose the specified constructor.");

        return this;
    }

    /// <summary>
    /// Verifies that a private constructor exists with the specified parameter types.
    /// </summary>
    public TypeAssertionBuilder HavePrivateConstructor(params Type[] parameterTypes)
    {
        parameterTypes ??= Array.Empty<Type>();

        PrivateConstructors
            .Any(c => MatchSignature(c, parameterTypes))
            .Should()
            .BeTrue($"{Type.FullName} should expose the specified private constructor.");

        return this;
    }

    /// <summary>
    /// Verifies that a protected constructor exists with the specified parameter types.
    /// </summary>
    public TypeAssertionBuilder HaveProtectedConstructor(params Type[] parameterTypes)
    {
        parameterTypes ??= Array.Empty<Type>();

        ProtectedConstructors
            .Any(c => MatchSignature(c, parameterTypes))
            .Should()
            .BeTrue($"{Type.FullName} should expose the specified protected constructor.");

        return this;
    }

    /// <summary>
    /// Verifies that an internal constructor exists with the specified parameter types.
    /// </summary>
    public TypeAssertionBuilder HaveInternalConstructor(params Type[] parameterTypes)
    {
        parameterTypes ??= Array.Empty<Type>();

        InternalConstructors
            .Any(c => MatchSignature(c, parameterTypes))
            .Should()
            .BeTrue($"{Type.FullName} should expose the specified internal constructor.");

        return this;
    }

    /// <summary>
    /// Verifies that exactly one constructor matches the specified signature.
    /// </summary>
    public TypeAssertionBuilder HaveSingleConstructor(params Type[] parameterTypes)
    {
        parameterTypes ??= Array.Empty<Type>();

        PublicConstructors
            .Count(c => MatchSignature(c, parameterTypes))
            .Should()
            .Be(1, $"{Type.FullName} should expose exactly one constructor with the specified signature.");

        return this;
    }

    /// <summary>
    /// Verifies that at least one constructor has the specified number of parameters.
    /// </summary>
    public TypeAssertionBuilder HaveConstructorWithParameterCount(int parameterCount)
    {
        PublicConstructors
            .Any(c => c.GetParameters().Length == parameterCount)
            .Should()
            .BeTrue();

        return this;
    }

    /// <summary>
    /// Verifies that no constructor has the specified number of parameters.
    /// </summary>
    public TypeAssertionBuilder NotHaveConstructorWithParameterCount(int parameterCount)
    {
        PublicConstructors
            .Any(c => c.GetParameters().Length == parameterCount)
            .Should()
            .BeFalse();

        return this;
    }

    #endregion

    #region Signature helpers

    /// <summary>
    /// Determines whether the specified constructor matches the supplied signature.
    /// </summary>
    private static bool MatchSignature(ConstructorInfo constructor, IReadOnlyList<Type> signature)
    {
        ParameterInfo[] parameters = constructor.GetParameters();

        if (parameters.Length != signature.Count)
            return false;

        return !parameters.Where((t, i) => t.ParameterType != signature[i]).Any();
    }

    #endregion

    #region Constructor parameter assertions

    /// <summary>
    /// Verifies that at least one public constructor receives
    /// a parameter of the specified type.
    /// </summary>
    public TypeAssertionBuilder ConstructorShouldReceive<TParameter>()
    {
        Type parameterType = typeof(TParameter);

        PublicConstructors
            .Any(c => c.GetParameters().Any(p => p.ParameterType == parameterType))
            .Should()
            .BeTrue($"{Type.FullName} should receive a parameter of type {parameterType.FullName}.");

        return this;
    }

    /// <summary>
    /// Verifies that no public constructor receives
    /// a parameter of the specified type.
    /// </summary>
    public TypeAssertionBuilder ConstructorShouldNotReceive<TParameter>()
    {
        Type parameterType = typeof(TParameter);

        PublicConstructors
            .Any(c => c.GetParameters().Any(p => p.ParameterType == parameterType))
            .Should()
            .BeFalse($"{Type.FullName} should not receive a parameter of type {parameterType.FullName}.");

        return this;
    }

    /// <summary>
    /// Verifies that exactly one constructor parameter
    /// is of the specified type.
    /// </summary>
    public TypeAssertionBuilder ConstructorShouldReceiveExactly<TParameter>()
    {
        Type parameterType = typeof(TParameter);

        PublicConstructors
            .Any(c => c.GetParameters().Count(p => p.ParameterType == parameterType) == 1)
            .Should()
            .BeTrue($"{Type.FullName} should receive exactly one parameter of type {parameterType.FullName}.");

        return this;
    }

    /// <summary>
    /// Verifies that a constructor receives a parameter assignable to the specified type.
    /// </summary>
    public TypeAssertionBuilder ConstructorShouldReceiveAssignableTo<TParameter>()
    {
        Type parameterType = typeof(TParameter);

        PublicConstructors
            .Any(c => c.GetParameters().Any(p => parameterType.IsAssignableFrom(p.ParameterType)))
            .Should()
            .BeTrue($"{Type.FullName} should receive a parameter assignable to {parameterType.FullName}.");

        return this;
    }

    /// <summary>
    /// Verifies that a constructor receives an interface parameter.
    /// </summary>
    public TypeAssertionBuilder ConstructorShouldReceiveInterface<TInterface>()
    {
        Type parameterType = typeof(TInterface);

        parameterType.IsInterface
            .Should()
            .BeTrue($"{parameterType.FullName} must be an interface.");

        return ConstructorShouldReceive<TInterface>();
    }

    /// <summary>
    /// Verifies that a constructor receives an abstract parameter.
    /// </summary>
    public TypeAssertionBuilder ConstructorShouldReceiveAbstract<TAbstract>()
    {
        Type parameterType = typeof(TAbstract);

        parameterType.IsAbstract
            .Should()
            .BeTrue($"{parameterType.FullName} must be abstract.");

        return ConstructorShouldReceive<TAbstract>();
    }

    /// <summary>
    /// Verifies that a constructor receives a generic parameter.
    /// </summary>
    public TypeAssertionBuilder ConstructorShouldReceiveGeneric<TParameter>()
    {
        Type parameterType = typeof(TParameter);

        parameterType.IsGenericType
            .Should()
            .BeTrue($"{parameterType.FullName} must be generic.");

        return ConstructorShouldReceive<TParameter>();
    }

    /// <summary>
    /// Verifies that a constructor receives a nullable parameter.
    /// </summary>
    public TypeAssertionBuilder ConstructorShouldReceiveNullable<T>() where T : struct
    {
        Type nullableType = typeof(T?);

        return ConstructorShouldReceive(nullableType);
    }

    /// <summary>
    /// Verifies that at least one constructor receives
    /// a parameter of the specified runtime type.
    /// </summary>
    public TypeAssertionBuilder ConstructorShouldReceive(Type parameterType)
    {
        ArgumentNullException.ThrowIfNull(parameterType);

        PublicConstructors
            .Any(c => c.GetParameters().Any(p => p.ParameterType == parameterType))
            .Should()
            .BeTrue($"{Type.FullName} should receive parameter {parameterType.FullName}.");

        return this;
    }

    /// <summary>
    /// Verifies that a constructor receives at least one value type.
    /// </summary>
    public TypeAssertionBuilder ConstructorShouldReceiveValueType()
    {
        PublicConstructors
            .Any(c =>
                c.GetParameters().Any(p => p.ParameterType is { IsValueType: true, IsEnum: false }))
            .Should()
            .BeTrue($"{Type.FullName} should receive at least one value type.");

        return this;
    }

    /// <summary>
    /// Verifies that a constructor receives at least one reference type.
    /// </summary>
    public TypeAssertionBuilder ConstructorShouldReceiveReferenceType()
    {
        PublicConstructors
            .Any(c =>
                c.GetParameters().Any(p => !p.ParameterType.IsValueType))
            .Should()
            .BeTrue($"{Type.FullName} should receive at least one reference type.");

        return this;
    }

    /// <summary>
    /// Verifies that no constructor receives reference types.
    /// </summary>
    public TypeAssertionBuilder ConstructorShouldNotReceiveReferenceType()
    {
        PublicConstructors
            .Any(c => c.GetParameters().Any(p => !p.ParameterType.IsValueType))
            .Should()
            .BeFalse($"{Type.FullName} should not receive reference types.");

        return this;
    }

    /// <summary>
    /// Verifies that no constructor receives value types.
    /// </summary>
    public TypeAssertionBuilder ConstructorShouldNotReceiveValueType()
    {
        PublicConstructors
            .Any(c =>
                c.GetParameters().Any(p => p.ParameterType is { IsValueType: true, IsEnum: false }))
            .Should()
            .BeFalse($"{Type.FullName} should not receive value types.");

        return this;
    }

    #endregion

    #region Optional parameters

    /// <summary>
    /// Verifies that at least one constructor contains optional parameters.
    /// </summary>
    public TypeAssertionBuilder HaveOptionalParameters()
    {
        PublicConstructors
            .Any(c => c.GetParameters().Any(p => p.IsOptional))
            .Should()
            .BeTrue($"{Type.FullName} should expose at least one constructor with optional parameters.");

        return this;
    }

    /// <summary>
    /// Verifies that no constructor contains optional parameters.
    /// </summary>
    public TypeAssertionBuilder NotHaveOptionalParameters()
    {
        PublicConstructors
            .Any(c => c.GetParameters().Any(p => p.IsOptional))
            .Should()
            .BeFalse($"{Type.FullName} should not expose constructors with optional parameters.");

        return this;
    }

    /// <summary>
    /// Verifies that all parameters of every constructor are mandatory.
    /// </summary>
    public TypeAssertionBuilder HaveOnlyRequiredParameters()
    {
        PublicConstructors
            .Should()
            .OnlyContain(c => c.GetParameters().All(p => !p.IsOptional));

        return this;
    }

    /// <summary>
    /// Verifies that a constructor has exactly the specified number of optional parameters.
    /// </summary>
    public TypeAssertionBuilder HaveOptionalParameterCount(int expectedCount)
    {
        PublicConstructors
            .Any(c => c.GetParameters().Count(p => p.IsOptional) == expectedCount)
            .Should()
            .BeTrue($"{Type.FullName} should expose a constructor with {expectedCount} optional parameter(s).");

        return this;
    }

    #endregion

    #region Params arrays

    /// <summary>
    /// Verifies that at least one constructor exposes a params array.
    /// </summary>
    public TypeAssertionBuilder HaveParamsArray()
    {
        PublicConstructors
            .Any(c =>
                c.GetParameters().Any(p => p.GetCustomAttributes(typeof(ParamArrayAttribute), false).Any()))
            .Should()
            .BeTrue($"{Type.FullName} should expose a params constructor.");

        return this;
    }

    /// <summary>
    /// Verifies that no constructor exposes a params array.
    /// </summary>
    public TypeAssertionBuilder NotHaveParamsArray()
    {
        PublicConstructors
            .Any(c =>
                c.GetParameters().Any(p => p.GetCustomAttributes(typeof(ParamArrayAttribute), false).Length != 0))
            .Should()
            .BeFalse($"{Type.FullName} should not expose params constructors.");

        return this;
    }

    #endregion

    #region Generic constructors

    /// <summary>
    /// Verifies that the type exposes at least one generic constructor.
    /// </summary>
    public TypeAssertionBuilder HaveGenericConstructor()
    {
        PublicConstructors
            .Any(c => c.IsGenericMethod)
            .Should()
            .BeTrue($"{Type.FullName} should expose a generic constructor.");

        return this;
    }

    /// <summary>
    /// Verifies that the type exposes no generic constructors.
    /// </summary>
    public TypeAssertionBuilder NotHaveGenericConstructor()
    {
        PublicConstructors
            .Any(c => c.IsGenericMethod)
            .Should()
            .BeFalse($"{Type.FullName} should not expose generic constructors.");

        return this;
    }

    #endregion

    #region Constructor accessibility

    /// <summary>
    /// Verifies that every constructor matching the supplied signature is public.
    /// </summary>
    public TypeAssertionBuilder ConstructorShouldBePublic(params Type[] signature)
    {
        signature ??= [];

        Constructors
            .Where(c => MatchSignature(c, signature))
            .Should()
            .OnlyContain(c => c.IsPublic);

        return this;
    }

    /// <summary>
    /// Verifies that every constructor matching the supplied signature is private.
    /// </summary>
    public TypeAssertionBuilder ConstructorShouldBePrivate(params Type[] signature)
    {
        signature ??= [];

        Constructors
            .Where(c => MatchSignature(c, signature))
            .Should()
            .OnlyContain(c => c.IsPrivate);

        return this;
    }

    /// <summary>
    /// Verifies that every constructor matching the supplied signature is protected.
    /// </summary>
    public TypeAssertionBuilder ConstructorShouldBeProtected(params Type[] signature)
    {
        signature ??= [];

        Constructors
            .Where(c => MatchSignature(c, signature))
            .Should()
            .OnlyContain(c => c.IsFamily);

        return this;
    }

    /// <summary>
    /// Verifies that every constructor matching the supplied signature is internal.
    /// </summary>
    public TypeAssertionBuilder ConstructorShouldBeInternal(params Type[] signature)
    {
        signature ??= [];

        Constructors
            .Where(c => MatchSignature(c, signature))
            .Should()
            .OnlyContain(c => c.IsAssembly);

        return this;
    }

    #endregion

    #region Constructor attributes

    /// <summary>
    /// Verifies that at least one constructor is decorated with the specified attribute.
    /// </summary>
    public TypeAssertionBuilder HaveConstructorWithAttribute<TAttribute>() where TAttribute : Attribute
    {
        Constructors
            .Any(c => c.IsDefined(typeof(TAttribute), inherit: true))
            .Should()
            .BeTrue($"{Type.FullName} should expose a constructor decorated with {typeof(TAttribute).FullName}.");

        return this;
    }

    /// <summary>
    /// Verifies that no constructor is decorated with the specified attribute.
    /// </summary>
    public TypeAssertionBuilder NotHaveConstructorWithAttribute<TAttribute>() where TAttribute : Attribute
    {
        Constructors
            .Any(c => c.IsDefined(typeof(TAttribute), inherit: true))
            .Should()
            .BeFalse($"{Type.FullName} should not expose constructors decorated with {typeof(TAttribute).FullName}.");

        return this;
    }

    #endregion

    #region Copy constructors

    /// <summary>
    /// Verifies that the type exposes a copy constructor.
    /// </summary>
    public TypeAssertionBuilder HaveCopyConstructor()
    {
        Constructors
            .Any(c =>
            {
                ParameterInfo[] parameters = c.GetParameters();

                return parameters.Length == 1 && parameters[0].ParameterType == Type;
            })
            .Should()
            .BeTrue($"{Type.FullName} should expose a copy constructor.");

        return this;
    }

    /// <summary>
    /// Verifies that the type does not expose a copy constructor.
    /// </summary>
    public TypeAssertionBuilder NotHaveCopyConstructor()
    {
        Constructors
            .Any(c =>
            {
                ParameterInfo[] parameters = c.GetParameters();

                return parameters.Length == 1 && parameters[0].ParameterType == Type;
            })
            .Should()
            .BeFalse($"{Type.FullName} should not expose a copy constructor.");

        return this;
    }

    #endregion

    #region Record constructors

    /// <summary>
    /// Verifies that the compiler generated a record copy constructor.
    /// </summary>
    public TypeAssertionBuilder HaveCompilerGeneratedCopyConstructor()
    {
        Constructors
            .Any(c =>
            {
                ParameterInfo[] parameters = c.GetParameters();

                return parameters.Length == 1 && parameters[0].ParameterType == Type && c.IsFamily;
            })
            .Should()
            .BeTrue($"{Type.FullName} should expose the compiler-generated record copy constructor.");

        return this;
    }

    /// <summary>
    /// Verifies that the primary constructor exists.
    /// </summary>
    public TypeAssertionBuilder HavePrimaryConstructor()
    {
        PublicConstructors
            .Should()
            .HaveCountGreaterThan(0, $"{Type.FullName} should expose a primary constructor.");

        return this;
    }

    #endregion

    #region Dependency Injection

    /// <summary>
    /// Verifies that the type exposes exactly one public constructor,
    /// making it suitable for constructor injection.
    /// </summary>
    public TypeAssertionBuilder HaveSingleInjectableConstructor()
    {
        PublicConstructors
            .Should()
            .HaveCount(1, $"{Type.FullName} should expose exactly one injectable constructor.");

        return this;
    }

    /// <summary>
    /// Verifies that the type follows Microsoft dependency injection conventions.
    /// </summary>
    public TypeAssertionBuilder BeDependencyInjectionFriendly()
    {
        PublicConstructors
            .Should()
            .HaveCount(1, $"{Type.FullName} should expose exactly one public constructor.");

        PublicConstructors
            .Single()
            .GetParameters()
            .Should()
            .OnlyContain(p => !p.IsOptional && !p.ParameterType.IsPointer && !p.ParameterType.IsByRef);

        return this;
    }

    /// <summary>
    /// Verifies that multiple injectable constructors are not exposed.
    /// </summary>
    public TypeAssertionBuilder NotExposeMultipleInjectableConstructors()
    {
        PublicConstructors
            .Count
            .Should()
            .BeLessThanOrEqualTo(1, $"{Type.FullName} should not expose multiple injectable constructors.");

        return this;
    }

    #endregion

    #region Metadata

    /// <summary>
    /// Verifies that every constructor is declared by the current type.
    /// </summary>
    public TypeAssertionBuilder ConstructorsShouldBelongToCurrentType()
    {
        Constructors
            .Should()
            .OnlyContain(c => c.DeclaringType == Type);

        return this;
    }

    /// <summary>
    /// Verifies that every constructor is instance-based.
    /// </summary>
    public TypeAssertionBuilder ConstructorsShouldBeInstanceConstructors()
    {
        Constructors
            .Should()
            .OnlyContain(c => !c.IsStatic);

        return this;
    }

    /// <summary>
    /// Verifies that every constructor is declared only once.
    /// </summary>
    public TypeAssertionBuilder ConstructorsShouldBeUnique()
    {
        Constructors
            .Select(c => string.Join("|", c.GetParameters().Select(p => p.ParameterType.FullName)))
            .Should()
            .OnlyHaveUniqueItems();

        return this;
    }

    #endregion

    #region Advanced parameter validation

    /// <summary>
    /// Verifies that no constructor contains <c>ref</c> parameters.
    /// </summary>
    public TypeAssertionBuilder NotHaveRefParameters()
    {
        Constructors
            .Should()
            .OnlyContain(c => c.GetParameters().All(p => !p.ParameterType.IsByRef));

        return this;
    }

    /// <summary>
    /// Verifies that at least one constructor contains a <c>ref</c> parameter.
    /// </summary>
    public TypeAssertionBuilder HaveRefParameter()
    {
        Constructors
            .Any(c => c.GetParameters().Any(p => p.ParameterType.IsByRef))
            .Should()
            .BeTrue();

        return this;
    }

    /// <summary>
    /// Verifies that no constructor contains <c>out</c> parameters.
    /// </summary>
    public TypeAssertionBuilder NotHaveOutParameters()
    {
        Constructors
            .Should()
            .OnlyContain(c => c.GetParameters().All(p => !p.IsOut));

        return this;
    }

    /// <summary>
    /// Verifies that at least one constructor contains an <c>out</c> parameter.
    /// </summary>
    public TypeAssertionBuilder HaveOutParameter()
    {
        Constructors
            .Any(c => c.GetParameters().Any(p => p.IsOut))
            .Should()
            .BeTrue();

        return this;
    }

    /// <summary>
    /// Verifies that no constructor receives pointer parameters.
    /// </summary>
    public TypeAssertionBuilder NotHavePointerParameters()
    {
        Constructors
            .Should()
            .OnlyContain(c => c.GetParameters().All(p => !p.ParameterType.IsPointer));

        return this;
    }

    /// <summary>
    /// Verifies that no constructor receives function pointer parameters.
    /// </summary>
    public TypeAssertionBuilder NotHaveFunctionPointerParameters()
    {
        Constructors
            .Should()
            .OnlyContain(c => c.GetParameters().All(p => !p.ParameterType.IsFunctionPointer));

        return this;
    }

    /// <summary>
    /// Verifies that all constructor parameter names are unique.
    /// </summary>
    public TypeAssertionBuilder HaveUniqueParameterNames()
    {
        foreach (var constructor in Constructors)
        {
            constructor
                .GetParameters()
                .Select(p => p.Name)
                .Should()
                .OnlyHaveUniqueItems();
        }

        return this;
    }

    /// <summary>
    /// Verifies that every constructor parameter has a name.
    /// </summary>
    public TypeAssertionBuilder HaveNamedParameters()
    {
        foreach (var constructor in Constructors)
        {
            constructor
                .GetParameters()
                .Should()
                .OnlyContain(p => !string.IsNullOrWhiteSpace(p.Name));
        }

        return this;
    }

    /// <summary>
    /// Verifies that parameter names follow the camelCase convention.
    /// </summary>
    public TypeAssertionBuilder HaveCamelCaseParameterNames()
    {
        foreach (ConstructorInfo constructor in Constructors)
        {
            foreach (ParameterInfo parameter in constructor.GetParameters())
            {
                parameter.Name!
                    .Should()
                    .MatchRegex(@"^[a-z][a-zA-Z0-9]*$");
            }
        }

        return this;
    }

    #endregion

    #region CancellationToken

    /// <summary>
    /// Verifies that no constructor receives a CancellationToken.
    /// </summary>
    public TypeAssertionBuilder NotReceiveCancellationToken()
    {
        Constructors
            .Should()
            .OnlyContain(c => c.GetParameters().All(p => p.ParameterType != typeof(CancellationToken)));

        return this;
    }

    /// <summary>
    /// Verifies that at least one constructor receives a CancellationToken.
    /// </summary>
    public TypeAssertionBuilder ReceiveCancellationToken()
    {
        Constructors
            .Any(c => c.GetParameters().Any(p => p.ParameterType == typeof(CancellationToken)))
            .Should()
            .BeTrue();

        return this;
    }

    #endregion

    #region Generic parameter validation

    /// <summary>
    /// Verifies that no constructor receives open generic parameters.
    /// </summary>
    public TypeAssertionBuilder NotReceiveOpenGenericTypes()
    {
        Constructors
            .Should()
            .OnlyContain(c => c.GetParameters().All(p => !p.ParameterType.ContainsGenericParameters));

        return this;
    }

    /// <summary>
    /// Verifies that at least one constructor receives a closed generic type.
    /// </summary>
    public TypeAssertionBuilder ReceiveClosedGenericType()
    {
        Constructors
            .Any(c =>
                c.GetParameters()
                 .Any(p => p.ParameterType is { IsGenericType: true, ContainsGenericParameters: false }))
            .Should()
            .BeTrue();

        return this;
    }

    #endregion

    #region Architectural conventions

    /// <summary>
    /// Verifies that every public constructor is deterministic.
    /// </summary>
    public TypeAssertionBuilder HaveDeterministicConstructors()
    {
        PublicConstructors
            .Should()
            .OnlyContain(c => c.GetParameters().All(p => p.ParameterType != typeof(IServiceProvider)));

        return this;
    }

    /// <summary>
    /// Verifies that constructors do not depend directly on IServiceProvider.
    /// </summary>
    public TypeAssertionBuilder NotDependOnServiceProvider()
    {
        Constructors
            .Should()
            .OnlyContain(c => c.GetParameters().All(p => p.ParameterType != typeof(IServiceProvider)));

        return this;
    }

    /// <summary>
    /// Verifies that constructors do not depend on dynamic.
    /// </summary>
    public TypeAssertionBuilder NotReceiveDynamic()
    {
        Constructors
            .Should()
            .OnlyContain(c => c.GetParameters().All(p => p.ParameterType != typeof(object)));

        return this;
    }

    #endregion

    #region Constructor discovery

    /// <summary>
    /// Returns the unique constructor matching the supplied signature.
    /// </summary>
    /// <param name="parameterTypes">Constructor parameter types.</param>
    /// <returns>The matching constructor.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when no constructor matches the supplied signature.
    /// </exception>
    public ConstructorInfo GetConstructor(params Type[] parameterTypes)
    {
        parameterTypes ??= [];

        return Constructors.SingleOrDefault(c => MatchSignature(c, parameterTypes))
            ?? throw new InvalidOperationException($"Constructor ({string.Join(", ", parameterTypes.Select(t => t.Name))}) was not found in '{Type.FullName}'.");
    }

    /// <summary>
    /// Attempts to locate a constructor matching the supplied signature.
    /// </summary>
    public bool TryGetConstructor(out ConstructorInfo? constructor, params Type[] parameterTypes)
    {
        parameterTypes ??= [];

        constructor = Constructors.SingleOrDefault(c => MatchSignature(c, parameterTypes));

        return constructor is not null;
    }

    /// <summary>
    /// Finds all constructors having the specified number of parameters.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> FindConstructorsByParameterCount(int parameterCount)
    {
        return [.. Constructors.Where(c => c.GetParameters().Length == parameterCount)];
    }

    /// <summary>
    /// Finds all constructors receiving the specified parameter type.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> FindConstructorsReceiving<TParameter>()
    {
        Type parameterType = typeof(TParameter);

        return
            [.. Constructors.Where(c => c.GetParameters().Any(p => p.ParameterType == parameterType))];
    }

    /// <summary>
    /// Finds all constructors satisfying the supplied predicate.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> FindConstructors(Func<ConstructorInfo, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return [.. Constructors.Where(predicate)];
    }

    #endregion

    #region Constructor selection

    /// <summary>
    /// Returns the only public constructor.
    /// </summary>
    public ConstructorInfo GetSinglePublicConstructor()
    {
        PublicConstructors
            .Should()
            .HaveCount(1, $"{Type.FullName} should expose exactly one public constructor.");

        return PublicConstructors.Single();
    }

    /// <summary>
    /// Returns the default constructor.
    /// </summary>
    public ConstructorInfo GetDefaultConstructor()
    {
        ConstructorInfo? constructor = Constructors.SingleOrDefault(c => c.GetParameters().Length == 0);

        constructor.Should().NotBeNull($"{Type.FullName} should expose a parameterless constructor.");

        return constructor!;
    }

    /// <summary>
    /// Returns the copy constructor.
    /// </summary>
    public ConstructorInfo GetCopyConstructor()
    {
        ConstructorInfo? constructor = Constructors.SingleOrDefault(c =>
        {
            ParameterInfo[] parameters = c.GetParameters();

            return parameters.Length == 1 && parameters[0].ParameterType == Type;
        });

        constructor.Should().NotBeNull($"{Type.FullName} should expose a copy constructor.");

        return constructor!;
    }

    /// <summary>
    /// Returns every public constructor ordered by parameter count.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> GetPublicConstructorsOrdered()
    {
        return [.. PublicConstructors.OrderBy(c => c.GetParameters().Length)];
    }

    /// <summary>
    /// Returns every constructor ordered by parameter count.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> GetConstructorsOrdered()
    {
        return [.. Constructors.OrderBy(c => c.GetParameters().Length)];
    }

    #endregion

    #region Constructor inspection

    /// <summary>
    /// Determines whether a constructor matching the supplied signature exists.
    /// </summary>
    public bool ContainsConstructor(params Type[] parameterTypes)
    {
        parameterTypes ??= [];

        return Constructors.Any(c => MatchSignature(c, parameterTypes));
    }

    /// <summary>
    /// Determines whether the type exposes a public default constructor.
    /// </summary>
    public bool HasDefaultConstructor()
    {
        return PublicConstructors.Any(c => c.GetParameters().Length == 0);
    }

    /// <summary>
    /// Determines whether the type exposes a copy constructor.
    /// </summary>
    public bool HasCopyConstructor()
    {
        return Constructors.Any(c =>
        {
            ParameterInfo[] parameters = c.GetParameters();

            return parameters.Length == 1 && parameters[0].ParameterType == Type;
        });
    }

    /// <summary>
    /// Gets the maximum constructor arity.
    /// </summary>
    public int MaximumConstructorArity()
    {
        return Constructors
            .Select(c => c.GetParameters().Length)
            .DefaultIfEmpty()
            .Max();
    }

    /// <summary>
    /// Gets the minimum constructor arity.
    /// </summary>
    public int MinimumConstructorArity()
    {
        return Constructors
            .Select(c => c.GetParameters().Length)
            .DefaultIfEmpty()
            .Min();
    }

    #endregion

    #region Protected helper methods

    /// <summary>
    /// Returns every constructor matching the supplied predicate.
    /// </summary>
    protected IReadOnlyCollection<ConstructorInfo> WhereConstructors(Func<ConstructorInfo, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return [.. Constructors.Where(predicate)
        ];
    }

    /// <summary>
    /// Returns every public constructor matching the supplied predicate.
    /// </summary>
    protected IReadOnlyCollection<ConstructorInfo> WherePublicConstructors(Func<ConstructorInfo, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return [.. PublicConstructors.Where(predicate)];
    }

    /// <summary>
    /// Returns every constructor parameter.
    /// </summary>
    protected IReadOnlyCollection<ParameterInfo> AllParameters()
    {
        return [.. Constructors.SelectMany(c => c.GetParameters())];
    }

    /// <summary>
    /// Returns every parameter of every public constructor.
    /// </summary>
    protected IReadOnlyCollection<ParameterInfo> PublicParameters()
    {
        return [.. PublicConstructors.SelectMany(c => c.GetParameters())];
    }

    /// <summary>
    /// Returns every constructor ordered by arity.
    /// </summary>
    protected IReadOnlyCollection<ConstructorInfo> OrderedConstructors()
    {
        return [.. Constructors.OrderBy(c => c.GetParameters().Length)];
    }

    #endregion

    #region Internal validation

    /// <summary>
    /// Throws if no constructors exist.
    /// </summary>
    protected void EnsureConstructorsExist()
    {
        Constructors
            .Should()
            .NotBeEmpty($"{Type.FullName} does not expose constructors.");
    }

    /// <summary>
    /// Throws if no public constructors exist.
    /// </summary>
    protected void EnsurePublicConstructorsExist()
    {
        PublicConstructors
            .Should()
            .NotBeEmpty($"{Type.FullName} does not expose public constructors.");
    }

    /// <summary>
    /// Throws if more than one public constructor exists.
    /// </summary>
    protected void EnsureSinglePublicConstructor()
    {
        PublicConstructors
            .Should()
            .HaveCount(1, $"{Type.FullName} should expose exactly one public constructor.");
    }

    #endregion

    #region Parameter utilities

    /// <summary>
    /// Determines whether a constructor parameter of the supplied type exists.
    /// </summary>
    protected bool HasParameter(Type parameterType)
    {
        ArgumentNullException.ThrowIfNull(parameterType);

        return Constructors
            .SelectMany(c => c.GetParameters())
            .Any(p => p.ParameterType == parameterType);
    }

    /// <summary>
    /// Determines whether a constructor parameter assignable to the supplied type exists.
    /// </summary>
    protected bool HasAssignableParameter(Type parameterType)
    {
        ArgumentNullException.ThrowIfNull(parameterType);

        return Constructors
            .SelectMany(c => c.GetParameters())
            .Any(p => parameterType.IsAssignableFrom(p.ParameterType));
    }

    /// <summary>
    /// Counts constructor parameters of the supplied type.
    /// </summary>
    protected int CountParameters(Type parameterType)
    {
        ArgumentNullException.ThrowIfNull(parameterType);

        return Constructors
            .SelectMany(c => c.GetParameters())
            .Count(p => p.ParameterType == parameterType);
    }

    #endregion
}
