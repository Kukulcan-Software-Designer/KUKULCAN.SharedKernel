using System;
using System.Linq;
using System.Reflection;

namespace KUKULCAN.SharedKernel.Domain;

/// <summary>
/// Represents a rich enumeration.
/// </summary>
public abstract class Enumeration : IComparable
{
    private static readonly ConcurrentDictionary<Type, object> _cache = new();

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
    /// <typeparam name="T">The enumeration type.</typeparam>
    /// <returns>
    /// All public static enumeration values declared by <typeparamref name="T"/>,
    /// ordered by identifier.
    /// </returns>
    public static IReadOnlyCollection<T> GetAll<T>()
        where T : Enumeration
    {
        return (IReadOnlyCollection<T>)_cache.GetOrAdd(
            typeof(T),
            static _ => CreateValues<T>());
    }

    private static IReadOnlyCollection<T> CreateValues<T>()
        where T : Enumeration
    {
        return
        [
            .. typeof(T)
                .GetFields(
                    BindingFlags.Public |
                    BindingFlags.Static |
                    BindingFlags.DeclaredOnly)
                .Where(field => field.FieldType == typeof(T))
                .Select(field => (T)field.GetValue(null)!)
                .OrderBy(value => value.Id)
        ];
    }

    /// <inheritdoc />
    public int CompareTo(object? obj)
        => CompareTo(obj as Enumeration);

        /// <summary>
        /// Compares this enumeration instance with another enumeration by identifier.
        /// </summary>
        /// <param name="other">
        /// The enumeration instance to compare with the current instance.
        /// </param>
        /// <returns>
        /// A value less than zero if the current identifier is less than
        /// <paramref name="other"/>'s identifier; zero if they are equal; or a value
        /// greater than zero if the current identifier is greater.
        /// Returns <c>1</c> when <paramref name="other"/> is <see langword="null"/>.
        /// </returns>
        public int CompareTo(Enumeration? other)
        => other is null
            ? 1
            : Id.CompareTo(other.Id);

    /// <inheritdoc />
    public sealed override string ToString()
        => Name;

    /// <inheritdoc />
    public sealed override bool Equals(object? obj)
    {
        return obj is Enumeration other &&
               GetType() == other.GetType() &&
               Id == other.Id;
    }

    /// <inheritdoc />
    public sealed override int GetHashCode()
        => HashCode.Combine(GetType(), Id);

    public static bool operator ==(
        Enumeration? left,
        Enumeration? right)
        => Equals(left, right);

    public static bool operator !=(
        Enumeration? left,
        Enumeration? right)
        => !Equals(left, right);
}
