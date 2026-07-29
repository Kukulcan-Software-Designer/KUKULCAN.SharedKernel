using System;

namespace KUKULCAN.SharedKernel.Guards;

/// <summary>
/// Provides guard methods for validating arguments that are not covered by
/// the .NET Base Class Library.
/// </summary>
public static class Guard
{
    /// <summary>
    /// Ensures that the specified value is not the default value of its type.
    /// </summary>
    /// <typeparam name="T">
    /// Value type.
    /// </typeparam>
    /// <param name="value">
    /// Value to validate.
    /// </param>
    /// <param name="parameterName">
    /// Name of the parameter.
    /// </param>
    /// <returns>
    /// The validated value.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// The value is the default value of <typeparamref name="T"/>.
    /// </exception>
    public static T NotDefault<T>(T value, [CallerArgumentExpression(nameof(value))] string? parameterName = null) where T : struct
    {
        return EqualityComparer<T>.Default.Equals(value, default) ? throw new ArgumentException("The value cannot be the default value.", parameterName) : value;
    }

    /// <summary>
    /// Ensures that the specified <see cref="Guid"/> is not empty.
    /// </summary>
    /// <param name="value">
    /// Guid to validate.
    /// </param>
    /// <param name="parameterName">
    /// Name of the parameter.
    /// </param>
    /// <returns>
    /// The validated Guid.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// The Guid is <see cref="Guid.Empty"/>.
    /// </exception>
    public static Guid NotEmpty(Guid value, [CallerArgumentExpression(nameof(value))] string? parameterName = null)
    {
        return value == Guid.Empty ? throw new ArgumentException("The Guid cannot be empty.", parameterName) : value;
    }

    /// <summary>
    /// Ensures that the specified collection is not empty.
    /// </summary>
    /// <typeparam name="T">
    /// Collection item type.
    /// </typeparam>
    /// <param name="collection">
    /// Collection to validate.
    /// </param>
    /// <param name="parameterName">
    /// Name of the parameter.
    /// </param>
    /// <returns>
    /// The validated collection.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// The collection is <see langword="null"/>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// The collection is empty.
    /// </exception>
    public static IReadOnlyCollection<T> NotEmpty<T>(IReadOnlyCollection<T> collection,
        [CallerArgumentExpression(nameof(collection))] string? parameterName = null)
    {
        ArgumentNullException.ThrowIfNull(collection);

        return collection.Count == 0 ? throw new ArgumentException("The collection cannot be empty.", parameterName) : collection;
    }
}
