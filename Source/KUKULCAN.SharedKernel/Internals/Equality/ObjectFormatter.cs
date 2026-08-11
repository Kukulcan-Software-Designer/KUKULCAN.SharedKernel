using System.Text;
using KUKULCAN.SharedKernel.Internals.ValueObjects;

namespace KUKULCAN.SharedKernel.Internals.Equality;

/// <summary>
/// Formats objects for debugging.
/// </summary>
internal static class ObjectFormatter
{
    public static string Format(object? value)
    {
        switch (value)
        {
            case null:
                return "null";
            case string s:
                return s;
            case IEnumerable enumerable and not string:
                return FormatEnumerable(enumerable);
        }

        Type type = value.GetType();
        if (ValueObjectCache.TryGet(type, out ValueObjectMetadata? metadata))
        {
            return FormatValueObject(value, metadata);
        }

        return value.ToString() ?? string.Empty;
    }

    private static string FormatEnumerable(IEnumerable enumerable)
    {
        StringBuilder sb = new();

        sb.Append('[');
        bool first = true;
        foreach (object? item in enumerable)
        {
            if (!first)
                sb.Append(", ");
            sb.Append(Format(item));
            first = false;
        }
        sb.Append(']');

        return sb.ToString();
    }

    private static string FormatValueObject(object instance, ValueObjectMetadata metadata)
    {
        StringBuilder sb = new();

        sb.Append(metadata.Type.Name);
        sb.Append(" { ");
        for (int i = 0; i < metadata.Members.Count; i++)
        {
            ValueObjectProperty member = metadata.Members[i];
            sb.Append(member.Name);
            sb.Append(" = ");
            sb.Append(Format(member.Getter(instance)));
            if (i < metadata.Members.Count - 1)
                sb.Append(", ");
        }
        sb.Append(" }");

        return sb.ToString();
    }
}
