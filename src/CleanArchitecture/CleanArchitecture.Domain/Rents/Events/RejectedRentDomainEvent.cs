using CleanArchitecture.Domain.Abstractions;


public sealed record RejectedRentDomainEvent(Guid RentId):IDomainEvent;