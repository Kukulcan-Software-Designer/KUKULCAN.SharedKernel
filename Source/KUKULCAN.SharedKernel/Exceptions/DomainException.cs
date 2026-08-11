using KUKULCAN.SharedKernel.Results;

namespace KUKULCAN.SharedKernel.Exceptions;

/// <summary>
/// Represents an exception caused by a domain rule violation.
/// </summary>
public sealed class DomainException : SharedKernelException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class.
    /// </summary>
    /// <param name="error">
    /// Domain error.
    /// </param>
    public DomainException(Error error) : base(error)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class.
    /// </summary>
    /// <param name="error">
    /// Domain error.
    /// </param>
    /// <param name="innerException">
    /// The exception that caused the current exception.
    /// </param>
    public DomainException(Error error, Exception? innerException) : base(error, innerException)
    {
    }
}
