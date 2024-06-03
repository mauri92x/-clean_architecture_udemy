using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews;

public static class ReviewErrors
{

    public static readonly Error NotEligible = new(
        "Review.NotEligible",
        "This review and rating for the car is not eligible because it has not been completed yet."
    );


}