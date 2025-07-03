using Bookify.Domain.Abstractions;
using Bookify.Domain.DomainShared;

namespace Bookify.Domain.Apartments;

public sealed class Apartment : Entity
{
    public Apartment(
        Guid id,
        Name name,
        Description description,
        Address address,
        Money price,
        Money cleaningFee,
        IEnumerable<Amenity> amenities)
        : base(id)
    {
        Name = name;
        Description = description;
        Address = address;
        Price = price;
        CleaningFee = cleaningFee;
        Amenities = amenities;
    }

    private Apartment()
    {
        // For EF Core
    }

    public Name Name { get; private set; } = default!;

    public Description Description { get; private set; } = default!;

    public Address Address { get; private set; } = default!;

    public Money Price { get; private set; } = default!;

    public Money CleaningFee { get; private set; } = default!;

    public DateTime? LastBookedOnUtc { get; internal set; }

    public IEnumerable<Amenity> Amenities { get; private set; } = [];
}
