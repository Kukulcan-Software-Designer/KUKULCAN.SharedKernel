namespace KUKULCAN.SharedKernel.Identifiers.Internals;

/// <summary>
/// Provides standard messages used by the Identifiers subsystem.
/// </summary>
internal static class IdentifierMessages
{
    /// <summary>
    /// Gets the message used when a Guid identifier is empty.
    /// </summary>
    internal static string GuidCannotBeEmpty() => "Identifier value cannot be Guid.Empty.";

    /// <summary>
    /// Gets the message used when an Ulid identifier is empty.
    /// </summary>
    internal static string UlidCannotBeEmpty() => "Identifier value cannot be Ulid.Empty.";

    /// <summary>
    /// Gets the message used when an integer identifier is not greater than zero.
    /// </summary>
    internal static string IntegerMustBeGreaterThanZero() => "Identifier value must be greater than zero.";

    /// <summary>
    /// Gets the message used when a long identifier is not greater than zero.
    /// </summary>
    internal static string LongMustBeGreaterThanZero() => "Identifier value must be greater than zero.";
}
