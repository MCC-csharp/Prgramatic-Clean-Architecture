using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(user => user.Id);

            builder.Property(user => user.FirstName)
                .IsRequired()
                .HasMaxLength(200)
                .HasConversion(name => name.Value, value => new FirstName(value));

            builder.Property(user => user.LastName)
                .IsRequired()
                .HasMaxLength(200)
                .HasConversion(name => name.Value, value => new LastName(value));
        
            builder.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(400)
                .HasConversion(email => email.Value, value => new Domain.Users.Email(value));

            builder.HasIndex(user => user.Email)
                .IsUnique();
        }
    }
}
