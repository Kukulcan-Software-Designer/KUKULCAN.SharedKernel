using System;

namespace KUKULCAN.SharedKernel.Identifiers.Interfaces;

/// <summary>
/// Represents a strongly typed entity identifier.
/// </summary>
public interface IEntityId : IEquatable<IEntityId>
{
    /// <summary>
    /// Gets the underlying identifier value.
    /// </summary>
    object? Value { get; }
}
