using Bookify.Domain.Reviews;


namespace Bookify.Infrastructure.Repositories;

internal sealed class ReviewRepository(ApplicationDBContext dbContext) : Repository<Review>(dbContext), IReviewRepository
{
}
