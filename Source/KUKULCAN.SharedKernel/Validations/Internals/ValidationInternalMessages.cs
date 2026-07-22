namespace KUKULCAN.SharedKernel.Validations.Internals;

/// <summary>
/// Provides internal validation messages used to enforce API invariants.
/// </summary>
internal static class ValidationInternalMessages
{
    /// <summary>
    /// Returns the message indicating that a validation result must contain one or more validation failures.
    /// </summary>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string FailuresCannotBeEmpty()
    {
        return "The collection of validation failures cannot be empty.";

    }

    /// <summary>
    /// Returns the message indicating that the specified expression must reference a property.
    /// </summary>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string ExpressionMustReferenceProperty()
    {
        return "The expression must reference a property.";
    }

    /// <summary>
    /// Returns the message indicating that the specified property must be readable.
    /// </summary>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string PropertyMustBeReadable()
    {
        return "The property must be readable.";
    }
}
