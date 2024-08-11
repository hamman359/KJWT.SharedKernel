namespace KJWT.SharedKernel.DomainEvents;

public abstract record DomainEvent(Guid Id) : IDomainEvent;