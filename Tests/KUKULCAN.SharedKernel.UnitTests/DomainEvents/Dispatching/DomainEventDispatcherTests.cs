using KUKULCAN.SharedKernel.DomainEvents.Abstractions;
using KUKULCAN.SharedKernel.DomainEvents.Dispatching;

namespace KUKULCAN.SharedKernel.UnitTests.DomainEvents.Dispatching;

/// <summary>
/// Contains unit tests for <see cref="DomainEventDispatcher"/>.
/// </summary>
[TestFixture]
public sealed class DomainEventDispatcherTests
{
    #region Construction

    /// <summary>
    /// Verifies that the constructor rejects a null dispatcher.
    /// </summary>
    [Test]
    public void Constructor_WithNullDispatcher_ShouldThrowArgumentNullException()
    {
        Assert.That(
            () => new DomainEventDispatcher(null!),
            Throws.ArgumentNullException);
    }

    /// <summary>
    /// Verifies that the constructor accepts a valid dispatcher.
    /// </summary>
    [Test]
    public void Constructor_WithValidDispatcher_ShouldCreateInstance()
    {
        var dispatcher = new TestDispatcher();
        var eventDispatcher = new DomainEventDispatcher(dispatcher);

        Assert.That(eventDispatcher, Is.Not.Null);
    }

    #endregion

    #region Dispatch

    /// <summary>
    /// Verifies that a single domain event is dispatched.
    /// </summary>
    [Test]
    public async Task DispatchAsync_WithSingleEvent_ShouldDispatchEvent()
    {
        var dispatcher = new TestDispatcher();
        var eventDispatcher = new DomainEventDispatcher(dispatcher);
        var domainEvent = new TestDomainEvent();

        await eventDispatcher.DispatchAsync([domainEvent]);
        Assert.That(dispatcher.DispatchedEvents, Has.Count.EqualTo(1));
        Assert.That(dispatcher.DispatchedEvents[0], Is.SameAs(domainEvent));
    }

    /// <summary>
    /// Verifies that multiple domain events are dispatched in their
    /// original order.
    /// </summary>
    [Test]
    public async Task DispatchAsync_WithMultipleEvents_ShouldPreserveOrder()
    {
        var dispatcher = new TestDispatcher();
        var eventDispatcher = new DomainEventDispatcher(dispatcher);
        var first = new TestDomainEvent();
        var second = new TestDomainEvent();
        var third = new TestDomainEvent();

        await eventDispatcher.DispatchAsync(
        [
            first,
            second,
            third
        ]);
        Assert.Multiple(() =>
        {
            Assert.That(dispatcher.DispatchedEvents[0], Is.SameAs(first));
            Assert.That(dispatcher.DispatchedEvents[1], Is.SameAs(second));
            Assert.That(dispatcher.DispatchedEvents[2], Is.SameAs(third));
        });
    }

    /// <summary>
    /// Verifies that dispatching an empty collection does not invoke
    /// the underlying dispatcher.
    /// </summary>
    [Test]
    public async Task DispatchAsync_WithEmptyCollection_ShouldNotDispatch()
    {
        var dispatcher = new TestDispatcher();
        var eventDispatcher = new DomainEventDispatcher(dispatcher);

        await eventDispatcher.DispatchAsync([]);

        Assert.That(dispatcher.DispatchedEvents, Is.Empty);
    }

    /// <summary>
    /// Verifies that a null domain-event collection is rejected.
    /// </summary>
    [Test]
    public void DispatchAsync_WithNullCollection_ShouldThrowArgumentNullException()
    {
        var dispatcher = new TestDispatcher();
        var eventDispatcher = new DomainEventDispatcher(dispatcher);

        Assert.That(async () => await eventDispatcher.DispatchAsync(null!),
            Throws.ArgumentNullException);
    }

    #endregion

    #region Cancellation

    /// <summary>
    /// Verifies that the cancellation token is forwarded to the underlying
    /// dispatcher.
    /// </summary>
    [Test]
    public async Task DispatchAsync_ShouldForwardCancellationToken()
    {
        var dispatcher = new TestDispatcher();
        var eventDispatcher = new DomainEventDispatcher(dispatcher);
        var domainEvent = new TestDomainEvent();
        using var cancellationTokenSource = new CancellationTokenSource();

        await eventDispatcher.DispatchAsync([domainEvent], cancellationTokenSource.Token);
        Assert.That(dispatcher.ReceivedCancellationToken, Is.EqualTo(cancellationTokenSource.Token));
    }

    /// <summary>
    /// Verifies that cancellation before dispatch prevents the event
    /// from being dispatched.
    /// </summary>
    [Test]
    public void DispatchAsync_WithAlreadyCancelledToken_ShouldThrowOperationCanceledException()
    {
        var dispatcher = new TestDispatcher();
        var eventDispatcher = new DomainEventDispatcher(dispatcher);
        var domainEvent = new TestDomainEvent();
        using var cancellationTokenSource = new CancellationTokenSource();

        cancellationTokenSource.Cancel();

        Assert.That(
            async () => await eventDispatcher.DispatchAsync(
                [domainEvent],
                cancellationTokenSource.Token),
            Throws.InstanceOf<OperationCanceledException>());
        Assert.That(dispatcher.DispatchedEvents, Is.Empty);
    }

    /// <summary>
    /// Verifies that cancellation stops dispatching subsequent events.
    /// </summary>
    [Test]
    public async Task DispatchAsync_WhenCancelledDuringDispatch_ShouldStopBeforeNextEvent()
    {
        var dispatcher = new TestDispatcher();
        var eventDispatcher = new DomainEventDispatcher(dispatcher);
        var first = new TestDomainEvent();
        var second = new TestDomainEvent();
        using var cancellationTokenSource = new CancellationTokenSource();

        dispatcher.OnDispatch = _ =>
        {
            cancellationTokenSource.Cancel();
        };

        Assert.That(
            async () => await eventDispatcher.DispatchAsync(
                [first, second],
                cancellationTokenSource.Token),
            Throws.InstanceOf<OperationCanceledException>());
        Assert.That(dispatcher.DispatchedEvents, Has.Count.EqualTo(1));
        Assert.That(dispatcher.DispatchedEvents[0], Is.SameAs(first));
    }

    #endregion

    #region Test infrastructure

    private sealed class TestDispatcher : IDomainEventDispatcher
    {
        public List<IDomainEvent> DispatchedEvents { get; } = [];

        public CancellationToken ReceivedCancellationToken { get; private set; }

        public Action<IDomainEvent>? OnDispatch { get; set; }

        public Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            DispatchedEvents.Add(domainEvent);
            ReceivedCancellationToken = cancellationToken;
            OnDispatch?.Invoke(domainEvent);

            return Task.CompletedTask;
        }
    }

    private sealed class TestDomainEvent : IDomainEvent
    {
        public DateTimeOffset OccurredOn { get; } =
            DateTimeOffset.UtcNow;
    }

    #endregion
}
