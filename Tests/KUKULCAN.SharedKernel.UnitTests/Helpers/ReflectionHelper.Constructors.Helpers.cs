using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides helper operations used while inspecting constructors.
/// </summary>
public partial class ReflectionHelper
{
    /// <summary>
    /// Determines whether the specified constructor is public.
    /// </summary>
    /// <param name="constructor">
    /// Constructor to inspect.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when the constructor is public;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    protected static bool IsPublicConstructor(ConstructorInfo constructor)
    {
        ArgumentNullException.ThrowIfNull(constructor);

        return constructor.IsPublic;
    }

    /// <summary>
    /// Determines whether the specified constructor is static.
    /// </summary>
    /// <param name="constructor">
    /// Constructor to inspect.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when the constructor is static;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    protected static bool IsStaticConstructor(ConstructorInfo constructor)
    {
        ArgumentNullException.ThrowIfNull(constructor);

        return constructor.IsStatic;
    }

    /// <summary>
    /// Determines whether the specified constructor has no parameters.
    /// </summary>
    /// <param name="constructor">
    /// Constructor to inspect.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when the constructor has no parameters;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    protected static bool HasNoParameters(ConstructorInfo constructor)
    {
        ArgumentNullException.ThrowIfNull(constructor);

        return constructor.GetParameters().Length == 0;
    }

    /// <summary>
    /// Determines whether the specified constructor has parameters.
    /// </summary>
    /// <param name="constructor">
    /// Constructor to inspect.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when the constructor has one or more parameters;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    protected static bool HasParameters(ConstructorInfo constructor)
    {
        ArgumentNullException.ThrowIfNull(constructor);

        return constructor.GetParameters().Length > 0;
    }

    /// <summary>
    /// Gets the parameters declared by the specified constructor.
    /// </summary>
    /// <param name="constructor">
    /// Constructor to inspect.
    /// </param>
    /// <returns>
    /// The constructor parameters.
    /// </returns>
    protected static IReadOnlyList<ParameterInfo> GetConstructorParameters(
        ConstructorInfo constructor)
    {
        ArgumentNullException.ThrowIfNull(constructor);

        return constructor.GetParameters();
    }

    /// <summary>
    /// Determines whether the specified constructor declares a parameter
    /// with the specified name.
    /// </summary>
    /// <param name="constructor">
    /// Constructor to inspect.
    /// </param>
    /// <param name="parameterName">
    /// Parameter name to find.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when a matching parameter exists;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    protected static bool HasParameter(
        ConstructorInfo constructor,
        string parameterName)
    {
        ArgumentNullException.ThrowIfNull(constructor);
        ArgumentException.ThrowIfNullOrWhiteSpace(parameterName);

        return constructor
            .GetParameters()
            .Any(parameter =>
                string.Equals(
                    parameter.Name,
                    parameterName,
                    StringComparison.Ordinal));
    }

    /// <summary>
    /// Finds a parameter with the specified name.
    /// </summary>
    /// <param name="constructor">
    /// Constructor to inspect.
    /// </param>
    /// <param name="parameterName">
    /// Parameter name to find.
    /// </param>
    /// <returns>
    /// The matching parameter, or <see langword="null"/> when no matching
    /// parameter exists.
    /// </returns>
    protected static ParameterInfo? FindParameter(
        ConstructorInfo constructor,
        string parameterName)
    {
        ArgumentNullException.ThrowIfNull(constructor);
        ArgumentException.ThrowIfNullOrWhiteSpace(parameterName);

        return constructor
            .GetParameters()
            .FirstOrDefault(parameter =>
                string.Equals(
                    parameter.Name,
                    parameterName,
                    StringComparison.Ordinal));
    }

    /// <summary>
    /// Determines whether the specified constructor contains a parameter
    /// of the specified type.
    /// </summary>
    /// <param name="constructor">
    /// Constructor to inspect.
    /// </param>
    /// <param name="parameterType">
    /// Parameter type to find.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when a matching parameter exists;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    protected static bool HasParameterType(
        ConstructorInfo constructor,
        Type parameterType)
    {
        ArgumentNullException.ThrowIfNull(constructor);
        ArgumentNullException.ThrowIfNull(parameterType);

        return constructor
            .GetParameters()
            .Any(parameter =>
                parameter.ParameterType == parameterType);
    }

    /// <summary>
    /// Determines whether the constructor has exactly the specified
    /// parameter types in the specified order.
    /// </summary>
    /// <param name="constructor">
    /// Constructor to inspect.
    /// </param>
    /// <param name="parameterTypes">
    /// Expected parameter types.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when the parameter types match exactly;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    protected static bool HasParameterTypes(
        ConstructorInfo constructor,
        params Type[] parameterTypes)
    {
        ArgumentNullException.ThrowIfNull(constructor);
        ArgumentNullException.ThrowIfNull(parameterTypes);

        var parameters = constructor.GetParameters();

        if (parameters.Length != parameterTypes.Length)
            return false;

        for (var index = 0; index < parameters.Length; index++)
        {
            if (parameters[index].ParameterType != parameterTypes[index])
                return false;
        }

        return true;
    }

    /// <summary>
    /// Determines whether the constructor has exactly the specified
    /// number of parameters.
    /// </summary>
    /// <param name="constructor">
    /// Constructor to inspect.
    /// </param>
    /// <param name="parameterCount">
    /// Expected parameter count.
    /// </param>
    /// <returns>
    /// <see langword="true"/> when the constructor contains the expected
    /// number of parameters; otherwise, <see langword="false"/>.
    /// </returns>
    protected static bool HasParameterCount(
        ConstructorInfo constructor,
        int parameterCount)
    {
        ArgumentNullException.ThrowIfNull(constructor);
        ArgumentOutOfRangeException.ThrowIfNegative(parameterCount);

        return constructor.GetParameters().Length == parameterCount;
    }
}
