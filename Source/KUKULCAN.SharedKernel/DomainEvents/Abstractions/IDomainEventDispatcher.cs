namespace KUKULCAN.SharedKernel.DomainEvents.Abstractions;

/// <summary>
/// Defines a contract for dispatching domain events.
/// </summary>
public interface IDomainEventDispatcher
{
    /// <summary>
    /// Dispatches a domain event.
    /// </summary>
    /// <param name="domainEvent">
    /// Domain event to dispatch.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
}
