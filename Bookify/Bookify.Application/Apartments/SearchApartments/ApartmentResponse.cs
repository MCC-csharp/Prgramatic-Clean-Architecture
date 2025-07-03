namespace Bookify.Application.Apartments.SearchApartments;

public sealed class ApartmentResponse
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required AddressResponse Address { get; init; }
    public required decimal Price { get; init; }
    public required string Currency { get; init; }
}
