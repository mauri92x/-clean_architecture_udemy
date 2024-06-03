using CleanArchitecture.Application.Abstractions.Messaging;

public record BookRentCommand(
    Guid VehicleId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate
):ICommand<Guid>;