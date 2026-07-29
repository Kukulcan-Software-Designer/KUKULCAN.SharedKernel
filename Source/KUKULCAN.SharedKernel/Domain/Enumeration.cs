using System;
using System.Linq;
using System.Reflection;

namespace KUKULCAN.SharedKernel.Domain;

/// <summary>
/// Represents a rich enumeration.
/// </summary>
public abstract class Enumeration : IComparable, IComparable<Enumeration>
{
    private static readonly ConcurrentDictionary<Type, IReadOnlyCollection<Enumeration>> _cache = new();

    /// <summary>
    /// Initializes a new enumeration.
    /// </summary>
    protected Enumeration(int id, string name)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Id = id;
        Name = name;
    }

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Gets the display name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Returns every declared value of the enumeration.
    /// </summary>
    public static IReadOnlyCollection<T> GetAll<T>() where T : Enumeration
    {
        return (IReadOnlyCollection<T>)_cache.GetOrAdd(
            typeof(T),
            static type =>
            {
                return
                [
                    .. type
                        .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                        .Where(f => f.FieldType == type)
                        .Select(f => (Enumeration)f.GetValue(null)!)
                        .OrderBy(e => e.Id)
                ];
            });
    }

    /// <inheritdoc/>
    public int CompareTo(object? obj)
        => CompareTo(obj as Enumeration);

    /// <inheritdoc/>
    public int CompareTo(Enumeration? other)
    {
        return other is null ? 1 : Id.CompareTo(other.Id);
    }

    /// <inheritdoc/>
    public sealed override string ToString() => Name;

    /// <inheritdoc/>
    public sealed override bool Equals(object? obj)
    {
        return obj is Enumeration other &&
               GetType() == other.GetType() &&
               Id == other.Id;
    }

    /// <inheritdoc/>
    public sealed override int GetHashCode() => HashCode.Combine(GetType(), Id);

    public static bool operator ==(Enumeration? left, Enumeration? right) => Equals(left, right);

    public static bool operator !=(Enumeration? left, Enumeration? right) => !Equals(left, right);
}
