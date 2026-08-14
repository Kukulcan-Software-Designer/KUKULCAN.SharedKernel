using KUKULCAN.SharedKernel.Identifiers.Internals;

namespace KUKULCAN.SharedKernel.Identifiers;

/// <summary>
/// Represents an entity identifier based on <see cref="long"/>.
/// </summary>
public abstract class LongEntityId : EntityId<long>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LongEntityId"/> class.
    /// This constructor is intended only for Entity Framework Core.
    /// </summary>
    protected LongEntityId()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LongEntityId"/> class.
    /// </summary>
    /// <param name="value">
    /// Identifier value.
    /// </param>
    protected LongEntityId(long value) : base(value)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), IdentifierMessages.LongMustBeGreaterThanZero());
        }
    }
}
