namespace KUKULCAN.SharedKernel.Abstractions;

/// <summary>
/// Represents a system clock.
/// </summary>
public interface IClock
{
    /// <summary>
    /// Gets the current instant.
    /// </summary>
    DateTimeOffset UtcNow { get; }
}
