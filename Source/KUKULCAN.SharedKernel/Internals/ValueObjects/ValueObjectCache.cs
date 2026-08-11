using System.Reflection;
using KUKULCAN.SharedKernel.Attributes;

namespace KUKULCAN.SharedKernel.Internals.ValueObjects;

/// <summary>
/// Caches metadata for ValueObject types.
/// </summary>
internal static class ValueObjectCache
{
    private static readonly ConcurrentDictionary<Type, ValueObjectMetadata> _cache = new();

    /// <summary>
    /// Gets cached metadata for the specified ValueObject type.
    /// </summary>
    public static ValueObjectMetadata Get(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        return _cache.GetOrAdd(type, CreateMetadata);
    }

    public static bool TryGet(Type type, [NotNullWhen(true)] out ValueObjectMetadata? metadata)
    {
        return _cache.TryGetValue(type, out metadata);
    }
    private static ValueObjectMetadata CreateMetadata(Type type)
    {
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        bool explicitMode =
            properties.Any(p => p.IsDefined(typeof(ValueObjectMemberAttribute), inherit: true));
        IEnumerable<PropertyInfo> selected = properties.Where(IsCandidate);
        if (explicitMode)
        {
            selected = selected.Where(p => p.IsDefined(typeof(ValueObjectMemberAttribute), inherit: true));
        }
        IReadOnlyList<ValueObjectProperty> members =
        [
            .. selected
                .OrderBy(GetOrder)
                .ThenBy(p => p.Name)
                .Select(CreateMember)
        ];

        return new ValueObjectMetadata
        {
            Type = type,
            Members = members
        };
    }

    private static bool IsCandidate(PropertyInfo property)
    {
        if (!property.CanRead)
            return false;

        MethodInfo? getter = property.GetMethod;

        if (getter is null)
            return false;
        if (!getter.IsPublic)
            return false;
        if (getter.IsStatic)
            return false;
        if (property.GetIndexParameters().Length != 0)
            return false;

        return !property.IsDefined(
            typeof(IgnoreEqualityAttribute),
            inherit: true);
    }

    private static int GetOrder(PropertyInfo property)
    {
        var attribute = property.GetCustomAttribute<EqualityOrderAttribute>(inherit: true);

        return attribute?.Order ?? int.MaxValue;
    }

    private static ValueObjectProperty CreateMember(PropertyInfo property)
    {
        return new ValueObjectProperty
        {
            Name = property.Name,
            Property = property,
            Getter = CompiledGetterFactory.Create(property)
        };
    }
}
