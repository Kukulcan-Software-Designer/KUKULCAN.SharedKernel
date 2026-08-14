namespace KUKULCAN.SharedKernel.Globalization.Culture;

/// <summary>
/// Represents a supported application culture.
/// </summary>
public sealed class SupportedCulture : IEquatable<SupportedCulture>, IComparable<SupportedCulture>
{
    private static readonly Dictionary<string, SupportedCulture> _registered = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Represents the invariant culture.
    /// </summary>
    public static readonly SupportedCulture Invariant = Register(CultureInfo.InvariantCulture);

    /// <summary>
    /// Represents Spanish (Spain).
    /// </summary>
    public static readonly SupportedCulture SpanishSpain = Register("es-ES");

    /// <summary>
    /// Represents Spanish (Mexico).
    /// </summary>
    public static readonly SupportedCulture SpanishMexico = Register("es-MX");

    /// <summary>
    /// Represents English (United States).
    /// </summary>
    public static readonly SupportedCulture EnglishUnitedStates = Register("en-US");

    /// <summary>
    /// Gets all registered supported cultures.
    /// </summary>
    public static IReadOnlyCollection<SupportedCulture> All =>
        new ReadOnlyCollection<SupportedCulture>([.. _registered.Values.OrderBy(c => c.Name)]);

    /// <summary>
    /// Gets the culture name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the underlying <see cref="CultureInfo"/>.
    /// </summary>
    public CultureInfo CultureInfo { get; }

    /// <summary>
    /// Gets the display name.
    /// </summary>
    public string DisplayName => CultureInfo.DisplayName;

    /// <summary>
    /// Gets the native display name.
    /// </summary>
    public string NativeName => CultureInfo.NativeName;

    /// <summary>
    /// Gets whether this culture is neutral.
    /// </summary>
    public bool IsNeutralCulture => CultureInfo.IsNeutralCulture;

    /// <summary>
    /// Gets the parent culture.
    /// </summary>
    public CultureInfo Parent => CultureInfo.Parent;

    private SupportedCulture(CultureInfo cultureInfo)
    {
        CultureInfo = cultureInfo;
        Name = cultureInfo.Name;
    }

    /// <summary>
    /// Gets a supported culture by its name.
    /// </summary>
    /// <param name="name">
    /// Culture name.
    /// </param>
    /// <returns>
    /// The corresponding supported culture.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// The supplied culture is not supported.
    /// </exception>
    public static SupportedCulture FromName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return _registered.TryGetValue(name, out SupportedCulture? culture)
            ? culture :
            throw new ArgumentException($"Culture '{name}' is not supported.", nameof(name));
    }

    /// <summary>
    /// Determines whether the specified culture is supported.
    /// </summary>
    /// <param name="name">Culture name to check.</param>
    /// <returns>
    /// <see langword="true"/> if the culture is registered; otherwise, <see langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="name"/> is null, empty, or whitespace.
    /// </exception>
    public static bool IsSupported(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return _registered.ContainsKey(name);
    }

    private static SupportedCulture Register(string cultureName)
    {
        return Register(new CultureInfo(cultureName));
    }

    private static SupportedCulture Register(CultureInfo cultureInfo)
    {
        SupportedCulture culture = new(cultureInfo);

        _registered[culture.Name] = culture;

        return culture;
    }

    /// <inheritdoc/>
    public bool Equals(SupportedCulture? other) => other is not null &&
                                                   StringComparer.OrdinalIgnoreCase.Equals(Name, other.Name);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as SupportedCulture);

    /// <inheritdoc/>
    public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(Name);

    /// <inheritdoc/>
    public int CompareTo(SupportedCulture? other)
        => other is null
            ? 1
            : StringComparer.OrdinalIgnoreCase.Compare(Name, other.Name);

    /// <inheritdoc/>
    public override string ToString() => Name;

    /// <summary>
    /// Determines whether two supported cultures are equal.
    /// </summary>
    /// <param name="left">The first culture to compare.</param>
    /// <param name="right">The second culture to compare.</param>
    /// <returns>
    /// <see langword="true"/> when both cultures represent the same culture;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(SupportedCulture? left, SupportedCulture? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two supported cultures are different.
    /// </summary>
    /// <param name="left">The first culture to compare.</param>
    /// <param name="right">The second culture to compare.</param>
    /// <returns>
    /// <see langword="true"/> when the cultures represent different cultures;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(SupportedCulture? left, SupportedCulture? right) => !(left == right);
}
