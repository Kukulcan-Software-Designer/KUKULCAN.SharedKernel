using System;
using System.Collections.Generic;
using System.Reflection;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Common reusable predicates for the reflection cache.
/// </summary>
internal static partial class ReflectionCache
{
    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool> ByCategory(string category)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(category);

        return pair => StringComparer.Ordinal.Equals(pair.Key.Category, category);
    }

    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool>
        ByType(
            Type ownerType)
    {
        ArgumentNullException.ThrowIfNull(ownerType);

        return pair =>
            pair.Key.OwnerType == ownerType;
    }

    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool>
        ByAssembly(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        return pair => pair.Key.OwnerType.Assembly == assembly;
    }

    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool>
        ByModule(Module module)
    {
        ArgumentNullException.ThrowIfNull(module);

        return pair => pair.Key.OwnerType.Module == module;
    }

    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool>
        ByAssignableType(Type baseType)
    {
        ArgumentNullException.ThrowIfNull(baseType);

        return pair => baseType.IsAssignableFrom(pair.Key.OwnerType);
    }

    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool>
        ByNamespace(string @namespace)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);

        return pair => pair.Key.OwnerType?.Namespace == @namespace;
    }

    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool>
        ByGenericType(Type genericTypeDefinition)
    {
        ArgumentNullException.ThrowIfNull(genericTypeDefinition);

        return pair =>
        {
            Type? owner = pair.Key.OwnerType;

            return owner is not null &&
                   owner.IsGenericType &&
                   owner.GetGenericTypeDefinition() == genericTypeDefinition;
        };
    }

    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool>
        Expired()
    {
        return pair => pair.Value.IsExpired;
    }

    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool>
        FromEntry(Func<ReflectionCacheEntry, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return pair => predicate(pair.Value);
    }

    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool>
        FromKey(Func<ReflectionCacheKey, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return pair => predicate(pair.Key);
    }

    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool>
        FromPredicate(Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        return predicate;
    }

    /// <summary>
    /// Creates a predicate that matches entries whose cached value
    /// is assignable to the specified type.
    /// </summary>
    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool> ByValueType(Type valueType)
    {
        ArgumentNullException.ThrowIfNull(valueType);

        return pair => pair.Value.Value is not null && valueType.IsAssignableFrom(pair.Value.Value.GetType());
    }

    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool>
        HasMetadata()
    {
        return pair => pair.Value.Metadata.Count > 0;
    }

    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool>
        WithoutMetadata()
    {
        return pair => pair.Value.Metadata.Count == 0;
    }

    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool>
        CreatedBefore(DateTimeOffset instant)
    {
        return pair => pair.Value.CreatedOn < instant;
    }

    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool>
        CreatedAfter(DateTimeOffset instant)
    {
        return pair => pair.Value.CreatedOn > instant;
    }

    /// <summary>
    /// Creates a predicate that matches entries accessed before the specified instant.
    /// </summary>
    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool> AccessedBefore(DateTimeOffset instant)
    {
        return pair => pair.Value.LastAccess < instant;
    }

    /// <summary>
    /// Creates a predicate that matches entries accessed after the specified instant.
    /// </summary>
    internal static Func<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>, bool> AccessedAfter(DateTimeOffset instant)
    {
        return pair => pair.Value.LastAccess > instant;
    }

}
