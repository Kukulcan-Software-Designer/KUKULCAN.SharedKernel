using KUKULCAN.SharedKernel.Results;

namespace KUKULCAN.SharedKernel.Exceptions;

/// <summary>
/// Represents an exception thrown when a concurrency conflict is detected.
/// </summary>
public sealed class ConcurrencyException : SharedKernelException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConcurrencyException"/> class.
    /// </summary>
    /// <param name="error">
    /// Concurrency error.
    /// </param>
    public ConcurrencyException(Error error) : base(error)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ConcurrencyException"/> class.
    /// </summary>
    /// <param name="error">
    /// Concurrency error.
    /// </param>
    /// <param name="innerException">
    /// The exception that caused the current exception.
    /// </param>
    public ConcurrencyException(Error error, Exception? innerException) : base(error, innerException)
    {
    }
}
