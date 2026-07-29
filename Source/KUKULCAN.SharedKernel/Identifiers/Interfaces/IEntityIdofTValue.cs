using System;

namespace KUKULCAN.SharedKernel.Identifiers.Interfaces;

/// <summary>
/// Represents a strongly typed entity identifier.
/// </summary>
/// <typeparam name="TValue">
/// Type of the underlying identifier.
/// </typeparam>
public interface IEntityId<TValue> : IEntityId, IEquatable<IEntityId<TValue>> where TValue: notnull
{
    /// <summary>
    /// Gets the underlying identifier value.
    /// </summary>
    new TValue Value { get; }
}
