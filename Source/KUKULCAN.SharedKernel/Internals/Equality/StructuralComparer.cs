using System;
using KUKULCAN.SharedKernel.Domain;

namespace KUKULCAN.SharedKernel.Internals.Equality;

/// <summary>
/// Provides structural equality comparison.
/// </summary>
internal static class StructuralComparer
{
    public static bool StructuralEquals(object? left, object? right)
    {
        if (ReferenceEquals(left, right))
            return true;
        if (left is null || right is null)
            return false;
        if (left.GetType() != right.GetType())
            return false;

        return left switch
        {
            ValueObject leftVo when right is ValueObject rightVo => leftVo.Equals(rightVo),
            IDictionary leftDictionary when right is IDictionary rightDictionary => DictionariesEqual(leftDictionary,
                rightDictionary),
            IEnumerable leftEnumerable when right is IEnumerable rightEnumerable && left is not string =>
                EnumerablesEqual(leftEnumerable, rightEnumerable),
            _ => Equals(left, right)
        };
    }

    private static bool EnumerablesEqual(IEnumerable left, IEnumerable right)
    {
        IEnumerator enumLeft = left.GetEnumerator();
        IEnumerator enumRight = right.GetEnumerator();

        try
        {
            while (true)
            {
                bool moveLeft = enumLeft.MoveNext();
                bool moveRight = enumRight.MoveNext();

                if (moveLeft != moveRight)
                    return false;
                if (!moveLeft)
                    return true;
                if (!Equals(enumLeft.Current, enumRight.Current))
                    return false;
            }
        }
        finally
        {
            (enumLeft as IDisposable)?.Dispose();
            (enumRight as IDisposable)?.Dispose();
        }
    }

    private static bool DictionariesEqual(IDictionary left, IDictionary right)
    {
        if (left.Count != right.Count)
            return false;
        foreach (DictionaryEntry entry in left)
        {
            if (!right.Contains(entry.Key))
                return false;
            if (!Equals(entry.Value, right[entry.Key]))
                return false;
        }

        return true;
    }
}
