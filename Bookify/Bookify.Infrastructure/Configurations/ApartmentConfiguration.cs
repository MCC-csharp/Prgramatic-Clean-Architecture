﻿using Bookify.Domain.Apartments;
using Bookify.Domain.DomainShared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations;

public sealed class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.ToTable("apartments");
        builder.HasKey(apartment => apartment.Id);
        builder.OwnsOne(apartment => apartment.Address);
        builder.Property(apartment => apartment.Name)
            .IsRequired()
            .HasMaxLength(200)
            .HasConversion(name => name.Value, value => new Name(value));

        builder.Property(apartment => apartment.Description)
             .IsRequired()
             .HasMaxLength(2000)
             .HasConversion(
                 description => description.Value,
                 value => new Description(value));

        builder.OwnsOne(apartment => apartment.Price, priceBuilder => priceBuilder.Property(money => money.Currency)
            .HasConversion(currency => currency.Code, code => Currency.FromCode(code)));

        builder.OwnsOne(apartment => apartment.CleaningFee, priceBuilder => priceBuilder.Property(money => money.Currency)
            .HasConversion(currency => currency.Code, code => Currency.FromCode(code)));

        // Optimistic concurrency control, we can't use the user id nor the booking as control so we use the appartment inside booking
        builder.Property<uint>("Version").IsRowVersion();
    }
}
