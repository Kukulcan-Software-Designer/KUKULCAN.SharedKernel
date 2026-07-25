using System;
using KUKULCAN.SharedKernel.Identifiers.Internals;

namespace KUKULCAN.SharedKernel.Identifiers;

/// <summary>
/// Represents an entity identifier based on <see cref="int"/>.
/// </summary>
public abstract class IntEntityId : EntityId<int>
{
    protected IntEntityId()
    {
    }

    protected IntEntityId(int value) : base(value)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), IdentifierMessages.IntegerMustBeGreaterThanZero());
        }
    }
}
