using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Rents;

public record DetailPrice(
    Currency PriceForPeriod,
    Currency Maintenance,
    Currency Accessories,
    Currency TotalPrice
);
