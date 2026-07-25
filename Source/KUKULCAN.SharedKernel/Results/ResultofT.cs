using System;
using KUKULCAN.SharedKernel.Results.Internals;

namespace KUKULCAN.SharedKernel.Results;

/// <summary>
/// Represents the outcome of an operation that returns a value.
/// </summary>
/// <typeparam name="T">
/// Type of the returned value.
/// </typeparam>
public sealed class Result<T> : Result
{
    private readonly T? _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    /// <param name="value">
    /// Operation result value.
    /// </param>
    /// <param name="isSuccess">
    /// Indicates whether the operation succeeded.
    /// </param>
    /// <param name="error">
    /// Operation error.
    /// </param>
    private Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    /// <summary>
    /// Gets the operation result value.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// The result does not contain a value because the operation failed.
    /// </exception>
    public T Value
    {
        get
        {
            return IsFailure
                ? throw new InvalidOperationException(CommonMessages.ResultValueUnavailable())
                : _value!;
        }
    }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <param name="value">
    /// Operation result value.
    /// </param>
    /// <returns>
    /// A successful <see cref="Result{T}"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="value"/> is <see langword="null"/> for a reference type.
    /// </exception>
    public static Result<T> Success(T value)
    {
        return value is null ? throw new ArgumentNullException(nameof(value)) : new Result<T>(value, true, Error.None);
    }

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    /// <param name="error">
    /// Operation error.
    /// </param>
    /// <returns>
    /// A failed <see cref="Result{T}"/>.
    /// </returns>
    public static new Result<T> Failure(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);

        return new Result<T>(default, false, error);
    }

    /// <summary>
    /// Returns a string representation of the current result.
    /// </summary>
    /// <returns>
    /// A string representation of the current result.
    /// </returns>
    public override string ToString()
    {
        return IsSuccess
            ? $"Success ({Value})"
            : $"Failure: {Error}";
    }
}
