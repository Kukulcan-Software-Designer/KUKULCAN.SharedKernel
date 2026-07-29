namespace KUKULCAN.SharedKernel.Globalization.Abstractions;

/// <summary>
/// Resolves text localizers.
/// </summary>
public interface ILocalizationProvider
{
    /// <summary>
    /// Gets the default localizer.
    /// </summary>
    ITextLocalizer Default { get; }

    /// <summary>
    /// Gets a localizer associated with the specified scope.
    /// </summary>
    /// <param name="scope">
    /// Localization scope.
    /// </param>
    /// <returns>
    /// Localizer associated with the supplied scope.
    /// </returns>
    ITextLocalizer GetLocalizer(string scope);
}
