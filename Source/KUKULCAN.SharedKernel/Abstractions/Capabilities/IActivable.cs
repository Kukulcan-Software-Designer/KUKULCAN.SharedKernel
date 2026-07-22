namespace KUKULCAN.SharedKernel.Abstractions.Capabilities;

/// <summary>
/// Represents an activable object.
/// </summary>
public interface IActivable
{
    bool IsActive { get; set; }
}
