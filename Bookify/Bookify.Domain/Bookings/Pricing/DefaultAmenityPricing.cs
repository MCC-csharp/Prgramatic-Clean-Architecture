using Bookify.Domain.Apartments;

namespace Bookify.Domain.Bookings.Pricing;

public static class DefaultAmenityPricing
{
    public static decimal Evaluate(Amenity amenity) =>
            amenity switch
            {
                Amenity.GardenView or Amenity.MountainView => 0.05m,
                Amenity.AirConditioning => 0.01m,
                Amenity.Parking => 0.01m,
                _ => 0
            };
}
