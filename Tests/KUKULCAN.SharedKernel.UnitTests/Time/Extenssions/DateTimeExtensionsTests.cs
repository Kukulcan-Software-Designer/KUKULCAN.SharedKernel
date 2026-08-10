using KUKULCAN.SharedKernel.Time.Extensions;

namespace KUKULCAN.SharedKernel.UnitTests.Time.Extenssions;

/// <summary>
/// Contains unit tests for
/// <see cref="DateTimeExtensions"/>.
/// </summary>
[TestFixture]
public sealed class DateTimeExtensionsTests
{
    /// <summary>
    /// Verifies that ToDateOnly returns the date portion of the value.
    /// </summary>
    [Test]
    public void ToDateOnly_ShouldReturnDatePortion()
    {
        var value = new DateTime(
            2026,
            8,
            10,
            14,
            35,
            27);

        var result = value.ToDateOnly();

        Assert.That(
            result,
            Is.EqualTo(new DateOnly(2026, 8, 10)));
    }

    /// <summary>
    /// Verifies that ToDateOnly ignores the time portion.
    /// </summary>
    [Test]
    public void ToDateOnly_WithDifferentTimes_ShouldReturnSameDate()
    {
        var morning = new DateTime(
            2026,
            8,
            10,
            8,
            0,
            0);

        var evening = new DateTime(
            2026,
            8,
            10,
            23,
            59,
            59);

        Assert.That(
            morning.ToDateOnly(),
            Is.EqualTo(evening.ToDateOnly()));
    }

    /// <summary>
    /// Verifies that ToDateOnly preserves the year, month and day.
    /// </summary>
    [Test]
    public void ToDateOnly_ShouldPreserveYearMonthAndDay()
    {
        var value = new DateTime(
            2035,
            12,
            31,
            23,
            59,
            59);

        var result = value.ToDateOnly();

        Assert.Multiple(() =>
        {
            Assert.That(result.Year, Is.EqualTo(2035));
            Assert.That(result.Month, Is.EqualTo(12));
            Assert.That(result.Day, Is.EqualTo(31));
        });
    }

    /// <summary>
    /// Verifies that ToDateOnly works with DateTime values whose Kind is Utc.
    /// </summary>
    [Test]
    public void ToDateOnly_WithUtcDateTime_ShouldReturnDatePortion()
    {
        var value = new DateTime(
            2026,
            8,
            10,
            14,
            35,
            27,
            DateTimeKind.Utc);

        var result = value.ToDateOnly();

        Assert.That(
            result,
            Is.EqualTo(new DateOnly(2026, 8, 10)));
    }

    /// <summary>
    /// Verifies that ToDateOnly works with DateTime values whose Kind is Local.
    /// </summary>
    [Test]
    public void ToDateOnly_WithLocalDateTime_ShouldReturnDatePortion()
    {
        var value = new DateTime(
            2026,
            8,
            10,
            14,
            35,
            27,
            DateTimeKind.Local);

        var result = value.ToDateOnly();

        Assert.That(
            result,
            Is.EqualTo(new DateOnly(2026, 8, 10)));
    }

    /// <summary>
    /// Verifies that ToDateOnly works with an unspecified DateTime kind.
    /// </summary>
    [Test]
    public void ToDateOnly_WithUnspecifiedDateTime_ShouldReturnDatePortion()
    {
        var value = new DateTime(
            2026,
            8,
            10,
            14,
            35,
            27,
            DateTimeKind.Unspecified);

        var result = value.ToDateOnly();

        Assert.That(
            result,
            Is.EqualTo(new DateOnly(2026, 8, 10)));
    }

    /// <summary>
    /// Verifies that ToTimeOnly returns the time portion of the value.
    /// </summary>
    [Test]
    public void ToTimeOnly_ShouldReturnTimePortion()
    {
        var value = new DateTime(
            2026,
            8,
            10,
            14,
            35,
            27);

        var result = value.ToTimeOnly();

        Assert.That(
            result,
            Is.EqualTo(new TimeOnly(14, 35, 27)));
    }

    /// <summary>
    /// Verifies that ToTimeOnly ignores the date portion.
    /// </summary>
    [Test]
    public void ToTimeOnly_WithDifferentDates_ShouldReturnSameTime()
    {
        var first = new DateTime(
            2026,
            1,
            1,
            14,
            35,
            27);

        var second = new DateTime(
            2035,
            12,
            31,
            14,
            35,
            27);

        Assert.That(
            first.ToTimeOnly(),
            Is.EqualTo(second.ToTimeOnly()));
    }

    /// <summary>
    /// Verifies that ToTimeOnly preserves hours, minutes and seconds.
    /// </summary>
    [Test]
    public void ToTimeOnly_ShouldPreserveTimeComponents()
    {
        var value = new DateTime(
            2035,
            12,
            31,
            23,
            59,
            58);

        var result = value.ToTimeOnly();

        Assert.Multiple(() =>
        {
            Assert.That(result.Hour, Is.EqualTo(23));
            Assert.That(result.Minute, Is.EqualTo(59));
            Assert.That(result.Second, Is.EqualTo(58));
        });
    }

    /// <summary>
    /// Verifies that ToTimeOnly preserves fractional-second precision.
    /// </summary>
    [Test]
    public void ToTimeOnly_ShouldPreserveTicks()
    {
        var value = new DateTime(
            2026,
            8,
            10,
            14,
            35,
            27,
            123);

        var result = value.ToTimeOnly();

        Assert.That(
            result.Ticks,
            Is.EqualTo(value.TimeOfDay.Ticks));
    }

    /// <summary>
    /// Verifies that ToTimeOnly works with a UTC DateTime.
    /// </summary>
    [Test]
    public void ToTimeOnly_WithUtcDateTime_ShouldReturnTimePortion()
    {
        var value = new DateTime(
            2026,
            8,
            10,
            14,
            35,
            27,
            DateTimeKind.Utc);

        var result = value.ToTimeOnly();

        Assert.That(
            result,
            Is.EqualTo(new TimeOnly(14, 35, 27)));
    }

    /// <summary>
    /// Verifies that ToTimeOnly works with a local DateTime.
    /// </summary>
    [Test]
    public void ToTimeOnly_WithLocalDateTime_ShouldReturnTimePortion()
    {
        var value = new DateTime(
            2026,
            8,
            10,
            14,
            35,
            27,
            DateTimeKind.Local);

        var result = value.ToTimeOnly();

        Assert.That(
            result,
            Is.EqualTo(new TimeOnly(14, 35, 27)));
    }

    /// <summary>
    /// Verifies that ToTimeOnly works with an unspecified DateTime kind.
    /// </summary>
    [Test]
    public void ToTimeOnly_WithUnspecifiedDateTime_ShouldReturnTimePortion()
    {
        var value = new DateTime(
            2026,
            8,
            10,
            14,
            35,
            27,
            DateTimeKind.Unspecified);

        var result = value.ToTimeOnly();

        Assert.That(
            result,
            Is.EqualTo(new TimeOnly(14, 35, 27)));
    }
}
