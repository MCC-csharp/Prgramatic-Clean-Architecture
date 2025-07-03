using Bookify.Application.Abstractions.Messaging;


namespace Bookify.Application.Users;

public sealed record RegisterUserCommand(string Email, string Password, string FirstName, string LastName) : ICommand<Guid>
{
}
