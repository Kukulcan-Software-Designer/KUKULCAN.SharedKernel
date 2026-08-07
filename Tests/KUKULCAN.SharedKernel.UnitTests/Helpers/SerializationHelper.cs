using System;
using System.Text.Json;

namespace KUKULCAN.SharedKernel.UnitTests.Helpers;

/// <summary>
/// Provides helper methods for object serialization during unit tests.
/// </summary>
public sealed class SerializationHelper
{
    private readonly JsonSerializerOptions _options =
        new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            IncludeFields = true
        };

    /// <summary>
    /// Serializes the specified value to JSON.
    /// </summary>
    public string Serialize<T>(T value)
    {
        return JsonSerializer.Serialize(value, _options);
    }

    /// <summary>
    /// Deserializes the specified JSON.
    /// </summary>
    public T? Deserialize<T>(string json)
    {
        ArgumentNullException.ThrowIfNull(json);

        return JsonSerializer.Deserialize<T>(json, _options);
    }

    /// <summary>
    /// Performs a serialization round-trip.
    /// </summary>
    public T? RoundTrip<T>(T value)
    {
        string json = Serialize(value);

        return Deserialize<T>(json);
    }

    /// <summary>
    /// Determines whether an object can be serialized.
    /// </summary>
    public bool CanSerialize<T>(T value)
    {
        try
        {
            Serialize(value);

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Determines whether the specified JSON can be deserialized.
    /// </summary>
    public bool CanDeserialize<T>(string json)
    {
        try
        {
            Deserialize<T>(json);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
