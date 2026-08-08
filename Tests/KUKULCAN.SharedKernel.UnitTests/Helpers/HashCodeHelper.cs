namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides helper methods for hash code validation in unit tests.
/// </summary>
public sealed class HashCodeHelper
{
    /// <summary>
    /// Gets the hash code of the specified value.
    /// </summary>
    public int GetHashCode<T>(T? value)
    {
        return value?.GetHashCode() ?? 0;
    }

    /// <summary>
    /// Determines whether two values produce the same hash code.
    /// </summary>
    public bool HaveSameHashCode<T>(T? left, T? right)
    {
        return GetHashCode(left) == GetHashCode(right);
    }

    /// <summary>
    /// Determines whether two values produce different hash codes.
    /// </summary>
    public bool HaveDifferentHashCode<T>(T? left, T? right)
    {
        return GetHashCode(left) != GetHashCode(right);
    }

    /// <summary>
    /// Determines whether the specified collection contains duplicated hash codes.
    /// </summary>
    public bool ContainsHashCollisions<T>(IEnumerable<T> values)
    {
        ArgumentNullException.ThrowIfNull(values);
        HashSet<int> hashes = [];

        foreach (T value in values)
        {
            if (!hashes.Add(GetHashCode(value)))
            {
                return true;
            }
        }

        return false;
    }
}
