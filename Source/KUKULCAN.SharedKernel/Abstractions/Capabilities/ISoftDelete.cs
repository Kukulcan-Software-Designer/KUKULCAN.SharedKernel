namespace KUKULCAN.SharedKernel.Abstractions.Capabilities;

/// <summary>
/// Represents an object that supports logical deletion.
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// Gets a value indicating whether the object has been logically deleted.
    /// </summary>
    bool IsDeleted { get; }

    /// <summary>
    /// Gets the date and time when the object was logically deleted.
    /// </summary>
    DateTimeOffset? DeletedOn { get; }
}
