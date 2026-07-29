using System;
using KUKULCAN.SharedKernel.Results;

namespace KUKULCAN.SharedKernel.Exceptions;

/// <summary>
/// Represents an exception thrown when a requested resource cannot be found.
/// </summary>
public sealed class NotFoundException : SharedKernelException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class.
    /// </summary>
    /// <param name="error">
    /// Not found error.
    /// </param>
    public NotFoundException(Error error) : base(error)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class.
    /// </summary>
    /// <param name="error">
    /// Not found error.
    /// </param>
    /// <param name="innerException">
    /// The exception that caused the current exception.
    /// </param>
    public NotFoundException(Error error, Exception? innerException) : base(error, innerException)
    {
    }
}
