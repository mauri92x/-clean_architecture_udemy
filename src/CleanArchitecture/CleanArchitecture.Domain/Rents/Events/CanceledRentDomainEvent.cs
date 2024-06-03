using CleanArchitecture.Domain.Abstractions;
public sealed record CanceledRentDomainEvent(Guid RentId):IDomainEvent;