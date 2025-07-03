namespace Bookify.Application.Apartments.SearchApartments;

public sealed class AddressResponse
{
    public required string Country { get; init; }
    public required string City { get; init; }
    public required string Street { get; init; }
    public required string ZipCode { get; init; }
    public required string State { get; init; }
}
