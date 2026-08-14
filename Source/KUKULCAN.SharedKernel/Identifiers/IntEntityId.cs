using KUKULCAN.SharedKernel.Identifiers.Internals;

namespace KUKULCAN.SharedKernel.Identifiers;

/// <summary>
/// Represents an entity identifier based on <see cref="int"/>.
/// </summary>
public abstract class IntEntityId : EntityId<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IntEntityId"/> class.
    /// This constructor is intended only for Entity Framework Core.
    /// </summary>
    protected IntEntityId()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntEntityId"/> class.
    /// </summary>
    /// <param name="value">
    /// Identifier value.
    /// </param>
    protected IntEntityId(int value) : base(value)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), IdentifierMessages.IntegerMustBeGreaterThanZero());
        }
    }
}
