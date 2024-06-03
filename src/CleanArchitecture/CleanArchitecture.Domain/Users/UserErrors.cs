
using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Users;

public static class UserErrors
{
    public static Error NotFound = new(
        "User.Found",
        "User don't exist search by this ID"
    );

    public static Error InvaidCredentials = new(
        "User.InvalidCredentials",
        "The credentials is incorrect"
    );
}