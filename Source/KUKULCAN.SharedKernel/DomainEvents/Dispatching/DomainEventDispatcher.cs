using KUKULCAN.SharedKernel.DomainEvents.Abstractions;

namespace KUKULCAN.SharedKernel.DomainEvents.Dispatching;

/// <summary>
/// Dispatches collections of domain events.
/// </summary>
public sealed class DomainEventDispatcher
{
    private readonly IDomainEventDispatcher _dispatcher;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="DomainEventDispatcher"/> class.
    /// </summary>
    /// <param name="dispatcher">
    /// Domain event dispatcher implementation.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="dispatcher"/> is <see langword="null"/>.
    /// </exception>
    public DomainEventDispatcher(IDomainEventDispatcher dispatcher)
    {
        ArgumentNullException.ThrowIfNull(dispatcher);
        _dispatcher = dispatcher;
    }

    /// <summary>
    /// Dispatches the supplied domain events.
    /// </summary>
    /// <param name="domainEvents">
    /// Domain events to dispatch.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="domainEvents"/> is <see langword="null"/>.
    /// </exception>
    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(domainEvents);
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _dispatcher.DispatchAsync(domainEvent, cancellationToken).ConfigureAwait(false);
        }
    }
}
