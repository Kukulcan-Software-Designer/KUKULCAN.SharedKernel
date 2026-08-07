using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides shared reflection infrastructure used by the unit-test suite.
/// </summary>
public partial class ReflectionHelper
{
    /// <summary>
    /// Gets the binding flags used when public instance constructors are inspected.
    /// </summary>
    protected const BindingFlags PublicInstanceConstructorFlags =
        BindingFlags.Instance |
        BindingFlags.Public;

    /// <summary>
    /// Gets the binding flags used when public static constructors are inspected.
    /// </summary>
    protected const BindingFlags PublicStaticConstructorFlags =
        BindingFlags.Static |
        BindingFlags.Public;

    /// <summary>
    /// Gets the binding flags used when all public constructors are inspected.
    /// </summary>
    protected const BindingFlags PublicConstructorFlags =
        BindingFlags.Instance |
        BindingFlags.Static |
        BindingFlags.Public;

    /// <summary>
    /// Gets the binding flags used when all constructors are inspected.
    /// </summary>
    protected const BindingFlags AllConstructorFlags =
        BindingFlags.Instance |
        BindingFlags.Static |
        BindingFlags.Public |
        BindingFlags.NonPublic;

    /// <summary>
    /// Gets the binding flags used when public instance members are inspected.
    /// </summary>
    protected const BindingFlags PublicInstanceMemberFlags =
        BindingFlags.Instance |
        BindingFlags.Public;

    /// <summary>
    /// Gets the binding flags used when public static members are inspected.
    /// </summary>
    protected const BindingFlags PublicStaticMemberFlags =
        BindingFlags.Static |
        BindingFlags.Public;

    /// <summary>
    /// Gets the binding flags used when all public members are inspected.
    /// </summary>
    protected const BindingFlags PublicMemberFlags =
        BindingFlags.Instance |
        BindingFlags.Static |
        BindingFlags.Public;

    /// <summary>
    /// Gets the binding flags used when all members are inspected.
    /// </summary>
    protected const BindingFlags AllMemberFlags =
        BindingFlags.Instance |
        BindingFlags.Static |
        BindingFlags.Public |
        BindingFlags.NonPublic;

    /// <summary>
    /// Gets the binding flags used when public fields are inspected.
    /// </summary>
    protected const BindingFlags PublicFieldFlags =
        BindingFlags.Instance |
        BindingFlags.Static |
        BindingFlags.Public;

    /// <summary>
    /// Gets the binding flags used when public properties are inspected.
    /// </summary>
    protected const BindingFlags PublicPropertyFlags =
        BindingFlags.Instance |
        BindingFlags.Static |
        BindingFlags.Public;

    /// <summary>
    /// Gets the binding flags used when public methods are inspected.
    /// </summary>
    protected const BindingFlags PublicMethodFlags =
        BindingFlags.Instance |
        BindingFlags.Static |
        BindingFlags.Public;

    /// <summary>
    /// Gets the binding flags used when all methods are inspected.
    /// </summary>
    protected const BindingFlags AllMethodFlags =
        BindingFlags.Instance |
        BindingFlags.Static |
        BindingFlags.Public |
        BindingFlags.NonPublic;

    /// <summary>
    /// Gets the binding flags used when nested types are inspected.
    /// </summary>
    protected const BindingFlags NestedTypeFlags =
        BindingFlags.Public |
        BindingFlags.NonPublic;

    /// <summary>
    /// Gets the reflection helper used by the current operation.
    /// </summary>
    protected ReflectionHelper Reflection => this;

    /// <summary>
    /// Creates a reflection helper.
    /// </summary>
    public ReflectionHelper()
    {
    }

    /// <summary>
    /// Gets the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// Type to return.
    /// </typeparam>
    /// <returns>
    /// The type represented by <typeparamref name="T"/>.
    /// </returns>
    protected static Type GetTypeOf<T>()
    {
        return typeof(T);
    }

    /// <summary>
    /// Gets the specified type when a runtime type is required.
    /// </summary>
    /// <param name="type">
    /// Type to validate and return.
    /// </param>
    /// <returns>
    /// The supplied type.
    /// </returns>
    protected static Type RequireType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type;
    }

    /// <summary>
    /// Determines whether the specified type is a class.
    /// </summary>
    /// <param name="type">
    /// Type to inspect.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when the type is a class; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    protected static bool IsClass(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type.IsClass;
    }

    /// <summary>
    /// Determines whether the specified type is an interface.
    /// </summary>
    /// <param name="type">
    /// Type to inspect.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when the type is an interface; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    protected static bool IsInterface(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type.IsInterface;
    }

    /// <summary>
    /// Determines whether the specified type is a value type.
    /// </summary>
    /// <param name="type">
    /// Type to inspect.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when the type is a value type; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    protected static bool IsValueType(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type.IsValueType;
    }

    /// <summary>
    /// Determines whether the specified type is an enumeration.
    /// </summary>
    /// <param name="type">
    /// Type to inspect.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when the type is an enumeration; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    protected static bool IsEnum(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type.IsEnum;
    }

    /// <summary>
    /// Determines whether the specified type is abstract.
    /// </summary>
    /// <param name="type">
    /// Type to inspect.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when the type is abstract; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    protected static bool IsAbstract(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type.IsAbstract;
    }

    /// <summary>
    /// Determines whether the specified type is sealed.
    /// </summary>
    /// <param name="type">
    /// Type to inspect.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when the type is sealed; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    protected static bool IsSealed(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type.IsSealed;
    }
}
