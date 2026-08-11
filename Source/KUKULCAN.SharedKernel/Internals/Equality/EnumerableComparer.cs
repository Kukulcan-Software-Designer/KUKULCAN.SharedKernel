namespace KUKULCAN.SharedKernel.Internals.Equality;

/// <summary>
/// Provides structural comparison for enumerable collections.
/// </summary>
internal static class EnumerableComparer
{
    /// <summary>
    /// Determines whether two enumerable sequences are structurally equal.
    /// </summary>
    public static bool Equals(IEnumerable? left, IEnumerable? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }
        if (left is null || right is null)
        {
            return false;
        }

        IEnumerator leftEnumerator = left.GetEnumerator();
        IEnumerator rightEnumerator = right.GetEnumerator();

        while (true)
        {
            bool leftNext = leftEnumerator.MoveNext();
            bool rightNext = rightEnumerator.MoveNext();

            if (leftNext != rightNext)
            {
                return false;
            }
            if (!leftNext)
            {
                return true;
            }
            if (!StructuralComparer.Equals(
                    leftEnumerator.Current,
                    rightEnumerator.Current))
            {
                return false;
            }
        }
    }
}
