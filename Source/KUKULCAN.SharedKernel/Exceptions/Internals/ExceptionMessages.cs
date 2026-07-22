namespace KUKULCAN.SharedKernel.Exceptions.Internals;

/// <summary>
/// Provides internal exception messages used to enforce API invariants.
/// </summary>
internal static class ExceptionMessages
{
    /// <summary>
    /// Returns the message indicating that the validation result must contain one or more validation failures.
    /// </summary>
    /// <returns>
    /// The formatted message.
    /// </returns>
    internal static string ValidationResultMustContainFailures()
    {
        return "The validation result must contain one or more validation failures.";
    }
}
