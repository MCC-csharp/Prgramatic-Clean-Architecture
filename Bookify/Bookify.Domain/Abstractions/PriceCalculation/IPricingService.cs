using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Bookings.Pricing;



namespace Bookify.Domain.Abstractions.PriceCalculation;

public interface IPricingService
{
    PricingDetails CalculatePrice(Apartment apartment, DateRange period);
}
