using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers.Internal;

/// <summary>
/// Add and update operations for the reflection cache.
/// </summary>
internal static partial class ReflectionCache
{
    /// <summary>
    /// Adds a cache entry.
    /// Throws if the key already exists.
    /// </summary>
    public static void Add<T>(
        ReflectionCacheKey key,
        T value)
    {
        ArgumentNullException.ThrowIfNull(key);

        ReflectionCacheEntry entry =
            ReflectionCacheEntry.Create(
                key,
                value);

        if (!_entries.TryAdd(key, entry))
        {
            throw new InvalidOperationException(
                $"Reflection cache already contains '{key}'.");
        }
    }

    /// <summary>
    /// Attempts to add a cache entry.
    /// </summary>
    public static bool TryAdd<T>(
        ReflectionCacheKey key,
        T value)
    {
        ArgumentNullException.ThrowIfNull(key);

        ReflectionCacheEntry entry =
            ReflectionCacheEntry.Create(
                key,
                value);

        return _entries.TryAdd(
            key,
            entry);
    }

    /// <summary>
    /// Adds or replaces a cache entry.
    /// </summary>
    public static void AddOrUpdate<T>(
        ReflectionCacheKey key,
        T value)
    {
        ArgumentNullException.ThrowIfNull(key);

        ReflectionCacheEntry entry =
            ReflectionCacheEntry.Create(
                key,
                value);

        _entries.AddOrUpdate(
            key,
            entry, (_, _) => entry);
    }

    /// <summary>
    /// Adds or replaces a cache entry.
    /// </summary>
    public static ReflectionCacheEntry AddOrUpdate(
        ReflectionCacheKey key,
        Func<ReflectionCacheKey, ReflectionCacheEntry> addFactory,
        Func<ReflectionCacheKey, ReflectionCacheEntry, ReflectionCacheEntry> updateFactory)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(addFactory);
        ArgumentNullException.ThrowIfNull(updateFactory);

