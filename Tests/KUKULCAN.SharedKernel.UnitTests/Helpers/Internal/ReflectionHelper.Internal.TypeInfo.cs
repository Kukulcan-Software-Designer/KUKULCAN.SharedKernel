using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Provides reusable generic reflection helpers used throughout the reflection framework.
/// </summary>
internal static class TypeInfo
{
    /// <summary>
    /// Gets the runtime type associated with <typeparamref name="T"/>.
    /// </summary>
    public static Type GetType<T>()
    {
        return typeof(T);
    }

    /// <summary>
    /// Gets the assembly containing <typeparamref name="T"/>.
    /// </summary>
    public static Assembly GetAssembly<T>()
    {
        return typeof(T).Assembly;
    }

    /// <summary>
    /// Gets the module containing <typeparamref name="T"/>.
    /// </summary>
    public static Module GetModule<T>()
    {
        return typeof(T).Module;
    }

    /// <summary>
    /// Gets the namespace containing <typeparamref name="T"/>.
    /// </summary>
    public static string? GetNamespace<T>()
    {
        return typeof(T).Namespace;
    }

    /// <summary>
    /// Gets the full name of <typeparamref name="T"/>.
    /// </summary>
    public static string GetFullName<T>()
    {
        return typeof(T).FullName
               ?? typeof(T).Name;
    }

    /// <summary>
    /// Gets the simple name of <typeparamref name="T"/>.
    /// </summary>
    public static string GetName<T>()
    {
        return typeof(T).Name;
    }

    /// <summary>
    /// Determines whether <typeparamref name="T"/> belongs to a namespace.
    /// </summary>
    public static bool HasNamespace<T>()
    {
        return !string.IsNullOrWhiteSpace(typeof(T).Namespace);
    }

    /// <summary>
    /// Determines whether <typeparamref name="T"/> is a generic type.
    /// </summary>
    public static bool IsGenericType<T>()
    {
        return typeof(T).IsGenericType;
    }

    /// <summary>
    /// Determines whether <typeparamref name="T"/> is a generic type definition.
    /// </summary>
    public static bool IsGenericTypeDefinition<T>()
    {
        return typeof(T).IsGenericTypeDefinition;
    }

    /// <summary>
    /// Determines whether <typeparamref name="T"/> is an interface.
    /// </summary>
    public static bool IsInterface<T>()
    {
        return typeof(T).IsInterface;
    }

    /// <summary>
    /// Determines whether <typeparamref name="T"/> is abstract.
    /// </summary>
    public static bool IsAbstract<T>()
    {
        return typeof(T).IsAbstract;
    }

    /// <summary>
    /// Determines whether <typeparamref name="T"/> is sealed.
    /// </summary>
    public static bool IsSealed<T>()
    {
        return typeof(T).IsSealed;
    }

    /// <summary>
    /// Determines whether <typeparamref name="T"/> is a value type.
    /// </summary>
    public static bool IsValueType<T>()
    {
        return typeof(T).IsValueType;
    }

    /// <summary>
    /// Determines whether <typeparamref name="T"/> is an enumeration.
    /// </summary>
    public static bool IsEnum<T>()
    {
        return typeof(T).IsEnum;
    }

    /// <summary>
    /// Determines whether <typeparamref name="T"/> is assignable to the specified base type.
    /// </summary>
    public static bool IsAssignableTo<T>(Type baseType)
    {
        ArgumentNullException.ThrowIfNull(baseType);

        return baseType.IsAssignableFrom(typeof(T));
    }
}
