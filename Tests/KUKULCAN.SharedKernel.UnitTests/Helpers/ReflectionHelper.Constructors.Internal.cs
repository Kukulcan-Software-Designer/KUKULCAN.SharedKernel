using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides internal constructor-analysis operations used by
/// <see cref="ReflectionHelper"/>.
/// </summary>
public partial class ReflectionHelper
{
    /// <summary>
    /// Gets all constructors declared directly by the specified type.
    /// </summary>
    /// <param name="type">
    /// Type whose constructors are inspected.
    /// </param>
    /// <param name="bindingFlags">
    /// Binding flags controlling constructor visibility.
    /// </param>
    /// <returns>
    /// Constructors declared directly by the specified type.
    /// </returns>
    private static IReadOnlyList<ConstructorInfo> GetDeclaredConstructors(
        Type type,
        BindingFlags bindingFlags)
    {
        ArgumentNullException.ThrowIfNull(type);

        return type
            .GetConstructors(bindingFlags)
            .ToArray();
    }

    /// <summary>
    /// Gets the constructors declared directly by the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// Type whose constructors are inspected.
    /// </typeparam>
    /// <param name="bindingFlags">
    /// Binding flags controlling constructor visibility.
    /// </param>
    /// <returns>
    /// Constructors declared directly by <typeparamref name="T"/>.
    /// </returns>
    private static IReadOnlyList<ConstructorInfo> GetDeclaredConstructors<T>(
        BindingFlags bindingFlags)
    {
        return GetDeclaredConstructors(
            typeof(T),
            bindingFlags);
    }

    /// <summary>
    /// Finds a declared constructor matching the specified parameter types.
    /// </summary>
    /// <param name="type">
    /// Type whose constructors are inspected.
    /// </param>
    /// <param name="bindingFlags">
    /// Binding flags controlling constructor visibility.
    /// </param>
    /// <param name="parameterTypes">
    /// Expected parameter types.
    /// </param>
    /// <returns>
    /// The matching constructor, or <see langword="null"/> when no matching
    /// constructor exists.
    /// </returns>
    private static ConstructorInfo? FindDeclaredConstructor(
        Type type,
        BindingFlags bindingFlags,
        params Type[] parameterTypes)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(parameterTypes);

        return GetDeclaredConstructors(type, bindingFlags)
            .FirstOrDefault(
                constructor => HasParameterTypes(
                    constructor,
                    parameterTypes));
    }

    /// <summary>
    /// Finds a declared constructor matching the specified parameter types.
    /// </summary>
    /// <typeparam name="T">
    /// Type whose constructors are inspected.
    /// </typeparam>
    /// <param name="bindingFlags">
    /// Binding flags controlling constructor visibility.
    /// </param>
    /// <param name="parameterTypes">
    /// Expected parameter types.
    /// </param>
    /// <returns>
    /// The matching constructor, or <see langword="null"/> when no matching
    /// constructor exists.
    /// </returns>
    private static ConstructorInfo? FindDeclaredConstructor<T>(
        BindingFlags bindingFlags,
        params Type[] parameterTypes)
    {
        return FindDeclaredConstructor(
            typeof(T),
            bindingFlags,
            parameterTypes);
    }

    /// <summary>
    /// Finds a public instance constructor matching the specified parameter
    /// types.
    /// </summary>
    /// <typeparam name="T">
    /// Type whose constructors are inspected.
    /// </typeparam>
    /// <param name="parameterTypes">
    /// Expected parameter types.
    /// </param>
    /// <returns>
    /// The matching constructor, or <see langword="null"/> when no matching
    /// constructor exists.
    /// </returns>
    private static ConstructorInfo? FindPublicInstanceConstructor<T>(
        params Type[] parameterTypes)
    {
        return FindDeclaredConstructor<T>(
            PublicInstanceConstructorFlags,
            parameterTypes);
    }

    /// <summary>
    /// Finds a public static constructor matching the specified parameter
    /// types.
    /// </summary>
    /// <typeparam name="T">
    /// Type whose constructors are inspected.
    /// </typeparam>
    /// <param name="parameterTypes">
    /// Expected parameter types.
    /// </param>
    /// <returns>
    /// The matching constructor, or <see langword="null"/> when no matching
    /// constructor exists.
    /// </returns>
    private static ConstructorInfo? FindPublicStaticConstructor<T>(
        params Type[] parameterTypes)
    {
        return FindDeclaredConstructor<T>(
            PublicStaticConstructorFlags,
            parameterTypes);
    }

    /// <summary>
    /// Finds a public constructor matching the specified parameter types.
    /// </summary>
    /// <typeparam name="T">
    /// Type whose constructors are inspected.
    /// </typeparam>
    /// <param name="parameterTypes">
    /// Expected parameter types.
    /// </param>
    /// <returns>
    /// The matching constructor, or <see langword="null"/> when no matching
    /// constructor exists.
    /// </returns>
    private static ConstructorInfo? FindPublicConstructor<T>(
        params Type[] parameterTypes)
    {
        return FindDeclaredConstructor<T>(
            PublicConstructorFlags,
            parameterTypes);
    }

    /// <summary>
    /// Finds a public parameterless instance constructor.
    /// </summary>
    /// <typeparam name="T">
    /// Type whose constructors are inspected.
    /// </typeparam>
    /// <returns>
    /// The parameterless constructor, or <see langword="null"/> when it does
    /// not exist.
    /// </returns>
    private static ConstructorInfo? FindPublicParameterlessConstructor<T>()
    {
        return FindPublicInstanceConstructor<T>();
    }

    /// <summary>
    /// Gets a required public parameterless instance constructor.
    /// </summary>
    /// <typeparam name="T">
    /// Type whose constructor is inspected.
    /// </typeparam>
    /// <returns>
    /// The required parameterless constructor.
    /// </returns>
    /// <exception cref="MissingMethodException">
    /// Thrown when the type does not expose a public parameterless constructor.
    /// </exception>
    private static ConstructorInfo GetRequiredPublicParameterlessConstructor<T>()
    {
        return FindPublicParameterlessConstructor<T>()
            ?? throw new MissingMethodException(
                $"{typeof(T).FullName} does not expose a public parameterless constructor.");
    }

    /// <summary>
    /// Determines whether the specified type exposes a public parameterless
    /// constructor.
    /// </summary>
    /// <typeparam name="T">
    /// Type to inspect.
    /// </typeparam>
    /// <returns>
    /// <see langword="true"/> when a public parameterless constructor exists;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    private static bool HasPublicParameterlessConstructor<T>()
    {
        return FindPublicParameterlessConstructor<T>() is not null;
    }

    /// <summary>
    /// Gets a constructor signature suitable for diagnostic output.
    /// </summary>
    /// <param name="constructor">
    /// Constructor to format.
    /// </param>
    /// <returns>
    /// A human-readable constructor signature.
    /// </returns>
    private static string FormatConstructorSignature(
        ConstructorInfo constructor)
    {
        ArgumentNullException.ThrowIfNull(constructor);

        return BuildConstructorSignature(constructor);
    }

    /// <summary>
    /// Gets a diagnostic description for a constructor.
    /// </summary>
    /// <param name="constructor">
    /// Constructor to describe.
    /// </param>
    /// <returns>
    /// Diagnostic constructor description.
    /// </returns>
    private static string DescribeConstructor(
        ConstructorInfo constructor)
    {
        ArgumentNullException.ThrowIfNull(constructor);

        return $"{constructor.DeclaringType?.FullName ?? "<unknown>"}::{FormatConstructorSignature(constructor)}";
    }
}
