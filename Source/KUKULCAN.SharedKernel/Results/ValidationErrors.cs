using KUKULCAN.SharedKernel.Results.Internals;

namespace KUKULCAN.SharedKernel.Results;

/// <summary>
/// Provides factory methods for validation errors.
/// </summary>
public static partial class ValidationErrors
{
    /// <summary>
    /// Creates an error indicating that one or more validation failures occurred.
    /// </summary>
    /// <returns>
    /// A validation <see cref="Error"/>.
    /// </returns>
    public static Error ValidationFailed()
    {
        return new Error(CommonErrorCodes.ValidationFailed, ValidationMessages.ValidationFailed());
    }
}
