using KUKULCAN.SharedKernel.Abstractions;
using KUKULCAN.SharedKernel.Domain;
using KUKULCAN.SharedKernel.DomainEvents.Abstractions;
using KUKULCAN.SharedKernel.Identifiers.Interfaces;

namespace KUKULCAN.SharedKernel.UnitTests.Domain;

/// <summary>
/// Contains unit tests for <see cref="AggregateRoot{TId}"/>.
/// </summary>
[TestFixture]
public sealed class AggregateRootTests
{
    #region Construction

    /// <summary>
    /// Verifies that the constructor assigns the aggregate identifier.
    /// </summary>
    [Test]
    public void Constructor_WithValidId_ShouldAssignId()
    {
        var id = new TestEntityId(Guid.NewGuid());
        var aggregate = new TestAggregateRoot(id);

        Assert.That(aggregate.Id, Is.SameAs(id));
    }

    /// <summary>
    /// Verifies that the parameterless constructor is available for
    /// serializers and ORMs.
    /// </summary>
    [Test]
    public void ParameterlessConstructor_ShouldCreateInstance()
    {
        var aggregate = new TestAggregateRoot();

        Assert.That(aggregate, Is.Not.Null);
        Assert.That(aggregate.DomainEvents, Is.Empty);
    }

    #endregion

    #region Aggregate root contract

    /// <summary>
    /// Verifies that the aggregate root implements <see cref="IAggregateRoot"/>.
    /// </summary>
    [Test]
    public void Aggregate_ShouldImplementIAggregateRoot()
    {
        var aggregate = new TestAggregateRoot();

        Assert.That(aggregate, Is.InstanceOf<IAggregateRoot>());
    }

    #endregion

    #region Domain events

    /// <summary>
    /// Verifies that a newly created aggregate has no pending domain events.
    /// </summary>
    [Test]
    public void DomainEvents_WhenCreated_ShouldBeEmpty()
    {
        var aggregate = new TestAggregateRoot();

        Assert.That(
            aggregate.DomainEvents,
            Is.Empty);
    }

    /// <summary>
    /// Verifies that adding a domain event exposes it through
    /// <see cref="AggregateRoot{TId}.DomainEvents"/>.
    /// </summary>
    [Test]
    public void AddDomainEvent_ShouldExposeEvent()
    {
        var aggregate = new TestAggregateRoot();
        var domainEvent = new TestDomainEvent();

        aggregate.RegisterDomainEvent(domainEvent);

        Assert.That(
            aggregate.DomainEvents,
            Has.Count.EqualTo(1));

        Assert.That(
            aggregate.DomainEvents.Single(),
            Is.SameAs(domainEvent));
    }

    /// <summary>
    /// Verifies that multiple domain events preserve insertion order.
    /// </summary>
    [Test]
    public void AddDomainEvent_MultipleEvents_ShouldPreserveOrder()
    {
        var aggregate = new TestAggregateRoot();

        var first = new TestDomainEvent();
        var second = new TestDomainEvent();
        var third = new TestDomainEvent();

        aggregate.RegisterDomainEvent(first);
        aggregate.RegisterDomainEvent(second);
        aggregate.RegisterDomainEvent(third);

        List<IDomainEvent> events =
            [.. aggregate.DomainEvents];

        Assert.Multiple(() =>
        {
            Assert.That(events[0], Is.SameAs(first));
            Assert.That(events[1], Is.SameAs(second));
            Assert.That(events[2], Is.SameAs(third));
        });
    }

