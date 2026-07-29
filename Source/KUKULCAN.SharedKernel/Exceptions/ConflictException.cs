using System;
using KUKULCAN.SharedKernel.Results;

namespace KUKULCAN.SharedKernel.Exceptions;

/// <summary>
/// Represents an exception thrown when an operation cannot be completed because of a conflict.
/// </summary>
public sealed class ConflictException : SharedKernelException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConflictException"/> class.
    /// </summary>
    /// <param name="error">
    /// Conflict error.
    /// </param>
    public ConflictException(Error error) : base(error)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConflictException"/> class.
    /// </summary>
    /// <param name="error">
    /// Conflict error.
    /// </param>
    /// <param name="innerException">
    /// The exception that caused the current exception.
    /// </param>
    public ConflictException(Error error, Exception? innerException) : base(error, innerException)
    {
    }
}
