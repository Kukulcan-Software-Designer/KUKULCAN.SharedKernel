namespace KUKULCAN.SharedKernel.Globalization.Abstractions;

/// <summary>
/// Defines the contract for retrieving localized text.
/// </summary>
public interface ITextLocalizer
{
    /// <summary>
    /// Gets the localized text associated with the specified key.
    /// </summary>
    /// <param name="key">
    /// Resource key.
    /// </param>
    /// <returns>
    /// Localized text if found; otherwise an implementation-defined fallback.
    /// </returns>
    string Get(string key);

    /// <summary>
    /// Gets the localized text associated with the specified key using the supplied arguments.
    /// </summary>
    /// <param name="key">
    /// Resource key.
    /// </param>
    /// <param name="arguments">
    /// Formatting arguments.
    /// </param>
    /// <returns>
    /// Formatted localized text.
    /// </returns>
    string Get(string key, params object?[] arguments);

    /// <summary>
    /// Attempts to retrieve the localized text associated with the specified key.
    /// </summary>
    /// <param name="key">
    /// Resource key.
    /// </param>
    /// <param name="value">
    /// Localized text if found.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the resource exists; otherwise <see langword="false"/>.
    /// </returns>
    bool TryGet(string key, out string value);
}
