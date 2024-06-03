using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rents;

public static class RentErrors
{
    public static Error NotFound = new Error(
        "Rent.Found",
        "Rent not found"
    );

    public static Error Overlap = new Error(
        "Rent.Overlap",
        "The rent is being taken by more than one client in the same date."
    );

    public static Error NotReserved = new Error(
        "Rent.NotReserved",
        "The rent is not reserved."
    );
    public static Error NotConfirmed = new Error(
        "Rent.NotConfirmed",
        "The rent is not Confirmed."
    );

    public static Error AlreadyStarted = new Error(
        "Rent.AlreadyStarted",
        "The rent has already Started."
    );

}