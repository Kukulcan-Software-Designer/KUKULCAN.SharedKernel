using KUKULCAN.SharedKernel.Results;

namespace KUKULCAN.SharedKernel.Exceptions;

/// <summary>
/// Represents an exception thrown when access to a resource is forbidden.
/// </summary>
public sealed class ForbiddenException : SharedKernelException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
    /// </summary>
    /// <param name="error">
    /// Forbidden error.
    /// </param>
    public ForbiddenException(Error error) : base(error)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ForbiddenException"/> class.
    /// </summary>
    /// <param name="error">
    /// Forbidden error.
    /// </param>
    /// <param name="innerException">
    /// The exception that caused the current exception.
    /// </param>
    public ForbiddenException(Error error, Exception? innerException) : base(error, innerException)
    {
    }
}
