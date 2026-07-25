using System;
using KUKULCAN.SharedKernel.Abstractions;

namespace KUKULCAN.SharedKernel.Time;

/// <summary>
/// Default implementation of <see cref="IClock"/>.
/// </summary>
public sealed class SystemClock : IClock
{
    /// <inheritdoc />
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
