using Bookify.Domain.Apartments;

namespace Bookify.Infrastructure.Repositories;

internal sealed class ApartmentRepository(ApplicationDBContext dbContext) : Repository<Apartment>(dbContext), IApartmentRepository
{
}
