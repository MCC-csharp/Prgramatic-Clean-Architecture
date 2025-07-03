using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Users;

public static class UserErrors
{
    public static readonly DomainError NotFound = new(
        "User.Found",
        "The user with the specified identifier was not found");

    public static readonly DomainError InvalidCredentials = new(
        "User.InvalidCredentials",
        "The provided credentials were invalid");
}