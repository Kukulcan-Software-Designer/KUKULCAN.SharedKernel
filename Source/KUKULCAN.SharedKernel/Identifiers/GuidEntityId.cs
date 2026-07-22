using KUKULCAN.SharedKernel.Identifiers.Internals;

namespace KUKULCAN.SharedKernel.Identifiers;

/// <summary>
/// Represents an entity identifier based on <see cref="Guid"/>.
/// </summary>
public abstract class GuidEntityId : EntityId<Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GuidEntityId"/> class.
    /// This constructor is intended only for Entity Framework Core.
    /// </summary>
    protected GuidEntityId()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GuidEntityId"/> class.
    /// </summary>
    /// <param name="value">
    /// Identifier value.
    /// </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="value"/> is <see cref="Guid.Empty"/>.
    /// </exception>
    protected GuidEntityId(Guid value)
        : base(value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException(
                IdentifierMessages.GuidCannotBeEmpty(),
                nameof(value));
        }
    }
}
