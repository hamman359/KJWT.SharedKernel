using MediatR;

namespace KJWT.SharedKernel.DomainEvents;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}