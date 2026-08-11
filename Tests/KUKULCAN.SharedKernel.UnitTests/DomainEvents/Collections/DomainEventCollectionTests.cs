using KUKULCAN.SharedKernel.DomainEvents.Abstractions;
using KUKULCAN.SharedKernel.DomainEvents.Collections;

namespace KUKULCAN.SharedKernel.UnitTests.DomainEvents.Collections;

/// <summary>
/// Contains unit tests for <see cref="DomainEventCollection"/>.
/// </summary>
[TestFixture]
public sealed class DomainEventCollectionTests
{
    #region Construction

    /// <summary>
    /// Verifies that a newly created collection is empty.
    /// </summary>
    [Test]
    public void Constructor_ShouldCreateEmptyCollection()
    {
        var collection = new DomainEventCollection();

        Assert.Multiple(() =>
        {
            Assert.That(collection.Count, Is.Zero);
            Assert.That(collection.IsEmpty, Is.True);
            Assert.That(collection.Items, Is.Empty);
        });
    }

    #endregion

    #region Add

    /// <summary>
    /// Verifies that adding an event increases the count and exposes
    /// the event through the collection.
    /// </summary>
    [Test]
    public void Add_WithEvent_ShouldAddEvent()
    {
        var collection = new DomainEventCollection();
        var domainEvent = new TestDomainEvent();

        collection.Add(domainEvent);

        Assert.Multiple(() =>
        {
            Assert.That(collection.Count, Is.EqualTo(1));
            Assert.That(collection.IsEmpty, Is.False);
            Assert.That(collection.Items.Single(), Is.SameAs(domainEvent));
        });
    }

    /// <summary>
    /// Verifies that multiple events preserve insertion order.
    /// </summary>
    [Test]
    public void Add_WithMultipleEvents_ShouldPreserveInsertionOrder()
    {
        var collection = new DomainEventCollection();

        var first = new TestDomainEvent();
        var second = new TestDomainEvent();
        var third = new TestDomainEvent();

        collection.Add(first);
        collection.Add(second);
        collection.Add(third);

        List<IDomainEvent> events = [.. collection];

        Assert.Multiple(() =>
        {
            Assert.That(events[0], Is.SameAs(first));
            Assert.That(events[1], Is.SameAs(second));
            Assert.That(events[2], Is.SameAs(third));
        });
    }

    /// <summary>
    /// Verifies that adding a null event is rejected.
    /// </summary>
    [Test]
    public void Add_WithNull_ShouldThrowArgumentNullException()
    {
        var collection = new DomainEventCollection();

        Assert.That(
            () => collection.Add(null!),
            Throws.ArgumentNullException);
    }

    #endregion

    #region Remove

    /// <summary>
    /// Verifies that removing an existing event returns true and
    /// removes the event.
    /// </summary>
    [Test]
    public void Remove_WithExistingEvent_ShouldRemoveEventAndReturnTrue()
    {
        var collection = new DomainEventCollection();
        var domainEvent = new TestDomainEvent();

        collection.Add(domainEvent);

        bool removed = collection.Remove(domainEvent);

        Assert.Multiple(() =>
        {
            Assert.That(removed, Is.True);
            Assert.That(collection.IsEmpty, Is.True);
            Assert.That(collection.Count, Is.Zero);
        });
    }

    /// <summary>
    /// Verifies that removing an unknown event returns false and
    /// leaves the collection unchanged.
    /// </summary>
    [Test]
    public void Remove_WithUnknownEvent_ShouldReturnFalseAndLeaveCollectionUnchanged()
    {
        var collection = new DomainEventCollection();
        var registered = new TestDomainEvent();
        var unknown = new TestDomainEvent();

        collection.Add(registered);

        bool removed = collection.Remove(unknown);

        Assert.Multiple(() =>
        {
            Assert.That(removed, Is.False);
            Assert.That(collection.Count, Is.EqualTo(1));
            Assert.That(collection.Items.Single(), Is.SameAs(registered));
        });
    }

    /// <summary>
    /// Verifies that removing a null event is rejected.
    /// </summary>
    [Test]
    public void Remove_WithNull_ShouldThrowArgumentNullException()
    {
        var collection = new DomainEventCollection();

        Assert.That(
            () => collection.Remove(null!),
            Throws.ArgumentNullException);
    }

    #endregion

    #region Clear

    /// <summary>
    /// Verifies that clearing the collection removes all events.
    /// </summary>
    [Test]
    public void Clear_WithEvents_ShouldRemoveAllEvents()
    {
        var collection = new DomainEventCollection { new TestDomainEvent(), new TestDomainEvent() };

        collection.Clear();
        Assert.Multiple(() =>
        {
            Assert.That(collection.Count, Is.Zero);
            Assert.That(collection.IsEmpty, Is.True);
            Assert.That(collection.Items, Is.Empty);
        });
    }

