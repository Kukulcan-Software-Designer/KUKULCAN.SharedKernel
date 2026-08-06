using System.Collections.ObjectModel;

namespace KUKULCAN.SharedKernel.UnitTests.Statistics;

/// <summary>
/// Represents an immutable statistical distribution.
/// </summary>
/// <typeparam name="TKey">Type of the distribution key.</typeparam>
public sealed class DistributionSnapshot<TKey>
    where TKey : notnull
{
    private readonly IReadOnlyList<DistributionEntry<TKey>> _entries;

    /// <summary>
    /// Initializes a new instance of the <see cref="DistributionSnapshot{TKey}"/> class.
    /// </summary>
    /// <param name="entries">Distribution entries.</param>
    public DistributionSnapshot(
        IEnumerable<DistributionEntry<TKey>> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);

        _entries = new ReadOnlyCollection<DistributionEntry<TKey>>(
            entries
                .OrderBy(e => e.Rank)
                .ToList());
    }

    /// <summary>
    /// Gets every distribution entry.
    /// </summary>
    public IReadOnlyList<DistributionEntry<TKey>> Entries
        => _entries;

    /// <summary>
    /// Gets the total number of entries.
    /// </summary>
    public int Count
        => _entries.Count;

    /// <summary>
    /// Gets whether the distribution is empty.
    /// </summary>
    public bool IsEmpty
        => Count == StatisticsConstants.EmptyCount;

    /// <summary>
    /// Gets the first entry.
    /// </summary>
    public DistributionEntry<TKey>? First
        => _entries.FirstOrDefault();

    /// <summary>
    /// Gets the last entry.
    /// </summary>
    public DistributionEntry<TKey>? Last
        => _entries.LastOrDefault();

    /// <summary>
    /// Gets the dominant entry.
    /// </summary>
    public DistributionEntry<TKey>? Dominant
        => _entries.FirstOrDefault(e => e.IsDominant);

    /// <summary>
    /// Gets every dominant entry.
    /// </summary>
    public IReadOnlyList<DistributionEntry<TKey>> DominantEntries
        => _entries
            .Where(e => e.IsDominant)
            .ToList();

    /// <summary>
    /// Gets every rare entry.
    /// </summary>
    public IReadOnlyList<DistributionEntry<TKey>> RareEntries
        => _entries
            .Where(e => e.IsRare)
            .ToList();

    /// <summary>
    /// Gets every single-occurrence entry.
    /// </summary>
    public IReadOnlyList<DistributionEntry<TKey>> SingleOccurrenceEntries
        => _entries
            .Where(e => e.IsSingleOccurrence)
            .ToList();

    /// <summary>
    /// Gets every non-empty entry.
    /// </summary>
    public IReadOnlyList<DistributionEntry<TKey>> NonEmptyEntries
        => _entries
            .Where(e => !e.IsEmpty)
            .ToList();

    /// <summary>
    /// Gets the specified number of top entries.
    /// </summary>
    public IReadOnlyList<DistributionEntry<TKey>> Top(
        int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        return _entries
            .OrderByDescending(e => e.Count)
            .Take(count)
            .ToList();
    }

    /// <summary>
    /// Gets the specified number of bottom entries.
    /// </summary>
    public IReadOnlyList<DistributionEntry<TKey>> Bottom(
        int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        return _entries
            .OrderBy(e => e.Count)
            .Take(count)
            .ToList();
    }

    /// <summary>
    /// Determines whether the specified key exists.
    /// </summary>
    public bool Contains(
        TKey key)
    {
        ArgumentNullException.ThrowIfNull(key);

        return _entries.Any(e => EqualityComparer<TKey>.Default.Equals(e.Key, key));
    }

    /// <summary>
    /// Gets an entry by key.
    /// </summary>
    public DistributionEntry<TKey>? Find(
        TKey key)
    {
        ArgumentNullException.ThrowIfNull(key);

        return _entries.FirstOrDefault(
            e => EqualityComparer<TKey>.Default.Equals(e.Key, key));
    }

    /// <summary>
    /// Gets the entry with the specified rank.
    /// </summary>
    public DistributionEntry<TKey>? FindByRank(
        int rank)
    {
        return _entries.FirstOrDefault(e => e.Rank == rank);
    }

    /// <summary>
    /// Returns an enumerator.
    /// </summary>
    public IEnumerator<DistributionEntry<TKey>> GetEnumerator()
        => _entries.GetEnumerator();

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"Entries = {Count}";
    }
}
