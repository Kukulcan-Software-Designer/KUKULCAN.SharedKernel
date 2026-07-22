namespace KUKULCAN.SharedKernel.Abstractions.Capabilities;

/// <summary>
/// Represents an object with optimistic concurrency.
/// </summary>
public interface IConcurrencyAware
{
    byte[] RowVersion { get; set; }
}
