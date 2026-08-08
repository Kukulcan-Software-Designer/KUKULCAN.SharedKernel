using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides constructor architecture analysis services.
/// </summary>
public partial class ReflectionHelper
{
    #region Statistics

    /// <summary>
    /// Gets the total number of constructors.
    /// </summary>
    public int GetConstructorCount(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type).Count;
    }

    /// <summary>
    /// Gets the number of public constructors.
    /// </summary>
    public int GetPublicConstructorCount(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetPublicConstructors(type).Count;
    }

    /// <summary>
    /// Gets the number of private constructors.
    /// </summary>
    public int GetPrivateConstructorCount(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetPrivateConstructors(type).Count;
    }

    /// <summary>
    /// Gets the average constructor arity.
    /// </summary>
    public double GetAverageConstructorArity(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        IReadOnlyCollection<ConstructorInfo> constructors = GetConstructors(type);

        if (constructors.Count == 0)
            return 0;

        return constructors.Average(c => c.GetParameters().Length);
    }

    /// <summary>
    /// Gets the constructor having the greatest number of parameters.
    /// </summary>
    public ConstructorInfo? GetLargestConstructor(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .OrderByDescending(c => c.GetParameters().Length)
            .FirstOrDefault();
    }

    /// <summary>
    /// Gets the constructor having the smallest number of parameters.
    /// </summary>
    public ConstructorInfo? GetSmallestConstructor(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type).OrderBy(c => c.GetParameters().Length).FirstOrDefault();
    }

    #endregion

    #region Overloads

    /// <summary>
    /// Determines whether constructor overloads exist.
    /// </summary>
    public bool HasOverloadedConstructors(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructorCount(type) > 1;
    }

    /// <summary>
    /// Determines whether constructor signatures are unique.
    /// </summary>
    public bool HasUniqueConstructorSignatures(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        string[] signatures = [.. GetConstructors(type).Select(BuildConstructorSignature)];

        return signatures.Length == signatures.Distinct().Count();
    }

    /// <summary>
    /// Determines whether duplicated constructor signatures exist.
    /// </summary>
    public bool HasDuplicatedConstructorSignatures(Type type)
    {
        return !HasUniqueConstructorSignatures(type);
    }

    #endregion

    #region Dependency Injection

    /// <summary>
    /// Counts injected dependencies.
    /// </summary>
    public int CountInjectedDependencies(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetPublicConstructors(type)
            .SelectMany(c => c.GetParameters())
            .Count();
    }

    /// <summary>
    /// Determines whether the type is constructor injection friendly.
    /// </summary>
    public bool IsDependencyInjectionFriendly(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        var constructors = GetPublicConstructors(type);

        if (constructors.Count != 1)
            return false;

        var constructor = constructors.Single();

        return constructor.GetParameters()
            .All(p =>
                !p.IsOptional &&
                !p.ParameterType.IsPointer &&
                !p.ParameterType.IsByRef);
    }

    /// <summary>
    /// Determines whether IServiceProvider is injected.
    /// </summary>
    public bool UsesServiceLocator(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return ContainsParameter(type, typeof(IServiceProvider));
    }

    #endregion

    #region Complexity

    /// <summary>
    /// Determines whether constructor complexity exceeds the specified threshold.
    /// </summary>
    public bool HasHighConstructorComplexity(
        Type type,
        int threshold = 8)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetMaximumParameterCount(type) > threshold;
    }

    /// <summary>
    /// Determines whether constructor complexity is below the specified threshold.
    /// </summary>
    public bool HasLowConstructorComplexity(
        Type type,
        int threshold = 3)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetMaximumParameterCount(type) <= threshold;
    }

    /// <summary>
    /// Determines whether constructor complexity is within the supplied range.
    /// </summary>
    public bool HasConstructorComplexityBetween(
        Type type,
        int minimum,
        int maximum)
    {
        ArgumentNullException.ThrowIfNull(type);

        int value = GetMaximumParameterCount(type);

        return value >= minimum &&
               value <= maximum;
    }

    #endregion

    #region Constructor ranking

    /// <summary>
    /// Gets constructors ordered by ascending complexity.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> GetConstructorsOrderedByComplexity(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .OrderBy(c => c.GetParameters().Length)
            .ToArray();
    }

    /// <summary>
    /// Gets constructors ordered by descending complexity.
    /// </summary>
    public IReadOnlyCollection<ConstructorInfo> GetConstructorsOrderedByDescendingComplexity(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return GetConstructors(type)
            .OrderByDescending(c => c.GetParameters().Length)
            .ToArray();
    }

    #endregion
}
