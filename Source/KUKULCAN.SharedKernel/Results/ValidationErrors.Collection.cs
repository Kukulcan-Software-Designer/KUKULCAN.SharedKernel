using System;
using KUKULCAN.SharedKernel.Results.Internals;

namespace KUKULCAN.SharedKernel.Results;

/// <summary>
/// Provides factory methods for collection validation errors.
/// </summary>
public static partial class ValidationErrors
{
    /// <summary>
    /// Creates an error indicating that a collection must be empty.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is null, empty or consists only of white-space characters.
    /// </exception>
    public static Error CollectionMustBeEmpty(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(
            CommonErrorCodes.CollectionMustBeEmpty,
            ValidationMessages.CollectionMustBeEmpty(propertyName));
    }

    /// <summary>
    /// Creates an error indicating that a collection cannot be empty.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is null, empty or consists only of white-space characters.
    /// </exception>
    public static Error CollectionMustNotBeEmpty(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(
            CommonErrorCodes.CollectionMustNotBeEmpty,
            ValidationMessages.CollectionMustNotBeEmpty(propertyName));
    }

    /// <summary>
    /// Creates an error indicating that a collection contains duplicate values.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is null, empty or consists only of white-space characters.
    /// </exception>
    public static Error Duplicate(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(
            CommonErrorCodes.Duplicate,
            ValidationMessages.Duplicate(propertyName));
    }

    /// <summary>
    /// Creates an error indicating that a collection contains an invalid item.
    /// </summary>
    /// <param name="propertyName">
    /// Name of the validated property.
    /// </param>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="propertyName"/> is null, empty or consists only of white-space characters.
    /// </exception>
    public static Error InvalidCollectionItem(string propertyName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

        return new Error(
            CommonErrorCodes.InvalidCollectionItem,
            ValidationMessages.InvalidCollectionItem(propertyName));
    }
}
