using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides constructor discovery services.
/// </summary>
public partial class ReflectionHelper
{
    #region Constructor discovery

    /// <summary>
    /// Returns every instance constructor declared by the specified type.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> GetConstructors(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type.GetConstructors(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic)
            .ToArray();
    }

    /// <summary>
    /// Returns every public instance constructor.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> GetPublicConstructors(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .Where(c => c.IsPublic)
            .ToArray();
    }

    /// <summary>
    /// Returns every private instance constructor.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> GetPrivateConstructors(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .Where(c => c.IsPrivate)
            .ToArray();
    }

    /// <summary>
    /// Returns every protected instance constructor.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> GetProtectedConstructors(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .Where(c => c.IsFamily)
            .ToArray();
    }

    /// <summary>
    /// Returns every internal instance constructor.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> GetInternalConstructors(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .Where(c => c.IsAssembly)
            .ToArray();
    }

    /// <summary>
    /// Returns every protected internal constructor.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> GetProtectedInternalConstructors(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .Where(c => c.IsFamilyOrAssembly)
            .ToArray();
    }

    /// <summary>
    /// Returns every private protected constructor.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> GetPrivateProtectedConstructors(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .Where(c => c.IsFamilyAndAssembly)
            .ToArray();
    }

    /// <summary>
    /// Determines whether the specified type exposes constructors.
    /// </summary>
    public bool HasConstructors(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type).Count != 0;
    }

    /// <summary>
    /// Determines whether the specified type exposes public constructors.
    /// </summary>
    public bool HasPublicConstructors(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetPublicConstructors(type).Count != 0;
    }

    /// <summary>
    /// Determines whether the specified type exposes non-public constructors.
    /// </summary>
    public bool HasNonPublicConstructors(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .Any(c => !c.IsPublic);
    }

    #endregion
}
