using KUKULCAN.SharedKernel.Abstractions;
using KUKULCAN.SharedKernel.Time;
using KUKULCAN.SharedKernel.Time.Extensions;

namespace KUKULCAN.SharedKernel.UnitTests.Time.Extenssions;

/// <summary>
/// Contains unit tests for <see cref="ClockExtensions"/>.
/// </summary>
[TestFixture]
public sealed class ClockExtensionsTests
{
    private static readonly DateTimeOffset _utcInstant =
        new(
            2026,
            8,
            10,
            14,
            35,
            27,
            TimeSpan.Zero);

    #region Today

    /// <summary>
    /// Verifies that Today returns the UTC date supplied by the clock.
    /// </summary>
    [Test]
    public void Today_ShouldReturnUtcDate()
    {
        var clock = new FakeClock(_utcInstant);

        var result = clock.Today();

        Assert.That(
            result,
            Is.EqualTo(new DateOnly(2026, 8, 10)));
    }

    /// <summary>
    /// Verifies that Today uses the UTC date rather than the local offset
    /// represented by the original DateTimeOffset.
    /// </summary>
    [Test]
    public void Today_WithNonUtcOffset_ShouldReturnUtcDate()
    {
        var instant = new DateTimeOffset(
            2026,
            8,
            10,
            00,
            30,
            00,
            TimeSpan.FromHours(-2));

        var clock = new FakeClock(instant);

        var result = clock.Today();

        Assert.That(
            result,
            Is.EqualTo(new DateOnly(2026, 8, 10)));
    }

    /// <summary>
    /// Verifies that Today uses the UTC date when the local representation
    /// belongs to the previous calendar day.
    /// </summary>
    [Test]
    public void Today_WhenUtcConversionChangesCalendarDay_ShouldReturnUtcDate()
    {
        var instant = new DateTimeOffset(
            2026,
            8,
            10,
            23,
            30,
            00,
            TimeSpan.FromHours(-2));

        var clock = new FakeClock(instant);

        var result = clock.Today();

        Assert.That(
            result,
            Is.EqualTo(new DateOnly(2026, 8, 11)));
    }

    /// <summary>
    /// Verifies that Today throws when the clock is null.
    /// </summary>
    [Test]
    public void Today_WithNullClock_ShouldThrowArgumentNullException()
    {
        IClock? clock = null;

        Assert.That(
            () => clock!.Today(),
            Throws.TypeOf<ArgumentNullException>());
    }

    #endregion

    #region CurrentTime

    /// <summary>
    /// Verifies that CurrentTime returns the UTC time supplied by the clock.
    /// </summary>
    [Test]
    public void CurrentTime_ShouldReturnUtcTime()
    {
        var clock = new FakeClock(_utcInstant);

        var result = clock.CurrentTime();

        Assert.That(
            result,
            Is.EqualTo(new TimeOnly(14, 35, 27)));
    }

    /// <summary>
    /// Verifies that CurrentTime uses the UTC representation of the instant.
    /// </summary>
    [Test]
    public void CurrentTime_WithNonUtcOffset_ShouldReturnUtcTime()
    {
        var instant = new DateTimeOffset(
            2026,
            8,
            10,
            16,
            35,
            27,
            TimeSpan.FromHours(2));

        var clock = new FakeClock(instant);

        var result = clock.CurrentTime();

        Assert.That(
            result,
            Is.EqualTo(new TimeOnly(14, 35, 27)));
    }

    /// <summary>
    /// Verifies that CurrentTime preserves fractional-second precision.
    /// </summary>
    [Test]
    public void CurrentTime_ShouldPreserveTicks()
    {
        var instant = new DateTimeOffset(
            2026,
            8,
            10,
            14,
            35,
            27,
            123,
            TimeSpan.Zero);

        var clock = new FakeClock(instant);

        var result = clock.CurrentTime();

        Assert.That(
            result.Ticks,
            Is.EqualTo(instant.UtcDateTime.TimeOfDay.Ticks));
    }

    /// <summary>
    /// Verifies that CurrentTime throws when the clock is null.
    /// </summary>
    [Test]
    public void CurrentTime_WithNullClock_ShouldThrowArgumentNullException()
    {
        IClock? clock = null;

        Assert.That(
            () => clock!.CurrentTime(),
            Throws.TypeOf<ArgumentNullException>());
    }

    #endregion

    #region IsWeekend

    /// <summary>
    /// Verifies that Saturday is considered a weekend day.
    /// </summary>
    [Test]
    public void IsWeekend_WithSaturday_ShouldReturnTrue()
    {
        var instant = new DateTimeOffset(
            2026,
            8,
            8,
            12,
            00,
            00,
            TimeSpan.Zero);

        var clock = new FakeClock(instant);

        Assert.That(
            clock.IsWeekend(),
            Is.True);
    }

