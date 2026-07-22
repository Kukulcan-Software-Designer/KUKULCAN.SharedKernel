namespace KUKULCAN.SharedKernel.Abstractions.Capabilities;

/// <summary>
/// Represents a versioned object.
/// </summary>
public interface IHasVersion
{
    int Version { get; set; }
}
