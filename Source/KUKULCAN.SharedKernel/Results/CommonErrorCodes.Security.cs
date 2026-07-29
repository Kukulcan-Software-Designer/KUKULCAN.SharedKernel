namespace KUKULCAN.SharedKernel.Results;

/// <summary>
/// Defines standard security error codes.
/// </summary>
public static partial class CommonErrorCodes
{
    /// <summary>
    /// Authentication is required.
    /// </summary>
    public const string Unauthorized = "SECURITY.UNAUTHORIZED";

    /// <summary>
    /// Access to the requested resource is forbidden.
    /// </summary>
    public const string Forbidden = "SECURITY.FORBIDDEN";
}
