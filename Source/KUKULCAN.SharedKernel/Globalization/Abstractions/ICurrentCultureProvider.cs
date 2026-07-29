namespace KUKULCAN.SharedKernel.Globalization.Abstractions;

/// <summary>
/// Provides access to the current application culture.
/// </summary>
public interface ICurrentCultureProvider
{
    /// <summary>
    /// Gets the current culture.
    /// </summary>
    CultureInfo CurrentCulture { get; }

    /// <summary>
    /// Gets the current UI culture.
    /// </summary>
    CultureInfo CurrentUiCulture { get; }
}