    /// <summary>
    /// Verifies that Sunday is considered a weekend day.
    /// </summary>
    [Test]
    public void IsWeekend_WithSunday_ShouldReturnTrue()
    {
        var instant = new DateTimeOffset(
            2026,
            8,
            9,
            12,
            00,
            00,
            TimeSpan.Zero);

        var clock = new FakeClock(instant);

        Assert.That(
            clock.IsWeekend(),
            Is.True);
    }

    /// <summary>
    /// Verifies that Monday is not considered a weekend day.
    /// </summary>
    [Test]
    public void IsWeekend_WithMonday_ShouldReturnFalse()
    {
        var instant = new DateTimeOffset(
            2026,
            8,
            10,
            12,
            00,
            00,
            TimeSpan.Zero);

        var clock = new FakeClock(instant);

        Assert.That(
            clock.IsWeekend(),
            Is.False);
    }

    /// <summary>
    /// Verifies that Friday is not considered a weekend day.
    /// </summary>
    [Test]
    public void IsWeekend_WithFriday_ShouldReturnFalse()
    {
        var instant = new DateTimeOffset(
            2026,
            8,
            7,
            12,
            00,
            00,
            TimeSpan.Zero);

        var clock = new FakeClock(instant);

        Assert.That(
            clock.IsWeekend(),
            Is.False);
    }

    /// <summary>
    /// Verifies that IsWeekend throws when the clock is null.
    /// </summary>
    [Test]
    public void IsWeekend_WithNullClock_ShouldThrowArgumentNullException()
    {
        IClock? clock = null;

        Assert.That(
            () => clock!.IsWeekend(),
            Throws.TypeOf<ArgumentNullException>());
    }

    #endregion

    #region IsWeekday

    /// <summary>
    /// Verifies that a weekday is identified as a weekday.
    /// </summary>
    [Test]
    public void IsWeekday_WithWeekday_ShouldReturnTrue()
    {
        var instant = new DateTimeOffset(
            2026,
            8,
            10,
            12,
            00,
            00,
            TimeSpan.Zero);

        var clock = new FakeClock(instant);

        Assert.That(
            clock.IsWeekday(),
            Is.True);
    }

    /// <summary>
    /// Verifies that Saturday is not considered a weekday.
    /// </summary>
    [Test]
    public void IsWeekday_WithSaturday_ShouldReturnFalse()
    {
        var instant = new DateTimeOffset(
            2026,
            8,
            8,
            12,
            00,
            00,
            TimeSpan.Zero);

        var clock = new FakeClock(instant);

        Assert.That(
            clock.IsWeekday(),
            Is.False);
    }

    /// <summary>
    /// Verifies that Sunday is not considered a weekday.
    /// </summary>
    [Test]
    public void IsWeekday_WithSunday_ShouldReturnFalse()
    {
        var instant = new DateTimeOffset(
            2026,
            8,
            9,
            12,
            00,
            00,
            TimeSpan.Zero);

        var clock = new FakeClock(instant);

        Assert.That(
            clock.IsWeekday(),
            Is.False);
    }

    /// <summary>
    /// Verifies that IsWeekday throws when the clock is null.
    /// </summary>
    [Test]
    public void IsWeekday_WithNullClock_ShouldThrowArgumentNullException()
    {
        IClock? clock = null;

        Assert.That(
            () => clock!.IsWeekday(),
            Throws.TypeOf<ArgumentNullException>());
    }

    #endregion

    #region IsToday

    /// <summary>
    /// Verifies that a date representing the same UTC calendar day
    /// is identified as today.
    /// </summary>
    [Test]
    public void IsToday_WithSameUtcDate_ShouldReturnTrue()
    {
        var clock = new FakeClock(_utcInstant);

        var date = new DateTimeOffset(
            2026,
            8,
            10,
            23,
            59,
            59,
            TimeSpan.Zero);

        Assert.That(
            clock.IsToday(date),
            Is.True);
    }

    /// <summary>
    /// Verifies that a date from another UTC calendar day is not identified
    /// as today.
    /// </summary>
    [Test]
    public void IsToday_WithDifferentUtcDate_ShouldReturnFalse()
    {
        var clock = new FakeClock(_utcInstant);

        var date = new DateTimeOffset(
            2026,
            8,
            11,
            00,
            00,
            00,
            TimeSpan.Zero);

        Assert.That(
            clock.IsToday(date),
            Is.False);
    }

    /// <summary>
    /// Verifies that IsToday compares UTC dates rather than the local
    /// calendar representation of the supplied DateTimeOffset.
    /// </summary>
    [Test]
    public void IsToday_WithDifferentOffsetSameUtcDate_ShouldReturnTrue()
    {
        var clock = new FakeClock(
            new DateTimeOffset(
                2026,
                8,
                10,
                23,
                30,
                00,
                TimeSpan.Zero));

        var date = new DateTimeOffset(
            2026,
            8,
            11,
            01,
            30,
            00,
            TimeSpan.FromHours(2));

        Assert.That(
            clock.IsToday(date),
            Is.True);
    }

