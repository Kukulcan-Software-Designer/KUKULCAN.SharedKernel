using System;
using System.Diagnostics.CodeAnalysis;
using KUKULCAN.SharedKernel.Maybe.Internals;

namespace KUKULCAN.SharedKernel.Maybe;

/// <summary>
/// Represents an optional value.
/// </summary>
/// <typeparam name="T">
/// Type of the wrapped value.
/// </typeparam>
public readonly struct Maybe<T> : IEquatable<Maybe<T>>
{
    private readonly T _value;
    private readonly bool _hasValue;

    private Maybe(T value, bool hasValue)
    {
        _value = value;
        _hasValue = hasValue;
    }

    /// <summary>
    /// Gets an empty <see cref="Maybe{T}"/>.
    /// </summary>
    public static Maybe<T> None => new(default!, false);

    /// <summary>
    /// Gets a value indicating whether the instance contains a value.
    /// </summary>
    public bool HasValue => _hasValue;

    /// <summary>
    /// Gets a value indicating whether the instance does not contain a value.
    /// </summary>
    public bool HasNoValue => !_hasValue;

    /// <summary>
    /// Gets the contained value.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// The instance does not contain a value.
    /// </exception>
    public T Value =>
        _hasValue
            ? _value
            : throw new InvalidOperationException(
                MaybeMessages.NoValuePresent());

    /// <summary>
    /// Attempts to retrieve the contained value.
    /// </summary>
    /// <param name="value">
    /// When this method returns, contains the wrapped value if present;
    /// otherwise the default value for <typeparamref name="T"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if a value exists;
    /// otherwise <see langword="false"/>.
    /// </returns>
    public bool TryGetValue(
        [MaybeNullWhen(false)] out T value)
    {
        value = _value;

        return _hasValue;
    }

    /// <summary>
    /// Gets the contained value or the specified default value.
    /// </summary>
    /// <param name="defaultValue">
    /// Value returned when the instance is empty.
    /// </param>
    /// <returns>
    /// The contained value if present;
    /// otherwise <paramref name="defaultValue"/>.
    /// </returns>
    public T GetValueOr(T defaultValue)
    {
        return _hasValue
            ? _value
            : defaultValue;
    }

    /// <summary>
    /// Gets the contained value or creates one using the specified factory.
    /// </summary>
    /// <param name="factory">
    /// Factory used when the instance is empty.
    /// </param>
    /// <returns>
    /// The contained value if present;
    /// otherwise the value produced by <paramref name="factory"/>.
    /// </returns>
    public T GetValueOr(Func<T> factory)
    {
        ArgumentNullException.ThrowIfNull(factory);

        return _hasValue
            ? _value
            : factory();
    }

    /// <inheritdoc/>
    public bool Equals(Maybe<T> other)
    {
        if (_hasValue != other._hasValue)
        {
            return false;
        }

        if (!_hasValue)
        {
            return true;
        }

        return EqualityComparer<T>.Default.Equals(
            _value,
            other._value);
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is Maybe<T> other &&
               Equals(other);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        if (!_hasValue)
        {
            return 0;
        }

        return EqualityComparer<T>.Default.GetHashCode(_value!);
    }

    /// <summary>
    /// Determines whether two optional values are equal.
    /// </summary>
    public static bool operator ==(
        Maybe<T> left,
        Maybe<T> right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two optional values are different.
    /// </summary>
    public static bool operator !=(
        Maybe<T> left,
        Maybe<T> right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    /// Converts a value to a <see cref="Maybe{T}"/>.
    /// </summary>
    /// <param name="value">
    /// Value to convert.
    /// </param>
    public static implicit operator Maybe<T>(T? value)
    {
        return value is null
            ? None
            : new Maybe<T>(value, true);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return _hasValue
            ? _value?.ToString() ?? string.Empty
            : "None";
    }
}
