using System;

namespace KUKULCAN.SharedKernel.Results;

/// <summary>
/// Represents the outcome of an operation.
/// </summary>
public  class Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="isSuccess">
    /// Indicates whether the operation succeeded.
    /// </param>
    /// <param name="error">
    /// Error associated with the operation.
    /// </param>
    /// <exception cref="ArgumentException">
    /// The success state and error are inconsistent.
    /// </exception>
    protected Result(bool isSuccess, Error error)
    {
        ArgumentNullException.ThrowIfNull(error);

        switch (isSuccess)
        {
            case true when error != Error.None:
                throw new ArgumentException("A successful result cannot contain an error.", nameof(error));
            case false when error == Error.None:
                throw new ArgumentException("A failed result must contain an error.", nameof(error));
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the operation succeeded.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets the operation error.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns>
    /// A successful <see cref="Result"/>.
    /// </returns>
    public static Result Success()
    {
        return new Result(true, Error.None);
    }

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    /// <param name="error">
    /// Operation error.
    /// </param>
    /// <returns>
    /// A failed <see cref="Result"/>.
    /// </returns>
    public static Result Failure(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);

        return new Result(false, error);
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
            ? "Success"
            : $"Failure: {Error}";
    }
}
