namespace KUKULCAN.SharedKernel.Domain.Internals;

/// <summary>
/// Provides structural equality and hash code computation for value objects.
/// </summary>
internal static class StructuralEqualityComparer
{
    /// <summary>
    /// Determines whether two sequences of equality components are structurally equal.
    /// </summary>
    /// <param name="left">
    /// Left sequence.
    /// </param>
    /// <param name="right">
    /// Right sequence.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if both sequences are structurally equal;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool Equals(IEnumerable<object?> left, IEnumerable<object?> right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        using IEnumerator<object?> leftEnumerator = left.GetEnumerator();
        using IEnumerator<object?> rightEnumerator = right.GetEnumerator();

        while (true)
        {
            bool hasLeft = leftEnumerator.MoveNext();
            bool hasRight = rightEnumerator.MoveNext();

            if (hasLeft != hasRight)
            {
                return false;
            }
            if (!hasLeft)
            {
                return true;
            }
            if (!AreEqual(leftEnumerator.Current, rightEnumerator.Current))
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Computes a structural hash code.
    /// </summary>
    /// <param name="type">
    /// Runtime type.
    /// </param>
    /// <param name="components">
    /// Equality components.
    /// </param>
    /// <returns>
    /// Structural hash code.
    /// </returns>
    public static int GetHashCode(Type type, IEnumerable<object?> components)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(components);

        HashCode hash = new();
        hash.Add(type);
        foreach (object? component in components)
        {
            AddHash(ref hash, component);
        }

        return hash.ToHashCode();
    }

    /// <summary>
    /// Formats the components of a value object.
    /// </summary>
    /// <param name="type">
    /// Runtime type.
    /// </param>
    /// <param name="components">
    /// Equality components.
    /// </param>
    /// <returns>
    /// Human-readable representation.
    /// </returns>
    public static string Format(Type type, IEnumerable<object?> components)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(components);

        return $"{type.Name} {{ {string.Join(", ", components.Select(FormatComponent))} }}";
    }

    private static bool AreEqual(object? left, object? right)
    {
        if (ReferenceEquals(left, right))
        {
            return true;
        }
        if (left is null || right is null)
        {
            return false;
        }

        return left switch
        {
            string when right is string => Equals(left, right),
            IEnumerable leftEnumerable when right is IEnumerable rightEnumerable => EnumerablesEqual(leftEnumerable,
                rightEnumerable),
            _ => Equals(left, right)
        };
    }

    private static bool EnumerablesEqual(IEnumerable left, IEnumerable right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        IEnumerator leftEnumerator = left.GetEnumerator();
        IEnumerator rightEnumerator = right.GetEnumerator();

        while (true)
        {
            bool hasLeft = leftEnumerator.MoveNext();
            bool hasRight = rightEnumerator.MoveNext();

            if (hasLeft != hasRight)
            {
                return false;
            }
            if (!hasLeft)
            {
                return true;
            }
            if (!AreEqual(leftEnumerator.Current, rightEnumerator.Current))
            {
                return false;
            }
        }
    }

    private static void AddHash(ref HashCode hash, object? value)
    {
        switch (value)
        {
            case null:
                hash.Add(0);
                return;
            case string:
                hash.Add(value);
                return;
            case IEnumerable enumerable:
            {
                foreach (object? item in enumerable)
                {
                    AddHash(ref hash, item);
                }

                return;
            }
            default:
                hash.Add(value);
                break;
        }
    }

    private static string FormatComponent(object? value)
    {
        switch (value)
        {
            case null:
                return "<null>";
            case string text:
                return text;
        }
        if (value is not IEnumerable enumerable)
        {
            return value.ToString() ?? string.Empty;
        }
        List<string> values = [];
        values.AddRange(from object? item in enumerable select FormatComponent(item));

        return $"[{string.Join(", ", values)}]";
    }
}
