namespace KUKULCAN.SharedKernel.Results;

/// <summary>
/// Defines standard infrastructure error codes.
/// </summary>
public static partial class CommonErrorCodes
{
    /// <summary>
    /// An unexpected error occurred.
    /// </summary>
    public const string Unexpected = "INFRASTRUCTURE.UNEXPECTED";

    /// <summary>
    /// The operation timed out.
    /// </summary>
    public const string Timeout = "INFRASTRUCTURE.TIMEOUT";

    /// <summary>
    /// The operation was cancelled.
    /// </summary>
    public const string Cancelled = "INFRASTRUCTURE.CANCELLED";
}
