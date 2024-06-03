using CleanArchitecture.Domain.Abstractions;


public sealed record CompletedRentDomainEvent(Guid RentId):IDomainEvent;