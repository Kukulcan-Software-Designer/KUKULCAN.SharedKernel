namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Generic cache invalidation operations.
/// </summary>
internal static partial class ReflectionCache
{
    /// <summary>
    /// Invalidates every cache entry associated with the specified type.
    /// </summary>
    public static int Invalidate<T>()
        => InvalidateType(TypeInfo.GetType<T>());

    /// <summary>
    /// Invalidates every cache entry belonging to the assembly containing the specified type.
    /// </summary>
    public static int InvalidateAssembly<T>()
        => InvalidateAssembly(TypeInfo.GetAssembly<T>());

    /// <summary>
    /// Invalidates every cache entry belonging to the module containing the specified type.
    /// </summary>
    public static int InvalidateModule<T>()
        => InvalidateModule(TypeInfo.GetModule<T>());

    /// <summary>
    /// Invalidates every cache entry belonging to the namespace containing the specified type.
    /// </summary>
    public static int InvalidateNamespace<T>()
    {
        string? @namespace = TypeInfo.GetNamespace<T>();

        return string.IsNullOrWhiteSpace(@namespace)
            ? 0
            : InvalidateNamespace(@namespace);
    }

    /// <summary>
    /// Invalidates every cache entry assignable to the specified type.
    /// </summary>
    public static int InvalidateAssignableTo<T>()
        => InvalidateAssignableTo(TypeInfo.GetType<T>());

    /// <summary>
    /// Invalidates every cache entry whose cached value is assignable to the specified type.
    /// </summary>
    public static int InvalidateValueType<T>()
        => InvalidateValueType(TypeInfo.GetType<T>());

    /// <summary>
    /// Invalidates every cache entry associated with the specified generic type definition.
    /// </summary>
    public static int InvalidateGenericType<T>()
        => InvalidateGenericType(TypeInfo.GetType<T>());

    /// <summary>
    /// Invalidates every cache entry associated with the specified type hierarchy.
    /// </summary>
    public static int InvalidateHierarchy<T>()
        => InvalidateAssignableTo<T>();

    /// <summary>
    /// Invalidates every cache entry associated with the specified generic hierarchy.
    /// </summary>
    public static int InvalidateGenericHierarchy<T>()
    {
        int removed = InvalidateGenericType<T>();

        removed += InvalidateAssignableTo<T>();

        return removed;
    }

    /// <summary>
    /// Invalidates every cache entry belonging to the reflection context containing the specified type.
    /// </summary>
    public static int InvalidateContext<T>()
    {
        int removed = InvalidateAssembly<T>();

        removed += InvalidateModule<T>();

        return removed;
    }

    /// <summary>
    /// Invalidates every cache entry belonging to the namespace and reflection context.
    /// </summary>
    public static int InvalidateScope<T>()
    {
        int removed = InvalidateNamespace<T>();

        removed += InvalidateContext<T>();

        return removed;
    }

    /// <summary>
    /// Invalidates every cache entry associated with the complete type scope.
    /// </summary>
    public static int InvalidateComplete<T>()
    {
        int removed = InvalidateHierarchy<T>();

        removed += InvalidateContext<T>();

        return removed;
    }
}