    /// <summary>
    /// Verifies that clearing an empty collection is harmless.
    /// </summary>
    [Test]
    public void Clear_WhenEmpty_ShouldRemainEmpty()
    {
        var collection = new DomainEventCollection();

        collection.Clear();

        Assert.That(collection.IsEmpty, Is.True);
    }

    #endregion

    #region Dequeue

    /// <summary>
    /// Verifies that dequeue returns all events in insertion order and
    /// clears the collection.
    /// </summary>
    [Test]
    public void Dequeue_WithEvents_ShouldReturnSnapshotAndClearCollection()
    {
        var collection = new DomainEventCollection();
        var first = new TestDomainEvent();
        var second = new TestDomainEvent();

        collection.Add(first);
        collection.Add(second);

        IReadOnlyCollection<IDomainEvent> dequeued =
            collection.Dequeue();
        List<IDomainEvent> events = [.. dequeued];

        Assert.Multiple(() =>
        {
            Assert.That(events, Has.Count.EqualTo(2));
            Assert.That(events[0], Is.SameAs(first));
            Assert.That(events[1], Is.SameAs(second));
            Assert.That(collection.IsEmpty, Is.True);
            Assert.That(collection.Count, Is.Zero);
        });
    }

    /// <summary>
    /// Verifies that dequeue on an empty collection returns an empty
    /// collection and leaves the collection empty.
    /// </summary>
    [Test]
    public void Dequeue_WhenEmpty_ShouldReturnEmptyCollection()
    {
        var collection = new DomainEventCollection();
        IReadOnlyCollection<IDomainEvent> dequeued =
            collection.Dequeue();

        Assert.Multiple(() =>
        {
            Assert.That(dequeued, Is.Empty);
            Assert.That(collection.IsEmpty, Is.True);
        });
    }

    /// <summary>
    /// Verifies that a dequeued snapshot is independent from subsequent
    /// additions to the collection.
    /// </summary>
    [Test]
    public void Dequeue_ShouldReturnIndependentSnapshot()
    {
        var collection = new DomainEventCollection();
        var first = new TestDomainEvent();

        collection.Add(first);

        IReadOnlyCollection<IDomainEvent> snapshot =
            collection.Dequeue();

        collection.Add(new TestDomainEvent());

        Assert.Multiple(() =>
        {
            Assert.That(snapshot, Has.Count.EqualTo(1));
            Assert.That(snapshot.Single(), Is.SameAs(first));
            Assert.That(collection.Count, Is.EqualTo(1));
        });
    }

    #endregion

    #region Items

    /// <summary>
    /// Verifies that the items property exposes the current events
    /// without exposing the internal list.
    /// </summary>
    [Test]
    public void Items_ShouldExposeCurrentEventsAsReadOnlyCollection()
    {
        var collection = new DomainEventCollection();
        var domainEvent = new TestDomainEvent();

        collection.Add(domainEvent);

        IReadOnlyCollection<IDomainEvent> items = collection.Items;

        Assert.Multiple(() =>
        {
            Assert.That(items, Has.Count.EqualTo(1));
            Assert.That(items.Single(), Is.SameAs(domainEvent));
        });
    }

    /// <summary>
    /// Verifies that the items view reflects subsequent changes
    /// to the collection.
    /// </summary>
    [Test]
    public void Items_WhenCollectionChanges_ShouldReflectCurrentEvents()
    {
        var collection = new DomainEventCollection();
        var first = new TestDomainEvent();

        collection.Add(first);

        IReadOnlyCollection<IDomainEvent> items =
            collection.Items;

        collection.Add(new TestDomainEvent());
        Assert.That(
            items,
            Has.Count.EqualTo(2));
    }

    #endregion

    #region Enumeration

    /// <summary>
    /// Verifies that enumeration exposes events in insertion order.
    /// </summary>
    [Test]
    public void GetEnumerator_ShouldEnumerateEventsInInsertionOrder()
    {
        var collection = new DomainEventCollection();
        var first = new TestDomainEvent();
        var second = new TestDomainEvent();

        collection.Add(first);
        collection.Add(second);
        List<IDomainEvent> events = [.. collection];
        Assert.Multiple(() =>
        {
            Assert.That(events, Has.Count.EqualTo(2));
            Assert.That(events[0], Is.SameAs(first));
            Assert.That(events[1], Is.SameAs(second));
        });
    }

    #endregion

    #region Test infrastructure

    private sealed class TestDomainEvent : IDomainEvent
    {
        public DateTimeOffset OccurredOn { get; } =
            DateTimeOffset.UtcNow;
    }

    #endregion
}
