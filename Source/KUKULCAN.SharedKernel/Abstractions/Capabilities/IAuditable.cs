namespace KUKULCAN.SharedKernel.Abstractions.Capabilities;

/// <summary>
/// Represents an auditable object.
/// </summary>
public interface IAuditable
{
    DateTimeOffset CreatedOn { get; }

    DateTimeOffset? ModifiedOn { get; }
}
