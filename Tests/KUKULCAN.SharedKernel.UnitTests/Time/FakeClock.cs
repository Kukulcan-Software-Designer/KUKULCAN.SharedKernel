using KUKULCAN.SharedKernel.Abstractions;
using KUKULCAN.SharedKernel.Time;

namespace KUKULCAN.SharedKernel.UnitTests.Time;

/// <summary>
/// Contains unit tests for <see cref="FakeClock"/>.
/// </summary>
[TestFixture]
public sealed class FakeClockTests
{
    private static readonly DateTimeOffset _initialInstant =
        new(
            2026,
            8,
            10,
            10,
            30,
            45,
            TimeSpan.Zero);

    #region Constructor

    /// <summary>
    /// Verifies that the constructor stores the supplied UTC instant.
    /// </summary>
    [Test]
    public void Constructor_WithUtcInstant_ShouldSetUtcNow()
    {
        var clock = new FakeClock(_initialInstant);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant));
    }

    /// <summary>
    /// Verifies that the constructor converts a non-UTC instant to UTC.
    /// </summary>
    [Test]
    public void Constructor_WithNonUtcInstant_ShouldNormalizeToUtc()
    {
        var localInstant = new DateTimeOffset(
            2026,
            8,
            10,
            12,
            30,
            45,
            TimeSpan.FromHours(2));

        var clock = new FakeClock(localInstant);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(localInstant.ToUniversalTime()));

        Assert.That(
            clock.UtcNow.Offset,
            Is.EqualTo(TimeSpan.Zero));
    }

    /// <summary>
    /// Verifies that FakeClock implements IClock.
    /// </summary>
    [Test]
    public void FakeClock_ShouldImplementIClock()
    {
        var clock = new FakeClock(_initialInstant);

        Assert.That(
            clock,
            Is.InstanceOf<IClock>());
    }

    #endregion

    #region Set

    /// <summary>
    /// Verifies that Set changes the current instant.
    /// </summary>
    [Test]
    public void Set_WithUtcInstant_ShouldUpdateUtcNow()
    {
        var clock = new FakeClock(_initialInstant);
        var newInstant = _initialInstant.AddHours(5);

        clock.Set(newInstant);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(newInstant));
    }

    /// <summary>
    /// Verifies that Set normalizes a non-UTC instant.
    /// </summary>
    [Test]
    public void Set_WithNonUtcInstant_ShouldNormalizeToUtc()
    {
        var clock = new FakeClock(_initialInstant);

        var localInstant = new DateTimeOffset(
            2026,
            8,
            10,
            15,
            30,
            45,
            TimeSpan.FromHours(3));

        clock.Set(localInstant);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(localInstant.ToUniversalTime()));

        Assert.That(
            clock.UtcNow.Offset,
            Is.EqualTo(TimeSpan.Zero));
    }

    #endregion

    #region Advance(TimeSpan)

    /// <summary>
    /// Verifies that Advance moves the clock forward by the supplied span.
    /// </summary>
    [Test]
    public void Advance_WithPositiveSpan_ShouldMoveClockForward()
    {
        var clock = new FakeClock(_initialInstant);
        var span = TimeSpan.FromHours(2);

        clock.Advance(span);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant.Add(span)));
    }

    /// <summary>
    /// Verifies that Advance accepts a zero time span without changing
    /// the current instant.
    /// </summary>
    [Test]
    public void Advance_WithZeroSpan_ShouldNotChangeClock()
    {
        var clock = new FakeClock(_initialInstant);

        clock.Advance(TimeSpan.Zero);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant));
    }

    /// <summary>
    /// Verifies that Advance also supports negative spans according to
    /// TimeSpan arithmetic.
    /// </summary>
    [Test]
    public void Advance_WithNegativeSpan_ShouldMoveClockBackward()
    {
        var clock = new FakeClock(_initialInstant);
        var span = TimeSpan.FromMinutes(-30);

        clock.Advance(span);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant.Add(span)));
    }

    #endregion

    #region AdvanceDays

    /// <summary>
    /// Verifies that AdvanceDays moves the clock forward by days.
    /// </summary>
    [Test]
    public void AdvanceDays_WithPositiveDays_ShouldMoveClockForward()
    {
        var clock = new FakeClock(_initialInstant);

        clock.AdvanceDays(3);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant.AddDays(3)));
    }

    /// <summary>
    /// Verifies that AdvanceDays with zero does not change the clock.
    /// </summary>
    [Test]
    public void AdvanceDays_WithZero_ShouldNotChangeClock()
    {
        var clock = new FakeClock(_initialInstant);

        clock.AdvanceDays(0);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant));
    }

    #endregion

    #region AdvanceHours

    /// <summary>
    /// Verifies that AdvanceHours moves the clock forward by fractional hours.
    /// </summary>
    [Test]
    public void AdvanceHours_WithPositiveHours_ShouldMoveClockForward()
    {
        var clock = new FakeClock(_initialInstant);

        clock.AdvanceHours(2.5);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant.AddHours(2.5)));
    }

    /// <summary>
    /// Verifies that AdvanceHours with zero does not change the clock.
    /// </summary>
    [Test]
    public void AdvanceHours_WithZero_ShouldNotChangeClock()
    {
        var clock = new FakeClock(_initialInstant);

        clock.AdvanceHours(0);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant));
    }

    #endregion

    #region AdvanceMinutes

    /// <summary>
    /// Verifies that AdvanceMinutes moves the clock forward by minutes.
    /// </summary>
    [Test]
    public void AdvanceMinutes_WithPositiveMinutes_ShouldMoveClockForward()
    {
        var clock = new FakeClock(_initialInstant);

        clock.AdvanceMinutes(90);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant.AddMinutes(90)));
    }

    /// <summary>
    /// Verifies that AdvanceMinutes accepts fractional minutes.
    /// </summary>
    [Test]
    public void AdvanceMinutes_WithFractionalMinutes_ShouldMoveClockForward()
    {
        var clock = new FakeClock(_initialInstant);

        clock.AdvanceMinutes(2.5);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant.AddMinutes(2.5)));
    }

    #endregion

    #region AdvanceSeconds

    /// <summary>
    /// Verifies that AdvanceSeconds moves the clock forward by seconds.
    /// </summary>
    [Test]
    public void AdvanceSeconds_WithPositiveSeconds_ShouldMoveClockForward()
    {
        var clock = new FakeClock(_initialInstant);

        clock.AdvanceSeconds(30);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant.AddSeconds(30)));
    }

    /// <summary>
    /// Verifies that AdvanceSeconds accepts fractional seconds.
    /// </summary>
    [Test]
    public void AdvanceSeconds_WithFractionalSeconds_ShouldMoveClockForward()
    {
        var clock = new FakeClock(_initialInstant);

        clock.AdvanceSeconds(2.5);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant.AddSeconds(2.5)));
    }

    #endregion

    #region Rewind(TimeSpan)

    /// <summary>
    /// Verifies that Rewind moves the clock backward by the supplied span.
    /// </summary>
    [Test]
    public void Rewind_WithPositiveSpan_ShouldMoveClockBackward()
    {
        var clock = new FakeClock(_initialInstant);
        var span = TimeSpan.FromHours(2);

        clock.Rewind(span);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant.Subtract(span)));
    }

    /// <summary>
    /// Verifies that Rewind with zero does not change the clock.
    /// </summary>
    [Test]
    public void Rewind_WithZeroSpan_ShouldNotChangeClock()
    {
        var clock = new FakeClock(_initialInstant);

        clock.Rewind(TimeSpan.Zero);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant));
    }

    /// <summary>
    /// Verifies that Rewind also follows TimeSpan subtraction semantics
    /// for negative spans.
    /// </summary>
    [Test]
    public void Rewind_WithNegativeSpan_ShouldMoveClockForward()
    {
        var clock = new FakeClock(_initialInstant);
        var span = TimeSpan.FromMinutes(-30);

        clock.Rewind(span);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant.Subtract(span)));
    }

    #endregion

    #region RewindDays

    /// <summary>
    /// Verifies that RewindDays moves the clock backward by days.
    /// </summary>
    [Test]
    public void RewindDays_WithPositiveDays_ShouldMoveClockBackward()
    {
        var clock = new FakeClock(_initialInstant);

        clock.RewindDays(3);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant.AddDays(-3)));
    }

    /// <summary>
    /// Verifies that RewindDays with zero does not change the clock.
    /// </summary>
    [Test]
    public void RewindDays_WithZero_ShouldNotChangeClock()
    {
        var clock = new FakeClock(_initialInstant);

        clock.RewindDays(0);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant));
    }

    #endregion

    #region RewindHours

    /// <summary>
    /// Verifies that RewindHours moves the clock backward by hours.
    /// </summary>
    [Test]
    public void RewindHours_WithPositiveHours_ShouldMoveClockBackward()
    {
        var clock = new FakeClock(_initialInstant);

        clock.RewindHours(2.5);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant.AddHours(-2.5)));
    }

    /// <summary>
    /// Verifies that RewindHours with zero does not change the clock.
    /// </summary>
    [Test]
    public void RewindHours_WithZero_ShouldNotChangeClock()
    {
        var clock = new FakeClock(_initialInstant);

        clock.RewindHours(0);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant));
    }

    #endregion

    #region RewindMinutes

    /// <summary>
    /// Verifies that RewindMinutes moves the clock backward by minutes.
    /// </summary>
    [Test]
    public void RewindMinutes_WithPositiveMinutes_ShouldMoveClockBackward()
    {
        var clock = new FakeClock(_initialInstant);

        clock.RewindMinutes(90);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant.AddMinutes(-90)));
    }

    /// <summary>
    /// Verifies that RewindMinutes accepts fractional minutes.
    /// </summary>
    [Test]
    public void RewindMinutes_WithFractionalMinutes_ShouldMoveClockBackward()
    {
        var clock = new FakeClock(_initialInstant);

        clock.RewindMinutes(2.5);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant.AddMinutes(-2.5)));
    }

    #endregion

    #region RewindSeconds

    /// <summary>
    /// Verifies that RewindSeconds moves the clock backward by seconds.
    /// </summary>
    [Test]
    public void RewindSeconds_WithPositiveSeconds_ShouldMoveClockBackward()
    {
        var clock = new FakeClock(_initialInstant);

        clock.RewindSeconds(30);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant.AddSeconds(-30)));
    }

    /// <summary>
    /// Verifies that RewindSeconds accepts fractional seconds.
    /// </summary>
    [Test]
    public void RewindSeconds_WithFractionalSeconds_ShouldMoveClockBackward()
    {
        var clock = new FakeClock(_initialInstant);

        clock.RewindSeconds(2.5);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(_initialInstant.AddSeconds(-2.5)));
    }

    #endregion

    #region Composition

    /// <summary>
    /// Verifies that consecutive operations are applied cumulatively.
    /// </summary>
    [Test]
    public void MultipleOperations_ShouldApplyCumulatively()
    {
        var clock = new FakeClock(_initialInstant);

        clock
            .AdvanceDays(1);

        clock
            .AdvanceHours(2);

        clock
            .AdvanceMinutes(30);

        clock
            .RewindSeconds(15);

        var expected = _initialInstant
            .AddDays(1)
            .AddHours(2)
            .AddMinutes(30)
            .AddSeconds(-15);

        Assert.That(
            clock.UtcNow,
            Is.EqualTo(expected));
    }

    #endregion
}
