namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Generic cache cleanup operations.
/// </summary>
internal static partial class ReflectionCache
{
    /// <summary>
    /// Removes every cache entry associated with the specified type.
    /// </summary>
    public static int Cleanup<T>()
        => CleanupType(TypeInfo.GetType<T>());

    /// <summary>
    /// Removes every cache entry belonging to the assembly containing the specified type.
    /// </summary>
    public static int CleanupAssembly<T>()
        => CleanupAssembly(TypeInfo.GetAssembly<T>());

    /// <summary>
    /// Removes every cache entry belonging to the module containing the specified type.
    /// </summary>
    public static int CleanupModule<T>()
        => CleanupModule(TypeInfo.GetModule<T>());

    /// <summary>
    /// Removes every cache entry belonging to the namespace containing the specified type.
    /// </summary>
    public static int CleanupNamespace<T>()
    {
        string? @namespace = TypeInfo.GetNamespace<T>();

        return string.IsNullOrWhiteSpace(@namespace)
            ? 0
            : CleanupNamespace(@namespace);
    }

    /// <summary>
    /// Removes every cache entry assignable to the specified type.
    /// </summary>
    public static int CleanupAssignableTo<T>()
        => CleanupAssignableTo(TypeInfo.GetType<T>());

    /// <summary>
    /// Removes every cache entry whose cached value is assignable to the specified type.
    /// </summary>
    public static int CleanupValueType<T>()
        => CleanupValueType(TypeInfo.GetType<T>());

    /// <summary>
    /// Removes every cache entry associated with the specified generic type definition.
    /// </summary>
    public static int CleanupGenericType<T>()
        => CleanupGenericType(TypeInfo.GetType<T>());

    /// <summary>
    /// Removes every cache entry associated with the specified hierarchy.
    /// </summary>
    public static int CleanupHierarchy<T>()
        => CleanupAssignableTo<T>();

    /// <summary>
    /// Removes every cache entry associated with the specified generic hierarchy.
    /// </summary>
    public static int CleanupGenericHierarchy<T>()
    {
        int removed = CleanupGenericType<T>();

        removed += CleanupAssignableTo<T>();

        return removed;
    }

    /// <summary>
    /// Removes every cache entry belonging to the reflection context containing the specified type.
    /// </summary>
    public static int CleanupContext<T>()
    {
        int removed = CleanupAssembly<T>();

        removed += CleanupModule<T>();

        return removed;
    }

    /// <summary>
    /// Removes every cache entry belonging to the namespace and reflection context.
    /// </summary>
    public static int CleanupScope<T>()
    {
        int removed = CleanupNamespace<T>();

        removed += CleanupContext<T>();

        return removed;
    }

    /// <summary>
    /// Removes every cache entry associated with the complete type scope.
    /// </summary>
    public static int CleanupComplete<T>()
    {
        int removed = CleanupHierarchy<T>();

        removed += CleanupContext<T>();

        return removed;
    }
}
