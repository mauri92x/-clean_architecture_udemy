using CleanArchitecture.Domain.Abstractions;


public sealed record ConfirmedRentDomainEvent(Guid RentId):IDomainEvent;