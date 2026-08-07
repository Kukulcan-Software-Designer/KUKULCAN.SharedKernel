using System;
using System.Collections.Generic;
using System.Linq;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Retrieval operations for the reflection cache.
/// </summary>
internal static partial class ReflectionCache
{
    /// <summary>
    /// Attempts to retrieve a cached value.
    /// </summary>
    public static bool TryGet<T>(ReflectionCacheKey key, out T? value)
    {
        ArgumentNullException.ThrowIfNull(key);

        value = default;

        if (!_entries.TryGetValue(key, out var entry))
            return false;

        if (!entry.TryGetValue(out value))
            return false;

        entry.Touch();

        return true;
    }

    /// <summary>
    /// Gets a cached value.
    /// </summary>
    public static T Get<T>(
        ReflectionCacheKey key)
    {
        ArgumentNullException.ThrowIfNull(key);

        if (!_entries.TryGetValue(key, out var entry))
            throw new KeyNotFoundException(
                $"Reflection cache entry '{key}' was not found.");

        if (!entry.TryGetValue(out T? value))
            throw new InvalidCastException(
                $"Reflection cache entry '{key}' is not of type {typeof(T).FullName}.");

        entry.Touch();

        return value!;
    }

    /// <summary>
    /// Gets an existing cached value or creates a new one.
    /// </summary>
    public static T GetOrAdd<T>(
        ReflectionCacheKey key,
        Func<T> factory)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(factory);

        if (_entries.TryGetValue(key, out var existingEntry))
        {
            if (existingEntry.TryGetValue(out T? existingValue))
            {
                existingEntry.Touch();
                return existingValue!;
            }
        }

        using (_lock.EnterScope())
        {
            if (_entries.TryGetValue(key, out existingEntry))
            {
                if (existingEntry.TryGetValue(out T? existingValue))
                {
                    existingEntry.Touch();
                    return existingValue!;
                }
            }

            T createdValue = factory();

            ReflectionCacheEntry newEntry =
                ReflectionCacheEntry.Create(
                    key,
                    createdValue);

            _entries[key] = newEntry;

            return createdValue;
        }
    }

    /// <summary>
    /// Gets an existing cached value or creates a new one.
    /// </summary>
    public static T GetOrAdd<T>(
        ReflectionCacheKey key,
        Func<ReflectionCacheKey, T> factory)
    {
        ArgumentNullException.ThrowIfNull(factory);

        return GetOrAdd(
            key,
            () => factory(key));
    }

    /// <summary>
    /// Gets an existing cached value or creates a new one.
    /// </summary>
    public static T GetOrAdd<T, TState>(
        ReflectionCacheKey key,
        TState state,
        Func<ReflectionCacheKey, TState, T> factory)
    {
        ArgumentNullException.ThrowIfNull(factory);

        return GetOrAdd(
            key,
            () => factory(key, state));
    }

    /// <summary>
    /// Gets every cache entry.
    /// </summary>
    public static IReadOnlyCollection<ReflectionCacheEntry> GetEntries()
    {
        return _entries.Values.ToArray();
    }

    /// <summary>
    /// Gets every cache key.
    /// </summary>
    public static IReadOnlyCollection<ReflectionCacheKey> GetKeys()
    {
        return _entries.Keys.ToArray();
    }

    /// <summary>
    /// Gets every cached value.
    /// </summary>
    public static IReadOnlyCollection<object?> GetValues()
    {
        return
        [
            .. _entries
                .Values
                .Select(static entry => entry.Value)
        ];
    }
}
