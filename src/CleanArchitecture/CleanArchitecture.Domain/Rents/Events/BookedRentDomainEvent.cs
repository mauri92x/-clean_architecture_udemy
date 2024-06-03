using CleanArchitecture.Domain.Abstractions;

public sealed record BookedRentDomainEvent(Guid RentId):IDomainEvent;