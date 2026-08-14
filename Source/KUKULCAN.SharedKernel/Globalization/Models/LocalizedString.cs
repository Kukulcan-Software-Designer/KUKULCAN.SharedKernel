namespace KUKULCAN.SharedKernel.Globalization.Models;

/// <summary>
/// Represents a localized text value and whether its resource was found.
/// </summary>
/// <param name="Key">
/// Translation resource key.
/// </param>
/// <param name="Value">
/// Localized text value.
/// </param>
/// <param name="ResourceNotFound">
/// Indicates whether the requested resource could not be found.
/// </param>
public sealed record LocalizedString(string Key, string Value, bool ResourceNotFound);
