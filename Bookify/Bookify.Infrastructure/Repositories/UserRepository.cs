using Bookify.Domain.Users;


namespace Bookify.Infrastructure.Repositories;

internal sealed class UserRepository(ApplicationDBContext dbContext) : Repository<User>(dbContext), IUserRepository
{
}
