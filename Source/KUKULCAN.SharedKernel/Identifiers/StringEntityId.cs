namespace KUKULCAN.SharedKernel.Identifiers;

/// <summary>
/// Represents an entity identifier based on <see cref="string"/>.
/// </summary>
public abstract class StringEntityId : EntityId<string>
{
    protected StringEntityId()
    {
    }

    protected StringEntityId(string value) : base(value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
    }
}
