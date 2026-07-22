using KUKULCAN.SharedKernel.Results.Internals;

namespace KUKULCAN.SharedKernel.Results;

/// <summary>
/// Provides factory methods for common errors.
/// </summary>
public static class CommonErrors
{
    /// <summary>
    /// Creates an error indicating that the requested resource was not found.
    /// </summary>
    /// <param name="resource">
    /// Resource name.
    /// </param>
    /// <returns>
    /// A <see cref="Error"/> representing the failure.
    /// </returns>
    public static Error NotFound(string resource)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(resource);

        return new Error(
            CommonErrorCodes.NotFound,
            CommonMessages.NotFound(resource));
    }

    /// <summary>
    /// Creates an error indicating that the specified resource already exists.
    /// </summary>
    /// <param name="resource">
    /// Resource name.
    /// </param>
    /// <returns>
    /// A <see cref="Error"/> representing the failure.
    /// </returns>
    public static Error Conflict(string resource)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(resource);

        return new Error(
            CommonErrorCodes.Conflict,
            CommonMessages.Conflict(resource));
    }

    /// <summary>
    /// Creates an error indicating that the requested operation is invalid.
    /// </summary>
    /// <param name="operation">
    /// Operation name.
    /// </param>
    /// <returns>
    /// A <see cref="Error"/> representing the failure.
    /// </returns>
    public static Error InvalidOperation(string operation)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(operation);

        return new Error(
            CommonErrorCodes.InvalidOperation,
            CommonMessages.InvalidOperation(operation));
    }

    /// <summary>
    /// Creates an error indicating that the requested operation is not supported.
    /// </summary>
    /// <param name="operation">
    /// Operation name.
    /// </param>
    /// <returns>
    /// A <see cref="Error"/> representing the failure.
    /// </returns>
    public static Error NotSupported(string operation)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(operation);

        return new Error(
            CommonErrorCodes.NotSupported,
            CommonMessages.NotSupported(operation));
    }

    /// <summary>
    /// Creates an error indicating that authentication is required.
    /// </summary>
    /// <returns>
    /// A <see cref="Error"/> representing the failure.
    /// </returns>
    public static Error Unauthorized()
    {
        return new Error(
            CommonErrorCodes.Unauthorized,
            CommonMessages.Unauthorized());
    }

    /// <summary>
    /// Creates an error indicating that access to the requested resource is forbidden.
    /// </summary>
    /// <returns>
    /// A <see cref="Error"/> representing the failure.
    /// </returns>
    public static Error Forbidden()
    {
        return new Error(
            CommonErrorCodes.Forbidden,
            CommonMessages.Forbidden());
    }

    /// <summary>
    /// Creates an error indicating that the operation timed out.
    /// </summary>
    /// <returns>
    /// A <see cref="Error"/> representing the failure.
    /// </returns>
    public static Error Timeout()
    {
        return new Error(
            CommonErrorCodes.Timeout,
            CommonMessages.Timeout());
    }

    /// <summary>
    /// Creates an error indicating that the operation was cancelled.
    /// </summary>
    /// <returns>
    /// A <see cref="Error"/> representing the failure.
    /// </returns>
    public static Error Cancelled()
    {
        return new Error(
            CommonErrorCodes.Cancelled,
            CommonMessages.Cancelled());
    }

    /// <summary>
    /// Creates an error indicating that an unexpected error occurred.
    /// </summary>
    /// <returns>
    /// A <see cref="Error"/> representing the failure.
    /// </returns>
    public static Error Unexpected()
    {
        return new Error(
            CommonErrorCodes.Unexpected,
            CommonMessages.Unexpected());
    }

    /// <summary>
    /// Creates an error indicating that the specified error is unknown.
    /// </summary>
    /// <returns>
    /// A <see cref="Error"/> representing the failure.
    /// </returns>
    public static Error Unknown()
    {
        return new Error(
            CommonErrorCodes.Unknown,
            CommonMessages.Unknown());
    }
}
