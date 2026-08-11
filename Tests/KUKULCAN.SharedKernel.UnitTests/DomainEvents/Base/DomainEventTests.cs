using KUKULCAN.SharedKernel.DomainEvents.Abstractions;
using KUKULCAN.SharedKernel.DomainEvents.Base;

namespace KUKULCAN.SharedKernel.UnitTests.DomainEvents.Base;

/// <summary>
/// Contains unit tests for <see cref="DomainEvent"/>.
/// </summary>
[TestFixture]
public sealed class DomainEventTests
{
    #region Construction

    /// <summary>
    /// Verifies that a derived domain event can be constructed.
    /// </summary>
    [Test]
    public void Constructor_ShouldCreateInstance()
    {
        DateTimeOffset before = DateTimeOffset.UtcNow;

        var domainEvent = new TestDomainEvent();

        DateTimeOffset after = DateTimeOffset.UtcNow;

        Assert.That(domainEvent, Is.Not.Null);
        Assert.That(domainEvent.OccurredOn, Is.GreaterThanOrEqualTo(before));
        Assert.That(domainEvent.OccurredOn, Is.LessThanOrEqualTo(after));
    }

    /// <summary>
    /// Verifies that the occurrence timestamp is expressed in UTC.
    /// </summary>
    [Test]
    public void Constructor_ShouldSetOccurredOnToUtc()
    {
        var domainEvent = new TestDomainEvent();

        Assert.That(
            domainEvent.OccurredOn.Offset,
            Is.EqualTo(TimeSpan.Zero));
    }

    /// <summary>
    /// Verifies that the occurrence timestamp is initialized close
    /// to the moment when the event is created.
    /// </summary>
    [Test]
    public void Constructor_ShouldSetOccurredOnToCurrentInstant()
    {
        DateTimeOffset before = DateTimeOffset.UtcNow;

        var domainEvent = new TestDomainEvent();

        DateTimeOffset after = DateTimeOffset.UtcNow;

        Assert.That(
            domainEvent.OccurredOn,
            Is.InRange(before, after));
    }

    #endregion

    #region Contract

    /// <summary>
    /// Verifies that a domain event implements <see cref="IDomainEvent"/>.
    /// </summary>
    [Test]
    public void DomainEvent_ShouldImplementIDomainEvent()
    {
        var domainEvent = new TestDomainEvent();

        Assert.That(
            domainEvent,
            Is.InstanceOf<IDomainEvent>());
    }

    /// <summary>
    /// Verifies that the occurrence timestamp exposed through
    /// <see cref="IDomainEvent"/> matches the timestamp of the base class.
    /// </summary>
    [Test]
    public void DomainEvent_AsIDomainEvent_ShouldExposeOccurredOn()
    {
        var domainEvent = new TestDomainEvent();

        IDomainEvent contract = domainEvent;

        Assert.That(
            contract.OccurredOn,
            Is.EqualTo(domainEvent.OccurredOn));
    }

    #endregion

    #region Test infrastructure

    private sealed class TestDomainEvent : DomainEvent
    {
    }

    #endregion
}
