namespace KUKULCAN.SharedKernel.Validations;

/// <summary>
/// Specifies the severity level of a validation failure.
/// </summary>
public enum ValidationSeverity
{
    /// <summary>
    /// Indicates informational feedback that does not affect the validity of the operation.
    /// </summary>
    Information = 0,

    /// <summary>
    /// Indicates a warning that should be reviewed but does not necessarily prevent the operation.
    /// </summary>
    Warning = 1,

    /// <summary>
    /// Indicates a validation error that prevents the operation from succeeding.
    /// </summary>
    Error = 2
}
