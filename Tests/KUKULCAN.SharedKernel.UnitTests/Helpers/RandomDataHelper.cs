using System;
using System.Collections.Generic;
using System.Linq;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides random data generation utilities for unit tests.
/// </summary>
public sealed class RandomDataHelper
{
    private readonly Random _random;

    /// <summary>
    /// Initializes a new instance of the helper.
    /// </summary>
    public RandomDataHelper() : this(Environment.TickCount)
    {
    }

    /// <summary>
    /// Initializes a new instance using the specified seed.
    /// </summary>
    public RandomDataHelper(int seed)
    {
        _random = new Random(seed);
    }

    /// <summary>
    /// Gets a random integer.
    /// </summary>
    public int NextInt()
    {
        return _random.Next();
    }

    /// <summary>
    /// Gets a random integer within the specified range.
    /// </summary>
    public int NextInt(int min, int max)
    {
        return _random.Next(min, max);
    }

    /// <summary>
    /// Gets a random boolean.
    /// </summary>
    public bool NextBoolean()
    {
        return _random.Next(0, 2) == 1;
    }

    /// <summary>
    /// Gets a random double.
    /// </summary>
    public double NextDouble()
    {
        return _random.NextDouble();
    }

    /// <summary>
    /// Gets a random decimal.
    /// </summary>
    public decimal NextDecimal(decimal min, decimal max)
    {
        decimal value = (decimal)_random.NextDouble();

        return min + (value * (max - min));
    }

    /// <summary>
    /// Gets a random GUID.
    /// </summary>
    public Guid NextGuid()
    {
        return Guid.NewGuid();
    }

    /// <summary>
    /// Gets a random date.
    /// </summary>
    public DateTime NextDate()
    {
        return DateTime.UtcNow.AddDays(_random.Next(-3650, 3650));
    }

    /// <summary>
    /// Gets a random date and time offset.
    /// </summary>
    public DateTimeOffset NextDateTimeOffset()
    {
        return new DateTimeOffset(NextDate(), TimeSpan.Zero);
    }

    /// <summary>
    /// Gets a random string.
    /// </summary>
    public string NextString(int length = 16)
    {
        const string chars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        return new string(
            Enumerable
                .Range(0, length)
                .Select(_ => chars[_random.Next(chars.Length)])
                .ToArray());
    }

    /// <summary>
    /// Selects a random item.
    /// </summary>
    public T NextItem<T>(IReadOnlyList<T> items)
    {
        ArgumentNullException.ThrowIfNull(items);

        if (items.Count == 0)
        {
            throw new ArgumentException("Collection cannot be empty.", nameof(items));
        }

        return items[_random.Next(items.Count)];
    }

    /// <summary>
    /// Creates a random byte array.
    /// </summary>
    public byte[] NextBytes(int length)
    {
        byte[] buffer = new byte[length];

        _random.NextBytes(buffer);

        return buffer;
    }
}
