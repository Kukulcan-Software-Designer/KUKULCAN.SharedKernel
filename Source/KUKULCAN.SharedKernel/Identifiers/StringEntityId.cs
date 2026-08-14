namespace KUKULCAN.SharedKernel.Identifiers;

/// <summary>
/// Represents an entity identifier based on <see cref="string"/>.
/// </summary>
public abstract class StringEntityId : EntityId<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StringEntityId"/> class.
    /// This constructor is intended only for Entity Framework Core.
    /// </summary>
    protected StringEntityId()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StringEntityId"/> class.
    /// </summary>
    /// <param name="value">
    /// Identifier value.
    /// </param>
    protected StringEntityId(string value) : base(value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
    }
}
