namespace CleanArchitecture.Domain.Abstractions;

public sealed record CreatedUserDomainEvent(Guid UserID):IDomainEvent;