    /// <summary>
    /// Verifies that adding a null domain event is rejected.
    /// </summary>
    [Test]
    public void AddDomainEvent_WithNull_ShouldThrowArgumentNullException()
    {
        var aggregate = new TestAggregateRoot();

        Assert.That(
            () => aggregate.RegisterDomainEvent(null!),
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that removing an existing domain event removes it.
    /// </summary>
    [Test]
    public void RemoveDomainEvent_WithExistingEvent_ShouldRemoveEvent()
    {
        var aggregate = new TestAggregateRoot();
        var domainEvent = new TestDomainEvent();

        aggregate.RegisterDomainEvent(domainEvent);

        aggregate.UnregisterDomainEvent(domainEvent);

        Assert.That(
            aggregate.DomainEvents,
            Is.Empty);
    }

    /// <summary>
    /// Verifies that removing a domain event that is not registered
    /// does not affect the other pending events.
    /// </summary>
    [Test]
    public void RemoveDomainEvent_WithUnknownEvent_ShouldLeaveExistingEvents()
    {
        var aggregate = new TestAggregateRoot();

        var registered = new TestDomainEvent();
        var unknown = new TestDomainEvent();

        aggregate.RegisterDomainEvent(registered);

        aggregate.UnregisterDomainEvent(unknown);

        Assert.Multiple(() =>
        {
            Assert.That(
                aggregate.DomainEvents,
                Has.Count.EqualTo(1));

            Assert.That(
                aggregate.DomainEvents.Single(),
                Is.SameAs(registered));
        });
    }

    /// <summary>
    /// Verifies that removing a null domain event is rejected.
    /// </summary>
    [Test]
    public void RemoveDomainEvent_WithNull_ShouldThrowArgumentNullException()
    {
        var aggregate = new TestAggregateRoot();

        Assert.That(
            () => aggregate.UnregisterDomainEvent(null!),
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that ClearDomainEvents removes every pending event.
    /// </summary>
    [Test]
    public void ClearDomainEvents_ShouldRemoveAllEvents()
    {
        var aggregate = new TestAggregateRoot();

        aggregate.RegisterDomainEvent(new TestDomainEvent());
        aggregate.RegisterDomainEvent(new TestDomainEvent());

        aggregate.ClearDomainEvents();

        Assert.That(
            aggregate.DomainEvents,
            Is.Empty);
    }

    /// <summary>
    /// Verifies that dequeuing returns the pending events and clears
    /// the aggregate.
    /// </summary>
    [Test]
    public void DequeueDomainEvents_ShouldReturnEventsAndClearCollection()
    {
        var aggregate = new TestAggregateRoot();

        var first = new TestDomainEvent();
        var second = new TestDomainEvent();

        aggregate.RegisterDomainEvent(first);
        aggregate.RegisterDomainEvent(second);

        List<IDomainEvent> dequeued =
            [.. aggregate.TakeDomainEvents()];

        Assert.Multiple(() =>
        {
            Assert.That(dequeued, Has.Count.EqualTo(2));
            Assert.That(dequeued[0], Is.SameAs(first));
            Assert.That(dequeued[1], Is.SameAs(second));
            Assert.That(
                aggregate.DomainEvents,
                Is.Empty);
        });
    }

    /// <summary>
    /// Verifies that dequeuing an empty aggregate returns an empty
    /// collection.
    /// </summary>
    [Test]
    public void DequeueDomainEvents_WhenEmpty_ShouldReturnEmptyCollection()
    {
        var aggregate = new TestAggregateRoot();

        var dequeued = aggregate.TakeDomainEvents();

        Assert.Multiple(() =>
        {
            Assert.That(dequeued, Is.Empty);
            Assert.That(
                aggregate.DomainEvents,
                Is.Empty);
        });
    }

    /// <summary>
    /// Verifies that a dequeued snapshot is independent from subsequent
    /// additions to the aggregate.
    /// </summary>
    [Test]
    public void DequeueDomainEvents_ShouldReturnSnapshot()
    {
        var aggregate = new TestAggregateRoot();

        var first = new TestDomainEvent();

        aggregate.RegisterDomainEvent(first);

        List<IDomainEvent> snapshot =
            [.. aggregate.TakeDomainEvents()];

        aggregate.RegisterDomainEvent(new TestDomainEvent());

        Assert.Multiple(() =>
        {
            Assert.That(snapshot, Has.Count.EqualTo(1));
            Assert.That(snapshot[0], Is.SameAs(first));
            Assert.That(
                aggregate.DomainEvents,
                Has.Count.EqualTo(1));
        });
    }

    #endregion

    #region Test infrastructure

    private sealed class TestAggregateRoot : AggregateRoot<TestEntityId>
    {
        public TestAggregateRoot()
        {
        }

        public TestAggregateRoot(TestEntityId id)
            : base(id)
        {
        }

        public void RegisterDomainEvent(IDomainEvent domainEvent)
        {
            AddDomainEvent(domainEvent);
        }

        public void UnregisterDomainEvent(IDomainEvent domainEvent)
        {
            RemoveDomainEvent(domainEvent);
        }

        public List<IDomainEvent> TakeDomainEvents()
        {
            return [.. DequeueDomainEvents()];
        }
    }

    private sealed class TestDomainEvent : IDomainEvent
    {
        public DateTimeOffset OccurredOn { get; } =
            DateTimeOffset.UtcNow;
    }

    private sealed class TestEntityId(Guid value) : IEntityId
    {
        public Guid Value { get; } = value;

        public override string ToString()
        {
            return Value.ToString();
        }

        public bool Equals(IEntityId? other) => throw new NotImplementedException();

        public override bool Equals(object? obj)
        {
            return obj is TestEntityId other &&
                   Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        object? IEntityId.Value => Value;
    }

    #endregion
}
