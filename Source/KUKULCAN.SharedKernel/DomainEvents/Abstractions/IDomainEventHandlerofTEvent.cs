using System.Threading;
using System.Threading.Tasks;

namespace KUKULCAN.SharedKernel.DomainEvents.Abstractions;

/// <summary>
/// Handles domain events.
/// </summary>
/// <typeparam name="TEvent">
/// Type of the domain event.
/// </typeparam>
public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
    /// <summary>
    /// Handles the specified domain event.
    /// </summary>
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);
}
