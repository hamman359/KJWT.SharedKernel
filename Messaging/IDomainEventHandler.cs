using KJWT.SharedKernel.DomainEvents;

using MediatR;

namespace KJWT.SharedKernel.Messaging;

public interface IDomainEventHandler<TEvent>
    : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
