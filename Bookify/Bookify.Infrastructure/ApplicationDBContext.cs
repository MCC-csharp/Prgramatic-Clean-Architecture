using Bookify.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;


namespace Bookify.Infrastructure
{
    public sealed class ApplicationDBContext(DbContextOptions options) : DbContext(options), IUnitOfWork
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
