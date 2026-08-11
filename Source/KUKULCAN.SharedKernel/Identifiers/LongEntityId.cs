using KUKULCAN.SharedKernel.Identifiers.Internals;

namespace KUKULCAN.SharedKernel.Identifiers;

/// <summary>
/// Represents an entity identifier based on <see cref="long"/>.
/// </summary>
public abstract class LongEntityId : EntityId<long>
{
    protected LongEntityId()
    {
    }

    protected LongEntityId(long value) : base(value)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), IdentifierMessages.LongMustBeGreaterThanZero());
        }
    }
}
