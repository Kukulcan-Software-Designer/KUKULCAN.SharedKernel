using System;

namespace KUKULCAN.SharedKernel.Abstractions.Capabilities;

/// <summary>
/// Represents an object whose creation and modification timestamps are tracked.
/// </summary>
public interface IAuditable
{
    /// <summary>
    /// Gets the date and time when the object was created.
    /// </summary>
    DateTimeOffset CreatedOn { get; }

    /// <summary>
    /// Gets the date and time of the last modification.
    /// </summary>
    DateTimeOffset? ModifiedOn { get; }
}
