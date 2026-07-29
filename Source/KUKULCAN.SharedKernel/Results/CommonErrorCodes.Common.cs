namespace KUKULCAN.SharedKernel.Results;

/// <summary>
/// Defines standard common error codes.
/// </summary>
public static partial class CommonErrorCodes
{
    /// <summary>
    /// The requested resource was not found.
    /// </summary>
    public const string NotFound = "COMMON.NOT_FOUND";

    /// <summary>
    /// A conflict occurred.
    /// </summary>
    public const string Conflict = "COMMON.CONFLICT";

    /// <summary>
    /// The operation is invalid.
    /// </summary>
    public const string InvalidOperation = "COMMON.INVALID_OPERATION";

    /// <summary>
    /// The requested operation is not supported.
    /// </summary>
    public const string NotSupported = "COMMON.NOT_SUPPORTED";
}
