using System;

namespace KUKULCAN.SharedKernel.Internals.Equality;

/// <summary>
/// Computes structural hash codes.
/// </summary>
internal static class StructuralHashCode
{
    public static int Compute(object? value)
    {
        return value switch
        {
            null => 0,
            string => value.GetHashCode(),
            IDictionary dictionary => ComputeDictionary(dictionary),
            IEnumerable enumerable => ComputeEnumerable(enumerable),
            _ => value.GetHashCode()
        };
    }

    private static int ComputeEnumerable(IEnumerable enumerable)
    {
        HashCode hash = new();

        foreach (object? item in enumerable)
        {
            hash.Add(Compute(item));
        }

        return hash.ToHashCode();
    }

    private static int ComputeDictionary(IDictionary dictionary)
    {
        HashCode hash = new();

        foreach (DictionaryEntry entry in dictionary)
        {
            hash.Add(Compute(entry.Key));
            hash.Add(Compute(entry.Value));
        }

        return hash.ToHashCode();
    }
}
