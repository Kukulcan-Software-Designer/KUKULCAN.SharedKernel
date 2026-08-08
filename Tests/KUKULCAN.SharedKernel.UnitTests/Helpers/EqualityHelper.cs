namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides helper methods for equality comparisons in unit tests.
/// </summary>
public sealed class EqualityHelper
{
    /// <summary>
    /// Determines whether two values are equal.
    /// </summary>
    public bool AreEqual<T>(T? left, T? right)
    {
        return EqualityComparer<T>.Default.Equals(left, right);
    }

    /// <summary>
    /// Determines whether two values are different.
    /// </summary>
    public bool AreNotEqual<T>(T? left, T? right)
    {
        return !EqualityComparer<T>.Default.Equals(left, right);
    }

    /// <summary>
    /// Determines whether two sequences are equal.
    /// </summary>
    public bool SequenceEqual<T>(IEnumerable<T>? left, IEnumerable<T>? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }
        if (left is null || right is null)
        {
            return false;
        }

        return left.SequenceEqual(right);
    }

    /// <summary>
    /// Determines whether the specified value is equal to its default value.
    /// </summary>
    public bool IsDefault<T>(T? value)
    {
        return EqualityComparer<T>.Default.Equals(value, default);
    }
}
