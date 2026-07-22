namespace KUKULCAN.SharedKernel.Abstractions.Capabilities;

public interface IHasDomainEvents
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
