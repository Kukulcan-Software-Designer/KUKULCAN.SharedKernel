using System;
using KUKULCAN.SharedKernel.Results;

namespace KUKULCAN.SharedKernel.Exceptions;

/// <summary>
/// Represents an exception thrown when an unexpected error occurs.
/// </summary>
public sealed class UnexpectedException : SharedKernelException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnexpectedException"/> class.
    /// </summary>
    /// <param name="error">
    /// Unexpected error.
    /// </param>
    public UnexpectedException(Error error) : base(error)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnexpectedException"/> class.
    /// </summary>
    /// <param name="error">
    /// Unexpected error.
    /// </param>
    /// <param name="innerException">
    /// The exception that caused the current exception.
    /// </param>
    public UnexpectedException(Error error, Exception? innerException) : base(error, innerException)
    {
    }
}
