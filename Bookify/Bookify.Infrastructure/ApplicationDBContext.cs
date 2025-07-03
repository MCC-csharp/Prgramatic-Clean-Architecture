using Bookify.Application.Exceptions;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Bookify.Infrastructure;

public sealed class ApplicationDBContext(DbContextOptions options, IPublisher publisher) : DbContext(options), IUnitOfWork
{
    private readonly IPublisher _publisher = publisher;

    private async Task PublishDomainEventsAsync()
    {
        var domainEntities = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                IReadOnlyList<IDomainEvent> domainEvents = entity.GetDomainEvents();
                entity.ClearDomainEvents();
                return domainEvents;
            })
            .ToList();

        foreach (IDomainEvent? domainEvent in domainEntities)
        {
            await _publisher.Publish(domainEvent);
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Danger with this approach, if PublishDomainEventsAsync fails because of an exception, the changes will still be saved to the database.

        try
        {
            int result = await base.SaveChangesAsync(cancellationToken);
            await PublishDomainEventsAsync();
            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("A concurrency error occurred while saving changes.", ex);
        }
    }
}
