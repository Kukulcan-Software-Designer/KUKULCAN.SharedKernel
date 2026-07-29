namespace KUKULCAN.SharedKernel.Internals.Equality;

/// <summary>
/// Provides structural comparison for dictionaries.
/// </summary>
internal static class DictionaryComparer
{
    /// <summary>
    /// Determines whether two dictionaries are structurally equal.
    /// </summary>
    public static bool Equals(IDictionary? left, IDictionary? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }
        if (left is null || right is null)
        {
            return false;
        }
        if (left.Count != right.Count)
        {
            return false;
        }
        foreach (DictionaryEntry entry in left)
        {
            if (!right.Contains(entry.Key))
            {
                return false;
            }
            object? rightValue = right[entry.Key];
            if (!StructuralComparer.Equals(entry.Value, rightValue))
            {
                return false;
            }
        }

        return true;
    }
}