        return _entries.AddOrUpdate(
            key,
            addFactory,
            updateFactory);
    }

    /// <summary>
    /// Replaces an existing cache entry.
    /// </summary>
    public static void Replace<T>(
        ReflectionCacheKey key,
        T value)
    {
        ArgumentNullException.ThrowIfNull(key);

        if (!_entries.TryGetValue(
                key,
                out ReflectionCacheEntry? existing))
        {
            throw new KeyNotFoundException(
                $"Reflection cache entry '{key}' was not found.");
        }

        ReflectionCacheEntry replacement =
            ReflectionCacheEntry.Create(
                key,
                value);

        if (!_entries.TryUpdate(
                key,
                replacement,
                existing))
        {
            throw new InvalidOperationException(
                $"Reflection cache entry '{key}' could not be replaced.");
        }
    }

    /// <summary>
    /// Attempts to replace an existing cache entry.
    /// </summary>
    public static bool TryReplace<T>(
        ReflectionCacheKey key,
        T value)
    {
        ArgumentNullException.ThrowIfNull(key);

        if (!_entries.TryGetValue(
                key,
                out ReflectionCacheEntry? existing))
        {
            return false;
        }

        ReflectionCacheEntry replacement =
            ReflectionCacheEntry.Create(
                key,
                value);

        return _entries.TryUpdate(
            key,
            replacement,
            existing);
    }

    /// <summary>
    /// Adds multiple cache entries.
    /// Throws if one of the keys already exists.
    /// </summary>
    public static void AddRange(
        IEnumerable<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);

        foreach (KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry> pair in entries)
        {
            if (!_entries.TryAdd(
                    pair.Key,
                    pair.Value))
            {
                throw new InvalidOperationException(
                    $"Reflection cache already contains '{pair.Key}'.");
            }
        }
    }

    /// <summary>
    /// Attempts to add multiple cache entries.
    /// </summary>
    public static bool TryAddRange(
        IEnumerable<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);

        bool success = true;

        foreach (KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry> pair in entries)
        {
            success &= _entries.TryAdd(
                pair.Key,
                pair.Value);
        }

        return success;
    }

    /// <summary>
    /// Replaces multiple cache entries.
    /// Every key must already exist.
    /// </summary>
    public static void ReplaceRange(
        IEnumerable<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);

        foreach (KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry> pair in entries)
        {
            if (!_entries.TryGetValue(
                    pair.Key,
                    out ReflectionCacheEntry? existing))
            {
                throw new KeyNotFoundException(
                    $"Reflection cache entry '{pair.Key}' was not found.");
            }

            if (!_entries.TryUpdate(
                    pair.Key,
                    pair.Value,
                    existing))
            {
                throw new InvalidOperationException(
                    $"Reflection cache entry '{pair.Key}' could not be replaced.");
            }
        }
    }

    /// <summary>
    /// Adds or updates multiple cache entries.
    /// </summary>
    public static void AddOrUpdateRange(
        IEnumerable<KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry>> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);

        foreach (KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry> pair in entries)
        {
            _entries.AddOrUpdate(
                pair.Key,
                pair.Value, (_, _) => pair.Value);
        }
    }

    /// <summary>
    /// Adds or updates multiple values.
    /// </summary>
    public static void AddOrUpdateRange<T>(
        IEnumerable<KeyValuePair<ReflectionCacheKey, T>> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);

        foreach (KeyValuePair<ReflectionCacheKey, T> pair in entries)
        {
            ReflectionCacheEntry entry =
                ReflectionCacheEntry.Create(
                    pair.Key,
                    pair.Value);

            _entries.AddOrUpdate(
                pair.Key,
                entry, (_, _) => entry);
        }
    }

    /// <summary>
    /// Adds multiple values.
    /// </summary>
    public static void AddRange<T>(
        IEnumerable<KeyValuePair<ReflectionCacheKey, T>> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);

        foreach (KeyValuePair<ReflectionCacheKey, T> pair in entries)
        {
            Add(
                pair.Key,
                pair.Value);
        }
    }

    /// <summary>
    /// Attempts to add multiple values.
    /// </summary>
    public static bool TryAddRange<T>(
        IEnumerable<KeyValuePair<ReflectionCacheKey, T>> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);

        bool success = true;

        foreach (KeyValuePair<ReflectionCacheKey, T> pair in entries)
        {
            success &= TryAdd(
                pair.Key,
                pair.Value);
        }

        return success;
    }

    /// <summary>
    /// Adds a lazily-created cache entry.
    /// </summary>
    public static void AddLazy<T>(
        ReflectionCacheKey key,
        Func<T> factory)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(factory);

        Lazy<T> lazy =
            new(
                factory,
                LazyThreadSafetyMode.ExecutionAndPublication);

        ReflectionCacheEntry entry =
            ReflectionCacheEntry.Create(
                key,
                lazy);

        if (!_entries.TryAdd(key, entry))
        {
            throw new InvalidOperationException(
                $"Reflection cache already contains '{key}'.");
        }
    }

    /// <summary>
    /// Attempts to add a lazily-created cache entry.
    /// </summary>
    public static bool TryAddLazy<T>(
        ReflectionCacheKey key,
        Func<T> factory)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(factory);

        Lazy<T> lazy =
            new(
                factory,
                LazyThreadSafetyMode.ExecutionAndPublication);

        ReflectionCacheEntry entry =
            ReflectionCacheEntry.Create(
                key,
                lazy);

        return _entries.TryAdd(
            key,
            entry);
    }

    /// <summary>
    /// Clones an existing cache entry.
    /// </summary>
    public static ReflectionCacheEntry CloneEntry(
        ReflectionCacheKey key)
    {
        ArgumentNullException.ThrowIfNull(key);

        if (!_entries.TryGetValue(
                key,
                out ReflectionCacheEntry? entry))
        {
            throw new KeyNotFoundException(
                $"Reflection cache entry '{key}' was not found.");
        }

        return entry.Clone();
    }

    /// <summary>
    /// Copies every cache entry into another dictionary.
    /// </summary>
    public static void CopyTo(
        IDictionary<ReflectionCacheKey, ReflectionCacheEntry> destination)
    {
        ArgumentNullException.ThrowIfNull(destination);

        foreach (KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry> pair in _entries)
        {
            destination[pair.Key] = pair.Value;
        }
    }

    /// <summary>
    /// Merges another cache into the current cache.
    /// </summary>
    public static void Merge(
        IReadOnlyDictionary<ReflectionCacheKey, ReflectionCacheEntry> source,
        bool overwrite = false)
    {
        ArgumentNullException.ThrowIfNull(source);

        foreach (KeyValuePair<ReflectionCacheKey, ReflectionCacheEntry> pair in source)
        {
            if (overwrite)
            {
                _entries[pair.Key] = pair.Value;
            }
            else
            {
                _entries.TryAdd(
                    pair.Key,
                    pair.Value);
            }
        }
    }

    /// <summary>
    /// Exports the current cache.
    /// </summary>
    public static IReadOnlyDictionary<
        ReflectionCacheKey,
        ReflectionCacheEntry> Export()
    {
        return _entries.ToDictionary(
            static pair => pair.Key,
            static pair => pair.Value);
    }

    /// <summary>
    /// Imports cache entries.
    /// </summary>
    public static void Import(
        IReadOnlyDictionary<
            ReflectionCacheKey,
            ReflectionCacheEntry> source,
        bool overwrite = true)
    {
        ArgumentNullException.ThrowIfNull(source);

        Merge(
            source,
            overwrite);
    }

    /// <summary>
    /// Gets the number of entries that can be imported without overwriting.
    /// </summary>
    public static int CountImportable(
        IReadOnlyDictionary<
            ReflectionCacheKey,
            ReflectionCacheEntry> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return source.Keys.Count(
            key => !_entries.ContainsKey(key));
    }

    /// <summary>
    /// Gets the number of entries that would be overwritten by an import.
    /// </summary>
    public static int CountOverwritten(
        IReadOnlyDictionary<
            ReflectionCacheKey,
            ReflectionCacheEntry> source)
    {
        ArgumentNullException.ThrowIfNull(source);

        return source.Keys.Count(
            key => _entries.ContainsKey(key));
    }
}
