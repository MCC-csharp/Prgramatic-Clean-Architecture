using Bookify.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;


namespace Bookify.Infrastructure.Repositories;

internal class Repository<T>(ApplicationDBContext dbContext) where T : Entity
{
    protected readonly ApplicationDBContext DBContext = dbContext;

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await DBContext
            .Set<T>()
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);


    public void Add(T entity)
    {
        DBContext.Add(entity);
    }
}
