namespace KUKULCAN.SharedKernel.Validations;

/// <summary>
/// Represents a single validation failure.
/// </summary>
/// <param name="PropertyName">
/// Name of the property that failed validation.
/// </param>
/// <param name="ErrorCode">
/// Validation error code.
/// </param>
/// <param name="Message">
/// Human-readable validation error message.
/// </param>
/// <param name="Severity">
/// Validation severity.
/// </param>
/// <param name="AttemptedValue">
/// Value that failed validation.
/// </param>
public sealed record ValidationFailure (string PropertyName, string ErrorCode, string Message,
    object? AttemptedValue = null, ValidationSeverity Severity = ValidationSeverity.Error)
{
    /// <summary>
    /// Returns a string representation of the validation failure.
    /// </summary>
    /// <returns>
    /// A string containing the property name and the validation message.
    /// </returns>
    public override string ToString()
    {
        return $"{PropertyName}: {Message}";
    }
}
