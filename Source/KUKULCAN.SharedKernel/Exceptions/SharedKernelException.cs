using System;
using KUKULCAN.SharedKernel.Results;

namespace KUKULCAN.SharedKernel.Exceptions;

/// <summary>
/// Represents the base class for all SharedKernel exceptions.
/// </summary>
public abstract class SharedKernelException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SharedKernelException"/> class.
    /// </summary>
    /// <param name="error">
    /// Associated error.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="error"/> is <see langword="null"/>.
    /// </exception>
    protected SharedKernelException(Error error) : base(error is null
        ? throw new ArgumentNullException(nameof(error))
        : error.Description)
    {
        Error = error;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SharedKernelException"/> class.
    /// </summary>
    /// <param name="error">
    /// Associated error.
    /// </param>
    /// <param name="innerException">
    /// Inner exception.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="error"/> is <see langword="null"/>.
    /// </exception>
    protected SharedKernelException(Error error, Exception? innerException) : base(error is null
            ? throw new ArgumentNullException(nameof(error))
            : error.Description,
        innerException)
    {
        Error = error;
    }

    /// <summary>
    /// Gets the error associated with the exception.
    /// </summary>
    public Error Error { get; }
}
