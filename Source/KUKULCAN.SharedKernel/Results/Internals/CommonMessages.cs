namespace KUKULCAN.SharedKernel.Results.Internals;

/// <summary>
/// Provides the default messages for common errors.
/// </summary>
internal static class CommonMessages
{
    /// <summary>
    /// Returns the message indicating that no error occurred.
    /// </summary>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string None()
    {
        return "No error.";
    }

    /// <summary>
    /// Returns the message indicating that the requested resource was not found.
    /// </summary>
    /// <param name="resource">
    /// Resource name.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    public static string NotFound(string resource)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(resource);

        return $"The resource '{resource}' was not found.";
    }

    /// <summary>
    /// Returns the message indicating that the specified resource already exists.
    /// </summary>
    /// <param name="resource">
    /// Resource name.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    public static string Conflict(string resource)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(resource);

        return $"The resource '{resource}' already exists.";
    }

    /// <summary>
    /// Returns the message indicating that authentication is required.
    /// </summary>
    /// <returns>
    /// The formatted message.
    /// </returns>
    public static string Unauthorized()
    {
        return "Authentication is required.";
    }

    /// <summary>
    /// Returns the message indicating that access to the requested resource is forbidden.
    /// </summary>
    /// <returns>
    /// The formatted message.
    /// </returns>
    public static string Forbidden()
    {
        return "Access to the requested resource is forbidden.";
    }

    /// <summary>
    /// Returns the message indicating that an unexpected error occurred.
    /// </summary>
    /// <returns>
    /// The formatted message.
    /// </returns>
    public static string Unexpected()
    {
        return "An unexpected error has occurred.";
    }

    /// <summary>
    /// Returns the message indicating that the operation timed out.
    /// </summary>
    /// <returns>
    /// The formatted message.
    /// </returns>
    public static string Timeout()
    {
        return "The operation timed out.";
    }

    /// <summary>
    /// Returns the message indicating that the operation was cancelled.
    /// </summary>
    /// <returns>
    /// The formatted message.
    /// </returns>
    public static string Cancelled()
    {
        return "The operation was cancelled.";
    }

    /// <summary>
    /// Returns the message indicating that the operation was unknown.
    /// </summary>
    /// <returns>
    /// The formatted message.
    /// </returns>
    public static string Unknown()
    {
        return "An unknown error occurred.";
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string NotSupported(string operation)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(operation);

        return $"Operation '{operation}' is not supported.";
    }

    /// <summary>
    /// Returns the message indicating that the specified operation is invalid.
    /// </summary>
    /// <param name="operation">
    /// Operation name.
    /// </param>
    /// <returns>
    /// The formatted message.
    /// </returns>
    public static string InvalidOperation(string operation)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(operation);

        return $"The operation '{operation}' is not valid.";
    }

    /// <summary>
    /// Returns the message indicating that the value of a failed result cannot be accessed.
    /// </summary>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string ResultValueUnavailable()
    {
        return "The value of a failed result cannot be accessed.";
    }
}
