using System;
using KUKULCAN.SharedKernel.Results;

namespace KUKULCAN.SharedKernel.Exceptions;

/// <summary>
/// Represents an exception thrown when authentication is required.
/// </summary>
public sealed class UnauthorizedException : SharedKernelException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
    /// </summary>
    /// <param name="error">
    /// Unauthorized error.
    /// </param>
    public UnauthorizedException(Error error) : base(error)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
    /// </summary>
    /// <param name="error">
    /// Unauthorized error.
    /// </param>
    /// <param name="innerException">
    /// The exception that caused the current exception.
    /// </param>
    public UnauthorizedException(Error error, Exception? innerException) : base(error, innerException)
    {
    }
}
