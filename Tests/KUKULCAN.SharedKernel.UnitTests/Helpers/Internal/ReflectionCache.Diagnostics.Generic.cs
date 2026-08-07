using System;
using System.Collections.Generic;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Generic diagnostic helpers for <see cref="ReflectionCache"/>.
/// </summary>
internal static partial class ReflectionCache
{
    #region Generic Contains

    /// <summary>
    /// Determines whether the cache contains at least one value
    /// assignable to the specified generic type.
    /// </summary>
    public static bool Contains<T>()
    {
        return ContainsValueType(typeof(T));
    }

    /// <summary>
    /// Determines whether the cache contains at least one value
    /// assignable to the specified generic type.
    /// </summary>
    public static bool Any<T>()
    {
        return ContainsValueType(typeof(T));
    }

    #endregion

    #region Generic Count

    /// <summary>
    /// Counts the cache entries assignable to the specified generic type.
    /// </summary>
    public static int Count<T>()
    {
        return CountValueType(typeof(T));
    }

    /// <summary>
    /// Counts the cache entries assignable to the specified type.
    /// </summary>
    public static int CountValueType(Type valueType)
    {
        ArgumentNullException.ThrowIfNull(valueType);

        return Count(
            entry =>
                entry.TryGetValue<object>(out object? value) &&
                value is not null &&
                valueType.IsAssignableFrom(value.GetType()));
    }

    #endregion

    #region Generic Find

    /// <summary>
    /// Finds the first cached value assignable to the specified type.
    /// </summary>
    public static T? FindFirst<T>()
    {
        ReflectionCacheEntry? entry = FindFirst(e => e.TryGetValue<T>(out _));

        if (entry is null)
        {
            return default;
        }

        return entry.TryGetValue(out T? value) ? value : default;
    }

    #endregion

    #region Generic Enumeration

    /// <summary>
    /// Returns every cached value assignable to the specified generic type.
    /// </summary>
    public static IReadOnlyList<T> FindAll<T>()
    {
        List<T> values = [];

        foreach (ReflectionCacheEntry entry in EnumerateEntries())
        {
            if (entry.TryGetValue(out T? value) &&
                value is not null)
            {
                values.Add(value);
            }
        }

        return values;
    }

    /// <summary>
    /// Enumerates every cached value assignable to the specified generic type.
    /// </summary>
    public static IReadOnlyList<T> EnumerateValues<T>()
    {
        List<T> values = [];

        foreach (ReflectionCacheEntry entry in EnumerateEntries())
        {
            if (entry.TryGetValue(out T? value) &&
                value is not null)
            {
                values.Add(value);
            }
        }

        return values;
    }

    /// <summary>
    /// Enumerates every cached value assignable to the specified type.
    /// </summary>
    public static IReadOnlyList<object> EnumerateValues(Type valueType)
    {
        ArgumentNullException.ThrowIfNull(valueType);

        List<object> values = [];

        foreach (ReflectionCacheEntry entry in EnumerateEntries())
        {
            if (entry.TryGetValue<object>(out object? value) &&
                value is not null &&
                valueType.IsInstanceOfType(value))
            {
                values.Add(value);
            }
        }

        return values;
    }

    #endregion

    #region Generic Predicates

    /// <summary>
    /// Determines whether at least one cached value satisfies the specified predicate.
    /// </summary>
    public static bool Any<T>(Predicate<T> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        foreach (ReflectionCacheEntry entry in EnumerateEntries())
        {
            if (entry.TryGetValue(out T? value) &&
                value is not null &&
                predicate(value))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Counts the cached values satisfying the specified predicate.
    /// </summary>
    public static int Count<T>(Predicate<T> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        int count = 0;

        foreach (ReflectionCacheEntry entry in EnumerateEntries())
        {
            if (entry.TryGetValue(out T? value) &&
                value is not null &&
                predicate(value))
            {
                count++;
            }
        }

        return count;
    }

    /// <summary>
    /// Finds all cached values satisfying the specified predicate.
    /// </summary>
    public static IReadOnlyList<T> FindAll<T>(Predicate<T> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);

        List<T> values = [];

        foreach (ReflectionCacheEntry entry in EnumerateEntries())
        {
            if (entry.TryGetValue(out T? value) &&
                value is not null &&
                predicate(value))
            {
                values.Add(value);
            }
        }

        return values;
    }

    #endregion
}
