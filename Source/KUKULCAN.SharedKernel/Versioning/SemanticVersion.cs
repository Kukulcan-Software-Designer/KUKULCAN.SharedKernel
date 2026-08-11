using KUKULCAN.SharedKernel.Domain;

namespace KUKULCAN.SharedKernel.Versioning;

/// <summary>
/// Represents an immutable Semantic Versioning 2.0.0 value.
/// </summary>
public sealed class SemanticVersion : ValueObject, IComparable<SemanticVersion>, IParsable<SemanticVersion>, ISpanParsable<SemanticVersion>
{
    /// <summary>
    /// Gets the major version.
    /// </summary>
    public int Major { get; }

    /// <summary>
    /// Gets the minor version.
    /// </summary>
    public int Minor { get; }

    /// <summary>
    /// Gets the patch version.
    /// </summary>
    public int Patch { get; }

    /// <summary>
    /// Gets the prerelease identifier.
    /// </summary>
    public string? Prerelease { get; }

    /// <summary>
    /// Gets the build metadata.
    /// </summary>
    public string? BuildMetadata { get; }

    /// <summary>
    /// Gets whether this version represents a prerelease.
    /// </summary>
    public bool IsPrerelease => !string.IsNullOrWhiteSpace(Prerelease);

    /// <summary>
    /// Gets whether this version contains build metadata.
    /// </summary>
    public bool HasBuildMetadata => !string.IsNullOrWhiteSpace(BuildMetadata);

    /// <summary>
    /// Initializes a new semantic version.
    /// </summary>
    public SemanticVersion(int major, int minor, int patch, string? prerelease = null, string? buildMetadata = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(major);
        ArgumentOutOfRangeException.ThrowIfNegative(minor);
        ArgumentOutOfRangeException.ThrowIfNegative(patch);

        ValidateIdentifier(prerelease, nameof(prerelease));
        ValidateIdentifier(buildMetadata, nameof(buildMetadata));
        Major = major;
        Minor = minor;
        Patch = patch;
        Prerelease = string.IsNullOrWhiteSpace(prerelease) ? null : prerelease;
        BuildMetadata = string.IsNullOrWhiteSpace(buildMetadata) ? null : buildMetadata;
    }

    /// <summary>
    /// Parses a semantic version.
    /// </summary>
    public static SemanticVersion Parse(string s, IFormatProvider? provider)
    {
        return !TryParse(s, provider, out SemanticVersion? version) ? throw new FormatException($"'{s}' is not a valid semantic version.") : version;
    }

    /// <summary>
    /// Parses a semantic version.
    /// </summary>
    public static SemanticVersion Parse(string s) => Parse(s, null);

    /// <summary>
    /// Attempts to parse a semantic version.
    /// </summary>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out SemanticVersion version)
    {
        version = null;

        if (string.IsNullOrWhiteSpace(s))
        {
            return false;
        }

        string? build = null;
        string? prerelease = null;
        string core = s;
        int plus = core.IndexOf('+');

        if (plus >= 0)
        {
            build = core[(plus + 1)..];
            core = core[..plus];
        }

        int dash = core.IndexOf('-');

        if (dash >= 0)
        {
            prerelease = core[(dash + 1)..];
            core = core[..dash];
        }

        string[] numbers = core.Split('.');

        if (numbers.Length != 3)
        {
            return false;
        }
        if (!int.TryParse(numbers[0], out int major))
        {
            return false;
        }
        if (!int.TryParse(numbers[1], out int minor))
        {
            return false;
        }
        if (!int.TryParse(numbers[2], out int patch))
        {
            return false;
        }
        version = new SemanticVersion(major, minor, patch, prerelease, build);

        return true;
    }

    /// <summary>
    /// Attempts to parse a semantic version.
    /// </summary>
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out SemanticVersion version)
        => TryParse(s, null, out version);

    /// <inheritdoc/>
    public static SemanticVersion Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => Parse(s.ToString(), provider);

    /// <inheritdoc/>
    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out SemanticVersion result)
        => TryParse(s.ToString(), provider, out result);

    /// <inheritdoc/>
    public int CompareTo(SemanticVersion? other)
    {
        if (other is null)
        {
            return 1;
        }
        int result = Major.CompareTo(other.Major);
        if (result != 0)
        {
            return result;
        }
        result = Minor.CompareTo(other.Minor);
        if (result != 0)
        {
            return result;
        }
        result = Patch.CompareTo(other.Patch);

        return result != 0 ? result : ComparePrerelease(Prerelease, other.Prerelease);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        string version = $"{Major}.{Minor}.{Patch}";

        if (IsPrerelease)
        {
            version += "-" + Prerelease;
        }
        if (HasBuildMetadata)
        {
            version += "+" + BuildMetadata;
        }

        return version;
    }

    /// <inheritdoc/>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Major;
        yield return Minor;
        yield return Patch;
        yield return Prerelease;
        yield return BuildMetadata;
    }

    private static void ValidateIdentifier(string? value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return;
        }
        if (value.Contains(' '))
        {
            throw new ArgumentException("Identifiers cannot contain spaces.", parameterName);
        }
    }

    private static int ComparePrerelease(string? left, string? right)
    {
        if (left == right)
        {
            return 0;
        }
        if (left is null)
        {
            return 1;
        }
        if (right is null)
        {
            return -1;
        }

        return StringComparer.Ordinal.Compare(left, right);
    }
}
