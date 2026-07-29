namespace KUKULCAN.SharedKernel.Internals.Equality;

/// <summary>
/// Provides structural equality comparison for arbitrary objects.
/// </summary>
internal static class StructuralComparer
{
    /// <summary>
    /// Determines whether two objects are structurally equal.
    /// </summary>
    /// <param name="left">
    /// Left object.
    /// </param>
    /// <param name="right">
    /// Right object.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if both objects are structurally equal;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public new static bool Equals(object? left, object? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }
        if (left is null || right is null)
        {
            return false;
        }
        if (left.GetType() != right.GetType())
        {
            return false;
        }

        return left switch
        {
            IDictionary leftDictionary when right is IDictionary rightDictionary => DictionaryComparer.Equals(
                leftDictionary, rightDictionary),
            IEnumerable leftEnumerable when right is IEnumerable rightEnumerable && left is not string &&
                                            right is not string => EnumerableComparer.Equals(leftEnumerable,
                rightEnumerable),
            _ => object.Equals(left, right)
        };
    }
}
