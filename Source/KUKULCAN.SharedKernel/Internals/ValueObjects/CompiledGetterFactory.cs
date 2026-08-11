using System.Reflection;

namespace KUKULCAN.SharedKernel.Internals.ValueObjects;

/// <summary>
/// Creates compiled property getters.
/// </summary>
internal static class CompiledGetterFactory
{
    /// <summary>
    /// Creates a compiled getter delegate.
    /// </summary>
    public static Func<object, object?> Create(PropertyInfo property)
    {
        ArgumentNullException.ThrowIfNull(property);
        ParameterExpression instance = Expression.Parameter(typeof(object), "instance");
        UnaryExpression cast = Expression.Convert(instance, property.DeclaringType!);
        MemberExpression propertyAccess = Expression.Property(cast, property);
        UnaryExpression convert = Expression.Convert(propertyAccess, typeof(object));

        return Expression
            .Lambda<Func<object, object?>>(convert, instance)
            .Compile();
    }
}
