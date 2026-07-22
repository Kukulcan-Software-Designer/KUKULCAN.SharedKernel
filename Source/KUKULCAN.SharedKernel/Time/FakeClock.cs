using KUKULCAN.SharedKernel.Abstractions;

namespace KUKULCAN.SharedKernel.Time;

/// <summary>
/// Fake implementation of <see cref="IClock"/> for unit tests.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="FakeClock"/> class.
/// </remarks>
/// <param name="now">
/// Initial instant.
/// </param>
public sealed class FakeClock(DateTimeOffset now) : IClock
{

    /// <inheritdoc />
    public DateTimeOffset UtcNow { get; private set; } = now.ToUniversalTime();

    /// <summary>
    /// Sets the current instant.
    /// </summary>
    /// <param name="utcNow">
    /// New current instant.
    /// </param>
    public void Set(DateTimeOffset utcNow)
    {
        UtcNow = utcNow.ToUniversalTime();
    }

    /// <summary>
    /// Advances the clock by the specified time span.
    /// </summary>
    /// <param name="span">
    /// Time span to advance.
    /// </param>
    public void Advance(TimeSpan span)
    {
        UtcNow = UtcNow.Add(span);
    }

    /// <summary>
    /// Advances the clock by the specified number of days.
    /// </summary>
    public void AdvanceDays(int days)
    {
        UtcNow = UtcNow.AddDays(days);
    }

    /// <summary>
    /// Advances the clock by the specified number of hours.
    /// </summary>
    public void AdvanceHours(double hours)
    {
        UtcNow = UtcNow.AddHours(hours);
    }

    /// <summary>
    /// Advances the clock by the specified number of minutes.
    /// </summary>
    public void AdvanceMinutes(double minutes)
    {
        UtcNow = UtcNow.AddMinutes(minutes);
    }

    /// <summary>
    /// Advances the clock by the specified number of seconds.
    /// </summary>
    public void AdvanceSeconds(double seconds)
    {
        UtcNow = UtcNow.AddSeconds(seconds);
    }

    /// <summary>
    /// Rewinds the clock by the specified time span.
    /// </summary>
    public void Rewind(TimeSpan span)
    {
        UtcNow = UtcNow.Subtract(span);
    }

    /// <summary>
    /// Rewinds the clock by the specified number of days.
    /// </summary>
    /// <param name="days">
    /// Number of days to rewind.
    /// </param>
    public void RewindDays(int days)
    {
        UtcNow = UtcNow.AddDays(-days);
    }

    /// <summary>
    /// Rewinds the clock by the specified number of hours.
    /// </summary>
    /// <param name="hours">
    /// Number of hours to rewind.
    /// </param>
    public void RewindHours(double hours)
    {
        UtcNow = UtcNow.AddHours(-hours);
    }

    /// <summary>
    /// Rewinds the clock by the specified number of minutes.
    /// </summary>
    /// <param name="minutes">
    /// Number of minutes to rewind.
    /// </param>
    public void RewindMinutes(double minutes)
    {
        UtcNow = UtcNow.AddMinutes(-minutes);
    }

    /// <summary>
    /// Rewinds the clock by the specified number of seconds.
    /// </summary>
    /// <param name="seconds">
    /// Number of seconds to rewind.
    /// </param>
    public void RewindSeconds(double seconds)
    {
        UtcNow = UtcNow.AddSeconds(-seconds);
    }
}
