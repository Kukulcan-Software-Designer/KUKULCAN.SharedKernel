using KUKULCAN.SharedKernel.Abstractions;
using KUKULCAN.SharedKernel.Time;

namespace KUKULCAN.SharedKernel.UnitTests.Time;

/// <summary>
/// Contains unit tests for <see cref="SystemClock"/>.
/// </summary>
[TestFixture]
public sealed class SystemClockTests
{
    /// <summary>
    /// Verifies that SystemClock implements IClock.
    /// </summary>
    [Test]
    public void SystemClock_ShouldImplementIClock()
    {
        var clock = new SystemClock();

        Assert.That(clock, Is.InstanceOf<IClock>());
    }

    /// <summary>
    /// Verifies that UtcNow returns an instant close to the current UTC time.
    /// </summary>
    [Test]
    public void UtcNow_ShouldReturnCurrentUtcTime()
    {
        DateTimeOffset before = DateTimeOffset.UtcNow;
        var clock = new SystemClock();
        DateTimeOffset actual = clock.UtcNow;
        DateTimeOffset after = DateTimeOffset.UtcNow;

        Assert.That(actual, Is.GreaterThanOrEqualTo(before));
        Assert.That(actual, Is.LessThanOrEqualTo(after));
    }

    /// <summary>
    /// Verifies that UtcNow always uses the UTC offset.
    /// </summary>
    [Test]
    public void UtcNow_ShouldHaveZeroOffset()
    {
        var clock = new SystemClock();
        DateTimeOffset actual = clock.UtcNow;

        Assert.That(
            actual.Offset,
            Is.EqualTo(TimeSpan.Zero));
    }

    /// <summary>
    /// Verifies that consecutive UtcNow readings do not move backwards.
    /// </summary>
    [Test]
    public void UtcNow_ConsecutiveReads_ShouldNotMoveBackwards()
    {
        var clock = new SystemClock();
        DateTimeOffset first = clock.UtcNow;
        DateTimeOffset second = clock.UtcNow;

        Assert.That(
            second,
            Is.GreaterThanOrEqualTo(first));
    }

    /// <summary>
    /// Verifies that consecutive UtcNow readings remain within a reasonable
    /// interval around the system UTC clock.
    /// </summary>
    [Test]
    public void UtcNow_ShouldRemainCloseToSystemUtcClock()
    {
        var clock = new SystemClock();

        DateTimeOffset systemBefore = DateTimeOffset.UtcNow;
        DateTimeOffset actual = clock.UtcNow;
        DateTimeOffset systemAfter = DateTimeOffset.UtcNow;

        Assert.Multiple(() =>
        {
            Assert.That(
                actual,
                Is.GreaterThanOrEqualTo(systemBefore));

            Assert.That(
                actual,
                Is.LessThanOrEqualTo(systemAfter));

            Assert.That(
                actual.Offset,
                Is.EqualTo(TimeSpan.Zero));
        });
    }

    /// <summary>
    /// Verifies that multiple reads represent the progression of real time.
    /// </summary>
    [Test]
    public void UtcNow_MultipleReads_ShouldRepresentMonotonicProgression()
    {
        var clock = new SystemClock();

        DateTimeOffset first = clock.UtcNow;

        Thread.Sleep(10);

        DateTimeOffset second = clock.UtcNow;

        Assert.That(
            second,
            Is.GreaterThan(first));
    }
}