    /// <summary>
    /// Verifies that IsToday returns false when the supplied instant has
    /// the same local calendar date but a different UTC calendar date.
    /// </summary>
    [Test]
    public void IsToday_WithSameLocalDateDifferentUtcDate_ShouldReturnFalse()
    {
        var clock = new FakeClock(
            new DateTimeOffset(
                2026,
                8,
                10,
                23,
                30,
                00,
                TimeSpan.Zero));

        var date = new DateTimeOffset(
            2026,
            8,
            10,
            23,
            30,
            00,
            TimeSpan.FromHours(-2));

        Assert.That(
            clock.IsToday(date),
            Is.False);
    }

    /// <summary>
    /// Verifies that IsToday throws when the clock is null.
    /// </summary>
    [Test]
    public void IsToday_WithNullClock_ShouldThrowArgumentNullException()
    {
        IClock? clock = null;

        var date = _utcInstant;

        Assert.That(
            () => clock!.IsToday(date),
            Throws.TypeOf<ArgumentNullException>());
    }

    #endregion

    #region IsFuture

    /// <summary>
    /// Verifies that an instant after the clock instant is in the future.
    /// </summary>
    [Test]
    public void IsFuture_WithFutureInstant_ShouldReturnTrue()
    {
        var clock = new FakeClock(_utcInstant);
        var date = _utcInstant.AddMinutes(1);

        Assert.That(
            clock.IsFuture(date),
            Is.True);
    }

    /// <summary>
    /// Verifies that an instant before the clock instant is not in the future.
    /// </summary>
    [Test]
    public void IsFuture_WithPastInstant_ShouldReturnFalse()
    {
        var clock = new FakeClock(_utcInstant);
        var date = _utcInstant.AddMinutes(-1);

        Assert.That(
            clock.IsFuture(date),
            Is.False);
    }

    /// <summary>
    /// Verifies that an instant equal to the clock instant is not in the future.
    /// </summary>
    [Test]
    public void IsFuture_WithSameInstant_ShouldReturnFalse()
    {
        var clock = new FakeClock(_utcInstant);

        Assert.That(
            clock.IsFuture(_utcInstant),
            Is.False);
    }

    /// <summary>
    /// Verifies that IsFuture compares instants correctly when offsets differ.
    /// </summary>
    [Test]
    public void IsFuture_WithDifferentOffset_ShouldCompareInstants()
    {
        var clock = new FakeClock(
            new DateTimeOffset(
                2026,
                8,
                10,
                14,
                35,
                27,
                TimeSpan.Zero));

        var future = new DateTimeOffset(
            2026,
            8,
            10,
            17,
                35,
                27,
                TimeSpan.FromHours(2));

        Assert.That(
            clock.IsFuture(future),
            Is.True);
    }

    /// <summary>
    /// Verifies that IsFuture throws when the clock is null.
    /// </summary>
    [Test]
    public void IsFuture_WithNullClock_ShouldThrowArgumentNullException()
    {
        IClock? clock = null;

        Assert.That(
            () => clock!.IsFuture(_utcInstant),
            Throws.TypeOf<ArgumentNullException>());
    }

    #endregion

    #region IsPast

    /// <summary>
    /// Verifies that an instant before the clock instant is in the past.
    /// </summary>
    [Test]
    public void IsPast_WithPastInstant_ShouldReturnTrue()
    {
        var clock = new FakeClock(_utcInstant);
        var date = _utcInstant.AddMinutes(-1);

        Assert.That(
            clock.IsPast(date),
            Is.True);
    }

    /// <summary>
    /// Verifies that an instant after the clock instant is not in the past.
    /// </summary>
    [Test]
    public void IsPast_WithFutureInstant_ShouldReturnFalse()
    {
        var clock = new FakeClock(_utcInstant);
        var date = _utcInstant.AddMinutes(1);

        Assert.That(
            clock.IsPast(date),
            Is.False);
    }

    /// <summary>
    /// Verifies that an instant equal to the clock instant is not in the past.
    /// </summary>
    [Test]
    public void IsPast_WithSameInstant_ShouldReturnFalse()
    {
        var clock = new FakeClock(_utcInstant);

        Assert.That(
            clock.IsPast(_utcInstant),
            Is.False);
    }

    /// <summary>
    /// Verifies that IsPast compares instants correctly when offsets differ.
    /// </summary>
    [Test]
    public void IsPast_WithDifferentOffset_ShouldCompareInstants()
    {
        var clock = new FakeClock(
            new DateTimeOffset(
                2026,
                8,
                10,
                14,
                35,
                27,
                TimeSpan.Zero));

        var past = new DateTimeOffset(
            2026,
            8,
            10,
            15,
                35,
                27,
                TimeSpan.FromHours(2));

        Assert.That(
            clock.IsPast(past),
            Is.True);
    }

    /// <summary>
    /// Verifies that IsPast throws when the clock is null.
    /// </summary>
    [Test]
    public void IsPast_WithNullClock_ShouldThrowArgumentNullException()
    {
        IClock? clock = null;

        Assert.That(
            () => clock!.IsPast(_utcInstant),
            Throws.TypeOf<ArgumentNullException>());
    }

    #endregion
}
