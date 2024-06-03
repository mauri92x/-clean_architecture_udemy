namespace CleanArchitecture.Domain.Abstractions;

public sealed record CreatedReviewDomainEvent(Guid UserID):IDomainEvent;