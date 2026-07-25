namespace KUKULCAN.SharedKernel.Maybe.Internals;

/// <summary>
/// Provides internal messages used by the Maybe module.
/// </summary>
internal static class MaybeMessages
{
    /// <summary>
    /// Gets the message used when attempting to access
    /// the value of an empty <see cref="Maybe{T}"/>.
    /// </summary>
    internal static string NoValuePresent()
    {
        return "The Maybe instance does not contain a value.";
    }
}
