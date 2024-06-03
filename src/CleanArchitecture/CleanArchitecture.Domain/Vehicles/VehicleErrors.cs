
using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Vehicles;

public static class VehicleErrors
{
    public static Error NotFound = new(
        "Vehicle.Found",
        "Vehicle don't exist search by this ID"
    );

    public static Error InvaidCredentials = new(
        "Vehicle.InvalidCredentials",
        "The credentials is incorrect"
    );
}