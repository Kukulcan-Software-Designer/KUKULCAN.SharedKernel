namespace KUKULCAN.SharedKernel.Abstractions.Capabilities;

/// <summary>
/// Represents a soft deletable object.
/// </summary>
public interface ISoftDelete
{
    bool IsDeleted { get; set; }

    DateTimeOffset? DeletedOn { get; set; }
}
