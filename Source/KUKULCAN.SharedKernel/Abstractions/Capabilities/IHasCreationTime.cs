namespace KUKULCAN.SharedKernel.Abstractions.Capabilities;

/// <summary>
/// Represents an object with a creation timestamp.
/// </summary>
public interface IHasCreationTime
{
    DateTimeOffset CreatedOn { get; set; }
}
